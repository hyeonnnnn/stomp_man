using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReadyPanel : MonoBehaviour
{
    [SerializeField] private Button _gameStartButtonUI;
    [SerializeField] private TextMeshProUGUI _countDownTextUI;
    [SerializeField] private GameObject _gameStartTextUI;

    [SerializeField] private float _effectScale = 1.5f;
    [SerializeField] private float _effectDuration = 0.1f;
    [SerializeField] private float _effectReturnDuration = 0.15f;
    [SerializeField] private Ease _effectEase = Ease.OutBack;

    private float _countDownTerm = 1f;
    private float _gameStartTextTerm = 0.6f;

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
            yield return new WaitForSecondsRealtime(_countDownTerm);
            count--;
        }

        _gameStartTextUI.SetActive(true);
        GameStartTextEffect();

        _gameStartButtonUI.gameObject.SetActive(false);
        
        yield return new WaitForSecondsRealtime(_gameStartTextTerm);
        
        _gameStartTextUI.SetActive(false);

        gameObject.SetActive(false);

        Time.timeScale = 1f;
    }

    private void GameStartTextEffect()
    {
        Transform t = _gameStartTextUI.transform;
        t.localScale = Vector3.one;

        t.DOScale(_effectScale, _effectDuration)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                t.DOScale(0f, _effectReturnDuration)
                    .SetEase(_effectEase)
                    .SetUpdate(true);
            });
    }


}
