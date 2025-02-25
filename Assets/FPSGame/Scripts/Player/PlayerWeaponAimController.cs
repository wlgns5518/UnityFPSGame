using UnityEngine;
using UnityEngine.UI;

namespace FPSGame
{
    // 플레이어의 발사 방향을 제어하는 스크립트.
    public class PlayerWeaponAimController : MonoBehaviour
    {
        // 발사 위치.
        public Transform muzzleTransform;

        // 조준 위치.
        public Image aimTarget;

        // 조준 방향에 물체가 있을 때 지정할 색상.
        public Color targetDetectedColor = Color.red;

        // 조준 방향에 물체가 없을 때 지정할 색상.
        public Color targetUndetectecColor = Color.white;

        // RayCast를 생성할 때 카메라를 사용하기 때문에 이를 저장하기 위한 변수 선언.
        private Camera mainCamera;

        // Start is called before the first frame update
        void Start()
        {
            // 메인 카메라 저장.
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        // Update is called once per frame
        void Update()
        {
            // UI 위치에서 카메라 방향으로 조준하는 Raycast 생성.
            Ray ray = mainCamera.ScreenPointToRay(aimTarget.transform.position);

            // 생성한 Ray를 발사(Raycast).
            if (Physics.Raycast(ray, out RaycastHit hit, 10000f))
            {
                // 조준된 물체가 있을 때, 조준 색상으로 설정.
                aimTarget.color = targetDetectedColor;

                // Ray와 부딪힌 물체가 있으면, 부딪힌 물체의 위치를 향하도록 발사 각도 조정.
                muzzleTransform.rotation = Quaternion.LookRotation(
                                hit.point - muzzleTransform.position
                            );
            }
            else
            {
                // 조준된 물체가 없으면, 원래 색상으로 설정.
                aimTarget.color = targetUndetectecColor;

                // Ray와 부딪힌 물체가 없으면, Ray 방향을 향하도록 발사 각도 조정.
                muzzleTransform.rotation = Quaternion.LookRotation(ray.direction);
            }
        }
    }
}