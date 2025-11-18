using UnityEngine;

public class GameStateController : MonoBehaviour
{
    private bool _isGameOver = false;
    private bool _isOpenedPanel = false;

    public bool IsGameOver => _isGameOver;

    public void ResetState()
    {
        _isGameOver = false;
        _isOpenedPanel = false;
    }

    public void OnSpeedDepleted()
    {
        GameOver();
    }

    public void GameOver()
    {
        if (_isGameOver == true) return;
        _isGameOver = true;

        HandleGameOver();
    }

    private void HandleGameOver()
    {
        if (_isOpenedPanel == true) return;
        _isOpenedPanel = true;

        SoundManager.Instance.PlaySFX(SoundManager.Sfx.GAMEOVER);
        PanelManager.Instance.ShowGameOverPanel();
        GameManager.Instance.PauseGame();
    }
}
