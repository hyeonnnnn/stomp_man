using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance = null;
    public static ScoreManager Instance => _instance;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _currentScoreTextUI;
    [SerializeField] private TextMeshProUGUI _BestScoreTextUI;

    private int _currentScore;
    private int _bestScore;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScore += score;

        UpdateCurrentScoreUI();
        UpdateBestScore();
    }

    private void Start()
    {
        InitScore();
    }

    private void InitScore()
    {

        LoadBestScore();
        UpdateCurrentScoreUI();
        UpdateBestScoreUI();
    }

    private void LoadBestScore()
    {
        SaveData loaded = SaveManager.Instance.Load();

        if (loaded != null)
        {
            _bestScore = loaded.score;
        }
        else
        {
            _bestScore = 0;
        }
    }

    private void UpdateCurrentScoreUI()
    {
        _currentScoreTextUI.text = $"Score: {_currentScore:N0}";
    }

    private void UpdateBestScore()
    {
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
            SaveBestScore();
            UpdateBestScoreUI();
        }
    }

    private void UpdateBestScoreUI()
    {
        _BestScoreTextUI.text = $"Best Score: {_bestScore:N0}";
    }

    private void SaveBestScore()
    {
        SaveManager.Instance.Save(_bestScore);
    }
}
