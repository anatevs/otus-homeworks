using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public sealed class StatsContentArea : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _rectTransform;

        [SerializeField]
        private GridLayoutGroup _gridLayout;

        public void AdjustScrollContentArea()
        {
            int rowsAmount;
            if (_gridLayout.transform.childCount is 0 or 1 )
            {
                rowsAmount = 1;
            }
            else
            {
                rowsAmount = (_gridLayout.transform.childCount - 1) / _gridLayout.constraintCount + 1;
            }

            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, rowsAmount * _gridLayout.cellSize.y);
        }
    }
}
