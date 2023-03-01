using Cards;
using Cards.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class CardManager : MonoBehaviour
    {
        private Material _baseMat; 
        private List<CardPropertiesData> _allCards; 
        private List<MageCardPack> _mageCards;
        private List<WarriorCardPack> _warriorCards;
        private Card[] _deck1; 
        private Card[] _deck2; 
        [SerializeField]
        private CardPackConfiguration[] _packs; 
        [SerializeField] private Card _cardPrefab; 
        [Space, SerializeField, Range(1f, 100f)]
        private int _countCardInDeck = 30; 
        [SerializeField, Space]
        private Transform _deck1Parent; 
        [SerializeField]
        private Transform _deck2Parent; 
        [SerializeField]
        private PlayerHand _playerHand1; 
        [SerializeField]
        private PlayerHand _playerHand2; 
        public Animation Anim; 
        private void Awake()
        {
            IEnumerable<CardPropertiesData> cards = new List<CardPropertiesData>(); 
            foreach (var pack in _packs) cards = pack.UnionProperties(cards); 
            _allCards = new List<CardPropertiesData>(cards); 

            _baseMat = new Material(Shader.Find("TextMeshPro/Sprite")); 
            _baseMat.renderQueue = 2997; 

        }

        private void Start()
        {
            _deck1 = CreateDeck(_deck1Parent);
            _deck2 = CreateDeck(_deck2Parent); 
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) 
            {
                for(int i = _deck1.Length -1; i >= 0; i--) 
                {
                    if (_deck1[i] == null) continue; 

                    _playerHand1.SetNewCard(_deck1[i]); 
                    _deck1[i] = null; 
                    break; 
                }
            }
        }

        private Card[] CreateDeck(Transform parent) 
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
                deck[i].Configuration(random, CardUtility.GetDescriptionById(random.Id), newMat); 
            }
            return deck; 
        }
    }
}
