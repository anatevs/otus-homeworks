using Scellecs.Morpeh;

public class ProjectileProvider : MovableProvider
{
    protected override void Initialize()
    {
        base.Initialize();
        Entity.SetComponent<ProjectileFlag>(new ProjectileFlag());
    }
}