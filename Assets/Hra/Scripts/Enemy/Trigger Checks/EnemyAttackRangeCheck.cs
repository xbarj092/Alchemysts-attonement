using UnityEngine;

public class EnemyAttackRangeCheck : MonoBehaviour
{
    private Enemy _enemy;

    void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GlobalConstants.Tags.Player.ToString()))
        {
            _enemy?.SetWithinAttackRange(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GlobalConstants.Tags.Player.ToString()))
        {
            _enemy?.SetWithinAttackRange(false);
        }
    }
}
