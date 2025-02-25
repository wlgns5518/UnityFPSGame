using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSGame
{
    public class PlayerAnimationController : MonoBehaviour
    {
        // Animator 컴포넌트 변수.
        public Animator animator;

        // 상하 조준 애니메이션 설정에 사용할 변수.
        public CameraRig cameraRig;
        public float rotationOffset = 0.5f;


        // Animator Controller에서 State 파라미터를 설정하는 함수.
        public void SetStateParameter(int state)
        {
            animator.SetInteger("State", state);
        }

        private void Update()
        {
            // 방향키 값을 읽기.
            //float horizontal = Input.GetAxis("Horizontal");
            //float vertical = Input.GetAxis("Vertical");

            // 파라미터에 설정.
            animator.SetFloat("Horizontal", PlayerInputManager.Horizontal);
            animator.SetFloat("Vertical", PlayerInputManager.Vertical);

            // 카메라 X축 회전 값을 읽고, 애니메이션에 맞게 오프셋 적용.
            // 이 값을 AimAngle 파리미터에 설정.
            animator.SetFloat("AimAngle", cameraRig.GetXRotation() 
                * rotationOffset);

        }

        // 재장전 애니메이션 재생 함수.
        // PlayerWeaponRifle에서 OnReloadEvent가 발생할 때 실행.
        public void OnReload()
        {
            // Reload 트리거를 설정해 재장전 애니메이션을 재생.
            animator.SetTrigger("Reload");
        }

        // 재장전 애니메이션이 완료될 때까지 걸리는 시간을 계산하는 함수.
        public float WaitTimeToReload()
        {
            // 두 번째 레이어(=Reload 레이어)에서 재생되고 있는 
            // 애니메이션의 길이 / 재생 속도(배수).
            return animator.GetCurrentAnimatorStateInfo(1).length
                / animator.GetFloat("ReloadSpeed");
        }

    }
}