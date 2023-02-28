using Cards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class PlayerHand : MonoBehaviour
    {
        private Card[] _cards;
        private Vector3 _endRot;
        private Vector3 _upPos;
        [SerializeField]
        private Transform[] _positions;
        private void Start()
        {
            _cards = new Card[_positions.Length]; 
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

        public bool SetNewCard(Card card)
        {
            var result = GetLastPosition();
            if (result == -1)
            {
                Destroy(card.gameObject);
                return false;
            }

            _cards[result] = card;
            _upPos = new Vector3(card.transform.position.x, card.transform.position.y + 100, card.transform.position.z);
            _endRot = new Vector3(card.transform.eulerAngles.x, card.transform.eulerAngles.y, card.transform.eulerAngles.z+360);
            StartCoroutine(MoveCardUp(card));
            StartCoroutine(RotateCard(card));
            StartCoroutine(MoveInHand(card, _positions[result]));

            return true;
        }
        private IEnumerator MoveCardUp(Card card)
        {
            var time = 0f;

            while (time < 1f)
            {
                card.transform.position = Vector3.Lerp(card.transform.position, _upPos, time);
                time += Time.deltaTime;
                yield return null;
            }
        }


        private IEnumerator RotateCard(Card card)
        {
            var time = 0.8f;
            yield return new WaitForSeconds(1f);
            card.SwitchVisual();
            while (time < 10f)
            {
                card.transform.eulerAngles = Vector3.Lerp(card.transform.eulerAngles, _endRot, time);
                
                time += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator MoveInHand(Card card, Transform parent)
        {
            var time = 0f;
            var endPos = parent.position;

            yield return new WaitForSeconds(1.5f);

            

            while (time < 1f)
            {
                card.transform.position = Vector3.Lerp(_upPos, endPos, time);
                time += Time.deltaTime;
                yield return null;
            }

            card.transform.parent = parent;
            card.State = CardStateType.InHand; 
        }
    }
}
