using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;
    private ItemDrop _itemDrop;

    private void Awake()
    {
        _itemDrop = GetComponent<ItemDrop>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }

    public void Die()
    {
        _itemDrop.TryDropItem();
        Destroy(gameObject);
    }
}
