using UnityEngine;

namespace FPSGame
{
    // 적 AI 캐릭터의 정찰 상태 스크립트.
    public class EnemyPatrolState : EnemyState
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            // 다음 정찰 지점으로 이동.
            MoveToWaypoint();
        }

        protected override void Update()
        {
            base.Update();

            // 회전 업데이트.
            UpdateRotation(data.moveRotateDamping);

            // 정찰 지점에 거의 도착 했는지 확인.
            if (manager.agent.velocity.sqrMagnitude >= 0.2f * 0.2f && manager.agent.remainingDistance <= 0.5f)
            {
                // 거의 도착했으면 대기(Idle) 상태로 변경.
                manager.SetState(EnemyStateManager.State.Idle);
            }

            // 플레이어가 살아있고,
            // 플레이어와 적 캐릭터의 거리가 추적 가능 거리보다 작으면
            // Trace 상태로 전환.
            if (manager.IsPlayerDead == false &&
                manager.DistanceToPlayerSqrt() <= data.traceDistance * data.traceDistance)
            {
                manager.SetState(EnemyStateManager.State.Trace);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        private void MoveToWaypoint()
        {
            if (manager.agent.isPathStale == false)
            {
                int index = Random.Range(0, data.waypoints.Count);
                manager.SetAgentDestination(data.waypoints[index].position, data.patrolSpeed);
            }
        }
    }
}