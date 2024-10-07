namespace Scripts.Chest.Reward
{
    public interface IReward
    {
        public int Value { get; }

        public void MakeReward();
    }
}