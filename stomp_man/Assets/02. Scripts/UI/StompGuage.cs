using UnityEngine;
using UnityEngine.UI;

public class StompGuage : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _smoothSpeed = 5f;
    
    private GameSpeedController _speed;

    private void Awake()
    {
        _speed = FindFirstObjectByType<GameSpeedController>();
    }

    private void Start()
    {
        if (_speed != null)
        {
            _slider.maxValue = _speed.MaxSpeed;
        }
    }

    private void Update()
    {
        if (_speed == null) return;

        float target = _speed.CurrentSpeed;
        _slider.value = Mathf.Lerp(_slider.value, target, Time.deltaTime * _smoothSpeed);
    }
}
