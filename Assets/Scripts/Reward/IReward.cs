namespace Scripts.Reward
{
    public interface IReward
    {
        public int RewardAmount { get; }

        public void MakeReward();
    }
}