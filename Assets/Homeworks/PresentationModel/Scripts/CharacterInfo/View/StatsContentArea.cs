using UnityEngine;
using UnityEngine.UI;

namespace Lessons.Architecture.PM
{
    public sealed class StatsContentArea : MonoBehaviour
    {
        [SerializeField]
        private GameObject _scrollContent;

        private RectTransform _rectTransform;
        private GridLayoutGroup _gridLayout;

        public void Initialize()
        {
            _gridLayout = _scrollContent.GetComponent<GridLayoutGroup>();
            _rectTransform = GetComponent<RectTransform>();
            AdjustScrollContentArea();
        }

        public void AdjustScrollContentArea()
        {
            int rowsAmount;
            if (_scrollContent.transform.childCount == 0 || _scrollContent.transform.childCount == 1 )
            {
                rowsAmount = 1;
            }
            else
            {
                rowsAmount = (_scrollContent.transform.childCount - 1) / _gridLayout.constraintCount + 1;
            }

            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, rowsAmount * _gridLayout.cellSize.y);
        }
    }
}
