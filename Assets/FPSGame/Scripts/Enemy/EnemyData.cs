using System.Collections.Generic;
using UnityEngine;

namespace FPSGame
{
    public class EnemyData : MonoBehaviour
    {
        // 변수.
        public float attackDistance = 7f;           // 공격 가능 거리(단위 미터).
        public float traceDistance = 25f;           // 추격 시작 거리(단위 미터).

        public float patrolWaitTime = 1f;           // 정찰할 때까지 대기하는 시간(초).
        public float moveRotateDamping = 7f;        // 이동 시 회전 지연 값.

        public float attackRotateDamping = 5f;     // 공격 시 회전 지연 값.
        public float fireRate = 0.2f;               // 발사 간격(초).

        public int maxBullet = 10;                   // 최대 탄약 수.

        public float maxHP = 100f;                  // 체력

        public readonly float patrolSpeed = 1.5f;   // 정찰 시 이동 속도.
        public readonly float traceSpeed = 5f;    // 추격 시 이동 속도.
        public readonly float reloadTime = 2f;      // 재장전 시간.

        public List<Transform> waypoints;           // 정찰 위치 (배열).

        // 정찰 위치 초기화 함수.
        public void Initialize(Transform waypointGroup)
        {
            // waypointGroup 트랜스폼의 자식 트랜스폼을 모두 정찰 지점으로 설정.
            waypointGroup.GetComponentsInChildren(waypoints);

            // 0번 트랜스폼은 자기 자신이기 때문에 제거.
            waypoints.RemoveAt(0);
        }
    }
}