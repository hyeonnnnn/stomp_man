using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("½ºÅèÇÁ")]
    [SerializeField] private float _stompForce = 20f;
    private bool _isStomping = false;
    private int _damage = 1;

    public float StompForce => _stompForce;
    public bool IsStomping => _isStomping;

    public bool TryStomp()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isStomping == false)
        {
            _isStomping = true;
            return true;
        }
        return false;
    }

    public void ResetCombatState()
    {
        _isStomping = false;
    }

    public void ApplyHitDamage(Collider2D collision)
    {
        HealthComponent health = collision.GetComponent<HealthComponent>();
        if (health == null) return;

        health.Damage(_damage);
    }
}
