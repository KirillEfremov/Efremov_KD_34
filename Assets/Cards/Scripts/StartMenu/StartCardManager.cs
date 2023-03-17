using UnityEngine;
using UnityEngine.UI;

namespace Cards
{
    public class StartCardManager : CardManager
    {
        public static StartCardManager Self;

        private Card[] _heap;

        [SerializeField]
        private Transform _parent;

        [Space, SerializeField]
        private Transform[] _positions;

        [Space]
        public Text _LeftChooseCardsCount1;

        [Space]
        public Text _messageText;


        [Space]
        public Button _playButton;


        [Space]
        public static GameObject _headerPlayer1;
        public static GameObject _headerPlayer2;


        private void Start()
        {
            Self = this;
            _heap = CreateCardsHip();
        }

        public void PlaceCardInDeck1(Card card, int i)
        {
            if (SelectToPanel1._disable == true && _cardNumber1 == 0)
            {
                _headerPlayer1.SetActive(false);
                _headerPlayer2.SetActive(true);
                _isPlayer1Turn = false;
            }
 
        }

        public void PlaceCardInDeck2(Card card, int i)
        {
           
             if (SelectToPanel1._disable == true && _cardNumber2 == 0)
             {
                _playButton.gameObject.SetActive(true);
             } 
        }


        private void LateUpdate()
        {
            if (_isPlayer1Turn) _LeftChooseCardsCount1.text = (_cardNumber1).ToString();
            if (!_isPlayer1Turn)
            {
                _LeftChooseCardsCount1.text = (_cardNumber2).ToString();
            }
        }
        public Card[] CreateCardsHip()
        {
            var heap = new Card[_allCards.Count];

            for (int i = 0, j = 0; i < _allCards.Count; i++, j++)
            {

                heap[i] = Instantiate(_cardPrefab, _positions[j]);
                heap[i].transform.position = _positions[j].position;
                heap[i].transform.eulerAngles = new Vector3(-90f, 180f, 0f);
                heap[i].transform.localScale = new Vector3(425f, 10f, 700f);

                var random = _allCards[Random.Range(0, _allCards.Count)];

                var newMaterial = new Material(_baseMat);
                newMaterial.mainTexture = random.Texture;

                heap[i].Configuration(random, CardUtility.GetDescriptionById(random.Id), newMaterial);
            }
            return heap;

            
            
        }
    }
}
