using Microsoft.Xna.Framework;
using NotImplementedException = System.NotImplementedException;

namespace RougeBuilder.Global;

public static class Time
{
    public static GameTime GameTime
    {
        get => _gameTime;
        set
        {
            DeltaTime = value.ElapsedGameTime.TotalMilliseconds;
            _gameTime = value;
        }
    }

    public static double DeltaTime { get; private set; }

    private static GameTime _gameTime;
}