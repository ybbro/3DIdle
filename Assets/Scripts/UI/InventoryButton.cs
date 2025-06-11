using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] Transform inventory;

    // 인벤토리 활성화/비활성화
    public void ChangeInventoryActive()
    {
        inventory.gameObject.SetActive(!inventory.gameObject.activeSelf);
    }
}
