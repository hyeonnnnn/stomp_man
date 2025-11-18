using UnityEngine;

public class GameSpeedController : MonoBehaviour
{
    [Header("스피드")]
    [SerializeField] private float _startSpeed = 20f;
    [SerializeField] private float _minSpeed = 0.2f;
    [SerializeField] private float _maxSpeed = 40f;

    [SerializeField] private float _increaseValue = 0.1f;
    [SerializeField] private float _increaseMultiplier = 1.15f;
    [SerializeField] private float _decreaseMultiplier = 0.6f;
    [SerializeField] private float _coolTime = 1f;

    private float _timer;

    public float CurrentSpeed { get; private set; }
    public float MaxSpeed => _maxSpeed;

    private void Awake()
    {
        ResetSpeed();
    }

    public void ResetSpeed()
    {
        CurrentSpeed = _startSpeed;
        _timer = 0f;
    }

    private void OnEnable()
    {
        PlayerBounce.OnBounceIncrease += HandleBounceIncrease;
        PlayerBounce.OnBounceDecrease += HandleBounceDecrease;
        PlayerBounce.OnBounceMinReached += HandleBounceMinReached;
    }

    private void OnDisable()
    {
        PlayerBounce.OnBounceIncrease -= HandleBounceIncrease;
        PlayerBounce.OnBounceDecrease -= HandleBounceDecrease;
        PlayerBounce.OnBounceMinReached -= HandleBounceMinReached;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        // 시간 지나면 속도 상승
        if (_timer >= _coolTime)
        {
            CurrentSpeed += _increaseValue;
            CurrentSpeed = Mathf.Min(CurrentSpeed, _maxSpeed);
            _timer = 0f;
        }

        // 너무 느려지면 게임 오버
        if (CurrentSpeed <= _minSpeed)
        {
            FindFirstObjectByType<GameStateController>().OnSpeedDepleted();
        }
    }

    private void HandleBounceIncrease()
    {
        CurrentSpeed *= _increaseMultiplier;
        CurrentSpeed = Mathf.Min(CurrentSpeed, _maxSpeed);
    }

    private void HandleBounceDecrease()
    {
        CurrentSpeed *= _decreaseMultiplier;
    }

    private void HandleBounceMinReached()
    {
        FindFirstObjectByType<GameStateController>().GameOver();
    }
}
