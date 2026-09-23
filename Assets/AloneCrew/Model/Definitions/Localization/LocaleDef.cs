using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace AloneCrew.Model.Definitions.Localization
{
    [CreateAssetMenu(fileName = "LocaleDef", menuName = "Defs/Locale")]
    public class LocaleDef : ScriptableObject
    {
        [SerializeField] private LocaleDefMode  _mode;
        [SerializeField] private TextAsset _tsvFile;
        [SerializeField] private string _url;
        [SerializeField] private List<LocaleItem> _locales;
        
        public Dictionary<string, string> Locales
        {
            get
            {
                var dict = new Dictionary<string, string>();
                foreach (var l in _locales)
                {
                    dict.Add(l.Key, l.Value);
                }

                return dict;
            }
        }

        private UnityWebRequest _request;

        [ContextMenu("Update locale")]
        public void UpdateLocale()
        {
            if (_request != null) return;
            if (_mode == LocaleDefMode.Url)
            {
                _request = UnityWebRequest.Get(_url);
                _request.SendWebRequest().completed += OnDataLoaded;
            }
            else if (_mode == LocaleDefMode.File)
            {
                parseText(_tsvFile.text);
            }
        }

        private void OnDataLoaded(AsyncOperation operation)
        {
            if (operation.isDone)
            {
                parseText(_request.downloadHandler.text);
            }
        }

        private void parseText(string text)
        {
            var rows = text.Split('\n');
            _locales.Clear();
            foreach (var row in rows)
            {
                AddLocalItem(row);
            }
        }

        private void AddLocalItem(string row)
        {
            try
            {
                var parts = row.Split('\t');
                var id = parts[0];
                _locales.Add(new LocaleItem(id, parts[1]));
            }
            catch (Exception e)
            {
                Debug.LogError($"Parse locale error, row={row}.\n  {e}");
            }
        }
        
        public enum LocaleDefMode
        {
            Url,
            File
        }
            
        [Serializable]
        public class LocaleItem
        {
            [SerializeField] private string _key;
            [SerializeField] private string _value;
            
            public string Key => _key;
            public string Value => _value;

            public LocaleItem(string key, string value)
            {
                _key = key;
                _value = value;
            }
        }
    }
}