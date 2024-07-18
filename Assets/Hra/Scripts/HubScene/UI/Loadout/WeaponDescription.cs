using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponDescription : MonoBehaviour
{
    private Dictionary<string, string> _descriptions = new()
    {
        { "Scorching", "Increases your weapon’s range and adds a dot effect by setting enemies on Fire." },
        { "Arctic", "Slows enemies, but also slows down ur weapon." },
        { "Zappy", "Increases attack speed and does chain damage for enemies close to each other." },
        { "Life Sucking", "By itself works as a healing source." },
        { "Scorching-Zappy", "A weapon with fiery and electric powers." },
        { "Scorching-Arctic", "A weapon with fiery and frosty powers." },
        { "Scorching-Life Sucking", "Adds life steal to Fire, lowers damage." },
        { "Arctic-Zappy", "A weapon with frosty and electric powers." },
        { "Life Sucking-Arctic", "Adds life steal to Frost, lowers damage." },
        { "Life Sucking-Zappy", "Adds life steal to Electric, lowers damage." },
        { "Scorching-Arctic-Zappy", "A weapon with fiery, frosty, and electric powers." },
        { "Scorching-Life Sucking-Arctic", "A weapon with fiery, frosty, and life powers." },
        { "Scorching-Life Sucking-Zappy", "A weapon with fiery, electric, and life powers." },
        { "Life Sucking-Arctic-Zappy", "A weapon with frosty, electric, and life powers." },
        { "Scorching-Life Sucking-Arctic-Zappy", "A weapon with fiery, frosty, electric, and life powers." }
    };

    public string GetWeaponDescription()
    {
        List<ElementItem> equippedElements = LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedElements;

        if (equippedElements == null || equippedElements.Count == 0)
        {
            return null;
        }

        equippedElements.Sort((x, y) => x.AdjectivePosition.CompareTo(y.AdjectivePosition));
        string key = string.Join("-", equippedElements.Select(e => e.Adjective).ToArray());
        if (_descriptions.TryGetValue(key, out string weaponDescription))
        {
            return weaponDescription;
        }

        return "A weapon with unknown powers.";
    }
}
