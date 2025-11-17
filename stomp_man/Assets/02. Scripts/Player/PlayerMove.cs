using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _startbounceForce = 3f;
    private float _bounceForce = 3f;

    [SerializeField] private float _stompForce = 10f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _bounceForce = _startbounceForce;
    }

    private void Update()
    {
        Stomp();
    }

    private void Stomp()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, -_stompForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") == true)
        {
            Bounce(collision, _bounceForce);
        }
    }

    private void Bounce(Collision2D collision, float _bounceForce)
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
