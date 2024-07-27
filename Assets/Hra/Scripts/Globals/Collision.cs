using System;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public event Action<Collision2D> OnCollisionEnter;
    public event Action<Collision2D> OnCollisionExit;
    public event Action<Collider2D> OnTriggerEnter;
    public event Action<Collider2D> OnTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerEnter?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnTriggerExit?.Invoke(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnCollisionEnter?.Invoke(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnCollisionExit?.Invoke(collision);
    }
}
