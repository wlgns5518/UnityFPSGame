using UnityEngine;

namespace FPSGame
{
    // 적 캐릭터의 추적 상태 스크립트.
    public class EnemyTraceState : EnemyState
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            // 플레이어의 위치 파악.
            UpdatePlayerPosition();

            // 플레이어가 죽었으면, 대기 상태로 전환.
            if (manager.IsPlayerDead == true)
            {
                manager.SetState(EnemyStateManager.State.Idle);
            }
        }

        protected override void Update()
        {
            base.Update();

            // 플레이어의 위치 파악.
            UpdatePlayerPosition();

            // 회전 업데이트.
            UpdateRotation(data.moveRotateDamping);

            // 플레이어와의 거리가 추적 가능 거리보다 멀어지면, 정찰 상태로 전환.
            if (manager.DistanceToPlayerSqrt() > data.traceDistance * data.traceDistance)
            {
                manager.SetState(EnemyStateManager.State.Patrol);
            }

            // 플레이어가 살아있고,
            // 공격 가능 거리 안으로 접근했으면 공격 상태로 전환.
            else if (manager.IsPlayerDead == false &&
                manager.DistanceToPlayerSqrt() <= data.attackDistance * data.attackDistance)
            {
                manager.SetState(EnemyStateManager.State.Attack);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        private void UpdatePlayerPosition()
        {
            // manager.agent.isPathStale은 현재 설정된 경로가 유효한지 확인할 때 사용.
            if (manager.agent.isPathStale == false)
            {
                // 플레이어의 위치를 에이전트의 목표 위치로 설정.
                manager.SetAgentDestination(
                    manager.playerTransform.position, manager.data.traceSpeed
                );
            }
        }
    }
}