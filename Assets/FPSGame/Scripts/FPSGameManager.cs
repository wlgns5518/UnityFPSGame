using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace FPSGame
{
    // 게임 매니저(관리자) 스크립트.
    public class FPSGameManager : MonoBehaviour

    {
        //카메라 조작 컴포넌트
        public CameraRig camerarig;

        // 적 캐릭터를 생성할 스폰 지점을 하위로 가지는 최상위 게임 오브젝트.
        public Transform spawnGroup;

        // 적 캐릭터를 생성할 트랜스폼 배열.
        public List<Transform> points;

        // 적 캐릭터 프리팹.
        public GameObject enemy;

        // 생성 시간 간격(초 단위).
        public float createTime = 1f;

        // 맵에 최대로 배치할 수 있는 적의 수.
        // 생성할 시점에 현재 배치된 적 캐릭터의 수를 확인하고,
        // maxEnemy 보다 작은 경우에만 추가로 생성.
        public int maxEnemy = 40;

        // 게임 종료 여부를 확인하기 위한 변수.
        public bool isGameOver = false;

        // 관리자(매니저) 클래스를 싱글톤으로 구성.
        // 싱글톤(Singleton)
        public static FPSGameManager Instance = null;

        // 플레이어가 획득한 킬(Kill) 수.
        public int killcount = 0;

        // 킬 수를 UI로 보여줄 때 사용하는 Text 컴포넌트.
        public TMPro.TextMeshProUGUI killcountText;

        private void OnEnable()
        {
            // 싱글톤 구성.
            if (Instance == null)
            {
                Instance = this;
            }

            // 이미 다른 게임 오브젝트가 있는 경우에는 이 게임 오브젝트 삭제.
            else if (Instance != null)
            {
                Destroy(gameObject);
            }


            // 생성 지점 배열 설정.
            if (spawnGroup != null)
            {
                points = new List<Transform>();
                spawnGroup.GetComponentsInChildren<Transform>(points);
                points.RemoveAt(0);

                // 생성 지점이 설정된 경우에만 생성.
                if (points.Count > 0)
                {
                    // 코루틴(Coroutine).
                    StartCoroutine(CreateEnemy());
                }
            }
        }

        // 적 캐릭터 생성 함수.
        IEnumerator CreateEnemy()
        {
            // 게임 종료가 아닌 경우에만 생성.
            while (isGameOver == false)
            {
                // Enemy 태그를 가진 게임 오브젝트를 모두 검사해 현재 맵에 배치된 
                // 적 캐릭터 수 확인.
                int enemyCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;

                // 현재 생성된 적 캐릭터가 maxEnemy에 설정된 숫자보다
                // 작은 경우에만 추가로 적 생성.
                if (enemyCount < maxEnemy)
                {
                    // createTime 초 만큼 대기.
                    yield return new WaitForSeconds(createTime);

                    // 생성 지점 랜덤으로 선택.
                    int index = Random.Range(0, points.Count);

                    // 적 캐릭터 생성.
                    Instantiate(enemy, points[index].position, points[index].rotation);
                }

                // 적 캐릭터가 maxEnemy 만큼 생성돼 있으면, 추가로 생성하지 않고 대기.
                else
                {
                    yield return new WaitForSeconds(createTime);
                }
            }
        }

        // 게임 종료 함수.
        public static void GameOver()
        {
            Instance.isGameOver = true;
            SceneManager.LoadScene("Endig");     //엔딩씬 호출

        }

        // 플레이어가 적 캐릭터를 처치했을 때 사용할 함수.
        // 점수 획득.
        public static void IncreaseKillCount()
        {
            // 플레이어가 획득한 킬(Kill) 수 증가시키기(점수 획득).
            ++Instance.killcount;

            // UI로 표시하기 위해 점수를 텍스트로 변환해서 설정하기.
            if (Instance.killcountText != null)
            {
                Instance.killcountText.text = "KILL " + Instance.killcount.ToString("0000");
            }
        }
    }
}