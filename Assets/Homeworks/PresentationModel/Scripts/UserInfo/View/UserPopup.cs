using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public sealed class UserPopup : MonoBehaviour
    {
        [SerializeField]
        private Text _username;
        
        [SerializeField]
        private Text _description;

        [SerializeField]
        private Image _image;

        private IUserPresenter _userPresenter;

        public void Show(IPresenter presenter)
        {
            if (presenter is not IUserPresenter userPresenter)
            {
                throw new InvalidDataException($"{presenter} must be a {typeof(IUserPresenter)} type");
            }
            _userPresenter = userPresenter;
            UpdateInfo();

            _userPresenter.OnUserinfoChanged += UpdateInfo;
            gameObject.SetActive(true);
        }

        private void UpdateInfo()
        {
            _username.text = _userPresenter.Name;
            _description.text = _userPresenter.Description;
            _image.sprite = _userPresenter.Icon;
        }

        public void Hide()
        {
            _userPresenter.OnUserinfoChanged -= UpdateInfo;
        }
    }
}
