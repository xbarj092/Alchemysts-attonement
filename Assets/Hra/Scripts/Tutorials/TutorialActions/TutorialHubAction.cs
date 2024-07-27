using Cinemachine;
using System.Collections;
using UnityEngine;

public class TutorialMovementAction : TutorialAction
{
    [Header("Colliders")]
    [SerializeField] private TutorialCollision _jumpCollider;
    [SerializeField] private TutorialCollision _jumpMadeCollider;
    [SerializeField] private TutorialCollision _doubleJumpCollider;
    [SerializeField] private TutorialCollision _doubleJumpMadeCollider;
    [SerializeField] private TutorialCollision _climbCollider;
    [SerializeField] private TutorialCollision _dashCollider;
    [SerializeField] private TutorialCollision _dashMadeCollider;
    [SerializeField] private TutorialCollision _nextTutorialCollider;

    [Header("TextPositions")]
    [SerializeField] private Transform _moveTransform;
    [SerializeField] private Transform _jumpTransform;
    [SerializeField] private Transform _doubleJumpTransform;
    [SerializeField] private Transform _climbTransform;
    [SerializeField] private Transform _dashTransform;
    [SerializeField] private Transform _afterDashTransform;

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void StartAction()
    {
        throw new System.NotImplementedException();
    }
}
