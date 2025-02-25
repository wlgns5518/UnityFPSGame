using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

namespace FPSGame
{
    // 게임 씬을 로드하면서 진행률을 보여주는 스크립트.
    public class SceneLoader : MonoBehaviour
    {
        public Image progressBar;          // 프로그레스 바 UI.
        public GameObject FrontBar;        //프로그레스 바 오프젝트
        public GameObject BackBar;          //프로그레스 바 오프젝트
        public GameObject Button;           //버튼 오프젝트
        public int loadSceneIndex = 1;          // 로드할 씬 인덱스.
        public float waitTimeToStart = 2f;      // 씬을 로드하기 전에 대기할 시간.

        private float elapsedTime = 0f;         // 경과 시간.

        private AsyncOperation async;           // 비동기로 씬 로드를 위해 필요함.

        private void OnEnable()
        {
            // 게임 씬 로드.
            StartCoroutine(LoadScene(1f));
        }
        private void Start()
        {
            FrontBar.SetActive(true);   //프로그레스바 활성화
            BackBar.SetActive(true);    //프로그레스바 활성화
            Button.SetActive(false);    //시작버튼 비활성화
        }

        private void Update()
        {

            // 씬을 비동기로 로드하면서, 로드 진행률 표시.
            if (async != null && async.progress >= 0.9f)
            {
                // 경과 시간 업데이트.
                elapsedTime += Time.deltaTime;

                // 프로그레스 바 UI 업데이트.
                progressBar.fillAmount = elapsedTime / waitTimeToStart;

                // 경과 시간이 대기 시간보다 더 지났으면, 게임씬 활성화.
                if (elapsedTime > waitTimeToStart)
                {
                    async.allowSceneActivation = true;
                }
            }
        }

        IEnumerator LoadScene(float time)
        {
            // time 시간 만큼 대기.
            // 바로 게임씬이 로드되는 경우가 있어 대기 시킴.
            yield return new WaitForSeconds(time);

            // 경과 시간 변수 초기화.
            elapsedTime = 0f;

            // 비동기로 게임씬 로드.
            async = SceneManager.LoadSceneAsync(loadSceneIndex);

            // 원하는 대기시간 전까지는 게임씬 비활성화.
            async.allowSceneActivation = false;

            // 씬이 로드되기 전까지 대기.
            yield return async;
        }
    }
}