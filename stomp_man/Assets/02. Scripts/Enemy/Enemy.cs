using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 5.0f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.left * _speed * Time.deltaTime);
    }


}
