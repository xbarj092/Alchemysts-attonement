public class EffectFactory
{
    public IPlayerEffect GetPlayerElementItem(Element element)
    {
        return element switch
        {
            Element.Life => new LifeStealEffect(),
            _ => null,
        };
    }

    public IEnemyEffect GetEnemyElementItem(Element element)
    {
        return element switch
        {
            Element.Fire => new DoTEffect(),
            Element.Frost => new FrostEffect(),
            Element.Electric => new ChainEffect(),
            _ => null,
        };
    }
}
