using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private GameObject _item;

    public void TryDropItem()
    {
        if (_item == null) return;

        DropItem();
    }

    private void DropItem()
    {
        Instantiate(_item, transform.position, Quaternion.identity);
    }
}
