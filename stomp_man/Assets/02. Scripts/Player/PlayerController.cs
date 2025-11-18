
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private PlayerBounce _bounce;
    private PlayerCombat _combat;
    private PlayerDamage _damage;
    private PlayerAnimation _animation;
    private PlayerVFX _vfx;

    private bool _isDead = false;
    private PlayerEvents _events;

    private GameStateController _state;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _bounce = GetComponent<PlayerBounce>();
        _combat = GetComponent<PlayerCombat>();
        _damage = GetComponent<PlayerDamage>();
        _animation = GetComponent<PlayerAnimation>();
        _vfx = GetComponent<PlayerVFX>();
        _events = GetComponent<PlayerEvents>();
        _state = Object.FindFirstObjectByType<GameStateController>();
    }

    private void Start()
    {
        _bounce.StartBounce(_rigidbody);
    }

    private void Update()
    {
        if (_isDead == true) return;

        if (_combat.TryStomp())
        {
            _animation.TriggerStomp();
            _rigidbody.linearVelocity = new Vector2(_rigidbody.linearVelocity.x, -_combat.StompForce);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDead == true) return;
        if (collision.CompareTag("Enemy") == false) return;

        if (_combat.IsStomping)
        {
            _damage.ApplyHitDamage(collision);
            _bounce.IncreaseBounceForce();
            _vfx.PlayBigHitEffect(collision.transform.position);

            _events.InvokeStompedEnemy(); // 적을 잘 밟았다는 알림
        }
        else
        {
            if (_bounce.DecreaseBounceForce() == false)
            {
                Die();
                return;
            }
            _vfx.PlayDamagedEffect(transform.position);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isDead == true) return;
        if (collision.collider.CompareTag("Ground") == false) return;

        if (_combat.IsStomping == false)
        {
            if (_bounce.DecreaseBounceForce() == false)
            {
                Die();
                return;
            }

            _events.InvokeHitGroundWithoutStomp(); // 대시를 못한채로 땅을 밟았다는 알림
            _vfx.PlayDamagedEffect(transform.position);
        }
        _bounce.Bounce(_rigidbody);
        _combat.ResetCombatState();
        _animation.TriggerJump();
    }


    private void Die()
    {
        if (_isDead == true) return;
        _isDead = true;

        _state.GameOver();
        _animation.TriggerIdle();
        Destroy(gameObject);
    }

}