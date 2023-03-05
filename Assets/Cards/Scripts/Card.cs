using Cards;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Card
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField]
        private GameObject _frontCard;
        [Space, SerializeField]
        private MeshRenderer _icon;
        [Space, SerializeField]
        private TextMeshPro _cost;
        [Space, SerializeField]
        private TextMeshPro _name;
        [Space, SerializeField]
        private TextMeshPro _description;
        [Space, SerializeField]
        private TextMeshPro _type;
        [Space, SerializeField]
        private TextMeshPro _attack;
        [Space, SerializeField]
        private TextMeshPro _health;
        private Transform[] _positions;
        private Card[] _cards;
        private PlayerHand _camerMove;

        [SerializeField]
        private Transform _card;

        public void OnStart()
        {
            _cards = new Card[_positions.Length];
        }
        public bool IsEnable
        {
            get => _icon.enabled;
            set
            {
                _icon.enabled = value;
                _frontCard.SetActive(value);
            }
        }


        public CardStateType State { get; set; } = CardStateType.InDeck;

        public void Configuration(CardPropertiesData data, string description, Material icon)
        {
            _icon.sharedMaterial = icon;
            _cost.text = data.Cost.ToString();
            _name.text = data.Name;
            _description.text = description;
            _type.text = data.Type == CardUnitType.None ? string.Empty : data.Type.ToString();
            _attack.text = data.Attack.ToString();
            _health.text = data.Health.ToString();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //if (Turn.Player1 != Side) return; 
            switch (State)
            {
                case CardStateType.InHand:
                    transform.localScale *= 3f;
                    transform.position += new Vector3(0f, 2f, 100f);
                    break;
                case CardStateType.OnTable:
                    transform.localScale *= 3f;
                    transform.position += new Vector3(0f, 2f, 100f);
                    break;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            switch (State)
            {
                case CardStateType.InHand:
                    transform.localScale /= 3f;
                    transform.position -= new Vector3(0f, 2f, 100f);
                    break;
                case CardStateType.OnTable:
                    transform.localScale /= 3f;
                    transform.position -= new Vector3(0f, 2f, 100f);
                    break;
            }

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
    
        }
        public void OnDrag(PointerEventData eventData)
        {
            transform.position += new Vector3(eventData.delta.x, 0f, eventData.delta.y);
           
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
        }


        [ContextMenu("Switch Visual")]

        public void SwitchVisual()
        {
            IsEnable = !IsEnable;
        } 
        /*
        public void SwitchNonVisual()
        {
            IsEnable = IsEnable;
        }
        */
    }
}


