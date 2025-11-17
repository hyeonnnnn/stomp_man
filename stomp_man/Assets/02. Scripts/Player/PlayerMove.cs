using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("바운스")]
    [SerializeField] private float _startbounceForce = 3f;
    [SerializeField] private float _minBounceForce = 0.2f;
    [SerializeField] private float _increaseValue = 1.2f;
    [SerializeField] private float _decreaseValue = 0.7f;
    private float _currentbounceForce = 3f;

    [Header("스톰프")]
    [SerializeField] private float _stompForce = 10f;
    private bool _isStomp = false;
    private bool _isHit = false;
    private bool _isDie = false;

    [Header("데미지")]
    [SerializeField] private int _damage = 1;

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
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _currentbounceForce = _startbounceForce;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDie == true) return;
        if (collision.CompareTag("Enemy") == false) return;

        if (_isStomp == true)
        {
            // Enemy 1회 타격만 판정
            if (_isHit == true) return;

            _isHit = true;
            Hit(collision);
            IncreaseBounceForce(_increaseValue);
        }
        else
        {
            DecreaseBounceForce(_decreaseValue);
        }
    }

    private void Hit(Collider2D collision)
    {
        HealthComponent health = collision.GetComponent<HealthComponent>();
        if (health == null) return;

        health.Damage(_damage);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDie == true) return;
        if (collision.collider.CompareTag("Ground") == false) return;

        if (_isHit == false)
        {
            DecreaseBounceForce(_decreaseValue);
        }

        Bounce();

        _isHit = false;
        _isStomp = false;
    }

    private void Bounce()
    {
        _rigidbody.AddForce(Vector2.up * _currentbounceForce, ForceMode2D.Impulse);
    }

    public void IncreaseBounceForce(float value)
    {
        Debug.Log("점프력 증가");
        _currentbounceForce *= value;
    }

    public void DecreaseBounceForce(float value)
    {
        Debug.Log("점프력 감소");
        _currentbounceForce *= value;
        _currentbounceForce = Mathf.Max(_minBounceForce, _currentbounceForce);

        StartCoroutine(FlashHitColor());

        if (_currentbounceForce <= _minBounceForce)
        {
            Die();
        }
    }

    private IEnumerator FlashHitColor()
    {
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
