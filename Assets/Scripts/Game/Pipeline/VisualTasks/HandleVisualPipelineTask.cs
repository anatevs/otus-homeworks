public class HandleVisualPipelineTask : Task
{
    private readonly VisualPipeline _visualPipeline;

    public HandleVisualPipelineTask(VisualPipeline visualPipeline)
    {
        _visualPipeline = visualPipeline;
    }

    protected override void OnRun()
    {
        _visualPipeline.OnFinished += OnFinishVisualPipelineTask;

        _visualPipeline.Run();
    }

    protected override void OnFinished()
    {
        _visualPipeline.OnFinished -= OnFinishVisualPipelineTask;
    }

    private void OnFinishVisualPipelineTask()
    {
        _visualPipeline.Clear();
        Finish();
    }
}