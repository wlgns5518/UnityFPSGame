using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPSGame
{
    public class PlayerWeaponRifle : PlayerWeapon
    {
        // 탄약 발사를 위한 변수.
        public GameObject bulletPrefab;
        public Transform muzzleTransform;       // 총구 위치.

        // 발사할 때 소리 재생을 위한 변수.
        public AudioSource audioPlayer;         // 사운드 재생기.
        public AudioClip fireSound;             // 사운드 파일.

        // 탄피 제거 효과 파티클.
        public ParticleSystem cartridgeEjectEffect;

        // 화염 효과 파티클.
        public ParticleSystem muzzleFlashEffect;

        // 카메라 흔들기 효과 재생을 위한 변수.
        public CameraShake cameraShake;

        // 플레이어 데이터.
        public PlayerData data;

        // 현재 남은 탄약 수.
        public int currentAmmo = 0;

        // 탄약 발사 간격(시간) 조정을 위한 변수.
        public float fireRate = 0.5f;       // 발사 간격(초 단위).
        private float nextFireTime = 0f;    // 다음 발사 시간 계산을 위한 변수.

        // 재장선 시 재생할 사운드 변수.
        public AudioClip reloadWeaponClip;

        // 애니메이션 컨트롤러.
        public PlayerAnimationController animationController;

        // 재장전을 알리기 위한 이벤트.
        public UnityEvent OnReloadEvent;

        // 탄약 수가 변경되면 알리기 위한 이벤트.
        // 앞의 int는 maxAmmo, 뒤의 int는 currentAmmo.
        public UnityEvent<int, int> OnAmmoCountChanged;

        private void Start()
        {
            // 시작할 때 플레이어 데이터에 지정한 탄약 수로 설정.
            currentAmmo = data.maxAmmo;

            // 탄약 수 변경 이벤트 발행.
            OnAmmoCountChanged?.Invoke(data.maxAmmo, currentAmmo);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                currentAmmo = 0;
                // 재장전 이벤트 발생 -> 알림 메시지 보내기.
                //if (OnReloadEvent != null)
                //{
                //    OnReloadEvent.Invoke();
                //}
                OnReloadEvent?.Invoke();

                // 재장전 사운드 재생.
                audioPlayer.PlayOneShot(reloadWeaponClip);

                // 재장전 처리 함수 실행.
                // 애니메이션 컨트롤러의 WaitTimeToReload 시간 만큼 대기.
                // Reload 함수 실행.
                Invoke("Reload", animationController.WaitTimeToReload());
            }
        }

        // 탄약 발사.
        public override void Fire()
        {
            base.Fire();

            // 발사가 가능한지 확인.
            // 가능하지 않다면, 함수 종료.
            if (CanFire() == false)
            {
                return;
            }

            // 다음에 발사가 가능해지는 시간 계산.
            nextFireTime = Time.time + fireRate;

            // 탄약 수 1개 제거.
            --currentAmmo;
            //currentAmmo = currentAmmo - 1;

            // 탄약 수 변경 이벤트 발행.
            OnAmmoCountChanged?.Invoke(data.maxAmmo, currentAmmo);

            // 탄약 생성.
            // 프리팹 게임 오브젝트 생성.
            // Instantiate.
            Instantiate(
                bulletPrefab,
                muzzleTransform.position,
                muzzleTransform.rotation
            );

            // 발사 사운드 재생.
            audioPlayer.PlayOneShot(fireSound);

            // 탄피 제거 효과 재생.
            cartridgeEjectEffect.Play();

            // 총구 화염 효과 재생.
            muzzleFlashEffect.Play();

            // 카메라 흔들기 효과 재생.
            cameraShake.Play();

           // 재장전이 필요한지 확인.
            if (currentAmmo == 0)
            {
                // 재장전 이벤트 발생 -> 알림 메시지 보내기.
                //if (OnReloadEvent != null)
                //{
                //    OnReloadEvent.Invoke();
                //}
                OnReloadEvent?.Invoke();

                // 재장전 사운드 재생.
                audioPlayer.PlayOneShot(reloadWeaponClip);

                // 재장전 처리 함수 실행.
                // 애니메이션 컨트롤러의 WaitTimeToReload 시간 만큼 대기.
                // Reload 함수 실행.
                Invoke("Reload", animationController.WaitTimeToReload());
            }

        }

            // 재장전 처리 함수.
            public void Reload()
        {
            // 재장전이 완료되면, 현재 가진 탄약을 가득 채움.
            currentAmmo = data.maxAmmo;

            // 탄약 수 변경 이벤트 발행.
            OnAmmoCountChanged?.Invoke(data.maxAmmo, currentAmmo);
        }

        // 발사가 가능한지 알려주는 함수.
        // 1. 현재 가진 탄약 수가 0보다 큰지 확인.
        // 2. 발사 가능한 시간이 충분히 지났는지 확인.
        bool CanFire()
        {
            return currentAmmo > 0 && Time.time >= nextFireTime;
        }

    }
}