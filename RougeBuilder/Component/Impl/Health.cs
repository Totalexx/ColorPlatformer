namespace RougeBuilder.Component.Impl;

public class Health : AbstractComponent
{
    public int HealthSize = 100;

    public Health(int health)
    {
        HealthSize = health;
    }
}