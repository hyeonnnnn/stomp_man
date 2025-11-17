using UnityEngine;
using UnityEngine.UI;

public class StompGuage : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _smoothSpeed = 5f;
    private float _maxGameSpeed;

    private void Start()
    {
        _maxGameSpeed = GameManager.Instance.MaxGameSpeed;
        _slider.maxValue = _maxGameSpeed;
    }

    private void Update()
    {
        float target = GameManager.Instance.CurrentGameSpeed;
        _slider.value = Mathf.Lerp(_slider.value, target, Time.deltaTime * _smoothSpeed);
    }
}
