using Cards;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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
        private Vector3 _pos;
        [SerializeField]
        private Transform _card;

        Camera MainCamera;

        public void OnStart()
        {
            _cards = new Card[_positions.Length];
        }
        void Awake()
        {
            MainCamera = Camera.allCameras[0];
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
        public void OnBeginDrag(PointerEventData eventData)
        {

        }
        public void OnDrag(PointerEventData eventData)
        {
            transform.position += new Vector3(eventData.delta.x, 0f, eventData.delta.y);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            /*
            var offset = new Vector3(100f, 0.1f, 0f);
            var time = 0f;
            var endPos = new Vector3(-400f ,0.1f ,0f);
            _pos = new Vector3(_card.transform.position.x, _card.transform.position.y, _card.transform.position.z);
            _card.transform.position = Vector3.Lerp(_pos, endPos, time);
            */
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

        [ContextMenu("Switch Visual")] 

        public void SwitchVisual() 
        {
            IsEnable = !IsEnable; 
        }
    }
}

