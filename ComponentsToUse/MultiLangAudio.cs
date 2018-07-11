/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using System.Collections.Generic;
using UnityEngine;
using System;

namespace SimpleLocalizator
{
    [System.Serializable]
    public struct SwitchAudio
    {
        public Language lang;
        public AudioClip clip;

        public SwitchAudio(Language l, AudioClip cl)
        {
            this.lang = l;
            this.clip = cl;
        }
    }

    [RequireComponent(typeof(AudioSource))]
    public class MultiLangAudio : MonoBehaviour
    {
        #region Unity scene settings
        [SerializeField] List<SwitchAudio> translations = new List<SwitchAudio>();
        #endregion

        #region Data
        AudioSource _source;

        public AudioSource source {
            get {
                if (_source == null)
                    _source = GetComponent<AudioSource>();
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
                Refresh(GetCurrentAudio());
            }
        }
        #endregion

        #region Methods
        private void Reset()
        {
            if (translations == null || translations.Count <= 0)
            {
                if (translations == null)
                    translations = new List<SwitchAudio>();
                else translations.Clear();
                Language[] langs = (Language[])Enum.GetValues(typeof(Language));
                for (int i = 0; i < langs.Length; i++)
                {
                    translations.Add(new SwitchAudio(langs[i], null));
                }
            }
        }

        AudioClip GetCurrentAudio()
        {
            for (int i = 0; i < translations.Count; i++)
            {
                if (translations[i].lang == currentLanguage)
                {
                    return translations[i].clip;
                }
            }
            return null;
        }

        protected virtual void Refresh(AudioClip audio)
        {
            source.clip = audio;
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