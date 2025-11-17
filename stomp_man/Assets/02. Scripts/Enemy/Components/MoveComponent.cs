using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }
}
