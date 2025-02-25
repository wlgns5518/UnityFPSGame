using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSGame
{
    public class CameraRig : MonoBehaviour
    {
        // 카메라가 따라다닐 대상.
        public Transform followTarget;

        // 이동할 때 딜레이(지연) 값.
        public float damping = 10f;

        // 회전할 때 딜레이(지연) 값.
        public float rotationDamping = 10f;

        // 상하 회전에 사용하는 변수.
        // 메인 카메라.
        public Transform cameraTransform;

        // 상하 회전 최소 각도.
        public float minAngle = -30f;

        // 상화 회전 최대 각도.
        public float maxAngle = 40f;

        // 카메라의 X축 누적 회전을 계산하기 위한 변수.
        private float xRotation = 0f;


        private void LateUpdate()
        {
            // 애니메이션 처리로 따라가기.
            transform.position = Vector3.Lerp(
                transform.position,
                followTarget.position,
                damping * Time.deltaTime
            );

            // 애니메이션 적용한 회전 처리.
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                followTarget.rotation,
                rotationDamping * Time.deltaTime
            );

            // 마우스 상하 드래그를 통해 위/아래 회전 설정.
            OnLook();

        }


        // 상하 최전을 처리하는 함수.
        void OnLook()
        {
            // 마우스 상하 드래그 값을 읽고, 그 값을 -1 ~ 1 사이로 고정.
            float mouseY = Mathf.Clamp(PlayerInputManager.Look, -1f, 1f);

            // 마우스 드래그 값을 사용해 x축 누적 회전 계산.
            xRotation -= mouseY;

            // 계산한 회전 값을 최소/최대 각도 사이의 값으로 고정.
            xRotation = Mathf.Clamp(xRotation, minAngle, maxAngle);

            // 회전 설정을 위한 각도 만들기.
            // 카메라의 현재 로컬 회전 각도를 오일러(Euler) 각으로 가져오기.
            Vector3 targetRotation = cameraTransform.localRotation.eulerAngles;

            // x축 회전을 앞서 구한 회전 값으로 설정.
            targetRotation.x = xRotation;

            // 카메라의 로컬 회전 값을 쿼터니언(4원수)으로 변환해 설정.
            cameraTransform.localRotation = Quaternion.Euler(targetRotation);
        }

        // 카메라의 X축 회전 값을 반환하는 함수.
        // 애니메이션 Aiming 레이어에서 사용.
        public float GetXRotation()
        {
            return xRotation;
        }

    }
}