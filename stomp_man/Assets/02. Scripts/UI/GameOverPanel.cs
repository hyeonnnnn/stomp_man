using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentScoreTextUI;
    [SerializeField] private TextMeshProUGUI _bestScoreTextUI;

    private void OnEnable()
    {
        ShowCurrentScore();
        ShowBestSocre();

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
