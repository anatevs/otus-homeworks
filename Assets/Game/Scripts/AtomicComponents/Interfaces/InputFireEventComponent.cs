using UnityEngine;

public class InputFireEventComponent : IInputFireEventComponent
{
    private IAtomicEvent<Vector3> _onInputFire;

    public InputFireEventComponent(IAtomicEvent<Vector3> onInputFire)
    {
        _onInputFire = onInputFire;
    }

    public void OnInputFire(Vector3 target)
    {
        _onInputFire.Invoke(target);
    }
}