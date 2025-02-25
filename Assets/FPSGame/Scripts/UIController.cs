using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPSGame
{
    public class UIController : MonoBehaviour
    {
        // 탄약 정보 텍스트.
        public TMPro.TextMeshProUGUI ammoText;

        // 무기 아이콘 이미지.
        public Image weaponIconImage;

        // 재장전 애니메이션 재생 시간 확인을 위해 추가.
        public PlayerAnimationController animationController;

        // 현재 재장전 중인지 확인하기 위한 변수.
        public bool isReloading = false;

        // 누적 시간 계산을 위한 변수.
        public float elapsedTime = 0f;


        // 탄약 수가 변경되면 호출될 함수.
        // OnAmmoCountChanged 이벤트가 발생되면 호출되도록 구독.
        public void OnAmmoCountChanged(int maxAmmo, int currentAmmo)
        {
            // 현재 탄약 수 / 최대 탄약수 정보를 UI에 표시.
            //ammoText.text = "<color=red>" + currentAmmo.ToString()
            //    + "</color>/" + maxAmmo.ToString();
            //ammoText.text = string.Format("<color=red>{0}</color>/{1}",
            //    currentAmmo, maxAmmo);
            ammoText.text = $"<color=red>{currentAmmo}</color>/{maxAmmo}";
        }

        // 재장전 이벤트에 구독할 함수.
        // OnReloadEvent 이벤트가 발생되면 호출됨.
        public void OnReloadStarted()
        {
            // 재장전 애니메이션을 위해 경과 시간 계산 변수 초기화.
            elapsedTime = 0f;

            // 재장전 설정.
            isReloading = true;
        }

        private void Update()
        {
            // 재장전 중인 경우, 이미지 애니메이션 재생.
            if (isReloading == true)
            {
                // 재장전이 완료되기까지 얼마나 지났는지 계산(퍼센티지 계산).
                weaponIconImage.fillAmount =
                    elapsedTime / animationController.WaitTimeToReload();

                // 경과 시간 업데이트.
                elapsedTime += Time.deltaTime;
                //elapsedTime = elapsedTime + Time.deltaTime;

                // 재장전 완료 시간이 지났는지 확인.
                if (elapsedTime > animationController.WaitTimeToReload())
                {
                    // 이미지를 최대로 채움.
                    weaponIconImage.fillAmount = 1f;

                    // 재장전 완료 설정.
                    isReloading = false;
                }

            }
        }

    }
}