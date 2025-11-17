using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _startbounceForce = 3f;
    private float _bounceForce = 3f;
    private float _bounceForceIncrease = 0.2f;
    private float _bounceForceDecrease = 0.7f;

    [SerializeField] private float _stompForce = 10f;
    private bool _isStomp = false;
    private bool _isAttack = false;

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
        if (Input.GetKeyDown(KeyCode.Space) == true && _isStomp == false)
        {
            _isStomp = true;
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, -_stompForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") == true)
        {
            if(_isAttack == true)
            {
                BounceForceIncrease();
            }
            else
            {
                BounceForceDecrease();
            }
            _isAttack = false;
            _isStomp = false;

            Bounce();
        }
    }

    private void Bounce()
    {
        _rigidbody.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);
    }

    private void BounceForceIncrease()
    {
        Debug.Log("점프력 증가");
        _bounceForce += _bounceForceIncrease;
    }

    private void BounceForceDecrease()
    {
        Debug.Log("점프력 감소");
        _bounceForce -= _bounceForceDecrease;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") == true)
        {
            if (_isStomp == true)
            {
                Attack(collision);
                BounceForceIncrease();
            }
            else
            {
                BounceForceDecrease();
            }
        }
    }

    private void Attack(Collider2D collision)
    {
        _isAttack = true;
        collision.GetComponent<Enemy>().Die();
    }
}
