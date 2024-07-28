public interface ITriggerCheckable
{
    bool IsAggroed { get; set; }
    bool IsWithinAttackRange { get; set; }

    public void SetAggro(bool Aggro)
    {
        IsAggroed = Aggro;
    }

    public void SetWithinAttackRange(bool AttackRangeCheck)
    {
        IsWithinAttackRange = AttackRangeCheck;
    }
}
