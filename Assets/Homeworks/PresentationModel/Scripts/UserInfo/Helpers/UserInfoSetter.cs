using UnityEngine;
using VContainer;

namespace Lessons.Architecture.PM
{
    public sealed class UserInfoSetter : MonoBehaviour
    {
        [SerializeField]
        private string _username;

        [SerializeField]
        private string _description;

        [SerializeField]
        private Sprite _icon;


        private UserInfo _userinfo;

        [Inject]
        public void Construct(UserInfo userInfo)
        {
            _userinfo = userInfo;
        }

        public void Awake()
        {
            SetAllUserParams();
        }

        public void SetUsername()
        {
            _userinfo.ChangeName(_username);
        }

        public void SetDescription()
        {
            _userinfo.ChangeDescription(_description);
        }

        public void SetIcon()
        {
            _userinfo.ChangeIcon(_icon);
        }

        public void SetAllUserParams()
        {
            _userinfo.ChangeName(_username);
            _userinfo.ChangeDescription(_description);
            _userinfo.ChangeIcon(_icon);
        }
    }
}
