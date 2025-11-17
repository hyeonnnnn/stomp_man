using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private float _gameStartSpeed = 15f;
    private float _currentGameSpeed;
    private float _gameSpeedIncreaseValue = 0.1f;
    private float _gameSpeedIncreaseMultipler = 1.15f;
    private float _gameSpeedDecreaseMultipler = 0.6f;
    private float _timer = 0f;
    [SerializeField] private float _coolTime = 1f;


    public float CurrentGameSpeed => _currentGameSpeed;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;

        _currentGameSpeed = _gameStartSpeed;
    }

    public void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _coolTime)
        {
            _currentGameSpeed += _gameSpeedIncreaseValue;
            _timer = 0;
        }
    }

    public void IncreaseGameSpeed()
    {
        _currentGameSpeed *= _gameSpeedIncreaseMultipler;
    }

    public void DecreaseGameSpeed()
    {
        _currentGameSpeed *= _gameSpeedDecreaseMultipler;
    }
}
