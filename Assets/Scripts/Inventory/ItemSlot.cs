using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private Image _itemicon;
    [SerializeField] private GameObject _stackObj;
    [SerializeField] private TextMeshProUGUI _stackNumber;

    [SerializeField] private Button assignLeftBtn;
    [SerializeField] private Button assignRightBtn;

    public void Set(InventoryItem item)
    {
        _itemName.text = item.data.itenName;
        _itemicon.sprite = item.data.itemIcon;

        if (item.stackSize <= 1)
        {
            _stackObj.SetActive(false);
        }
        else
        {
            _stackObj.SetActive(true);
            _stackNumber.text = item.stackSize.ToString();
        }

        assignLeftBtn.onClick.RemoveAllListeners();
        assignRightBtn.onClick.RemoveAllListeners();

        assignLeftBtn.onClick.AddListener(() =>
        {
            HandSystem.Instance.AssignToLeftHand(item.data);
        });

        assignRightBtn.onClick.AddListener(() =>
        {
            HandSystem.Instance.AssignToRightHand(item.data);
        });
    }
}
