using Cards;
using Cards.ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

namespace Cards
{
    public class CardManager : MonoBehaviour
    {
        public static CardManager Self;

        protected Material _baseMat; 
        protected List<CardPropertiesData> _allCards;
        protected Card[] _deck1;
        protected Card[] _deck2;
        public int manaPlayer1;
        public TMP_Text manaPlayer1Text;
        public int manaPlayer2;
        public TMP_Text manaPlayer2Text;
        [SerializeField]
        private CardPackConfiguration[] _packs; 
        [SerializeField] 
        protected Card _cardPrefab; 
        [Space, SerializeField, Range(1f, 100f)]
        private int _countCardInDeck = 30;

        [SerializeField, Space]
        private Transform _deck1Parent; 
        [SerializeField]
        private Transform _deck2Parent; 
        [SerializeField]
        public PlayerHand _playerHand1; 
        [SerializeField]
        public PlayerHand _playerHand2;
        [SerializeField]
        private PlayerHand _camerMove;

        protected bool _isPlayer1Turn = true;

        public static int _cardNumber1 = 3;
        public static int _cardNumber2 = 3;

        public int walkingPlayer = 1;

        public TMP_Text healthPlayer1;
        public TMP_Text healthPlayer2;
  
        private void Awake()
        {
            IEnumerable<CardPropertiesData> cards = new List<CardPropertiesData>(); 
            foreach (var pack in _packs) cards = pack.UnionProperties(cards); 
            _allCards = new List<CardPropertiesData>(cards); 

            _baseMat = new Material(Shader.Find("TextMeshPro/Sprite")); 
            _baseMat.renderQueue = 2997; 

        }

        public bool GetIsPlayer1Turn()
        {
            return _isPlayer1Turn;
        }
        public int GetCardNumber1() => _cardNumber1;

        public int GetCardNumber2() => _cardNumber2;

       
        private void Start()
        {
            _deck1 = CreateDeck(_deck1Parent, 1);
            _deck2 = CreateDeck(_deck2Parent, 2); 
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && walkingPlayer == 1)
            {
                for (int i = _deck1.Length - 1; i >= 0; i--)
                {
                    if (_deck1[i] == null) continue;
                    _playerHand1.SetNewCard1(_deck1[i]);
                    _deck1[i] = null;
                    break;
                }
                if (_deck1Parent.childCount == 0)
                {
                    healthPlayer1.text = (Convert.ToInt32(healthPlayer1.text) - 1).ToString();
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) && walkingPlayer == 2)
            {
                for (int i = _deck2.Length - 1; i >= 0; i--)
                {
                    if (_deck2[i] == null) continue;
                    _playerHand2.SetNewCard2(_deck2[i]);
                    _deck2[i] = null;
                    if (_deck2Parent.childCount == 0)
                    {
                        healthPlayer2.text = (Convert.ToInt32(healthPlayer2.text) - 1).ToString();
                    }
                    break;
                }
            }
        }

        public void StartGame() => SceneManager.LoadScene("SampleScene");


        private Card[] CreateDeck(Transform parent, int ownerPlayer) 
        {
            var deck = new Card[_countCardInDeck]; 
            var offset = 0.8f; 
            for(int i = 0; i < _countCardInDeck; i++) 
            {
                deck[i] = Instantiate(_cardPrefab, parent); 
                deck[i].transform.localPosition = new Vector3(0f,offset, 0f); 
                deck[i].transform.eulerAngles = new Vector3(0f, 0f, 180f); 
                deck[i].SwitchVisual(); 
                offset += 0.8f; 
                var random = _allCards[Random.Range(0, _allCards.Count)]; 
                var newMat = new Material(_baseMat);  
                newMat.mainTexture = random.Texture; 
                deck[i].Configuration(random, CardUtility.GetDescriptionById(random.Id), newMat, ownerPlayer); 
            }
            return deck; 
        }

        public void SetWalkingPlayer(int newWalkingPlayer)
        {
            if (walkingPlayer == 1 && walkingPlayer != newWalkingPlayer)
            {
                manaPlayer1++;
                manaPlayer1Text.text = "<color=blue>Мана: <color=yellow>" + manaPlayer1.ToString();
            }
            else if (walkingPlayer == 2 && walkingPlayer != newWalkingPlayer)
            {
                manaPlayer2++;
                manaPlayer2Text.text = "<color=blue>Мана: <color=yellow>" + manaPlayer2.ToString();
            }
            walkingPlayer = newWalkingPlayer;
        }
    }
}
