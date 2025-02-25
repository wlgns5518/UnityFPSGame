using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSGame
{
    public class PlayerWeaponController : MonoBehaviour
    {
        // 무기를 장착할 플레이어 손(뼈대)의 위치.
        public Transform weaponHolder;

        // 장착할 무기.
        public PlayerWeapon weapon;

        private void Start()
        {
            // 무기 장착.
            weapon.LaunchWeapon(weaponHolder);
        }

        private void Update()
        {
            // 입력 확인.
            if (PlayerInputManager.IsFire == true)
            {
                // 발사 명령 전달.
                weapon.Fire();
            }
        }

    }
}