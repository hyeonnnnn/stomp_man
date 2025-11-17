using UnityEngine;
using UnityEngine.UIElements;

public class PanelManager : MonoBehaviour
{
    private static PanelManager _instance = null;
    public static PanelManager Instance => _instance;

    [SerializeField] private GameObject _gameStartPanel;
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _gameOvertPanel;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    public void ShowGameStartPanel()
    {
        HideAll();
        _gameStartPanel.SetActive(true);
    }

    public void ShowMainPanel()
    {
        HideAll();
        Time.timeScale = 1;
        _mainPanel.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        HideAll();
        Time.timeScale = 0;
        _gameOvertPanel.SetActive(true);
    }

    private void HideAll()
    {
        _gameStartPanel.SetActive(false);
        _mainPanel.SetActive(false);
        _gameOvertPanel.SetActive(false);
    }
}
