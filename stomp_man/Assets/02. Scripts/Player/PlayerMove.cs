using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 1f;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") == true)
        {
            _rigidbody.AddForce(Vector2.up * _bounceForce, ForceMode2D.Impulse);
        }
    }
}
