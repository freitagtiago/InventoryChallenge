using TMPro;
using UnityEngine;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText = null;
    [SerializeField] private TextMeshProUGUI _bodyText = null;

    public void Setup(ItemSO item)
    {
        _titleText.text = item.GetItemName();
        _bodyText.text = item.GetItemDescription();
    }
}