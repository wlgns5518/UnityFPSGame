using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSGame
{
    public class PlayerStateManager : MonoBehaviour
    {
        // 플레이어 상태를 정의하는 열거형.
        public enum EPlayerState
        {
            Idle, Move, Dead
        }
        
        // 플레이어의 현재 상태를 추적하기 위한 변수.
        public EPlayerState currentState = EPlayerState.Idle;

        // 플레이어 가질 수 있는 모든 상태(스크립트).
        public PlayerState[] states;

        // 플레이어의 애니메이션 설정 컨트롤러.
        public PlayerAnimationController animationController;

        // 플레이어 데이터.
        public PlayerData data;

        // 상태 설정 함수.
        public void SetState(EPlayerState newState)
        {
            // 예외처리.
            if (currentState == newState || currentState == EPlayerState.Dead)
            {
                // 아래 코드 실행하지 않고, 함수 종료.
                return;
            }

            // 1. 현재 상태 스크립트를 비활성화.
            states[(int)currentState].enabled = false;

            // 2. 변경할 상태(새 상태)의 스크립트를 활성화.
            states[(int)newState].enabled = true;

            // 3. 현재 상태를 새로운 상태 값으로 설정.
            currentState = newState;

            // 4. 애니메이션 State 파라미터 설정.
            animationController.SetStateParameter((int)newState);
        }


        private void OnEnable()
        {
            // 시작할 때 대기(Idle) 스테이트로 시작하도록 설정.
            SetState(EPlayerState.Idle);

            // 스테이트 관리자가 가지고있는 모든 상태 스크립트에 data 설정.
            // foreach.
            foreach (PlayerState state in states)
            {
                state.SetData(data);
            }
        }


        private void Update()
        {
            // 상태 전환을 위한 코드.
            // 입력 확인.
            //float horizontal = Input.GetAxis("Horizontal");
            //float vertical = Input.GetAxis("Vertical");

            // 상태 전환 처리.
            // 입력이 없는지 확인.
            if (PlayerInputManager.Horizontal == 0f && 
                PlayerInputManager.Vertical == 0f)
            {
                // Idle 스테이트.
                SetState(EPlayerState.Idle);
            }
            else
            {
                // Move 스테이트.
                SetState(EPlayerState.Move);
            }

        }

        // 플레이어가 죽으면 실행되는 함수.
        public void OnPlayerDead()
        {
            SetState(EPlayerState.Dead);
        }
    }
}