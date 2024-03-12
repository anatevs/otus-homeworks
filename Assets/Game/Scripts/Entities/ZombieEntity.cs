public class ZombieEntity : Entity
{
    private Zombie _zombie;

    public void Inint(Zombie zombie)
    {
        _zombie = zombie;

        AddComponentToEntity(new DeathComponent(_zombie.isDead));
        AddComponentToEntity(new HPComponent(_zombie.hp));
    }
}