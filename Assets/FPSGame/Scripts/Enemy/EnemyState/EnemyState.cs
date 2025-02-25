using UnityEngine;
using UnityEngine.AI;

namespace FPSGame
{
    public class EnemyState : MonoBehaviour
    {
        // 변수.
        protected EnemyStateManager manager;
        protected EnemyData data;               // 적 데이터.
        protected Transform myTransform;        // 트랜스폼 컴포넌트.

        // 데이터를 설정해주는 함수.
        // EnemyStateManager에서 시작할 때 설정해줌.
        public void SetData(EnemyData data)
        {
            this.data = data;
        }

        protected virtual void OnEnable()
        {
            // 컴포넌트 초기화.
            if (manager == null)
            {
                manager = GetComponent<EnemyStateManager>();
            }

            if (myTransform == null)
            {
                myTransform = GetComponent<Transform>();
            }
        }

        protected virtual void Update()
        {

        }

        protected virtual void OnDisable()
        {

        }

        // 정찰/추적할 때 사용 할 회전 업데이트 함수.
        protected virtual void UpdateRotation(float damping)
        {
            // 목표 위치에 도달하지 않은 경우, 목표 위치를 향하도록 회전 업데이트.
            if (manager.agent.desiredVelocity != Vector3.zero)
            {
                // 적 AI 캐릭터의 이동 방향을 바라보도록 목표 회전 방향 계산.
                Quaternion rotation = Quaternion.LookRotation(manager.agent.desiredVelocity);

                // Slerp 함수를 사용해 부드럽게 회전 처리.
                myTransform.rotation = Quaternion.Slerp(
                    myTransform.rotation, rotation, damping * Time.deltaTime
                );
            }
        }

        // 캐릭터를 공격할 때 사용할 회전 업데이트 함수.
        protected virtual void UpdateRotation(Vector3 target, float damping)
        {
            if (target != Vector3.zero)
            {
                // 적 AI 캐릭터가 target 방향을 바라보도록 회전 설정.
                Quaternion rotation = Quaternion.LookRotation(target);

                // Slerp 함수를 사용해 부드럽게 회전 처리.
                myTransform.rotation = Quaternion.Slerp(
                    myTransform.rotation, rotation, damping * Time.deltaTime
                );
            }
        }
    }
}