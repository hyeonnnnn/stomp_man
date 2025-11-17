using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    private float _minOffset = 0.5f;
    private float _maxOffset = 1f;
    private float _speedOffset;

    private void Start()
    {
        _speedOffset = Random.Range(_minOffset, _maxOffset);
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.left * (GameManager.Instance.CurrentGameSpeed + _speedOffset) * Time.deltaTime);
    }
}
