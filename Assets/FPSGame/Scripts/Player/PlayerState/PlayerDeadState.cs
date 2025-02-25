using UnityEngine;

namespace FPSGame
{
    // 플레이어가 죽을 때 실행되는 상태 스크립트.
    public class PlayerDeadState : PlayerState
    {
        // 적 캐릭터가 발사한 총알과 충돌하는 충돌 컴포넌트.
        public CapsuleCollider playerCollider;


        protected override void OnEnable()
        {
            base.OnEnable();

            // 캐릭터가 죽으면 충돌 컴포넌트 비활성화.
            playerCollider.enabled = false;

            // 플레이어가 죽으면, 게임 오버 처리.
            FPSGameManager.GameOver();
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }
    }
}