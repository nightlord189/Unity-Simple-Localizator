/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using System.Collections.Generic;
using UnityEngine;
using System;

namespace SimpleLocalizator
{
    [System.Serializable]
    public struct SwitchText
    {
        public Language lang;
        [TextArea(3, 10)]
        public string text;

        public SwitchText(Language l, string s) 
        {
            this.lang = l;
            this.text = s;
        }
    }

    public class MultiLangSimpleTextBase : MonoBehaviour
    {
        #region Unity scene settings
        [SerializeField] List<SwitchText> translations = new List<SwitchText>();
        #endregion

        #region Data
        [SerializeField] Language _currentLanguage=Language.Russian;

        Language currentLanguage {
            get {
                return _currentLanguage;
            }
            set {
                //oldLanguage = currentLanguage;
                _currentLanguage = value;
                RefreshString(GetCurrentText());
            }
        }
        #endregion

        #region Methods
        private void Reset()
        {
            if (translations==null || translations.Count<=0)
            {
                if (translations == null)
                    translations = new List<SwitchText>();
                else translations.Clear();
                Language[] langs = (Language[])Enum.GetValues(typeof(Language));
                for (int i = 0; i < langs.Length; i++)
                {
                    translations.Add(new SwitchText(langs[i], "not translated"));
                }
            }
        }

        string GetCurrentText()
        {
            for (int i=0; i<translations.Count; i++)
            {
                if (translations[i].lang==currentLanguage)
                {
                    return translations[i].text;
                }
            }
            return "not translated";
        }

        protected virtual void RefreshString(string str)
        {
            
        }

        public void OnValidate()
        {
            currentLanguage = _currentLanguage;
        }

        void OnEnable()
        {
            OnLanguageRefresh();
            LanguageManager.onLanguageChanged += OnLanguageRefresh;
        }

        void OnDisable()
        {
            LanguageManager.onLanguageChanged -= OnLanguageRefresh;
        }

        void OnLanguageRefresh()
        {
            currentLanguage = LanguageManager.currentLang;
        }
        #endregion
    }
}