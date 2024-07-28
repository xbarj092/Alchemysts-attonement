using System.Collections.Generic;

public class ElementHandler
{
    private EffectFactory _effectFactory = new();
    private Enemy _enemy;

    private List<Element> _nonRecursiveElements = new()
    {
        Element.Electric
    };

    public void ApplyElements(Enemy enemy = null, bool recursive = false)
    {
        _enemy = enemy;

        List<ElementItem> elements = new(LocalDataStorage.Instance.PlayerData.LoadoutData.EquippedElements);
        if (recursive)
        {
            elements.RemoveAll(element => _nonRecursiveElements.Contains(element.Element));
        }

        foreach (ElementItem element in elements)
        {
            ApplyElementEffect(element);
        }
    }

    private void ApplyElementEffect(ElementItem element)
    {
        element.PlayerEffect = _effectFactory.GetPlayerElementItem(element.Element);
        element.EnemyEffect = _effectFactory.GetEnemyElementItem(element.Element);

        if (element.PlayerEffect != null)
        {
            element.ApplyPlayerEffect(LocalDataStorage.Instance.PlayerData.PlayerStats);
        }

        if (element.EnemyEffect != null)
        {
            element.ApplyEnemyEffect(_enemy);
        }
    }
}
