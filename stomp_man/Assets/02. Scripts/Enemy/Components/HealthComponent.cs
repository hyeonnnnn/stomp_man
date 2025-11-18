using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 1;
    private int _currentHealth;

    private ItemDrop _itemDrop;
    private Enemy _enemy;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _itemDrop = GetComponent<ItemDrop>();
        _enemy = GetComponent<Enemy>();
    }

    public void Damage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _itemDrop.TryDropItem();

        switch (_enemy.enemyType)
        {
            case EnemyType.Ground:
                EffectManager.Instance.PlayExplosionEffect(transform.position);
                break;

            case EnemyType.Sky:
                EffectManager.Instance.PlayLightEffect(transform.position);
                break;
        }
        SoundManager.Instance.PlaySFX(SoundManager.Sfx.ENEMYEXPLOSION);
        Destroy(gameObject);
    }

}
