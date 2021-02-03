using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DropdownPanel
{
    [RequireComponent(typeof(Image))]
    public class RollItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private Image arrow;
        [SerializeField] private Text text;
     
        public int id;
        
        private static RollItem selected;
        
        private Image image;
        private Color color;
        
        public event Action<int> OnRollItemSelected;
        public event Action<int> OnRollItemStartHover;
        public event Action<int> OnRollItemEndHover;

        private bool isSelected
        {
            get
            {
                return selected == this;
            }
        }
    
        public void Init(Structs.Floor floor)
        {
            image = GetComponent<Image>();
            color = image.color;
            
            text.text = floor.name;
            id = floor.id;
        }

        public void SubscribeMeToEvents(Action<int> selected, Action<int> startHover,
            Action<int> endHover)
        {
            OnRollItemSelected += selected;
            OnRollItemStartHover += startHover;
            OnRollItemEndHover += endHover;
        }

        public void UnSubscribeMeFromEvents(Action<int> selected, Action<int> startHover,
            Action<int> endHover)
        {
            OnRollItemSelected -= selected;
            OnRollItemStartHover -= startHover;
            OnRollItemEndHover -= endHover;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (selected != null)
                selected.Reset();
            
            selected = this;
            
            color.a = 0.6f;
            image.color = color;
            
            arrow.gameObject.SetActive(true);
            
            if (OnRollItemSelected != null)
                OnRollItemSelected.Invoke(id);
        }

        private void Reset()
        {
            color.a = 0f;
            image.color = color;
            
            arrow.gameObject.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isSelected)
            {
                color.a = 0.3f;
                image.color = color;
            }

            if (OnRollItemStartHover != null)
                OnRollItemStartHover.Invoke(id);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isSelected)
            {
                color.a = 0f;
                image.color = color;
            }

            if (OnRollItemEndHover != null)
                OnRollItemEndHover.Invoke(id);
        }
    }
}