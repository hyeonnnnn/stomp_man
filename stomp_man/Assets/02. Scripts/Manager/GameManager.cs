using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GameSpeedController _speed;
    private GameStateController _state;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _speed = GetComponent<GameSpeedController>();
        _state = GetComponent<GameStateController>();
    }

    private void Start()
    {
        PauseGame();
    }

    public void StartGame()
    {
        ResumeGame();
        _speed.ResetSpeed();
        _state.ResetState();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
