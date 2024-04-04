using Scellecs.Morpeh;

public sealed class ProjectileProvider : MovableProvider
{
    protected override void Initialize()
    {
        base.Initialize();
        Entity.SetComponent<ProjectileFlag>(new ProjectileFlag());
    }
}