using UnityEngine;

namespace FPSGame
{
    // 적 캐릭터 죽음 상태 스크립트.
    public class EnemyDeadState : EnemyState
    {
        // 플레이어 탄약(총알)과 충돌할 때 사용하는 콜라이더(충돌체) 컴포넌트.
        // 적 캐릭터가 죽으면 충돌을 끄기 위해 사용.
        public CapsuleCollider enemyCollider;

        // 삭제할 적 캐릭터의 최상위 게임 오브젝트.
        // 적 캐릭터가 계층구조로 구성됐기 때문에,
        // 적 캐릭터를 삭제할 때는 최상위 게임 오브젝트를 삭제해야함.
        public GameObject destroyTarget;

        protected override void OnEnable()
        {
            base.OnEnable();

            // 죽었기 때문에 정지.
            manager.StopAgent();

            // 충돌 비활성화.
            enemyCollider.enabled = false;

            // 5초 후 적 캐릭터 삭제.
            // 5초 정도면 Dead 애니메이션 재생이 완료될 충분한 시간.
            Destroy(destroyTarget, 5f);

            // 태그 값 초기화.
            transform.tag = "Untagged";

            // 플레이어의 킬(Kill) 수 증가 처리.
            FPSGameManager.IncreaseKillCount();
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