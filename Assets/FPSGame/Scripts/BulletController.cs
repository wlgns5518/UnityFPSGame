using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSGame
{
    // 탄약이 어떤 물체와 충돌하면, 충돌 위치에 데칼(Decal) 효과를 보여주는 스크립트.
    public class BulletController : MonoBehaviour
    {
        // 충동한 지점에 보여줄 데칼(Decal) 효과.
        public GameObject collisionDecal;

        // 충돌 처리.
        // Collision 유형으로 충돌을 시작했을 때 유니티에서 자동으로 실행해주는 함수.
        private void OnCollisionEnter(Collision collision)
        {
            // 충돌한 물체의 레이어를 확인. Wall이면 탄흔 표시.
            if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                // 충돌 지점에 대한 위치 받아오기.
                // 충돌 지점 중에 첫 번째 충돌 지점 정보 읽기.
                ContactPoint contact = collision.contacts[0];

                // 데칼(-> 이미지) 회전 설정.
                // 데칼의 회전 방향을 충돌 지점의 노멀(법선) 방향을 바라보도록 계산.
                Quaternion rotation = Quaternion.LookRotation(contact.normal);

                // 데칼 물체 생성(-> 프리팹(Prefab)으로 생성).
                // contact.point: 충돌 지점 위치.
                // rotation: 생성할 회전 값.
                Instantiate(collisionDecal, contact.point, rotation);
            }

            // 탄약 게임 오브젝트 삭제.
            Destroy(gameObject);
        }
    }
}