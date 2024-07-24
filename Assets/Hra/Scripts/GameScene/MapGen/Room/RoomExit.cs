using System;
using UnityEngine;

public class RoomExit : MonoBehaviour
{
    [SerializeField] private Direction _direction;

    public event Action<Direction> OnPlayerTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GlobalConstants.Tags.Player.ToString()))
        {
            OnPlayerTrigger?.Invoke(_direction);
        }
    }
}
