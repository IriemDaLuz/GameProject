using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _itemName;

    [SerializeField]
    private Image _itemicon;

    [SerializeField]
    private GameObject _stackObj;

    [SerializeField]
    private TextMeshProUGUI _stackNumber;

    public void Set(InventoryItem item){
        _itemName.text= item.data.itenName;
        _itemicon.sprite = item.data.itemIcon;

        if (item.stackSize <= 1)
        {
            _stackObj.SetActive(false);
            return;
        }
        _stackNumber.text = item.stackSize.ToString();
    }
}
