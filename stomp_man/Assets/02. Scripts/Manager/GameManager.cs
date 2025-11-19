using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private float _gameStartSpeed = 20f;
    private float _currentGameSpeed;
    private float _minGameSpeed = 1f;
    private float _maxGameSpeed = 40f;
    private float _gameSpeedIncreaseValue = 0.1f;
    public float GameSpeedIncrease = 5f;
    public float GameSpeedDecrease = 7f;
    private float _timer = 0f;
    [SerializeField] private float _coolTime = 1f;

    public bool IsGameOver = false;
    public float MaxGameSpeed => _maxGameSpeed;
    private bool _isOpenGameOverPanel = false;

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
        Time.timeScale = 0;
    }


    public void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _coolTime)
        {
            _currentGameSpeed += _gameSpeedIncreaseValue;
            _timer = 0;
        }

        if (_currentGameSpeed <= _minGameSpeed)
        {
            IsGameOver = true;
        }

        if (IsGameOver == true && _isOpenGameOverPanel == false)
        {
            _isOpenGameOverPanel = true;
            SoundManager.Instance.PlaySFX(SoundManager.Sfx.GAMEOVER);
            PanelManager.Instance.ShowGameOverPanel();
        }
    }

    public void IncreaseGameSpeed()
    {
        _currentGameSpeed *= GameSpeedIncrease;
        _currentGameSpeed = Mathf.Min(MaxGameSpeed, _currentGameSpeed);
    }

    public void DecreaseGameSpeed()
    {
        _currentGameSpeed *= GameSpeedDecrease;
    }
}
