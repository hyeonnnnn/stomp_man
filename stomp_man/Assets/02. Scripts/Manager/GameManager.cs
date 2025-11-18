using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private float _gameStartSpeed = 20f;
    private float _currentGameSpeed;
    private float _minGameSpeed = 0.2f;
    private float _maxGameSpeed = 40f;
    private float _gameSpeedIncreaseValue = 0.1f;
    private float _gameSpeedIncreaseMultipler = 1.15f;
    private float _gameSpeedDecreaseMultipler = 0.6f;
    private float _timer = 0f;
    [SerializeField] private float _coolTime = 1f;

    private bool _isGameOver = false;
    public bool IsGameOver => _isGameOver;
    public float MaxGameSpeed => _maxGameSpeed;


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

    private void Start()
    {
        PanelManager.Instance.ShowHomePanel();
    }

    public void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _coolTime)
        {
            _currentGameSpeed += _gameSpeedIncreaseValue;
            _timer = 0;
        }

        if (_currentGameSpeed < _minGameSpeed)
        {
            _isGameOver = true;
        }

        if (_isGameOver == true)
        {
            PanelManager.Instance.ShowGameOverPanel();
        }
    }

    public void IncreaseGameSpeed()
    {
        _currentGameSpeed *= _gameSpeedIncreaseMultipler;
        _currentGameSpeed = Mathf.Min(MaxGameSpeed, _currentGameSpeed);
    }

    public void DecreaseGameSpeed()
    {
        _currentGameSpeed *= _gameSpeedDecreaseMultipler;
    }
}
