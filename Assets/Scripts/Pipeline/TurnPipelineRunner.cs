using UnityEngine;
using VContainer;

public sealed class TurnPipelineRunner : MonoBehaviour
{
    private bool _isRunOnFinish = true;

    private TurnPipeline _turnPipeline;

    [Inject]
    public void Construct(TurnPipeline turnPipeline)
    {
        _turnPipeline = turnPipeline;
    }

    private void OnEnable()
    {
        _turnPipeline.OnFinished += RunAgain;
    }

    private void Start()
    {
        _turnPipeline.Run();
    }

    private void OnDisable()
    {
        _turnPipeline.OnFinished -= RunAgain;
    }

    private void RunAgain()
    {
        if (_isRunOnFinish)
        {
            _turnPipeline.Run();
        }
    }

}