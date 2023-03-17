using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

using UnityEditor;

using UnityEngine;

namespace Cards
{
    public static class CardUtility
    {
        private const string c_ConfigPath = "//Cards//Resources//CommonCardDescription.xml";
        private const string c_ConfigMage = "//Cards//Resources//MageCardDescription.xml";
        private const string c_ConfigWarrior = "//Cards//Resources//WarriorCardDescription.xml";

        private static readonly Dictionary<uint, string> _descriptions = new Dictionary<uint, string>();
        private static readonly List<uint> _uncollectibleIds = new List<uint>();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Configuration()
        {
            var id = 0;
            try
            {
                var root = XDocument.Load(Application.dataPath + c_ConfigPath).Root;
                id = 1;
                var mage = XDocument.Load(Application.dataPath + c_ConfigMage).Root;
                id = 2;
                var warrior = XDocument.Load(Application.dataPath + c_ConfigWarrior).Root;
                id = 3;
                
                ConfigurationDescriptions(root, mage, warrior);  
            }
            //Обработка исключения
            catch (Exception e)
            {
                Debug.LogError($"Конфигурационный данные содержат ошибку. Парсинг остановился с идентификатором :{id}");

#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#endif
                throw e;
            }
        }

        private static void ConfigurationDescriptions(XElement root, XElement mage, XElement warrior)
        {
            foreach (var packs in root.Elements("Pack"))
            {
                //Проходка по всем юнитам в паке
                foreach (var unit in packs.Elements("Unit"))
                {
                    var id = (uint)unit.Attribute("Id");
                    var description = unit.Value.Replace("/#", "</b>").Replace("#", "<b>");

                    _descriptions.Add(id, description);

                    var uncollect = unit.Attribute("Uncollectible");
                    if (uncollect != null && uncollect.Value.ToLower() == "true") _uncollectibleIds.Add(id);//todo УДАЛИТЬ ПЕРЕД ВЫДАЧЕЙ СТУДЕНТАМ  
                }
            }
            foreach (var packs in mage.Elements("Pack"))
            {
                //Проходка по всем юнитам в паке
                foreach (var unit in packs.Elements("Unit"))
                {
                    var id = (uint)unit.Attribute("Id");
                    var description = unit.Value.Replace("/#", "</b>").Replace("#", "<b>");

                    _descriptions.Add(id, description);

                    var uncollect = unit.Attribute("Uncollectible");
                    if (uncollect != null && uncollect.Value.ToLower() == "true") _uncollectibleIds.Add(id);//todo УДАЛИТЬ ПЕРЕД ВЫДАЧЕЙ СТУДЕНТАМ  
                }
            }
            foreach (var packs in warrior.Elements("Pack"))
            {
                //Проходка по всем юнитам в паке
                foreach (var unit in packs.Elements("Unit"))
                {
                    var id = (uint)unit.Attribute("Id");
                    var description = unit.Value.Replace("/#", "</b>").Replace("#", "<b>");

                    _descriptions.Add(id, description);

                    var uncollect = unit.Attribute("Uncollectible");
                    if (uncollect != null && uncollect.Value.ToLower() == "true") _uncollectibleIds.Add(id);//todo УДАЛИТЬ ПЕРЕД ВЫДАЧЕЙ СТУДЕНТАМ  
                }
            }
        }


        /// <summary>
        /// Возвращает описание карты по ее идентификаторы
        /// </summary>
        /// <param name="id">Идентификатор карты</param>
        /// <returns>Описание карты</returns>
        public static string GetDescriptionById(uint id)
        {
#if UNITY_EDITOR
            if (!_descriptions.ContainsKey(id)) Debug.LogError($" XML-документ не содержит ключ {id}");
#endif
            return _descriptions[id];
        }

        public static bool CheckUncollectible(uint id)
        {
#if UNITY_EDITOR
            if (!_descriptions.ContainsKey(id)) Debug.LogError($" XML-документ не содержит ключ {id}");
#endif
            return _uncollectibleIds.Contains(id);
        }
    }
}
