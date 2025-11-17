using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _smoothTime = 0.12f;

    private Vector3 _velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, _player.position.y, transform.position.z);

        transform.position = Vector3.SmoothDamp( transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}
