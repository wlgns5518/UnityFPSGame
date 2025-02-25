using UnityEngine;

namespace FPSGame
{
    public class EnemyIdleState : EnemyState
    {
        // 변수.
        public float waitTime = 0f;         // 정찰할 때까지 대기할 시간(초).
        private float elapsedTime = 0f;     // 경과 시간을 계산할 때 사용할 변수.

        protected override void OnEnable()
        {
            base.OnEnable();

            // 대기 시간 설정
            // 0.8초 ~ 1.2초 사이의 숫자를 랜덤으로 설정.
            waitTime = Random.Range(data.patrolWaitTime * 0.8f, data.patrolWaitTime * 1.2f);

            // 경과시간 초기화.
            elapsedTime = 0f;
        }

        protected override void Update()
        {
            base.Update();

            // 경과 시간 업데이트.
            elapsedTime += Time.deltaTime;

            // 대기할 시간만큼 기다렸으면,
            if (elapsedTime > waitTime)
            {
                // 정찰 상태로 변경.
                manager.SetState(EnemyStateManager.State.Patrol);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            // 시간 관련 변수 초기화.
            elapsedTime = 0f;
            waitTime = 0f;
        }
    }
}