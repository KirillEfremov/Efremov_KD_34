using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Cards
{
    public class PanelManager : MonoBehaviour
    {
        [SerializeField]
        protected Image _player1Panel;
        [SerializeField]
        protected Image _player2Panel;
        [SerializeField]
        protected Image _deckSelectionPanel1;
        [SerializeField]
        protected SideType _sideType;

        public SideType GetSideType()
        {
            return _sideType;
        }

        #region SetSideType
        public void SetMageType()
        {
            _sideType = SideType.Mage;
        }

        public void SetWarriorType()
        {
            _sideType = SideType.Warrior;
        }

        #endregion
       
    }
}