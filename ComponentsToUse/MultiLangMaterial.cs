/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using System.Collections.Generic;
using UnityEngine;
using System;

namespace SimpleLocalizator
{
    [System.Serializable]
    public struct SwitchTexture
    {
        public Language lang;
        public Texture texture;

        public SwitchTexture(Language l, Texture t)
        {
            this.lang = l;
            this.texture = t;
        }
    }

    [RequireComponent(typeof(Renderer))]
    public class MultiLangMaterial : MonoBehaviour
    {
        #region Unity scene settings
        [SerializeField] List<SwitchTexture> translations = new List<SwitchTexture>();
        [SerializeField] bool setEmission = true;
        #endregion

        #region Data
        Renderer _source;

        public Renderer source {
            get {
                if (_source == null)
                    _source = GetComponent<Renderer>();
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
            source.sharedMaterial.SetTexture("_MainTex", texture);
            if (setEmission)
                source.sharedMaterial.SetTexture("_EmissionMap", texture);
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

        private void Start()
        {
            currentLanguage = Language.Kazakh;
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