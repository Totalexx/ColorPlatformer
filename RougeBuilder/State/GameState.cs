namespace RougeBuilder.State;

public abstract class GameState
{
    protected GameState()
    {
        Start();
    }
    
    protected virtual void Start() {}
    public abstract GameState Update();
}