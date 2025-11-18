using UnityEngine;
using UnityEngine.UIElements;

public class PanelManager : MonoBehaviour
{
    private static PanelManager _instance = null;
    public static PanelManager Instance => _instance;

    [SerializeField] private GameObject _HomeUI;
    [SerializeField] private GameObject _mainPanelUI;
    [SerializeField] private GameObject _gameOvertPanelUI;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }

    public void ShowHomePanel()
    {
        Debug.Log("ShowHomePanel");
        HideAll();
        Time.timeScale = 0;
        _HomeUI.SetActive(true);
    }

    public void ShowMainPanel()
    {
        Debug.Log("ShowMainPanel");
        HideAll();
        Time.timeScale = 1;
        _mainPanelUI.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        HideAll();
        Time.timeScale = 0;
        _gameOvertPanelUI.SetActive(true);
    }

    private void HideAll()
    {
        _HomeUI.SetActive(false);
        _mainPanelUI.SetActive(false);
        _gameOvertPanelUI.SetActive(false);
    }
}
