using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Button _retryButtonUI;
    [SerializeField] private Button _homeButtonUI;

    [SerializeField] private TextMeshProUGUI _currentScoreTextUI;
    [SerializeField] private TextMeshProUGUI _bestScoreTextUI;

    private void OnEnable()
    {
        ShowCurrentScore();
        ShowBestSocre();

    }

    public void Retry()
    {
        PanelManager.Instance.ShowMainPanel();
    }

    public void GoHome()
    {
        PanelManager.Instance.ShowHomePanel();
    }

    public void ShowCurrentScore()
    {
        _currentScoreTextUI.text = $"{ScoreManager.Instance.CurrentScore:N0}";
    }

    public void ShowBestSocre()
    {
        _bestScoreTextUI.text = $"{ScoreManager.Instance.BestScore:N0}";
    }
}
