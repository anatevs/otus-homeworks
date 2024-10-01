namespace Scripts.Chest
{
    public sealed class TimersConstructor
    {
        private readonly ChestTimer[] _chestTimers;

        public TimersConstructor(
            ChestTimer[] chestTimers,
            TimeService timeService,
            AppInOutTimeService inOutTimeService,
            ChestsData chestsData
            )
        {
            _chestTimers = chestTimers;

            foreach (var timer in _chestTimers)
            {
                timer.Construct(timeService, inOutTimeService, chestsData);
            }
        }
    }
}