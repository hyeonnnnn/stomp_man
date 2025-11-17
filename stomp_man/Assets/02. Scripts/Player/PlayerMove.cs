using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("바운스")]
    [SerializeField] private float _startbounceForce = 3f;
    private float _bounceForce = 3f;
    private float _minBounceForce = 0.2f;
    private float _bounceForceIncrease = 0.2f;
    private float _bounceForceDecrease = 0.7f;

    [Header("스톰프")]
    [SerializeField] private float _stompForce = 10f;
    private bool _isStomp = false;

    [SerializeField] private int _damage = 1;
    private bool _isHit = false;
    private bool _isDie = false;

    [Header("피격 이펙트")]
    [SerializeField] private float _hitFlashDuration = 0.1f;
    [SerializeField] private Color _hitFlashColor = Color.red;
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;

    private Rigidbody2D _rigidbody;

    public bool IsDie => _isDie;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _bounceForce = _startbounceForce;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _originalColor = _spriteRenderer.color;
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
        if (_isDie == true) return;

        if (collision.collider.CompareTag("Ground") == true)
        {
            if(_isHit == true)
            {
                BounceForceIncrease(_bounceForceIncrease);
            }
            else
            {
                BounceForceDecrease(_bounceForceDecrease);
            }

            Bounce();

            _isHit = false;
            _isStomp = false;
        }
    }

    private void Bounce()
    {
        _rigidbody.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);
    }

    public void BounceForceIncrease(float value)
    {
        Debug.Log("점프력 증가");
        _bounceForce += _bounceForceIncrease;
    }

    public void BounceForceDecrease(float value)
    {
        Debug.Log("점프력 감소");
        _bounceForce -= value;
        _bounceForce = Mathf.Max(_minBounceForce, _bounceForce);

        StartCoroutine(FlashHitColor());

        if (_bounceForce <= _minBounceForce)
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDie == true) return;

        if (collision.CompareTag("Enemy") == true)
        {
            if (_isStomp == true)
            {
                if (_isHit == true) return;

                _isHit = true;
                Hit(collision);
                BounceForceIncrease(_bounceForceIncrease);
            }
            else
            {
                BounceForceDecrease(_bounceForceDecrease);
            }
        }
    }

    private void Hit(Collider2D collision)
    {
        collision.GetComponent<HealthComponent>().Damage(_damage);
    }

    private IEnumerator FlashHitColor()
    {
        Debug.Log("FlashHitColor");
        _spriteRenderer.color = _hitFlashColor;
        yield return new WaitForSeconds(_hitFlashDuration);
        _spriteRenderer.color = _originalColor;
    }

    private void Die()
    {
        if (_isDie) return;
        _isDie = true;

        Debug.Log("죽었습니다.");
    }


}
