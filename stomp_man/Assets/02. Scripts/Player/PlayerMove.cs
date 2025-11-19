using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("바운스")]
    [SerializeField] private float _startbounceForce = 5f;
    [SerializeField] private float _maxBounceForce = 8f;
    [SerializeField] private float _minBounceForce = 0.2f;
    [SerializeField] private float _increaseValue = 5f;
    [SerializeField] private float _decreaseValue = 7f;
    private float _currentbounceForce = 3f;

    [Header("카메라 흔들기 값")]
    [SerializeField] private float _smallShakeValue = 0.2f;
    [SerializeField] private float _bigShakeValue = 0.6f;
    private float _shakeValue;

    [Header("스톰프")]
    [SerializeField] private float _stompForce = 20f;
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

    [SerializeField] private Animator _animator;

    private Camera _camera;

    private Rigidbody2D _rigidbody;

    public bool IsDie => _isDie;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _currentbounceForce = _startbounceForce;
        _originalColor = _spriteRenderer.color;

        _camera = Camera.main;
        _shakeValue = _smallShakeValue;
    }

    private void Start()
    {
        _rigidbody.AddForce(Vector3.up * _currentbounceForce, ForceMode2D.Impulse);

        _increaseValue = GameManager.Instance.GameSpeedIncrease;
        _decreaseValue = GameManager.Instance.GameSpeedDecrease;
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
            _animator.SetTrigger("Stomp");
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, -_stompForce);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDie == true) return;
        if (collision.CompareTag("Enemy") == false) return;

        if (_isStomp == true)
        {
            if (_isHit == true) return;

            _isHit = true;
            Hit(collision);
            IncreaseBounceForce(_increaseValue);
            GameManager.Instance.IncreaseGameSpeed();
        }
        else
        {
            DecreaseBounceForce(_decreaseValue);
            GameManager.Instance.DecreaseGameSpeed();
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
        _animator.ResetTrigger("Jump");

        if (_isHit == false)
        {
            DecreaseBounceForce(_decreaseValue);

            GameManager.Instance.DecreaseGameSpeed();
        }

        Bounce();
        

        _isHit = false;
        _isStomp = false;
        
        _animator.ResetTrigger("Stomp");
    }

    private void Bounce()
    {
        if (_isStomp == true)
        {
            _shakeValue = _bigShakeValue;
        }
        else
        {
            _shakeValue = _smallShakeValue;
        }
        _camera.GetComponent<CameraShake>().Play(_shakeValue);

        _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, 0f);
        _rigidbody.AddForce(Vector2.up * _currentbounceForce, ForceMode2D.Impulse);
        SoundManager.Instance.PlaySFX(SoundManager.Sfx.STOMP);
        _animator.SetTrigger("Jump");
    }

    public void IncreaseBounceForce(float value)
    {
        _currentbounceForce *= value;
        _currentbounceForce = Mathf.Min(_maxBounceForce, _currentbounceForce);
    }

    public void DecreaseBounceForce(float value)
    {
        _currentbounceForce *= value;

        Debug.Log(_currentbounceForce);
        if (_currentbounceForce <= _minBounceForce)
        {
            _currentbounceForce = _minBounceForce;
            Die();
            Debug.Log("죽음");
        }


        StartCoroutine(FlashHitColor());
        Vector3 bloodPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, 0f);
        EffectManager.Instance.PlayBloodEffect(bloodPosition);
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

        _animator.SetTrigger("Idle");
        _animator.ResetTrigger("Stomp");
        _animator.ResetTrigger("Jump");

        GameManager.Instance.IsGameOver = true;
        Destroy(gameObject);
    }

}