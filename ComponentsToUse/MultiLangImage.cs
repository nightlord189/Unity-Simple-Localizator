/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace SimpleLocalizator
{
    [System.Serializable]
    public struct SwitchSprite
    {
        public Language lang;
        public Sprite sprite;

        public SwitchSprite(Language l, Sprite spr)
        {
            this.lang = l;
            this.sprite = spr;
        }
    }

    [RequireComponent(typeof(Image))]
    public class MultiLangImage : MonoBehaviour
    {
        #region Unity scene settings
        [SerializeField] List<SwitchSprite> translations = new List<SwitchSprite>();
        #endregion

        #region Data
        Image _source;

        public Image source {
            get {
                if (_source == null)
                    _source = GetComponent<Image>();
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
                Refresh(GetCurrentSprite());
            }
        }
        #endregion

        #region Methods
        private void Reset()
        {
            if (translations == null || translations.Count <= 0)
            {
                if (translations == null)
                    translations = new List<SwitchSprite>();
                else translations.Clear();
                Language[] langs = (Language[])Enum.GetValues(typeof(Language));
                for (int i = 0; i < langs.Length; i++)
                {
                    translations.Add(new SwitchSprite(langs[i], null));
                }
            }
        }

        Sprite GetCurrentSprite()
        {
            for (int i = 0; i < translations.Count; i++)
            {
                if (translations[i].lang == currentLanguage)
                {
                    return translations[i].sprite;
                }
            }
            return null;
        }

        protected virtual void Refresh(Sprite sprite)
        {
            source.sprite = sprite;
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