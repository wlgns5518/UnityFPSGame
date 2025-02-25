using UnityEngine;

namespace FPSGame
{
    // 적 캐릭터의 발사 동작을 처리하는 스크립트.
    public class EnemyFireController : MonoBehaviour
    {
        // 발사/재장전할 때 사운드를 재생할 AudioSource 컴포넌트.
        public AudioSource audioSource;

        // 발사할 때 재생할 사운드 애셋.
        public AudioClip fireClip;

        // 재장전할 때 재생할 사운드 애셋.
        public AudioClip reloadClip;

        // 탄약 프리팹.
        public GameObject bulletPrefab;

        // 발사 위치 트랜스폼.
        public Transform firePosition;

        // 총구 화염 효과.
        public ParticleSystem muzzleFlashEffect;

        // 플레이어 트랜스폼.
        private Transform playerTransform;

        private void OnEnable()
        {
            // audioSource가 설정되지 않았으면,
            // 같은 게임오브젝트 안에서 검색해서 설정하기.
            if (audioSource == null)
            {
                audioSource = GetComponent<AudioSource>();
            }

            if (playerTransform == null)
            {
                playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        // 발사를 담당하는 함수.
        public void Fire()
        {
            // 총구 위치에서 플레이어 위치로 향하는 방향 구하기.
            Quaternion direction = Quaternion.LookRotation(
                playerTransform.position - firePosition.position + new Vector3(0, 1.5f, 0)
            ) ;

            // 탄약 생성.
            GameObject bullet = Instantiate(
                bulletPrefab, firePosition.position, direction
            );

            // 3초뒤 탄약 삭제하도록 설정.
            Destroy(bullet, 3f);

            // 발사 사운드 재생.
            audioSource.PlayOneShot(fireClip, 1f);

            // 총구 화염 효과 재생.
            muzzleFlashEffect.Play();
        }

        // 재장전을 담당하는 함수.
        public void Reload()
        {
            // 재장전 사운드 재생.
            audioSource.PlayOneShot(reloadClip, 1f);
        }
    }
}