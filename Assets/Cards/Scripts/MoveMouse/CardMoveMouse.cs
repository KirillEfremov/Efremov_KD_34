using Card;
using Cards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Card
{
    public class CardMoveMouse : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField]
        private Transform[] _positionsParent;
        private Card[] _cards;
        private Card _card;

        private void Start()
        {
            _cards = new Card[_positionsParent.Length];
        }

        private int GetLastPosition()
        {
            for (int i = 0; i < _cards.Length; i++)
            {
                if (_cards[i] == null)
                    return i;
            }
            return -1;
        }
        private IEnumerator MoveOnTable(Card card, Transform parent)
        {
            var time = 0f;
            var endPos = parent.position;

            yield return new WaitForSeconds(1.5f);

            while (time < 1f)
            {
                card.transform.position = Vector3.Lerp(card.transform.position, endPos, time);
                time += Time.deltaTime;
                yield return null;
            }

            card.transform.parent = parent;
            card.State = CardStateType.OnTable;
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
            var result = GetLastPosition();
            StartCoroutine(MoveOnTable(_card, _positionsParent[result]));
        }
    }
}
