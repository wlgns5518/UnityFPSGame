using System.Collections;
using UnityEngine;

namespace FPSGame
{
    // 카메라를 흔드는 효과 스크립트.
    public class CameraShake : MonoBehaviour
    {
        // 흔들 카메라의 트랜스폼 컴포넌트.
        public Transform targetCamera;

        // 카메라를 흔들 때 사용할 값.
        public float duration = 0.1f;           // 흔드는 시간(초 단위).
        public float oscillation = 0.02f;       // 흔들기 떨림 정도.

        // 카메라의 원래 위치.
        private Vector3 originPosition;

        void OnEnable()
        {
            // 시작할 때 원래 위치 저장.
            originPosition = targetCamera.localPosition;
        }

        // 카메라 흔들기 효과를 재생하는 함수.
        public void Play()
        {
            // ShakeCamera 코루틴 함수 실행.
            StartCoroutine("ShakeCamera");
        }

        // 코루틴(Coroutine)으로 카메라 흔드는 효과를 구현한 함수.
        IEnumerator ShakeCamera()
        {
            // 경과 시간을 계산할 변수 선언.
            float elapsedTime = 0f;

            // duration 변수에 설정한 시간이 지날 때까지 효과 재생.
            while (elapsedTime < duration)
            {
                // 카메라를 흔들 위치를 랜덤 값으로 구하기.
                Vector3 shakePosition = Random.insideUnitSphere;

                // 흔들 대상으로 지정한 카메라의 위치를 변경해 흔든다.
                // 공식 = 원래위치 + 랜덤 위치 * 흔드는 정도;
                targetCamera.localPosition =
                    originPosition + shakePosition * oscillation;

                // 경과 시간 업데이트.
                elapsedTime += Time.deltaTime;

                // 한 프레임 대기.
                yield return null;
            }

            // 흔드는 효과를 모두 재생한 다음에는 카메라 위치를 원래 위치로 설정.
            targetCamera.localPosition = originPosition;
        }
    }
}