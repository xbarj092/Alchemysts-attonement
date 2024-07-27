using UnityEngine;

public enum AnimationTrigger
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

    public void PlayAnimation(AnimationTrigger animationTrigger)
    {
        _animator.StopPlayback();
        _animator.Play(animationTrigger.ToString());
    }
}
