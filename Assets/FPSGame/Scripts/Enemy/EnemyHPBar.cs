using UnityEngine;

namespace FPSGame
{
    public class EnemyHPBar : MonoBehaviour
    {
        // HP Bar를 그릴 UI Canvas 에서 사용하는 카메라.
        private Camera uiCamera;

        // HP Bar를 그릴 UI Canvas 
        private Canvas canvas;

        // HP Bar를 생성할 부모 트랜스폼.
        private RectTransform rectParent;

        // HP Bar 트랜스폼.
        private RectTransform rectHP;

        // HP를 생성할 때 사용할 오프셋 (처음 설정은 0으로).
        [HideInInspector]
        public Vector3 offset = Vector3.zero;

        // HP가 따라다닐 물체.
        //[HideInInspector]
        public Transform targetTransform;

        private void Start()
        {
            // Canvas 컴포넌트를 찾아서 설정.
            canvas = GetComponentInParent<Canvas>();

            // UI 카메라 설정.
            uiCamera = canvas.worldCamera;

            // 부모 트랜스폼으로 사용할 Canvas의 RectTransform 구하기.
            rectParent = canvas.GetComponent<RectTransform>();

            // 내 트랜스폼 설정.
            rectHP = GetComponent<RectTransform>();
        }

        private void LateUpdate()
        {
            if (targetTransform == null)
            {
                return;
            }

            // 적위 위치(월드 공간)를 화면 공간으로 변환하기.
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position + offset);

            // 화면 좌표는 음수가 없기 때문에 값이 음수인 경우에는 양수로 만들어줌.
            if (screenPosition.z < 0f)
            {
                screenPosition *= -1f;
            }

            // 화면 공간의 위치로 변경한 값을 UI 공간으로 변환.
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                rectParent, screenPosition, uiCamera, out Vector2 localPosition
            );

            // 앞에서 구한 위치로 업데이트.
            rectHP.localPosition = localPosition;
        }
    }
}