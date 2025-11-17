using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothing = 0.2f;
    [SerializeField] Vector2 minCameraBoundary;
    [SerializeField] Vector2 maxCameraBoundary;

    private void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

        targetPosition.x = Mathf.Clamp(targetPosition.x, minCameraBoundary.x, maxCameraBoundary.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minCameraBoundary.y, maxCameraBoundary.y);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
    }
}