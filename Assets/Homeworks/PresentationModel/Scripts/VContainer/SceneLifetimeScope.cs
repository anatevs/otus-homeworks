using VContainer;
using VContainer.Unity;

namespace Lessons.Architecture.PM
{
    public sealed class SceneLifetimeScope : LifetimeScope
    {
        private UserInfo _userInfo = new UserInfo();
        private CharacterInfo _characterInfo = new CharacterInfo();
        private PlayerLevel _playerLevel = new PlayerLevel();
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterUserInfo(builder);
            RegisterPlayerLevel(builder);
            RegisterCharacterInfo(builder);
        }

        public void RegisterUserInfo(IContainerBuilder builder)
        {
            builder.RegisterComponent<UserInfo>(_userInfo);
            builder.Register<UserPresenter>(Lifetime.Singleton);
        }

        public void RegisterCharacterInfo(IContainerBuilder builder)
        {
            builder.RegisterComponent<CharacterInfo>(_characterInfo);
            builder.Register<CharacterInfoPresenter>(Lifetime.Singleton);
        }

        public void RegisterPlayerLevel(IContainerBuilder builder)
        {
            builder.RegisterComponent<PlayerLevel>(_playerLevel);
            builder.Register<PlayerLevelPresenter>(Lifetime.Singleton);
        }
    }
}