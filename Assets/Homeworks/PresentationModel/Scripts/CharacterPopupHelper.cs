using UnityEngine;
using VContainer;

namespace Lessons.Architecture.PM
{
    public sealed class CharacterPopupHelper : MonoBehaviour
    {
        [SerializeField]
        private CharacterPopup _characterPopup;

        [SerializeField]
        private CharacterInfoPopup _characterInfoPopup;

        [SerializeField]
        private UserPopup _userPopup;

        [SerializeField]
        private PlayerLevelPopup _playerLevelPopup;

        private CharacterInfoPresenter _characterInfoPresenter;

        private UserPresenter _userPresenter;

        private PlayerLevelPresenter _playerLevelPresenter;

        [Inject]
        public void Constructor(CharacterInfoPresenter characterInfoPresenter, UserPresenter userPresenter, PlayerLevelPresenter playerLevelPresenter)
        {
            _characterInfoPresenter = characterInfoPresenter;
            _userPresenter = userPresenter;
            _playerLevelPresenter = playerLevelPresenter;
        }

        public void Show()
        {
            _characterPopup.Show();
            ShowPopups();

            _characterPopup.OnHidePopup += HidePopups;
        }

        private void ShowPopups()
        {
            _userPopup.Show(_userPresenter);
            _playerLevelPopup.Show(_playerLevelPresenter);
            _characterInfoPopup.Show(_characterInfoPresenter);
        }

        private void HidePopups()
        {
            _characterInfoPopup.Hide();
            _userPopup.Hide();
            _playerLevelPopup.Hide();

            _characterPopup.OnHidePopup -= HidePopups;
        }
    }
}