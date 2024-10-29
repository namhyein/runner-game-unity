using UnityEngine;

public class CameraController : InitBase
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        Camera.main.orthographic = true;

        return true;
    }

    public void SetCamera(int numColumns, float tileSize)
    {
        /************************************************************************
         * Summary: 
         * 스크린 사이즈에 가로 화면이 딱 맞도록 카메라의 위치 (수직거리) 조정
		 *
		 * Parameters:
		 * numColumns: 한 줄에 들어갈 타일의 개수
		 * tileSize: 타일의 크기
		 ************************************************************************/

        float aspectRatio = (float)Screen.width / Screen.height;
        Camera.main.orthographicSize = numColumns * tileSize / 2f / aspectRatio;
    }

    public void FollowHero(Vector3 heroPosition, float destinationY = 4.0f)
    {
        /************************************************************************
		 * Summary: 
		 * 용사를 따라 카메라 이동
		 * 카메라는 용사의 Y 좌표에 맞춰 이동하며, X 좌표는 고정
		 * 카메라는 용사가 중앙에서 4칸 아래에 위치하도록 조정
		 *
		 * Parameters:
		 * heroPosition: 용사의 위치
		 ************************************************************************/

        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 destination = heroPosition + Vector3.up * destinationY;

        cameraPos.y = destination.y;
        Camera.main.transform.position = cameraPos;
    }
}
