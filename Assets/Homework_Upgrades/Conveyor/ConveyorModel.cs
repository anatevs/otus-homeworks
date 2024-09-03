using Atomic.Elements;
using Declarative;

public class ConveyorModel : DeclarativeModel
{
    public AtomicVariable<int> LoadStorageCapacity;
    public AtomicVariable<int> UnloadStorageCapacity;
    public AtomicVariable<float> ProduceTime;
}