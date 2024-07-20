public interface ITriggerCheckable
{
    bool isAggroed { get; set; }
    bool isWithingAttackRange { get; set; }

    public void SetAggro(bool Aggro)
    {
        isAggroed = Aggro;
    }

    public void setwithinAttackRange(bool AttackRangeCheck)
    {
        isWithingAttackRange = AttackRangeCheck;
    }

}
