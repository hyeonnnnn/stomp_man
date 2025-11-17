using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 1f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") == true)
        {
            Bounce(collision);
        }
    }

    private void Bounce(Collision2D collision)
    {
        _rigidbody.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") == true)
        {
            Attack(collision);
        }
    }

    private void Attack(Collider2D collision)
    {
        collision.GetComponent<Enemy>().Die();
    }
}
