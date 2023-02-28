using Cards;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Card
{
    public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField]
        private GameObject _frontCard; //_лицеваяСторонКарты
        [Space, SerializeField]
        private MeshRenderer _icon; //_символ
        [Space, SerializeField]
        private TextMeshPro _cost; //_стоимость
        [Space, SerializeField]
        private TextMeshPro _name; //_имя
        [Space, SerializeField]
        private TextMeshPro _description; //_описание
        [Space, SerializeField]
        private TextMeshPro _type; //_тип
        [Space, SerializeField]
        private TextMeshPro _attack; //_атака
        [Space, SerializeField]
        private TextMeshPro _health; //_здоровье

        Camera MainCamera;
        public Transform DefaultParent;
        void Awake()
        {
            MainCamera = Camera.allCameras[0];
        }
        public bool IsEnable //булевый метод разрешения
        {
            get => _icon.enabled; //свойство чтения поля: _символ.включение(разрешение)
            set //свойство изменения
            {
                _icon.enabled = value; //разрешение на присвоение значения полю _символ и присвоение величины
                _frontCard.SetActive(value); //полю _лицевойСтороныКарты включаем игровой объект (делаем активным, видимым)
            }
        }

        public CardStateType State { get; set; } = CardStateType.InDeck; //создаем свойство Состояние типа ТипСостоянияКарты (в классе Перечисления создано перечисление ТипСостоянияКарты с константой ВКолоде) со свойствами (чтение, запись) и присваеваем Состоянию константу ВКолоде

        public void Configuration(CardPropertiesData data, string description, Material icon) //создаем метод Конфигурация с аргументами данные типа ДанныеОСвойствахКарты, (в структуре ДанныеОСвойствахКарты есть переменная ИД (индентификатор) типа целочисленная укороченная, Стоимость типа укороченная короткая с плавающей запятой, Имя типа строка, Текстура типа Текстура, Атака типа укороченная короткая с плавающей запятой, Здоровье типа укороченная короткая с плавающей запятой, Тип типа ТипБлокаКрты (перечисления, где константы Ничего с нулевой стоимостью, Мурлок стоимостью 1, Зверь стоимостью 2, Элементаль стоимостью 3, Мех (механический) стоимостью 4)) описание типа строка, символ типа Материал) 
        {
            _icon.sharedMaterial = icon; //обращаемся к свойству общийМатериал через поле _символ, где чтение возвращает метод ПолучитьОбщийМатериал, а запись УстановитьМатериал с аргументом значение (величина), записываем туда переменную символ типа Материал
            _cost.text = data.Cost.ToString(); //обращаемся к свойству текст через поле _стоимость и записываем туда перегруженный метод КСтроке типа УкороченныйЦелочисленный16
            _name.text = data.Name; //обращаемся к свойству текст через поле _имя и записывем туда Имя типа строка через переменную данные 
            _description.text = description; //обращаемся к свойству текст через поле _описание и записываем туду переменную описание типа строка
            _type.text = data.Type == CardUnitType.None ? string.Empty : data.Type.ToString(); //тернарная условная операция (условие?истина:ложь) присваиваем тексту через поле _тип Тип типа ТипБлокаКарты и сравниваем с тернарной условной операцией (если ТипБлокаКарты Ничего, то пишем пустую строку в случае истины или записываем тип ТипаБлокаКарты через метод КСтроке в случае лжи
            _attack.text = data.Attack.ToString(); //через поле _атака присваиваем тексту поле КСтроке через переменную Атака и данные
            _health.text = data.Health.ToString(); //через поле _здоровье присваиваем тексту поле КСтроке через переменную Здоровье и данные
        }

        public void OnBeginDrag(PointerEventData eventData) //инициализация процесса перетаскивания
        {
            Vector3 offset;
            offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);
            DefaultParent = transform.parent;

            transform.SetParent(DefaultParent.parent);
            GetComponent<CanvasGroup>().blocksRaycasts = false;

        }
    

        public void OnDrag(PointerEventData eventData) //перемещение объекта
        {
            transform.position += new Vector3(eventData.delta.x, 0f, eventData.delta.y); //задаем новую позицию объекта через Вектор3 (данныеОСобытии.разница.по х, 0, данныеОСобытии.разница.по y
            /*                                                                             // Continue moving object around screen.
            transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0) / transform.lossyScale.x;// Thanks to the canvas scaler we need to devide pointer delta by canvas scale to match pointer movement.
            */
        }

        public void OnEndDrag(PointerEventData eventData) //завершение процесса перетаскивания, помещаем объект в нужное место
        {
            /*
            // On end we need to test if we can drop item into new slot.
            var results = new List<RaycastResult>();
            graphicRaycaster.Raycast(eventData, results
        // Check all hits.
        foreach (var hit in results)
            {
                // If we found slot.
                var slot = hit.gameObject.GetComponent<UIDropSlot>();
                if (slot)
                {
                    // We should check if we can place ourselves​ there.
                    if (!slot.SlotFilled)
                    {
                        // Swapping references.
                        currentSlot.currentItem = null;
                        currentSlot = slot;
                        currentSlot.currentItem = this;
                    }
                    // In either cases we should break check loop.
                    break;
                }
            }
            // Changing parent back to slot.
            transform.SetParent(currentSlot.transform);
            // And centering item position.
            transform.localPosition = Vector3.zero;
            */
        }

        public void OnPointerEnter(PointerEventData eventData) //используется для определения того, когда мышь начинает наводиться на определенный игровой объект.
        {
            //if (Turn.Player1 != Side) return; 
            switch (State) //переключатель (Состояние): 
            {
                case CardStateType.InHand: //кейс1 ТипСостоянияКарты с константой ВКолоде:
                    transform.localScale *= 3f; //увеличиваем на 3 масштаб карты
                    transform.position += new Vector3(0f, 2f, 100f); //новая позиция (0,2,100) (изначальная в руке)
                    break;
                case CardStateType.OnTable: //кейс2 ТипСостоянияКарты на столе:
                    //прописать, чтобы карты бросались на нужные координаты стола
                    //увеличиваем при наведении
                    //возвращаем на изначальную позицию стола
                    break;
            }
        }

        public void OnPointerExit(PointerEventData eventData) //используется когда мышь перестает зависать над игровым объектом
        {
            switch (State)
            {
                case CardStateType.InHand:
                    transform.localScale /= 3f; //уменьшаем на 3 масштаб карты
                    transform.position -= new Vector3(0f, 2f, 100f); //новая позиция (изначальная в руке)
                    break;
                case CardStateType.OnTable:
                    //прописать, чтобы карты бросались на нужные координаты стола
                    //уменьшаем при наведении
                    //возвращаем на изначальную позицию стола
                    break;
            }
            //CamerMove.CamerMoveToPlayer2();
        }

        [ContextMenu("Switch Visual")] //визуализировать переключатель

        public void SwitchVisual() //метод визуализации переключения
        {
            IsEnable = !IsEnable; //разрешение ставим в состояние запрета
        }
    }
}

