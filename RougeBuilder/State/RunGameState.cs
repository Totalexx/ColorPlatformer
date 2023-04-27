using NotImplementedException = System.NotImplementedException;

namespace RougeBuilder.State;

public class RunGameState : GameState
{
    public override GameState Update()
    {
        return new GameLoadingState();
    }
}