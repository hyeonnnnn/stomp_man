using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 1;
    private int _currentHealth;

    [SerializeField] private int _score;

    private ItemDrop _itemDrop;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _itemDrop = GetComponent<ItemDrop>();
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
        ScoreManager.Instance.AddScore(_score);

        _itemDrop.TryDropItem();

        Destroy(gameObject);
    }

}
