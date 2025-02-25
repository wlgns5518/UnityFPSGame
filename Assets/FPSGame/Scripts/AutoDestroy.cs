using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSGame
{
    public class AutoDestroy : MonoBehaviour
    {
        // 삭제할 때 까지 대기 시간(초 단위).
        public float destroyTime = 3f;

        private void Start()
        {
            // 물체 삭제 명령.
            // destroyTime 시간이 지나면 나 자신(게임 오브젝트)을 삭제.
            // gameObject: 이 컴포넌트가 추가된 게임 오브젝트.
            Destroy(gameObject, destroyTime);
        }
    }
}