using System;
using System.Collections.Generic;
using Ingame;
using UnityEngine;
using static Define;

public class GameManager
{
    public bool isRunning = false;
    public bool gyroEnabled = true;
    public int currentCheckpointIndex = 1;

    public event Action<int> OnCatnipChanged;
    public event Action<float> OnDistanceChanged;
    public event Action<float> OnHeroSpeedChanged;
    public event Action<Vector2> OnMoveDirChanged;
    public event Action<ETouchPadState> OnTouchPadStateChanged;

    public float heroSpeed;

    private int catnip = 0;
    private float distance = 0;
    private float itemProbability = 0.4f;
    private const float horizontalTiltThreshold = 0.1f;

    private Hero hero = null;
    private Gyroscope gyro = null;
    private List<Item> items = new();

    public int Catnip
    {
        get { return catnip; }
        set
        {
            catnip = value;
            OnCatnipChanged?.Invoke(value);
        }
    }

    public float Distance
    {
        get { return distance; }
        set
        {
            distance = value;
            OnDistanceChanged?.Invoke(value);
        }
    }

    #region Game Main Logic
    public void Init()
    {
        /************************************************************************
         * Summary: 
         * 게임 초기화
         ************************************************************************/
        gyro = Input.gyro;
        gyro.enabled = true;

        Managers.Map.Init();
        Managers.Map.LoadMap();

        hero = SpawnHero();
    }

    public void Start()
    {
        /***************************************
         * Summary: 
         * 게임 시작
         ***************************************/
        SetCheckpointStat();

        isRunning = true;
        hero.Move();
    }

    public void Over()
    {
        /**************************************************************************
         * Summary: 
         * 게임 종료
         ************************************************************************/
        isRunning = false;
        hero.Dead();
    }

    public void Clear()
    {
        /**************************************************************************
         * Summary: 
         * 게임 클리어
         ************************************************************************/
    }

    public void Resume()
    {
        /**************************************************************************
         * Summary: 
         * 게임 재개
         ************************************************************************/
        // Countdown 후 게임 재개
        isRunning = true;
    }

    public void Pause()
    {
        /**************************************************************************
         * Summary: 
         * 게임 일시정지
         ************************************************************************/
        isRunning = false;
    }
    #endregion

    public Vector3 GetHeroPosition()
    {
        /************************************************************************
         * Summary: 
         * 용사의 위치를 반환
         ************************************************************************/

        return hero.transform.position;
    }

    public void MoveHeroForward()
    {
        if (!isRunning || !hero.IsMovable) return;

        Vector3 forwardMove = heroSpeed * Time.deltaTime * Vector3.up;
        Vector3 horizontalMove = Vector3.zero;

#if !UNITY_EDITOR
        float tilt = Input.acceleration.x;
        if (Mathf.Abs(tilt) > horizontalTiltThreshold)
            horizontalMove = new Vector3(tilt * heroSpeed * 3 * Time.deltaTime, 0, 0);
#else
        float tilt = Input.GetAxis("Horizontal");
        horizontalMove = new Vector3(tilt * heroSpeed * 3 * Time.deltaTime, 0, 0);
#endif
        horizontalMove.x = Mathf.Min(Mathf.Max(horizontalMove.x, -2.0f), 2.0f);

        Vector3 totalMove = forwardMove + horizontalMove;
        hero.MoveTo(totalMove);
        Distance += heroSpeed * Time.deltaTime;
    }

    public void UpdateCheckpointStat()
    {
        if (Distance < Managers.Data.CheckPoints[$"C0{currentCheckpointIndex}"].cumulative)
            return;
        if (Managers.Data.CheckPoints.ContainsKey($"C0{currentCheckpointIndex + 1}"))
            currentCheckpointIndex++;

        SetCheckpointStat();
    }

    private void SetCheckpointStat()
    {
        Checkpoint currentCheckpoint = Managers.Data.CheckPoints[$"C0{currentCheckpointIndex}"];
        itemProbability = currentCheckpoint.probabilityIncrease;
        heroSpeed = currentCheckpoint.speed;
    }

    public void HeroJump()
    {
        if (!isRunning) return;
        hero.Jump();
    }

    public void HeroRoll()
    {
        if (!isRunning) return;
        hero.Attack();
    }

    public bool CheckifDie()
    {
        return Catnip <= 0;
    }

    public void LoseCoin()
    {
        int minLose = Mathf.Max(3, Mathf.CeilToInt(catnip * 0.3f));
        Catnip = Mathf.Max(0, catnip - minLose);
    }

    public void EarnCoin()
    {
        Catnip += 1;
    }

    public void UseItem()
    {
        if (items.Count == 0) return;

        Item item = items[0];
        items.RemoveAt(0);
    }

    private Hero SpawnHero()
    {
        /************************************************************************
         * Summary: 
         * 용사가 스폰될 초기 위치를 계산 (맵의 중앙에서 4칸 아래)
         ************************************************************************/

        Vector3 position = Managers.Map.GetTilemapCenter();
        position.y -= 4.0f;

        Hero hero = Managers.Object.SpawnHero(position);

        return hero;
    }

    public void CheckIfCheckpoint()
    {

    }
}
