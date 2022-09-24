public abstract class MovementBaseState
{
    public abstract void EnterState(MovementStateManager movementStateManager);

    public abstract void UpdateState(MovementStateManager movementStateManager);

    /*
    public virtual void EnterState(MovementStateManager movementStateManager)
    {
        Debug.Log("Baslangicta veri okuturken");

        base ile kullaniliyor.
    }
    */
}
