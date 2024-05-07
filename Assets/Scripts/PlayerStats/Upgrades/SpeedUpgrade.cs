using UnityEngine;
using VContainer;

namespace Sample
{
    public class SpeedUpgrade : Upgrade
    {
        private PlayerStats _playerStats;

        private int _startSpeed;

        private readonly int _upgradeAmount;

        public SpeedUpgrade(SpeedConfig config) : base(config)
        {
            _upgradeAmount = config.UpgradeAmount;
        }

        [Inject]
        public void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
            _startSpeed = _playerStats.GetStat(Id);
        }

        protected override void LevelUp(int level)
        {
            _playerStats.SetStat(Id, _startSpeed + (level - 1) * _upgradeAmount);
        }
    }
}