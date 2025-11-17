using System.ComponentModel;
using UnityEngine;

public class DebuffComponent : MonoBehaviour
{
    [SerializeField] private float _damage = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == true && collision.GetComponent<PlayerMove>().IsDie == false)
        {
            collision.GetComponent<PlayerMove>().BounceForceDecrease(_damage);
        }
    }
}
