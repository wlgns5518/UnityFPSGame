using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 네임 스페이스/이름 공간.
namespace FPSGame
{
    public class PlayerControl : MonoBehaviour
    {

        // 플레이어의 상태(스테이트, 동작)을 나타내는 열거형(Enum).
        public enum EPlayerState
        {
            Idle, Move
        }

        // 이동 속도.
        public float speed = 5f;

        // Animator 컴포넌트 변수.
        public Animator animator;

        // 플레이어 상태.
        public EPlayerState state = EPlayerState.Idle;

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical);

            transform.position = 
                transform.position + direction * speed * Time.deltaTime;

            // 애니메이션 상태 설정.
            // 입력이 없으면 state를 Idle로 설정, 있으면 state를 Move로 설정.
            // 열거형을 이용해서 애니메이션의 State 파라미터 설정.
            if (horizontal == 0f && vertical == 0f)
            {
                state = EPlayerState.Idle;
            }
            else
            {
                state = EPlayerState.Move;
            }

            // 애니메이션 설정.
            animator.SetInteger("State", (int)state);
        }
    }
}