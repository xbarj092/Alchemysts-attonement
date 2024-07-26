using UnityEngine;

public class StraightSword : MonoBehaviour
{
    private PlayerController playerController;
    private Animator animator;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
    }
}