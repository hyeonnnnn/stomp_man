using System.ComponentModel;
using UnityEngine;

public class DebuffComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == true && collision.GetComponent<PlayerMove>().IsDie == false)
        {
            // collision.GetComponent<PlayerMove>().DecreaseBounceForce(_damage);
        }
    }
}
