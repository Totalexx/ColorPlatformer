using RougeBuilder.Model;

namespace RougeBuilder.Component.Impl;

public class MoveToTarget : AbstractComponent
{
    public Positional target;

    public MoveToTarget(Positional target)
    {
        this.target = target;
    }
}