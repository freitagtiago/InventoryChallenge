using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SlotIcon : MonoBehaviour
{
    [SerializeField] private GameObject _textContainer = null;
    [SerializeField] private TextMeshProUGUI _itemQuantity = null;

    public void SetItem(ItemSO item)
    {
        SetItem(item, 0);
    }

    public void SetItem(ItemSO item, int number)
    {
        var iconImage = GetComponent<Image>();
        if (item == null)
        {
            iconImage.enabled = false;
        }
        else
        {
            iconImage.enabled = true;
            iconImage.sprite = item.GetIcon();
        }

        if (_itemQuantity)
        {
            if (number <= 1)
            {
                _textContainer.SetActive(false);
            }
            else
            {
                _textContainer.SetActive(true);
                _itemQuantity.text = number.ToString();
            }
        }
    }
}
