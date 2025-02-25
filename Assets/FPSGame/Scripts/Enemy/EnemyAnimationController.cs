using UnityEngine;

namespace FPSGame
{
    public class EnemyAnimationController : MonoBehaviour
    {
        // Animator 컴포넌트.
        public Animator animator;

        // 적 캐릭터가 상태를 변경할 때 사용.
        public void SetState(EnemyStateManager.State newState)
        {
            if (newState == EnemyStateManager.State.Dead)
            {
                animator.SetTrigger("Dead");
                return;
            }

            animator.SetInteger("State", (int)newState);
        }

        // 적이 발사할 때 Fire 트리거 설정.
        public void OnFire()
        {
            animator.SetTrigger("Fire");
        }

        // 적이 재장전할 때 Reload 트리거 설정.
        public void OnReload()
        {
            animator.SetTrigger("Reload");
        }

    }
}