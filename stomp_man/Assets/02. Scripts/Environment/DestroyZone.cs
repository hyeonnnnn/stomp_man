using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") == true)
        {
            Debug.Log("ªË¡¶");
            Destroy(collision.gameObject);
        }
    }
}