using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    [Header("점수")]
    [SerializeField] private int _score;

    [Header("아이템 이동")]
    [SerializeField] private float _flySpeed = 7f;
    [SerializeField] private float _coolTime = 2f;
    [SerializeField] private float _curveSpeedFactor = 10f;
    private GameObject _target;

    [Header("베지어곡선 제어")]
    [SerializeField] private float _controlPointHeightMin = 2f;
    [SerializeField] private float _controlPointHeightValue = 4f;
    [SerializeField] private float _controlPointWidthMin = -2f;
    [SerializeField] private float _controlPointWidthMax = 2f;

    private Vector2 _startPoint;
    private Vector2 _controlPoint;
    private Vector2 _endPoint;

    private float _curveProgression = 0f;
    private float _timer = 0f;
    private bool _isFlying = false;

    private void Awake()
    {
        _target = GameObject.FindWithTag("ScoreTarget");
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_isFlying == false && _timer >= _coolTime)
        {
            StartBezierFly();
        }
        else if (_isFlying == true)
        {
            MoveAlongBezier();
        }
    }

    protected void Disappear()
    {
        Destroy(gameObject);
    }

    protected void StartBezierFly()
    {
        if (_target.transform == null) return;

        _isFlying = true;
        _startPoint = transform.position;
        _endPoint = _target.transform.position;

        Vector2 midPoint = (_startPoint + _endPoint) / 2;
        float heightOffset = Random.Range(_controlPointHeightMin, _controlPointHeightValue);

        // 아치형 곡선 만들기
        float widthOffset = Random.Range(_controlPointWidthMin, _controlPointWidthMax);
        _controlPoint = new Vector2(midPoint.x + widthOffset, midPoint.y + heightOffset);

        _curveProgression = 0f;
    }

    protected void MoveAlongBezier()
    {
        if (_target.transform == null) return;

        _endPoint = _target.transform.position;

        _curveProgression += Time.deltaTime * (_flySpeed / _curveSpeedFactor);
        _curveProgression = Mathf.Min(_curveProgression, 1f);

        Vector2 bezierPosition = Mathf.Pow(1 - _curveProgression, 2) * _startPoint +
                                  2 * (1 - _curveProgression) * _curveProgression * _controlPoint +
                                  Mathf.Pow(_curveProgression, 2) * _endPoint;

        transform.position = bezierPosition;

        if (_curveProgression >= 1f)
        {
            _isFlying = false;
            ScoreManager.Instance.AddScore(_score);
            Disappear();
        }
    }
}
