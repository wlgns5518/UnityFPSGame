using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace FPSGame
{
    public class PlayerDamageController : MonoBehaviour
    {
        // 적 캐릭터의 총알을 확인하기 위한 태그 값.
        private const string bulletTag = "EnemyBullet";

        // 플레이어 캐릭터 데이터.
        public PlayerData data;

        // 현재 체력.
        public float currentHP = 0f;

        // 피격효과 재생을 위한 UI Image 컴포넌트.
        public Image bloodEffect;

        // 체력 HP UI.
        public Image hpBar;

        // 체력 게이지에 적용할 초기 색상 값.
        private readonly Color initColor = new Color(0f, 1.0f, 0f, 1.0f);

        // 체력 게이지 현재 색상 값.
        private Color currentColor;

        // 플레이어가 죽을 때 발생하는 이벤트.
        public UnityEvent OnPlayerDeadEvent;

        // 다른 스크립트 어디에서나 접근할 수 있도록 스태틱으로 설정.
        private static PlayerDamageController instance;

        // Start 함수와 비슷. Awake 더 빨리 호출됨.
        // Awake - OnEnable - Start.
        private void Awake()
        {
            // 스태틱 속성을 이 컴포넌트로 설정.
            instance = this;
        }

        private void OnEnable()
        {
            // 시작하면 체력 값을 데이터에 있는 값으로 설정.
            currentHP = data.maxHP;

            // 체력바(UI)가 설정돼 있으면 UI 값 설정.
            if (hpBar != null)
            {
                // 체력바 색상을 초기 색상으로 설정.
                hpBar.color = initColor;
                // 현재 색상도 초기 색상 값으로 설정.
                currentColor = initColor;
            }
        }

        // 적 캐릭터 총알과 충돌했을 때 처리할 함수.
        private void OnTriggerEnter(Collider other)
        {
            // 충돌한 물체의 태그를 확인해 적의 총알인지 확인.
            if (other.CompareTag(bulletTag) == true)
            {
                // 적 총알 삭제.
                Destroy(other.gameObject);

                // 적 총알의 대미지만큼 체력 감소시키기.
                currentHP -= other.GetComponent<BulletDamage>().damage;

                // 체력바 UI 상태 업데이트.
                UpdateHPBar();

                // 피격 효과가 설정돼 있으면 효과 재생.
                if (bloodEffect != null)
                {
                    StartCoroutine(ShowBloodEffect());
                }

                // 체력이 모두 소진되면 플레이어가 죽었다는 사실을 알림
                // 이벤트 발생.
                if (currentHP <= 0f)
                {
                    OnPlayerDeadEvent?.Invoke();
                }
            }
        }

        // 피격효과 재생하는 메소드.
        private IEnumerator ShowBloodEffect()
        {
            // 색상 값 설정. 랜덤으로 알파 값 설정.
            bloodEffect.color = new Color(1f, 0f, 0f, Random.Range(0.2f, 0.3f));

            // 0.2초 대기.
            yield return new WaitForSeconds(0.2f);

            // 색상 값을 초기화해 화면에서 안보이도록 처리.
            bloodEffect.color = Color.clear;
        }

        // 체력바 업데이트 함수.
        private void UpdateHPBar()
        {
            // UI가 설정되지 않았으면 실행하지 않도록 처리.
            if (hpBar == null)
            {
                return;
            }

            // 체력 값의 범위로 색상값 지정.
            // 남은 체력이 50% 이상인 경우.
            if ((currentHP / data.maxHP) > 0.5f)
            {
                currentColor.r = (1f - (currentHP / data.maxHP)) * 2f;
            }
            // 남은 체력이 50% 미만인 경우.
            else
            {
                currentColor.g = (currentHP / data.maxHP) * 2f;
            }

            // 위에서 계산한 색상 값 설정.
            hpBar.color = currentColor;
            // 체력 게이지 값 설정.
            hpBar.fillAmount = (currentHP / data.maxHP);
        }

        // 플레이어가 죽을 때 발생하는 이벤트에 등록하는 함수.
        public static void SubscribePlayerDeadEvent(UnityAction action)
        {
            // 필요한 값이 모두 설정돼 있는지 확인.
            if (instance == null)
            {
                Debug.Log("instance is null");
                return;
            }

            else if (instance.OnPlayerDeadEvent == null)
            {
                Debug.Log("instance.OnPlayerDeadEvent is null");
                return;
            }

            // 전달된 함수를 이벤트에 등록.
            instance.OnPlayerDeadEvent.AddListener(action);
        }
    }
}