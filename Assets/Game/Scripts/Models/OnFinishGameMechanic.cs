using UnityEngine;

public class OnFinishGameMechanic
{
    private readonly IAtomicVariable<bool> _isGameFinished;

    public OnFinishGameMechanic(IAtomicVariable<bool> isGameFinished)
    {
        _isGameFinished = isGameFinished;
    }

    public void OnFinishGame()
    {
        _isGameFinished.Value = true;
    }
}