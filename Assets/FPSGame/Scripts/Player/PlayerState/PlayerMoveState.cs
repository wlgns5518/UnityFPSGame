using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

namespace FPSGame
{
    public class PlayerMoveState : PlayerState
    {
        // 이동 속도.
        //public float speed = 5f;

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void Update()
        {
            base.Update();

            // 입력 받기.
            //float horizontal = Input.GetAxis("Horizontal");
            //float vertical = Input.GetAxis("Vertical");

            // 받은 입력을 바탕으로 이동 방향 만들기.
            //Vector3 direction = new Vector3(
            //    PlayerInputManager.Horizontal, 
            //    0f, 
            //    PlayerInputManager.Vertical
            //);

            Vector3 direction = 
                transform.right * PlayerInputManager.Horizontal
                + transform.forward * PlayerInputManager.Vertical;

            // 주석 처리 단축키: Ctrl + K / Ctrl + C.
            //// 이동 처리.
            //transform.position =
            //    transform.position
            //    + direction
            //    * speed
            //    * Time.deltaTime;

            characterController.Move(
                direction.normalized * data.moveSpeed * Time.deltaTime
            );

        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }
    }
}