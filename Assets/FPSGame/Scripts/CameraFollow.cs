using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSGame
{
    public class CameraFollow : MonoBehaviour
    {
        // Ctrl + K / Ctrl + D. -> 라인 맞추기.

        // 따라다닐 대상 (Transform).
        public Transform followTarget;

        // 따라다닐 대상과의 거리(처음에 한번만 계산).
        public Vector3 distance;

        // 얼마나 빨리 쫓아갈지를 결정하는 옵션.
        public float damping = 5f;


        private void Start()
        {
            // 대상과의 거리 계산.
            distance = transform.position - followTarget.position;
        }

        private void LateUpdate()
        {
            // 일정 거리를 유지하며 쫒아가기.
            //transform.position = followTarget.position + distance;

            // 시작 위치 (현재 위치).
            Vector3 currentPos = transform.position;

            // 이동할 목표 지점(위치).
            Vector3 targetPos = followTarget.position + distance;

            // 지연(Delay) 효과를 적용한 이동 구현.
            transform.position = Vector3.Lerp(
                currentPos,
                targetPos,
                damping * Time.deltaTime
            );


        }

    }
}