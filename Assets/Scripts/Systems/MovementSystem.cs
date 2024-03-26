using Scellecs.Morpeh;

public sealed class MovementSystem : ISystem
{
    public World World
    {
        get => World.Default;
        set { }
    }

    private Filter _filter;

    public void OnAwake()
    {
        _filter = this.World.Filter
            .With<Position>()
            .With<MoveDirection>()
            .With<Speed>()
            .Without<Standing>()
            .Build();
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var entity in _filter)
        {
            ref Position position = ref entity.GetComponent<Position>();
            MoveDirection moveDirection = entity.GetComponent<MoveDirection>();
            Speed speed = entity.GetComponent<Speed>();

            position.value += moveDirection.value * speed.value * deltaTime;
        }
    }

    public void Dispose()
    {
    }
}