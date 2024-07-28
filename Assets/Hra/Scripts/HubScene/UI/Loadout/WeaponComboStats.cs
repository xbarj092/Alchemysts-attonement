using System.Collections.Generic;
using System.Linq;

public class WeaponComboStats
{
    private Dictionary<string, Dictionary<WeaponStat, float>> _comboStats = new()
    {
        { "Scorching-Zappy", new() },
        { "Scorching-Arctic", new() },
        { "Scorching-Life-Sucking", new() },
        { "Arctic-Zappy", new() },
        { "Life-Sucking-Arctic", new() },
        { "Life-Sucking-Zappy", new() },
        { "Scorching-Arctic-Zappy", new() },
        { "Scorching-Life-Sucking-Arctic", new() },
        { "Scorching-Life-Sucking-Zappy", new() },
        { "Life-Sucking-Arctic-Zappy", new() },
        { "Scorching-Life-Sucking-Arctic-Zappy", new() },
    };

    public Dictionary<WeaponStat, float> GetComboStat()
    {
        List<ElementItem> equippedElements = LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedElements;
        if (equippedElements == null || equippedElements.Count == 0)
        {
            return null;
        }

        equippedElements.Sort((x, y) => x.AdjectivePosition.CompareTo(y.AdjectivePosition));
        string key = string.Join("-", equippedElements.Select(e => e.Adjective).ToArray());
        if (_comboStats.TryGetValue(key, out Dictionary<WeaponStat, float> comboStat))
        {
            return comboStat;
        }

        return null;
    }
}
