using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Camera _camera;
    private Vector3 _initialViewportPosition;
    private Vector3 _velocity = Vector3.zero;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _initialViewportPosition.y = transform.position.y;
    }

    private void LateUpdate()
    {
        if (_player == null) return;

        Vector3 viewportPosition = _camera.WorldToViewportPoint(_player.position);

        bool isOutSide = viewportPosition.y < 0.5f || viewportPosition.y > 0.5f;

        if (isOutSide == true)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, _player.position.y, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, 0.12f);
        }
        else
            _velocity = Vector3.zero;
    }
}