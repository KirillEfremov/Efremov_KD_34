using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Cards
{
    public class CardPanel1 : PanelManager
    {
        public static CardPanel1 Self;

        private void Start() => Self = this;
        public void MovePanel1() => _player1Panel.gameObject.SetActive(false);
        public void MovePanel2() => _player2Panel.gameObject.SetActive(true);
 
    }
}
