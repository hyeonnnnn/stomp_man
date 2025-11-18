using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private int _damage;

    public void ApplyHitDamage(Collider2D collider)
    {
        HealthComponent health = collider.GetComponent<HealthComponent>();
        if (health == null) return;

        health.Damage(_damage);
    }
}
