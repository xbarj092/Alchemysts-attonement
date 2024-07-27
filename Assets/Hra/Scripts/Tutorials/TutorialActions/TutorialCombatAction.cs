using UnityEngine;

public class TutorialCombatAction : TutorialAction
{
    [Header("Colliders")]
    [SerializeField] private TutorialCollision _nearMoveableCollider;

    [Header("TextPositions")]
    [SerializeField] private Transform _nearMoveableTransform;

    public override void StartAction()
    {
        _nearMoveableCollider.OnTriggerEntered += OnNearMoveableTutorial;
    }

    private void OnNearMoveableTutorial()
    {
        _nearMoveableCollider.OnTriggerEntered -= OnNearMoveableTutorial;
        _tutorialPlayer.SetTextPosition(_nearMoveableTransform.localPosition);
        _tutorialPlayer.MoveToNextNarratorText();
    }

    public override void Exit()
    {
    }
}
