using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public sealed class HeroListView : MonoBehaviour
    {
        private const int FORWARD_LAYER = 10;
        private const int BACK_LAYER = 0;

        public event Action<HeroView> OnHeroClicked;

        [SerializeField]
        private HeroView[] _views;

        private Canvas canvas;

        private void Awake()
        {
            this.canvas = this.GetComponent<Canvas>();
        }

        private void OnEnable()
        {
            foreach (var view in this._views)
            {
                view.OnClicked += () => this.OnHeroClicked?.Invoke(view);
            }
        }

        private void OnDisable()
        {
            Action<HeroView> @event = this.OnHeroClicked;
            if (@event == null)
            {
                return;
            }

            foreach (var @delegate in @event.GetInvocationList())
            {
                this.OnHeroClicked -= (Action<HeroView>) @delegate;
            }
        }

        public IReadOnlyList<HeroView> GetViews()
        {
            return this._views;
        }

        public HeroView GetView(int index)
        {
            return this._views[index];
        }

        public int GetIndex(HeroView view)
        {
            return Array.IndexOf(_views, view);
        }

        public void SetActive(bool isActive)
        {
            this.canvas.sortingOrder = isActive ? FORWARD_LAYER : BACK_LAYER;
        }

        public void OnViewSetActive(int index, bool isActive)
        {
            _views[index].SetActive(isActive);
        }

        public void OnViewDestroyed(int index)
        {
            _views[index].gameObject.SetActive(false);
        }

        public void SetStats(int index, int hp, int damage)
        {
            _views[index].SetStats($"{damage}/{hp}");
        }
    }
}