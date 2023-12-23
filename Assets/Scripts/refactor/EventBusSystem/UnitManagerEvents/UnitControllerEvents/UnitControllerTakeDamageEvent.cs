public class UnitControllerTakeDamageEvent
{
    public UnitController Emitter { get; private set; }
    public int Damage { get; private set; }

    public UnitControllerTakeDamageEvent(UnitController emitter, int damage)
    {
        Emitter = emitter;
        Damage = damage;
    }
}