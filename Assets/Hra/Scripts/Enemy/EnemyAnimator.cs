using UnityEngine;

public enum EnemyAnimationTrigger
{
    EnemyIdle,
    EnemyRoam,
    EnemyChase,
    EnemyAttack,
    EnemyHit,
    EnemyDeath,
    PlaySounds
}

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void PlayAnimation(EnemyAnimationTrigger animationTrigger)
    {
        _animator.StopPlayback();
        _animator.Play(animationTrigger.ToString());
    }
}
