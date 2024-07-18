using System.Collections.Generic;

public class WeaponAdjectives
{
    public string GetWeaponAdjectives()
    {
        List<ElementItem> elements = LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedElements;
        if (elements.Count == 0)
        {
            return null;
        }

        elements.Sort((x, y) => x.AdjectivePosition.CompareTo(y.AdjectivePosition));
        System.Text.StringBuilder adjectivesBuilder = new System.Text.StringBuilder();

        foreach (ElementItem equippedElement in elements)
        {
            if (!string.IsNullOrEmpty(equippedElement.Adjective))
            {
                if (adjectivesBuilder.Length > 0)
                {
                    adjectivesBuilder.Append(" ");
                }

                adjectivesBuilder.Append(equippedElement.Adjective);
            }
        }

        return adjectivesBuilder.ToString();
    }
}
