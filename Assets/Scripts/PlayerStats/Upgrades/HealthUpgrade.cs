using VContainer;

namespace Sample
{
    public class HealthUpgrade : Upgrade
    {
        private readonly int _upgradeBase;

        private PlayerStats _playerStats;

        public HealthUpgrade(HealthConfig config) : base(config)
        {
            _upgradeBase = config.upgradeBase;
        }

        [Inject]
        public void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }

        protected override void LevelUp(int level)
        {
            int currentHP = _playerStats.GetStat(Id);

            _playerStats.SetStat(Id, currentHP + level * _upgradeBase);
        }
    }
}