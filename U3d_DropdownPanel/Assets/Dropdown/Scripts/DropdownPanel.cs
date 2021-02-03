using System;
using System.Collections.Generic;
using UnityEngine;

namespace DropdownPanel
{
    public class DropdownPanel : MonoBehaviour
    {
        [SerializeField] private RollHead rollHeadPrefab;
        [SerializeField] private RectTransform rollHeadsParent;

        private List<RollHead> rollHeads = new List<RollHead>();

        private event Action<int> OnDropdownClick, OnItemClick, OnItemStartHover, OnItemEndHover;

        public void Init(List<Structs.Stage> stages)
        {
            foreach (var stage in stages)
            {
                RollHead rollHead = Instantiate(rollHeadPrefab, rollHeadsParent);

                rollHead.Init(stage);

                rollHeads.Add(rollHead);
            }

            SubscribeToHeadEvents();
        }

        public void Clear()
        {
            if (rollHeads != null)
                foreach (var rollHead in rollHeads)
                {
                    Destroy(rollHead.gameObject);
                }
        }

        public void SubscribeMeToDropdownClick(Action<int> callback)
        {
            OnDropdownClick += callback;
        }


        public void SubscribeMeToItemClick(Action<int> callback)
        {
            OnItemClick += callback;
        }

        public void SubscribeMeToItemStartHower(Action<int> callback)
        {
            OnItemStartHover += callback;
        }

        public void SubscribeMeToItemEndHover(Action<int> callback)
        {
            OnItemEndHover += callback;
        }

        public void UnSubscribeMeFromDropdownClick(Action<int> callback)
        {
            OnDropdownClick -= callback;
        }

        public void UnSubscribeMeFromItemClick(Action<int> callback)
        {
            OnItemClick -= callback;
        }

        public void UnSubscribeMeFromItemStartHower(Action<int> callback)
        {
            OnItemStartHover -= callback;
        }

        public void UnSubscribeMeFromItemEndHover(Action<int> callback)
        {
            OnItemEndHover -= callback;
        }

        private void SubscribeToHeadEvents()
        {
            foreach (var rollHead in rollHeads)
                rollHead.SubscribeMeToItemsEvents(DropdownSelected, ItemSelected, ItemHoverStart, ItemHoverEnd);
        }

        private void OnDestroy()
        {
            foreach (var rollHead in rollHeads)
                rollHead.UnSubscribeMeToItemsEvents(DropdownSelected, ItemSelected, ItemHoverStart, ItemHoverEnd);
        }

        private void DropdownSelected(RollHead obj)
        {
            if (OnDropdownClick != null)
                OnDropdownClick.Invoke(obj.id);
        }

        private void ItemSelected(int id)
        {
            if (OnItemClick != null)
                OnItemClick.Invoke(id);
        }

        private void ItemHoverStart(int id)
        {
            if (OnItemStartHover != null)
                OnItemStartHover.Invoke(id);
        }

        private void ItemHoverEnd(int id)
        {
            if (OnItemEndHover != null)
                OnItemEndHover.Invoke(id);
        }
    }
}