using Cards;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
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
        public void OnPointerDown(PointerEventData eventData)
        {
            switch (State)
            {
                case CardStateType.InHand:
                    break;
                case CardStateType.OnTable:
                    break;
                case CardStateType.Discard:
                    break;
                default:
                    if (CardManager.Self.GetIsPlayer1Turn())
                    {
                        StartCardManager.Self.PlaceCardInDeck1(this, CardManager.Self.GetCardNumber1());
                    }
                    else if (!CardManager.Self.GetIsPlayer1Turn())
                    {
                        StartCardManager.Self.PlaceCardInDeck2(this, CardManager.Self.GetCardNumber2());
                    }

                    break;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //if (Turn.Player1 != Side) return; 
            switch (State)
            {
                case CardStateType.InHand:
                    transform.localScale *= 3f;
                    transform.position += new Vector3(0f, 0f, 100f);
                    break;
                case CardStateType.OnTable:
                    transform.localScale *= 3f;
                    transform.position += new Vector3(0f, 0f, 100f);
                    break;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            switch (State)
            {
                case CardStateType.InHand:
                    transform.localScale /= 3f;
                    transform.position -= new Vector3(0f, 0f, 100f);
                    break;
                case CardStateType.OnTable:
                    transform.localScale /= 3f;
                    transform.position -= new Vector3(0f, 0f, 100f);
                    break;
            }

        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        
            
        }
        public void OnDrag(PointerEventData eventData)
        {
           
                transform.position += new Vector3(eventData.delta.x, 0f, eventData.delta.y);
           

            switch (State)
            {
                case CardStateType.InHand:
                    var hitPos = eventData.pointerCurrentRaycast.worldPosition;
                    var pos = transform.position;
                    transform.position = new Vector3(hitPos.x, 0.1f, hitPos.z);

                    break;
                case CardStateType.OnTable:
                    break;

            }
        }

      

        public void OnEndDrag(PointerEventData eventData)
        {
      
        }

        [ContextMenu("Switch Visual")]

        public void SwitchVisual()
        {
            IsEnable = !IsEnable;
        }

    }
}


