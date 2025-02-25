using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace FPSGame
{
    // 사용자 정의 유니티 이벤트 클래스 선언.
    // 이벤트 발생시 제공할 파라미터 타입이 기본형이 아닌 경우에는 클래스를 선언해야 함.
    // 사용자 정의 클래스를 인스펙터에서 확인 가능하려면 [System.Serializable] 어트리뷰트를 추가해야 함.
    [System.Serializable]
    public class EnemyStateChangedEvent : UnityEvent<EnemyStateManager.State>
    {
    }

    // 적 AI 캐릭터의 FSM 관리자 스크립트.
    public class EnemyStateManager : MonoBehaviour
    {
        // 적 캐릭터 상태를 열거형으로 정의.
        public enum State
        {
            Idle, Patrol, Trace, Attack, Dead
        }

        // 적 캐릭터의 현재 상태.
        public State state = State.Idle;

        // 적 캐릭터의 상태 스크립트 배열.
        public EnemyState[] states;

        // 적 캐릭터의 상태가 변경됐을 때 발행할 이벤트.
        public EnemyStateChangedEvent OnEnemyStateChanged;

        // 적 캐릭터 데이터.
        public EnemyData data;

        // 추격 가능 거리에 접근했을 때 쫓아갈 플레이어 트랜스폼.
        public Transform playerTransform;

        // 적 캐릭터 트랜스폼.
        public Transform enemyTransform;

        // 길찾기 기능을 가진 NavMeshAgent 컴포넌트.
        public NavMeshAgent agent;

        // 플레이어가 죽었는지 확인.
        public bool IsPlayerDead = false;

        private void OnEnable()
        {
            // 회전을 정지 상태로 설정.
            agent.updateRotation = false;

            // 적 캐릭터 트랜스폼 설정.
            if (enemyTransform == null)
            {
                enemyTransform = GetComponent<Transform>();
            }

            // NavMeshAgent 설정.
            if (agent == null)
            {
                agent = GetComponent<NavMeshAgent>();
            }

            // 적 캐릭터 데이터에 정찰 지점 설정.
            data.Initialize(GameObject.FindGameObjectWithTag("WaypointGroup").transform);

            // 모든 상태 스크립트에 데이터 설정.
            foreach (EnemyState state in states)
            {
                state.SetData(data);
            }

            // Player 태그로 플레이어의 트랜스폼 검색 후 설정.
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

            // 플레이어가 죽었을 때 발생하는 이벤트에 등록.
            PlayerDamageController.SubscribePlayerDeadEvent(OnPlayerDead);

            // 시작 상태를 대기 상태로 설정.
            SetState(State.Idle);
        }

        public void SetState(State newState)
        {
            // 적 캐릭터가 죽어 있으면, 처리 안하고 함수 종료.
            if (state == State.Dead)
            {
                return;
            }

            // 현재 상태 스크립트를 비활성화.
            states[(int)state].enabled = false;

            // 현재 상태 열거형 값을 변경할 상태로 업데이트.
            state = newState;

            // 변경할 상태 스크립트를 활성화.
            states[(int)state].enabled = true;

            // 적 캐릭터의 상태 변경 이벤트 발행.
            OnEnemyStateChanged?.Invoke(newState);
        }

        // NavMeshAgent를 멈춤.
        public void StopAgent()
        {
            // 정지 상태로 설정.
            agent.isStopped = true;

            // 이동 속도 초기화.
            agent.velocity = Vector3.zero;
        }

        // 이동을 위해 NavMeshAgent의 목표지점 설정.
        public void SetAgentDestination(Vector3 destination, float speed = 0f)
        {
            // 목표 위치 지정.
            agent.destination = destination;

            // 파라미터로 지정된 speed 값이 0이면, 정찰 속도 설정.
            // speed 파라미터에 값이 전달되면, 전달된 값으로 설정.
            agent.speed = speed == 0f ? data.patrolSpeed : speed;

            // 정지 상태를 이동 상태로 변경.
            agent.isStopped = false;
        }

        // 캐릭터와 플레이어 사이의 거리의 제곱 값 계산.
        // 거리를 제대로 계산하려면 제곱근 계산을 해야 하는데, 이 계산이 비용이 비쌈.
        // 정확한 거리 계산이 필요하지 않고, 대략적인 정보가 필요할 때는 
        // 제곱근 계산을 생략한 값을 사용할 수 있다.
        public float DistanceToPlayerSqrt()
        {
            return (playerTransform.position - enemyTransform.position).sqrMagnitude;
        }

        // 플레이어가 죽었을 때 실행되는 함수.
        public void OnPlayerDead()
        {
            IsPlayerDead = true;
            SetState(State.Idle);
        }

        // 적이 죽었을 때 실행될 함수.
        public void OnEnemyDead()
        {
            SetState(State.Dead);
        }

    }
}