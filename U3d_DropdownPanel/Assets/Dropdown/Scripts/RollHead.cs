using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DropdownPanel
{
    public class RollHead : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private RectTransform arrow;
        [SerializeField] private Text text;
        [SerializeField] private RectTransform itemsParent;
        [SerializeField] private RollItem rollItemPrefab;

        public int id;

        private static RollHead selected;

        private List<RollItem> rollItems = new List<RollItem>();

        private RectTransform rectTransform;
        private float rollSpeed = 10f;
        private float initialHeight = 0;

        private event Action<RollHead> OnRollHeadSelected;

        private Coroutine _sizeRoutine;
        private Coroutine _rotateRoutine;

        private bool isSelected
        {
            get { return selected == this; }
        }

        public void Init(Structs.Stage stage)
        {
            text.text = stage.name;
            id = stage.id;

            if (stage.floors != null)
                foreach (var floor in stage.floors)
                {
                    RollItem rollItem = Instantiate(rollItemPrefab, itemsParent);
                    rollItem.Init(floor);

                    rollItems.Add(rollItem);
                }

            rectTransform = GetComponent<RectTransform>();
            initialHeight = rectTransform.sizeDelta.y;

            OpenClose(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (selected != null && !isSelected)
                selected.OpenClose(false);

            if (isSelected)
            {
                selected = null;
                OpenClose(false);
            }
            else
            {
                selected = this;
                OpenClose(true);
            }
        }

        public void SubscribeMeToItemsEvents(Action<RollHead> dropdownSelected, Action<int> itemSelected,
            Action<int> itemHoverStart, Action<int> itemHverEnd)
        {
            OnRollHeadSelected += dropdownSelected;

            foreach (var item in rollItems)
            {
                item.SubscribeMeToEvents(itemSelected, itemHoverStart, itemHverEnd);
            }
        }

        public void UnSubscribeMeToItemsEvents(Action<RollHead> dropdownSelected, Action<int> itemSelected,
            Action<int> itemHoverStart, Action<int> itemHoverEnd)
        {
            OnRollHeadSelected -= dropdownSelected;

            foreach (var item in rollItems)
            {
                item.UnSubscribeMeFromEvents(itemSelected, itemHoverStart, itemHoverEnd);
            }
        }

        private bool _toMax;

        private void OpenClose(bool open)
        {
            _toMax = open;
            
            if (_sizeRoutine == null)
            {
                _sizeRoutine = StartCoroutine(SizeRoutine());
            }
 
            if (_rotateRoutine == null)
            {
                _rotateRoutine = StartCoroutine(RotateRoutine());
            }
            
            if (OnRollHeadSelected != null)
                OnRollHeadSelected.Invoke(this);
        }
   
        private IEnumerator SizeRoutine()
        {
            var maxHeight = initialHeight + itemsParent.sizeDelta.y;

            while (rectTransform.sizeDelta.y != Direction(maxHeight, initialHeight))
            {
                yield return null;

                float y = Mathf.Lerp(rectTransform.sizeDelta.y, _toMax ? maxHeight : initialHeight,
                    rollSpeed * Time.deltaTime);
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, y);
            }

            _sizeRoutine = null;
        }
        private IEnumerator RotateRoutine()
        {
            var maxHeight = initialHeight + itemsParent.sizeDelta.y;

            while (arrow.eulerAngles.z != Direction(180, 0))
            {
                yield return null;

                 float z = Mathf.Lerp(arrow.eulerAngles.z, _toMax ? 180 : 0, rollSpeed * Time.deltaTime);
                 var rotation = arrow.rotation;
                 arrow.rotation  = Quaternion.Euler(new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, z));
            }

            _rotateRoutine = null;
        }

        private float Direction(float max, float min)
        {
            return _toMax ? max : min;
        }
    }
}