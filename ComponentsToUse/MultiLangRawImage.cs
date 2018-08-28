/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace SimpleLocalizator
{
    [RequireComponent(typeof(RawImage))]
    public class MultiLangRawImage : MonoBehaviour
    {
        #region Unity scene settings
        [SerializeField] List<SwitchTexture> translations = new List<SwitchTexture>();
        #endregion

        #region Data
        RawImage _source;

        public RawImage source {
            get {
                if (_source == null)
                    _source = GetComponent<RawImage>();
                return _source;
            }
        }

        [SerializeField] Language _currentLanguage = Language.Russian;

        Language currentLanguage {
            get {
                return _currentLanguage;
            }
            set {
                //oldLanguage = currentLanguage;
                _currentLanguage = value;
                Refresh(GetCurrentTexture());
            }
        }
        #endregion

        #region Methods
        private void Reset()
        {
            if (translations == null || translations.Count <= 0)
            {
                if (translations == null)
                    translations = new List<SwitchTexture>();
                else translations.Clear();
                Language[] langs = (Language[])Enum.GetValues(typeof(Language));
                for (int i = 0; i < langs.Length; i++)
                {
                    translations.Add(new SwitchTexture(langs[i], null));
                }
            }
        }

        Texture GetCurrentTexture()
        {
            for (int i = 0; i < translations.Count; i++)
            {
                if (translations[i].lang == currentLanguage)
                {
                    return translations[i].texture;
                }
            }
            return null;
        }

        protected virtual void Refresh(Texture texture)
        {
            source.texture = texture;
        }

        public void OnValidate()
        {
            if (!Application.isPlaying)
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