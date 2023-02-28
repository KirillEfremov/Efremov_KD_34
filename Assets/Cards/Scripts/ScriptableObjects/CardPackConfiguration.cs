using OneLine;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Cards.ScriptableObjects
{
	[CreateAssetMenu(fileName = "NewCardPackConfiguration", menuName = "CardConfigs/Card Pack Configuration")]
	public class CardPackConfiguration : ScriptableObject
	{

		[SerializeField]
		private SideType _sideType;
		[SerializeField]
		private ushort _cost;
		[SerializeField, OneLine(Header = LineHeader.Short)] //плагин для компактной визуализации
		private CardPropertiesData[] _cards;

		public IEnumerable<CardPropertiesData> UnionProperties(IEnumerable<CardPropertiesData> array)
		{
            for (int i = 0; i < _cards.Length; i++)
            {
                _cards[i].Cost = _cost;
            }

            return array.Union(_cards);
		}
	}
} 