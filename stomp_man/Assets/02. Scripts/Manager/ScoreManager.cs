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
    }

    private void Start()
    {
        InitScore();
    }

    private void InitScore()
    {
        _currentScoreTextUI.text = $"Score: {_currentScore:N0}";
        _BestScoreTextUI.text = $"Best Score: {_bestScore:N0}";
    }

    private void UpdateCurrentScoreUI()
    {
        _currentScoreTextUI.text = $"Score: {_currentScore:N0}";
    }
}
