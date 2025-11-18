using UnityEngine;
using System;

public class PlayerBounce : MonoBehaviour
{
    public static event Action OnBounceIncrease;
    public static event Action OnBounceDecrease;
    public static event Action OnBounceMinReached;

    [Header("바운스")]
    [SerializeField] private float _startbounceForce = 5f;
    [SerializeField] private float _maxBounceForce = 8f;
    [SerializeField] private float _minBounceForce = 0.2f;
    [SerializeField] private float _increaseValue = 1.2f;
    [SerializeField] private float _decreaseValue = 0.7f;

    private float _currentbounceForce = 3f;

    public float CurrentBounceForce => _currentbounceForce;

    private void Awake()
    {
        _currentbounceForce = _startbounceForce;
    }

    public void StartBounce(Rigidbody2D rigidbody)
    {
        rigidbody.AddForce(Vector3.up * _currentbounceForce, ForceMode2D.Impulse);
    }

    public void Bounce(Rigidbody2D rb)
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * _currentbounceForce, ForceMode2D.Impulse);
    }

    public void IncreaseBounceForce()
    {
        _currentbounceForce *= _increaseValue;
        _currentbounceForce = Mathf.Min(_maxBounceForce, _currentbounceForce);

        OnBounceIncrease?.Invoke(); // 플레이어의 속도가 감소하면 알리기
    }

    public bool DecreaseBounceForce()
    {
        _currentbounceForce *= _decreaseValue;

        if (_currentbounceForce <= _minBounceForce)
        {
            _currentbounceForce = _minBounceForce;
            return false;
        }
        OnBounceDecrease?.Invoke(); // 플레이어의 속도가 감소하면 알리기
        return true;
    }
}
