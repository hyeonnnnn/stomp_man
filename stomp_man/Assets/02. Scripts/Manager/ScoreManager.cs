using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance = null;
    public static ScoreManager Instance => _instance;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _currentScoreTextUI;
    [SerializeField] private GameObject _jemIcon;

    private float _imageEffectScale = 1.9f;
    private float _imageEffectDuration = 0.2f;
    private float _imageEffectReturnDuration = 0.5f;

    private int _currentScore;
    private int _bestScore;

    public int CurrentScore => _currentScore;
    public int BestScore => _bestScore;

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
        IconEffect();
    }

    private void Start()
    {
        InitScore();
    }

    private void InitScore()
    {
        LoadBestScore();
        UpdateCurrentScoreUI();
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
        EffectManager.Instance.PlayScoreEffect(_currentScoreTextUI.transform.position);
        _currentScoreTextUI.text = $"{_currentScore:N0}";
    }

    private void UpdateBestScore()
    {
        if (_currentScore > _bestScore)
        {
            _bestScore = _currentScore;
            SaveBestScore();
        }
    }

    private void SaveBestScore()
    {
        SaveManager.Instance.Save(_bestScore);
    }

    private void IconEffect()
    {
        if (_jemIcon == null) return;

        _jemIcon.transform.DOKill();
        _jemIcon.transform.DOScale(_imageEffectScale, _imageEffectDuration).OnComplete(() =>
        {
            _jemIcon.transform.DOScale(1f, _imageEffectReturnDuration);
        });

    }
}
