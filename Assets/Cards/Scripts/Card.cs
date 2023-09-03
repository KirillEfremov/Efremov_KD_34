using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Experimental.GraphView;

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
        [Space, SerializeField]
        private TextMeshPro _healthOfHero1;
        [Space, SerializeField]
        private TextMeshPro _healthOfHero2;
        public int ownerPlayer;
        private Transform[] _positions;
        private Card[] _cards;
        private PlayerHand _camerMove;
        [SerializeField]
        private Transform _card;
        private CardManager cardManager;

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

        void Start()
        {
            cardManager = GameObject.FindGameObjectWithTag("CardManager").GetComponent<CardManager>();
        }

		void Update()
		{
            if (int.Parse(_health.text) <= 0)
            {
                Destroy(gameObject);
            }
			if (transform.parent.gameObject.tag == "DropArea")
			{
				gameObject.transform.localPosition =  new Vector3(0, 0.1f, 0);
			}
		}

        public CardStateType State { get; set; } = CardStateType.InDeck;

        public void Configuration(CardPropertiesData data, string description, Material icon,  int _ownerPlayer)
        {
            _icon.sharedMaterial = icon;
            _cost.text = data.Cost.ToString();
            _name.text = data.Name;
            _description.text = description;
            _type.text = data.Type == CardUnitType.None ? string.Empty : data.Type.ToString();
            _attack.text = data.Attack.ToString();
            _health.text = data.Health.ToString();
			ownerPlayer = _ownerPlayer;
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

			Text text = GameObject.FindGameObjectWithTag("SelectUnitText").GetComponent<Text>();
			if (text != null && text.text == "Выбирите дружественного юнита!")
			{
				text.text = "";
				int newAttackValue = int.Parse(_attack.text) + 1;
				int newHealthValue = int.Parse(_health.text) + 1;
				_attack.text = newAttackValue.ToString();
				_health.text = newHealthValue.ToString();
			}
            else if (text != null && text.text == "Выбирите вражеского юнита!")
            {
                text.text = "";
                if (ownerPlayer == 1)
                {
                    if (int.Parse(_health.text) - int.Parse(_attack.text) <= 0)
                    {
                        int HealthOfHero = int.Parse(_healthOfHero1.text) - int.Parse(_attack.text);
                        _healthOfHero1.text = HealthOfHero.ToString();
                        Destroy(gameObject);
                    }
                    else
                    {
                        int newHealth = int.Parse(_health.text) - int.Parse(_attack.text);
                        _health.text = newHealth.ToString();
                    }
                    
                }
                else if (ownerPlayer == 2)
                {
                    if (int.Parse(_health.text) - int.Parse(_attack.text) <= 0)
                    {
                        int HealthOfHero = int.Parse(_healthOfHero1.text) - int.Parse(_attack.text);
                        _healthOfHero2.text = HealthOfHero.ToString();
                        Destroy(gameObject);
                    }
                    else
                    {
                        int newHealth = int.Parse(_health.text) - int.Parse(_attack.text);
                        _health.text = newHealth.ToString();
                    }
                }
            }
            else if (text != null && text.text == "Попробуй! Атакуй!")
            {
                text.text = "";
                int newHealthValue = int.Parse(_health.text);
                _health.text = newHealthValue.ToString();
            }
            else if (text != null && text.text == "Выбирите вражеского юнита! Атака пройдёт позже.")
            {
                StartCoroutine(DelayedDamage());
            }
        }

        public IEnumerator DelayedDamage()
        {
            Text text = GameObject.FindGameObjectWithTag("SelectUnitText").GetComponent<Text>();
            text.text = "";
            yield return new WaitForSeconds (10f);
            if (ownerPlayer == 1)
            {
                if (int.Parse(_health.text) - int.Parse(_attack.text) <= 0)
                {
                    int HealthOfHero = int.Parse(_healthOfHero1.text) - int.Parse(_attack.text);
                    _healthOfHero1.text = HealthOfHero.ToString();
                    Destroy(gameObject);
                }
                else
                {
                    int newHealth = int.Parse(_health.text) - int.Parse(_attack.text);
                    _health.text = newHealth.ToString();
                }
            }
            else if (ownerPlayer == 2)
            {
                if (int.Parse(_health.text) - int.Parse(_attack.text) <= 0)
                {
                    int HealthOfHero = int.Parse(_healthOfHero1.text) - int.Parse(_attack.text);
                    _healthOfHero2.text = HealthOfHero.ToString();
                    Destroy(gameObject);
                }
                else
                {
                    int newHealth = int.Parse(_health.text) - int.Parse(_attack.text);
                    _health.text = newHealth.ToString();
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
        }

        public void OnMouseEnter()
        {
            //if (Turn.Player1 != Side) return; 

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    Debug.Log("Курсор наведен на объект с BoxCollider");
                    transform.localScale *= 3f;
                    GetComponent<BoxCollider>().size /= 2f;
                    transform.position += new Vector3(0f, 0f, 50f);
                }
            }
        }

        void OnMouseExit()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == null || hit.collider.gameObject.tag != "Card" || hit.collider.GetComponent<BoxCollider>() == null)
                {
                    Debug.Log("Курсор не наведен на объект с BoxCollider");
                    transform.localScale /= 3f;
                    GetComponent<BoxCollider>().size *= 2f;
                    transform.position -= new Vector3(0f, 0f, 50f);
                }
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }
		
        public void OnDrag(PointerEventData eventData)
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (cardManager != null && ownerPlayer == cardManager.walkingPlayer)
			{
                string thisManaPlayer1string = Regex.Replace(cardManager.manaPlayer1Text.text, "[^0-9]", "");
                string thisManaPlayer2string = Regex.Replace(cardManager.manaPlayer2Text.text, "[^0-9]", "");
                int thisManaPlayer1 = Convert.ToInt32(thisManaPlayer1string);
                int thisManaPlayer2 = Convert.ToInt32(thisManaPlayer2string);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("CardConnectionPlayer1")) && hit.transform.childCount < 1 && Convert.ToInt32(_cost.text) <= thisManaPlayer1)
                {
                    gameObject.transform.SetParent(hit.transform);
                    _cards = cardManager._playerHand1._cards;
                    thisManaPlayer1 -= Convert.ToInt32(_cost.text);
                    Debug.Log(thisManaPlayer1);
                    if (thisManaPlayer1 < 1)
                    {
                        cardManager.walkingPlayer = 2;
                        cardManager.manaPlayer1++;
                        cardManager.manaPlayer1Text.text = "<color=blue>Мана: <color=yellow>" + cardManager.manaPlayer1.ToString();
                    }
                    else
                    {
                        cardManager.manaPlayer1Text.text = "<color=blue>Мана: <color=yellow>" + thisManaPlayer1.ToString();
                    }
                    for (int i = 0; i < _cards.Length; i++)
                    {
                        if (_cards[i] == gameObject.GetComponent<Card>())
                            _cards[i] = null;
                    }
                }
                else if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("CardConnectionPlayer2")) && hit.transform.childCount < 1 && Convert.ToInt32(_cost.text) <= thisManaPlayer2)
                {
                    gameObject.transform.SetParent(hit.transform);
                    _cards = cardManager._playerHand2._cards;
                    thisManaPlayer2 -= Convert.ToInt32(_cost.text);
                    Debug.Log(thisManaPlayer2);
                    if (thisManaPlayer2 < 1) {
                        cardManager.walkingPlayer = 1;
                        cardManager.manaPlayer2++;
                        cardManager.manaPlayer2Text.text = "<color=blue>Мана: <color=yellow>" + cardManager.manaPlayer2.ToString();
                    }
                    else
                    {
                        cardManager.manaPlayer2Text.text = "<color=blue>Мана: <color=yellow>" + thisManaPlayer2.ToString();
                    }
                    for (int i = 0; i < _cards.Length; i++)
                    {
                        if (_cards[i] == gameObject.GetComponent<Card>())
                            _cards[i] = null;
                    }
                }
				
                
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
        }

      

        public void OnEndDrag(PointerEventData eventData)
        {
			if (transform.parent.gameObject.tag == "DropArea")
			{
				if (_description.text.IndexOf("Battlecry", StringComparison.OrdinalIgnoreCase) >= 0)
				{
					GameObject.FindGameObjectWithTag("SelectUnitText").GetComponent<Text>().text = "Выбирите дружественного юнита!";
				}
                else if (_description.text.IndexOf("Spell Damage", StringComparison.OrdinalIgnoreCase) >= 0 || _description.text.IndexOf("Charge", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    GameObject.FindGameObjectWithTag("SelectUnitText").GetComponent<Text>().text = "Выбирите вражеского юнита!";
                }
                else if (_description.text.IndexOf("Taunt", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    GameObject.FindGameObjectWithTag("SelectUnitText").GetComponent<Text>().text = "Попробуй! Атакуй!";
                }
                else if (_description.text == "")
                {
                    GameObject.FindGameObjectWithTag("SelectUnitText").GetComponent<Text>().text = "Выбирите вражеского юнита! Атака пройдёт позже.";
                }
            }
        }

        [ContextMenu("Switch Visual")]

        public void SwitchVisual()
        {
            IsEnable = !IsEnable;
        }

    }
}