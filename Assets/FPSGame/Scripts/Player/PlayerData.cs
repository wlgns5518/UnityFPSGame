using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSGame
{
    // 생성 메뉴.
    [CreateAssetMenu(fileName = "PlayerData", 
        menuName = "ScriptableObject/Create PlayerData")]
    public class PlayerData : ScriptableObject
    {
        // 플레이어 데이터.
        public float moveSpeed = 5f;        // 이동속도.
        public float rotationSpeed = 540f;  // 회전속도.
        public float maxHP = 100f;          // 체력(HP).
        public int maxAmmo = 20;            // 탄창에 채울 수 있는 탄약 수.
    }
}