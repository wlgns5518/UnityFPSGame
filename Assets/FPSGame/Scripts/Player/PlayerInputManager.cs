using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSGame
{
    public class PlayerInputManager : MonoBehaviour
    {
        // 입력 변수.
        public static float Horizontal;
        public static float Vertical;

        // 캐릭터 회전에 사용.
        public static float Turn;       // 마우스 좌우 드래그.
        public static float Look;       // 마우스 상하 드래그.

        // 탄약 발사에 사용.
        // 부울(Boolean) 참 또는 거짓.
        public static bool IsFire;      // 마우스 왼쪽 클릭.

        // 플레이어가 죽었는지 확인.
        public bool isPlayerDead = false;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update()
        {

            // 플레이어가 죽으면 입력처리 안함.
            if (isPlayerDead == true)
            {
                Cursor.lockState = CursorLockMode.None;
                return;
            }

            // 입력을 읽어서 변수에 저장.
            Horizontal = Input.GetAxis("Horizontal");
            Vertical = Input.GetAxis("Vertical");

            Turn = Input.GetAxis("Mouse X")*0.25f;
            Look = Input.GetAxis("Mouse Y");

            // 마우스 클릭 입력을 읽어서 변수에 저장.
            // 0: 왼쪽 버튼 / 1: 오른쪽 버튼 / 2: 휠 버튼.
            //IsFire = Input.GetMouseButtonDown(0);
            IsFire = Input.GetMouseButton(0);
        }

        // 플레이어가 죽으면 실행될 함수.
        public void OnPlayerDead()
        {
            // 플레이어가 죽었다고 처리.
            isPlayerDead = true;
        }

    }
}