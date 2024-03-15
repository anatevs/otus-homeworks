public class ZombieEntity : Entity
{
    private Zombie _zombie;

    public void Init(Zombie zombie)
    {
        _zombie = zombie;

        AddComponentToEntity(new DeathComponent(_zombie.isDead));
        AddComponentToEntity(new MoveDirectionComponent(_zombie.moveDirection));
        AddComponentToEntity(new UnspawnComponent(_zombie.OnUnspawn));
    }
}