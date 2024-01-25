using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public sealed class StatView : MonoBehaviour
    {
        public Text StatText => _text;

        [SerializeField]
        private Text _text;
    }
}
