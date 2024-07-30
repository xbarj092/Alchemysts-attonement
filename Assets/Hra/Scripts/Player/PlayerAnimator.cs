using UnityEngine;

public enum PlayerAnimationTrigger
{
    PlayerIdle,
    PlayerMoveRight,
    PlayerMoveLeft,
    PlayerMoveUp,
    PlayerMoveDown,
    PlayerDash,
    PlayerAttack,
    PlayerHit,
    PlayerDeath,
    PlaySounds
}

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void PlayAnimation(PlayerAnimationTrigger animationTrigger)
    {
        _animator.StopPlayback();
        _animator.Play(animationTrigger.ToString());
    }
}
