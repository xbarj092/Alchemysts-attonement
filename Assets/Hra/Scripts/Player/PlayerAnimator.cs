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

    public void PlayAnimation(PlayerAnimationTrigger animationTrigger, int layer = 0, bool stopPlayback = true)
    {
        if (stopPlayback)
        {
            _animator.StopPlayback();
        }

        _animator.Play(animationTrigger.ToString(), layer);
    }

    public void SetLayerWeight(int layerIndex, float weight)
    {
        _animator.SetLayerWeight(layerIndex, weight);
    }
}
