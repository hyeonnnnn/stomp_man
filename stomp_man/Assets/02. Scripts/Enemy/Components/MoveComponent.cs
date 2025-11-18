using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private float _minOffset = 0.5f;
    private float _maxOffset = 1f;
    private float _speedOffset;

    private GameSpeedController _speed;

    private void Awake()
    {
        _speed = FindFirstObjectByType<GameSpeedController>();
    }

    private void Start()
    {
        _speedOffset = Random.Range(_minOffset, _maxOffset);
    }

    private void Update()
    {
        if (_speed == null) return;
        Move();
    }

    private void Move()
    {
        float moveSpeed = _speed.CurrentSpeed + _speedOffset;
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
    }
}
