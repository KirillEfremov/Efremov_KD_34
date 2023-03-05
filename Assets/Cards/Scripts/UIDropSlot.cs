using UnityEngine;

/// <summary>
/// Item slot class.
/// Store reference to the object inside slot.
/// </summary>
/// 
namespace Card
{
    public class UIDropSlot : MonoBehaviour
    {
        // Reference to the item inside slot.
        public Card _currentItem;

        // Tells if slot is filled by other item.
        public bool SlotFilled => _currentItem;
    }
}
