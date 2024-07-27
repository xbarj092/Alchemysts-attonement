public interface ITriggerCheckable
{
    bool IsAggroed { get; set; }
    bool IsWithingAttackRange { get; set; }

    public void SetAggro(bool Aggro)
    {
        IsAggroed = Aggro;
    }

    public void SetWithinAttackRange(bool AttackRangeCheck)
    {
        IsWithingAttackRange = AttackRangeCheck;
    }
}
