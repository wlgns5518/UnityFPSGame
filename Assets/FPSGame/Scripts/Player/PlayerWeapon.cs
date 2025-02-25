using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSGame
{
    public class PlayerWeapon : MonoBehaviour
    {
        // WeaponHolder에 무기를 장착하는 함수.
        public void LaunchWeapon(Transform weaponHolder)
        {
            // 부모-자식 계층 설정.
            transform.SetParent(weaponHolder);

            // 트랜스폼 초기화.
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        // 탄약 발사할 때 사용할 함수.
        public virtual void Fire()
        {

        }
    }
}