using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadoutElement : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;

    private ElementItem _elementItem;

    public void Init(ElementItem elementItem)
    {
        _elementItem = elementItem;

        _image.sprite = elementItem.Icon;

        int level = LocalDataStorage.Instance.PlayerData.UpgradesData.UpgradeData.FirstOrDefault(item => item.FriendlyID == elementItem.FriendlyID).Level;
        _text.text = elementItem.Name + " " + level.ToString();
    }
}
