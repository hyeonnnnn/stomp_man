using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _smoothing = 0.2f;
    [SerializeField] private GameObject _minBoundary;
    [SerializeField] private GameObject _maxBoundary;

    private void FixedUpdate()
    {
        if (_player == null) return;

        Vector3 targetPosition = new Vector3(_player.position.x, _player.position.y, transform.position.z);

        Vector3 _minBoundaryPosition = _minBoundary.transform.position;
        Vector3 _maxBoundaryPosition = _maxBoundary.transform.position;

        // 카메라는 화면 밖으로 나가지 않음
        targetPosition.x = Mathf.Clamp(targetPosition.x, _minBoundaryPosition.x, _maxBoundaryPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, _minBoundaryPosition.y, _maxBoundaryPosition.y);

        transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothing);
    }
}