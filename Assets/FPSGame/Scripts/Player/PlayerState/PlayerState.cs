using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSGame
{
    public class PlayerState : MonoBehaviour
    {

        // 캐릭터 컨트롤러 컴포넌트.
        public CharacterController characterController;

        // 회전 속도 변수.
        //public float rotationSpeed = 540f;

        // 플레이어 데이터 ScriptableObject.
        protected PlayerData data;

        // 플레이어 데이터를 설정하는 함수.
        // 의존성 주입 - Dependency Injection.
        public void SetData(PlayerData data)
        {
            // this 키워드는 나자신(PlayerState)를 가리킨다.
            this.data = data;
        }

        // 스테이트 진입점.
        protected virtual void OnEnable()
        {
            // 캐릭터 컨트롤러가 설정되지 않았으면, 검색해서 설정.
            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }
        }

        // 스테이트 업데이트.
        protected virtual void Update()
        {
            // 캐릭터에 중력 적용.
            Vector3 gravity = new Vector3(0f, -9.8f, 0f);
            characterController.Move(gravity * Time.deltaTime);

            // 회전 방향 설정.
            Vector3 rotation = new Vector3(
                0f,
                PlayerInputManager.Turn * data.rotationSpeed * 
                Time.deltaTime,
                0f
            );

            // 캐릭터 좌우 회전.
            transform.Rotate(rotation);

        }

        // 스테이트 종료.
        protected virtual void OnDisable()
        {
            
        }

    }
}