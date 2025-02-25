using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace FPSGame
{
    // 적 캐릭터의 공격 상태 스크립트.
    public class EnemyAttackState : EnemyState
    {
        // 적 캐릭터가 발사할 때 발생시킬 이벤트.
        public UnityEvent OnFireEvent;

        // 적 캐릭터가 장전할 때 발생시킬 이벤트.
        public UnityEvent OnReloadEvent;

        // 총을 발사하고 대기하는 시간.
        private float nextFireTime = 0f;

        // 적 캐릭터가 현재 가지고 있는 탄약 개수.
        private int currentBullet = 0;

        // 재장전 하는 중인지 확인하기 위한 변수.
        private bool isReload = false;

        // 재장전하기 위해 대기할 시간 변수.
        // 코루틴(Coroutine)에 사용.
        private WaitForSeconds wsReload = null;

        protected override void OnEnable()
        {
            base.OnEnable();

            // 적 캐릭터가 생성되면 현재 탄약 개수를
            // 데이터에 설정된 탄약 개수로 할당.
            if (currentBullet == 0)
            {
                currentBullet = data.maxBullet;
            }

            // 발사 간격 조정을 위한 시간 설정.
            if (wsReload == null)
            {
                wsReload = new WaitForSeconds(data.reloadTime);
            }

            // 시작할 때는 에이전트 정지.
            manager.StopAgent();

            // 발사 가능한 시간 설정.
            // 랜덤성 적용.
            nextFireTime = Time.time + data.fireRate + Random.Range(0f, 0.3f);

            // 플레이어가 죽으면 대기 상태로 전환.
            if (manager.IsPlayerDead == true)
            {
                manager.SetState(EnemyStateManager.State.Idle);
            }
        }

        protected override void Update()
        {
            base.Update();

            // 재장전 중이면, 아무런 처리 하지 않고 대기.
            if (isReload == true)
            {
                return;
            }

            // 플레이어가 죽으면 정찰 상태로 전환.
            if (manager.IsPlayerDead == true)
            {
                manager.SetState(EnemyStateManager.State.Patrol);
                return;
            }

            // 총 발사 처리.
            UpdateFireState();

            // 플레이어를 향하는 방향 계산.
            Vector3 directionToPlayer = manager.playerTransform.position - manager.enemyTransform.position;

            // 플레이어를 바라보도록 회전 처리.
            UpdateRotation(directionToPlayer, data.attackRotateDamping);

            // 플레이어와의 거리가 추적 가능 거리보다 멀어지면,
            // 정찰 상태로 전환.
            if (manager.DistanceToPlayerSqrt() > data.traceDistance * data.traceDistance)
            {
                manager.SetState(EnemyStateManager.State.Patrol);
            }

            // 플레이어가 살아있고,
            // 플레이어와의 거리가 공격 가능 거리보다 멀어지면,
            // 추적 상태로 전환.
            else if (manager.IsPlayerDead == false &&
                manager.DistanceToPlayerSqrt() > data.attackDistance * data.attackDistance)
            {
                manager.SetState(EnemyStateManager.State.Trace);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }

        // 총 발사 처리 함수.
        private void UpdateFireState()
        {
            // 발사가 가능한지 시간 값 확인.
            if (Time.time > nextFireTime)
            {
                // 발사가 가능하면, 발사 이벤트 발생.
                OnFireEvent?.Invoke();

                // 발사 간격 조정을 위해 다음번 발사할 시간 설정.
                nextFireTime = Time.time + data.fireRate + Random.Range(0.1f, 0.3f);

                // 발사 후에는 탄약 1개 감소.
                --currentBullet;

                // 재장전 여부 설정.
                // 남은 탄약 개수가 0이면 재장전 처리.
                isReload = currentBullet == 0;

                // 재장전이 필요하면, 재장전 실행.
                if (isReload == true)
                {
                    StartCoroutine(Reload());
                }
            }
        }

        // 재장전 처리 함수 (코루틴).
        private IEnumerator Reload()
        {
            // 0.5초 대기.
            yield return new WaitForSeconds(0.5f);

            // 재장전 이벤트 발생시키기.
            OnReloadEvent?.Invoke();

            // 재장전 시간 대기.
            yield return wsReload;

            // 탄약 개수를 초기 상태로 설정.
            currentBullet = data.maxBullet;

            // 재장전 완료.
            isReload = false;
        }
    }
}