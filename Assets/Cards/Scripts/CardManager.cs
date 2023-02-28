using Cards;
using Cards.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card
{
    public class CardManager : MonoBehaviour
    {
        private Material _baseMat; //_основнойМатериал
        private List<CardPropertiesData> _allCards; //список _всеКарты типа ДанныеОСвойствахКарты 
        private List<MageCardPack> _mageCards;
        private List<WarriorCardPack> _warriorCards;
        private Card[] _deck1; //_колода1
        private Card[] _deck2; //_колода1
        [SerializeField]
        private CardPackConfiguration[] _packs; //массив типа КонфигурацияПакетаКарт _пакеты
        [SerializeField] private Card _cardPrefab; //_префабКарты
        [Space, SerializeField, Range(1f, 100f)]
        private int _countCardInDeck = 30; //_счетчикКартВКолоде
        [SerializeField, Space]
        private Transform _deck1Parent; //_родительКолоды1
        [SerializeField]
        private Transform _deck2Parent; //_родительКолоды2
        [SerializeField]
        private PlayerHand _playerHand1; //_рукаИгрока1
        [SerializeField]
        private PlayerHand _playerHand2; //_рукаИгрока2
        public Animation Anim; //аниматор
        private void Awake()
        {
            IEnumerable<CardPropertiesData> cards = new List<CardPropertiesData>(); //Интерфейсу IEnumerable (вызывается для реализации поиска по foreach и ключевого слова yield) карты типа ДанныеОСвойствахКарты присваивается список типа ДанныеОСвойствахКарты
            foreach (var pack in _packs) cards = pack.UnionProperties(cards); //Поиск по foreach (неопределенная переменная пакет в поле _пакеты) и в переменную карты присваивается значение через переменную пакет ОбъединенныхСвойств с аргументом карты
            _allCards = new List<CardPropertiesData>(cards); //полю _все карты присваивается новый аргументы карты списка ДанныеОСвойствахКарты

            _baseMat = new Material(Shader.Find("TextMeshPro/Sprite")); //_основномуМатериалу присваивается новый материал (найти шейдер "ТекстоваяСеткаПро/тип расширения для картинок в unity)
            _baseMat.renderQueue = 2997; //_основномуМатериалу задать очередь рендеринга равную 2997

        }

        private void Start()
        {
            _deck1 = CreateDeck(_deck1Parent); //вызываем метод СозданияКард с аргументом _родительКолоды1
            _deck2 = CreateDeck(_deck2Parent); //вызываем метод СозданияКард с аргументом _родительКолоды2
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)) //по нажатию на пробел вызываем цикл
            {
                for(int i = _deck1.Length -1; i >= 0; i--) //i равен длине колоды1 -1, пока i >= 0 идем в цикл
                {
                    if (_deck1[i] == null) continue; //если массив колоды1 от i равен 0 продолжаем

                    _playerHand1.SetNewCard(_deck1[i]); //_рука1Игрока устанавливает новую карту из массива колоды1 от i
                    _deck1[i] = null; //массив колоды 1 обнуляется
                    break; //выходим из цикла
                }
            }
        }

        private Card[] CreateDeck(Transform parent) //создаем метод СоздатьКолоду типа массив Карт с аргументом (типа Преобразованный родитель)
        {
            var deck = new Card[_countCardInDeck]; //переменной колода присваивается новое поле в массиве Карты _счетчикКартВКолоде (30 карт)
            var offset = 0.8f; //отступ 0.8 вверх для колоды
            for(int i = 0; i < _countCardInDeck; i++) //цикл пока i < 30 плюсуем i и идем в цикл
            {
                deck[i] = Instantiate(_cardPrefab, parent); //в массив колоды i создается _префабКарты, родитель
                deck[i].transform.localPosition = new Vector3(0f,offset, 0f); //в массив колоды i, в его локальную позицию записываем новую позицию (0,0.8,0) - карты располагаются в колоде с отступом в 0.8 вверх
                deck[i].transform.eulerAngles = new Vector3(0f, 0f, 180f); //поворачиваем  карту на 180
                deck[i].SwitchVisual(); //вызываем метод из класса Card
                offset += 0.8f; //увеличиваем отступ для расположения карт
                var random = _allCards[Random.Range(0, _allCards.Count)]; //карты располагаются в колоде рандомно
                var newMat = new Material(_baseMat); //создаем новый материал 
                newMat.mainTexture = random.Texture; //рисуем рандомную текстуру на созданном материале
                deck[i].Configuration(random, CardUtility.GetDescriptionById(random.Id), newMat); //ставим конфигурацию карты согласно её ID
            }
            return deck; //возвращаем колоду
        }
    }
}
