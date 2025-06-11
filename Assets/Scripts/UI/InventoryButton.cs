using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    [SerializeField] Transform inventory;

    // �κ��丮 Ȱ��ȭ/��Ȱ��ȭ
    public void ChangeInventoryActive()
    {
        inventory.gameObject.SetActive(!inventory.gameObject.activeSelf);
    }
}
