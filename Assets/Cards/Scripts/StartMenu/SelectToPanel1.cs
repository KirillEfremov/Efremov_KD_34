using UnityEngine;
using UnityEngine.EventSystems;

namespace Cards
{
    public class SelectToPanel1 : PanelManager, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private GameObject _page1;
        [SerializeField]
        private GameObject _page2;
        [SerializeField]
        private GameObject _page3;
        [SerializeField]
        private GameObject _page4;
        [SerializeField]
        private GameObject _page5;
        [SerializeField]
        private GameObject _page6;
        [SerializeField]
        private GameObject _page7;

        public static bool _disable = false;

       
        [SerializeField]
        private GameObject _prefabCard;

        public CardStateType State { get; set; } = CardStateType.InDeck;
        public void OnPointerEnter(PointerEventData eventData)
        { 
            switch (State)
            {
                case CardStateType.InDeck:
                    _prefabCard.transform.localScale *= 3f;
                    _prefabCard.transform.position += new Vector3(0f, 2f, 100f);
                    break;
                case CardStateType.InHand:
                    _prefabCard.transform.localScale *= 3f;
                    _prefabCard.transform.position += new Vector3(0f, 2f, 100f);
                    break;
                case CardStateType.OnTable:
                    _prefabCard.transform.localScale *= 3f;
                    _prefabCard.transform.position += new Vector3(0f, 2f, 100f);
                    break;
            }
            
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            switch (State)
            {
                case CardStateType.InDeck:
                    _prefabCard.transform.localScale /= 3f;
                    _prefabCard.transform.position -= new Vector3(0f, 2f, 100f);
                    break;
                case CardStateType.InHand:
                    _prefabCard.transform.localScale /= 3f;
                    _prefabCard.transform.position -= new Vector3(0f, 2f, 100f);
                    break;
                case CardStateType.OnTable:
                    _prefabCard.transform.localScale /= 3f;
                    _prefabCard.transform.position -= new Vector3(0f, 2f, 100f);
                    break;
            }
            
        }

        public void MoveDeckPanel1() => _deckSelectionPanel1.gameObject.SetActive(true);
        public void MoveDeckPanel2() => _player2Panel.gameObject.SetActive(false);

        public void DisableAllPages()
        {
            if (StartCardManager._headerPlayer1 == true)
            {
                CardManager._cardNumber1--;

                if (StartCardManager._headerPlayer2 == true)
                {
                    CardManager._cardNumber2--;
                }
            }
            _disable = true;
            _page1.SetActive(false);
            _page2.SetActive(false);
            _page3.SetActive(false);
            _page4.SetActive(false);
            _page5.SetActive(false);
            _page6.SetActive(false);
            _page7.SetActive(false);
        }

        #region Enable Pages

        public void EnablePage1() => _page1.SetActive(true);

        public void EnablePage2() => _page2.SetActive(true);

        public void EnablePage3() => _page3.SetActive(true);

        public void EnablePage4() => _page4.SetActive(true);

        public void EnablePage5() => _page5.SetActive(true);

        public void EnablePage6() => _page6.SetActive(true);

        public void EnablePage7() => _page7.SetActive(true);

        #endregion

    }
}