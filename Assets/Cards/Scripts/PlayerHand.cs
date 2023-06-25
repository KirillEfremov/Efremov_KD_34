using Cards;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Cards
{
    public class PlayerHand : MonoBehaviour
    {
        public Card[] _cards;
        private Vector3 _endRot;
        private Vector3 _upPos;
        private Vector3 _previewPos1;
        private Vector3 _previewPos2;
        private int _offset1 = -200;
        private int _offset2 = 200;
        [SerializeField]
        private Transform[] _positions;
        [SerializeField]
        private TextMeshPro _health1Player;
        [SerializeField]
        private TextMeshPro _health2Player;
        private bool _camerMove = false;



        #region CamerMove 
        [SerializeField] private Transform camer;
        [SerializeField] private Vector3 positionPlayer1;
        [SerializeField] private Vector3 positionPlayer2;

      
        public void CamerMoveToPlayer2()
        {
            StartCoroutine(PositionCamer(camer, positionPlayer2, 1.5f));
            StartCoroutine(PositionCamerToPlayer2Rotation(camer, 1.5f));
            CamerMove = true;
        }

        public void CamerMoveToPlayer1()
        {
            StartCoroutine(PositionCamer(camer, positionPlayer1, 1.5f));
            StartCoroutine(PositionCamerToPlayer1Rotation(camer, 1.5f));
            CamerMove = false;
        }
        public bool CamerMove
        {
            get { return _camerMove; }
            set { _camerMove = value; }
            
        }

        private IEnumerator PositionCamerToPlayer2Rotation(Transform obj, float TravelTime)
        {
            yield return new WaitForSeconds(0.5f);

            float t = 0;

            while (t < 1)
            {
                obj.localRotation = Quaternion.Slerp(obj.localRotation, Quaternion.Euler(50, 180, 0), t * t * 0.1f);

                t += Time.deltaTime / TravelTime;

                yield return null;
            }
            obj.rotation = Quaternion.Euler(50, 180, 0);
        }

        private IEnumerator PositionCamerToPlayer1Rotation(Transform obj, float TravelTime)
        {
            yield return new WaitForSeconds(0.5f);

            float t = 0;

            while (t < 1)
            {
                obj.localRotation = Quaternion.Slerp(obj.localRotation, Quaternion.Euler(50, 0, 0), t * t * 0.1f);

                t += Time.deltaTime / TravelTime;

                yield return null;
            }
            obj.rotation = Quaternion.Euler(50, 0, 0);
        }

        private IEnumerator PositionCamer(Transform obj, Vector3 target, float TravelTime)
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 startPosition = obj.position;
            float t = 0;

            while (t < 1)
            {
                obj.position = Vector3.Lerp(startPosition, target, t);

                t += Time.deltaTime / TravelTime;

                yield return null;
            }
            obj.position = target;
        }

        private IEnumerator Position(Transform obj, Vector3 target, float TravelTime)
        {
            Vector3 startPosition = obj.position;
            float t = 0;

            while (t < 1)
            {
                obj.position = Vector3.Lerp(startPosition, target + new Vector3(0, 0.2f, 0), t * t);
                t += Time.deltaTime / TravelTime;
                yield return null;
            }
            obj.transform.position = target + new Vector3(0, 0.2f, 0);
        }
        #endregion



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

        public bool SetNewCard1(Card card)
        {
            var result = GetLastPosition();

            if (result == -1)
            {
                Destroy(card.gameObject);
                return false;
            }

            _cards[result] = card;
            _upPos = new Vector3(card.transform.position.x, card.transform.position.y + 100, card.transform.position.z);
            _endRot = new Vector3(card.transform.eulerAngles.x, card.transform.eulerAngles.y, card.transform.eulerAngles.z + 360);
            _previewPos1 = new Vector3(_offset1, 400, -130);
            

            StartCoroutine(RotateCard(card));
            StartCoroutine(MoveCardUp1(card));
            _offset1 += 50;
            StartCoroutine(MoveInHand(card, _positions[result]));

            return true;
        }

        public bool SetNewCard2(Card card)
        {
            var result = GetLastPosition();

            if (result == -1)
            {
                Destroy(card.gameObject);
                return false;
            }

            _cards[result] = card;
            _upPos = new Vector3(card.transform.position.x, card.transform.position.y + 100, card.transform.position.z);
            _endRot = new Vector3(card.transform.eulerAngles.x, card.transform.eulerAngles.y + 180, card.transform.eulerAngles.z + 360);
            _previewPos2 = new Vector3(_offset2, 400, 130);

            StartCoroutine(RotateCard(card));
            StartCoroutine(MoveCardUp2(card));
            _offset2 += -50;
            StartCoroutine(MoveInHand(card, _positions[result]));

            return true;
        }
        private IEnumerator MoveCardUp1(Card card)
        {
            var time = 0f;

            while (time < 1f)
            {
                card.transform.position = Vector3.Lerp(card.transform.position, _upPos, time);
                card.transform.position = Vector3.Lerp(card.transform.position, _previewPos1, time);
                time += Time.deltaTime;
                yield return null;
            }
        }

        private IEnumerator MoveCardUp2(Card card)
        {
            var time = 0f;

            while (time < 1f)
            {
                card.transform.position = Vector3.Lerp(card.transform.position, _upPos, time);
                card.transform.position = Vector3.Lerp(card.transform.position, _previewPos2, time);
                time += Time.deltaTime;
                yield return null;
            }
        }


        private IEnumerator RotateCard(Card card)
        {
            var time = 0.8f;
            yield return new WaitForSeconds(1f);
            card.SwitchVisual();
            while (time < 2f)
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

            yield return new WaitForSeconds(2f);

            

            while (time < 1f)
            {
                card.transform.position = Vector3.Lerp(card.transform.position, endPos, time);
                time += Time.deltaTime;
                yield return null;
            }

            card.transform.parent = parent;
            card.State = CardStateType.InHand; 
        }
    }
}
