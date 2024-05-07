using VContainer;

namespace Sample
{
    public class DamageUpgrade : Upgrade
    {
        private readonly int _upgradeAmount;

        private PlayerStats _playerStats;

        private int _startDamage;

        public DamageUpgrade(DamageConfig config) : base(config)
        {
            _upgradeAmount = config.UpgradeAmount;
        }

        [Inject]
        public void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
            _startDamage = _playerStats.GetStat(Id);
        }

        protected override void LevelUp(int level)
        {
            _playerStats.SetStat(Id, _startDamage + (level - 1) * _upgradeAmount);
        }
    }
}