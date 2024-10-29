using System.Collections.Generic;
using Ingame;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Define;

public class MapManager
{
  private ETheme themeId;
  private Tilemap tilemapBG;
  private Tilemap tilemapWall;
  private int screenTileHeight;
  private int lastCameraPositionY;

  private const int MAP_WIDTH = 7;
  private const int TILE_BUFFER = 2;
  private const float TILE_SIZE = 1f;

  private float catnipProbability = 1f;
  private int consecutiveItemRows = 0;
  private int consecutiveEmptyRows = 0;
  private const int MAX_EMPTY_ROWS = 5;
  private const int ITEM_SPAWN_DISTANCE = 100;

  private List<PlacementPattern> currentPatterns = new();
  private Dictionary<string, MapPattern> mapPatternDict = new();
  private Dictionary<string, PlacementPattern> placementPatternDict = new();
  private Dictionary<float, Dictionary<int, float>> monsterRateDict = new();

  #region Initialize
  public void Init()
  {
    /************************************************************************
     * Summary: 
     * 맵 매니저 초기화
     * 맵 테마, 맵 패턴, 몬스터 등장 확률 데이터 로드
     ************************************************************************/
    //  TODO: 유저의 레벨에 따른 테마 설정
    themeId = ETheme.Palace;
    mapPatternDict = Managers.Data.MapPatterns;
    placementPatternDict = Managers.Data.PlacementPatterns;

    foreach (var spawnRate in Managers.Data.MonsterRates)
    {
      if (!monsterRateDict.ContainsKey(spawnRate.Value.checkpoint))
        monsterRateDict[spawnRate.Value.checkpoint] = new Dictionary<int, float>();

      if (!monsterRateDict[spawnRate.Value.checkpoint].ContainsKey(spawnRate.Value.monsterId))
        monsterRateDict[spawnRate.Value.checkpoint].Add(spawnRate.Value.monsterId, spawnRate.Value.probability);
      else
        monsterRateDict[spawnRate.Value.checkpoint][spawnRate.Value.monsterId] = spawnRate.Value.probability;
    }
  }

  public void LoadMap()
  {
    /************************************************************************
     * Summary: 
     * 테마에 맞는 타일맵을 로드
     * 타일맵의 위치에 맞게 카메라 설정
     ************************************************************************/
    LoadTilemap();
    SetCamera();

    screenTileHeight = Mathf.CeilToInt(Camera.main.orthographicSize * 2);
    lastCameraPositionY = Mathf.FloorToInt(Camera.main.transform.position.y);
  }

  private void LoadTilemap()
  {
    /************************************************************************
     * Summary: 
     * 테마에 맞는 타일맵을 로드
     * 타일맵의 초기 위치를 설정
     ************************************************************************/
    GameObject go = Managers.Resource.Instantiate($"Theme{themeId}");
    go.name = "@Tilemap";

    tilemapBG = go.transform.Find("TilemapBG").GetComponent<Tilemap>();
    tilemapWall = go.transform.Find("TilemapWall").GetComponent<Tilemap>();

    tilemapBG.CompressBounds();
    tilemapWall.CompressBounds();

    tilemapBG.transform.position = new Vector3(-0.5f, -tilemapBG.cellBounds.center.y, 0);
    tilemapWall.transform.position = new Vector3(-0.5f, -tilemapBG.cellBounds.center.y, 0);
  }

  private void SetCamera()
  {
    /************************************************************************
     * Summary: 
     * 카메라 설정
     ************************************************************************/

    Camera.main.GetComponent<CameraController>().SetCamera(numColumns: MAP_WIDTH, tileSize: TILE_SIZE);
  }
  #endregion

  #region MapDesign
  public void UpdateMap()
  {
    /************************************************************************
     * Summary: 
     * 카메라 위치에 따라 타일을 동적으로 재배치
     ************************************************************************/

    int cameraPositionY = Mathf.FloorToInt(Camera.main.transform.position.y);
    if (cameraPositionY > lastCameraPositionY)
    {
      MoveTiles(cameraPositionY - lastCameraPositionY);
      lastCameraPositionY = cameraPositionY;
    }
  }

  private void MoveTiles(int deltaY)
  {
    /************************************************************************
     * Summary: 
     * 카메라가 위쪽으로 움직일 때 타일을 동적으로 재배치
     * 
     * Parameters:
     * deltaY: 카메라가 움직인 거리
     * spawn: 몬스터를 스폰할지 여부
     ************************************************************************/
    int lowerBoundary = Mathf.FloorToInt(Camera.main.transform.position.y - screenTileHeight / 2) - TILE_BUFFER;
    for (int y = tilemapBG.cellBounds.yMin; y < lowerBoundary + deltaY; y++)
    {
      int newY = tilemapBG.cellBounds.yMax;
      bool catnipSpawned = false;

      PlacementPattern pattern = SelectMonsterPattern();
      for (int x = tilemapBG.cellBounds.xMin; x < tilemapBG.cellBounds.xMax; x++)
      {
        Vector3Int oldPosition = new(x, y, 0);
        Vector3Int newPosition = new(x, newY, 0);

        // 타일을 새 위치로 옮김
        if (tilemapBG.HasTile(oldPosition))
        {
          int ObjectPos = x + Mathf.CeilToInt(MAP_WIDTH / 2) + 1;
          if (ObjectPos <= 5)
          {
            // if (newY % 48 == 0)
            //   SpawnItem(new Vector3Int(x + 1, newY, 0));
            if (pattern.GetType().GetField($"col{ObjectPos}").GetValue(pattern).Equals(1))
              SpawnMonster(new Vector3Int(x + 1, newY, 0));
            else if (!catnipSpawned && Random.value < catnipProbability)
            {
              Managers.Object.SpawnCatnip(new Vector3Int(x + 1, newY, 0));
              catnipProbability = Mathf.Max(0.1f, catnipProbability - 0.1f);
              catnipSpawned = true;
            }
          }
          TileBase tile = tilemapBG.GetTile(oldPosition);
          tilemapBG.SetTile(newPosition, tile);
          tilemapBG.SetTile(oldPosition, null);
        }

        if (tilemapWall.HasTile(oldPosition))
        {
          TileBase tile = tilemapWall.GetTile(oldPosition);
          tilemapWall.SetTile(newPosition, tile);
          tilemapWall.SetTile(oldPosition, null);
        }
      }
    }

    tilemapBG.CompressBounds();
    tilemapWall.CompressBounds();
  }

  private PlacementPattern SelectMonsterPattern()
  {
    /************************************************************************
     * Summary: 
     * 몬스터가 1줄에서 6줄 단위로 등장할 수 있는 패턴을 생성
     ************************************************************************/

    // 몬스터 등장 패턴이 없으면 새로운 패턴 추가
    if (currentPatterns.Count == 0)
    {
      if (consecutiveEmptyRows < MAX_EMPTY_ROWS)
      {
        consecutiveEmptyRows++;
        return new PlacementPattern
        {
          id = "Empty",
          patternId = "Empty",
          row = 1,
          col1 = 0,
          col2 = 0,
          col3 = 0,
          col4 = 0,
          col5 = 0
        };
      }

      float rateSum = 0f;
      float randomValue = Random.value;

      foreach (var mapPattern in mapPatternDict)
      {
        if (mapPattern.Value.checkpoint != Managers.Game.currentCheckpointIndex) continue;

        // 확률에 따라 패턴을 선택
        rateSum += mapPattern.Value.probability;
        if (randomValue <= rateSum)
        {
          var randomPatterns = mapPattern.Value.placementPatternIds;
          string patternId = randomPatterns[Random.Range(0, randomPatterns.Count)];

          foreach (var placementPattern in placementPatternDict)
          {
            if (placementPattern.Value.patternId == patternId)
              currentPatterns.Add(placementPattern.Value);
          }
          break;
        }
      }

      consecutiveEmptyRows = 0;
    }

    PlacementPattern pattern = currentPatterns[0];
    currentPatterns.RemoveAt(0);
    return pattern;
  }
  #endregion

  #region Obstacle

  private void SpawnMonster(Vector3 position)
  {
    /************************************************************************
     * Summary: 
     * 몬스터를 지정된 위치에 스폰
     * 챕터 & 진행도에 따라 등장하는 몬스터의 종류가 달라짐
     ************************************************************************/

    foreach (var spawnRate in monsterRateDict)
    {
      if (spawnRate.Key != Managers.Game.currentCheckpointIndex) continue;

      float rateSum = 0f;
      float randomValue = Random.value;
      foreach (var monster in spawnRate.Value)
      {
        rateSum += monster.Value;
        if (randomValue <= rateSum)
        {
          Managers.Object.SpawnMonster(position, monster.Key.ToString());
          return;
        }
      }
    }
  }

  private void SpawnItem(Vector3 position)
  {
    /************************************************************************
     * Summary: 
     * 몬스터를 지정된 위치에 스폰
     * 챕터 & 진행도에 따라 등장하는 몬스터의 종류가 달라짐
     ************************************************************************/

    Managers.Object.SpawnItem(position);
  }
  #endregion

  #region Utility
  public Vector3Int WorldToCell(Vector3 worldPosition)
  {
    /************************************************************************
     * Summary:
     * 월드 좌표를 타일 좌표로 변환
     ************************************************************************/
    return tilemapBG.WorldToCell(worldPosition);
  }

  public Vector3 CellToWorld(Vector3Int cellPosition)
  {
    /************************************************************************
     * Summary:
     * 타일 좌표를 월드 좌표로 변환
     ************************************************************************/
    return tilemapBG.CellToWorld(cellPosition);
  }

  public Vector3 GetTilemapCenter()
  {
    /************************************************************************
     * Summary:
     * 타일맵의 중앙 좌표를 반환
     ************************************************************************/
    float tilemapXCenter = (tilemapBG.cellBounds.xMin + tilemapBG.cellBounds.xMax) / 2;
    float tilemapYCenter = (tilemapBG.cellBounds.yMin + tilemapBG.cellBounds.yMax) / 2;
    return new Vector3(tilemapXCenter, tilemapYCenter, 0);
  }

  public float GetTilemapEdgeX()
  {
    /************************************************************************
     * Summary:ㄹ
     * 타일맵의 좌우 벽 위치를 반환
     ************************************************************************/
    return tilemapBG.cellBounds.xMax - tilemapBG.cellBounds.xMin;
  }
  #endregion
}