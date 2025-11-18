using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadyPanel : MonoBehaviour
{
    [SerializeField] private Button _gameStartButtonUI;
    [SerializeField] private TextMeshProUGUI _countDownTextUI;
    [SerializeField] private GameObject _gameStartTextUI;
    private float _term = 1;

    public void StartGame()
    {
        Time.timeScale = 0f;
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        int count = 3;

        while (count > 0)
        {
            _countDownTextUI.text = count.ToString();
            yield return new WaitForSecondsRealtime(_term);
            count--;
        }

        _gameStartTextUI.SetActive(true);
        _gameStartButtonUI.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(_term);
        _gameStartTextUI.SetActive(false);

        gameObject.SetActive(false);

        Time.timeScale = 1f;
    }
}
