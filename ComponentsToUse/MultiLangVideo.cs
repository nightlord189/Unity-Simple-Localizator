/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Video;

namespace SimpleLocalizator
{
    [System.Serializable]
    public class SwitchVideo
    {
        public Language lang;
        public VideoClip clip;
        public string url;

        public SwitchVideo(Language l, VideoClip cl)
        {
            this.lang = l;
            this.clip = cl;
            this.url = string.Empty;
        }
    }

    [RequireComponent(typeof(VideoPlayer))]
    public class MultiLangVideo : MonoBehaviour
    {
        #region Unity scene settings
        [SerializeField] List<SwitchVideo> translations = new List<SwitchVideo>();
        #endregion

        #region Data
        VideoPlayer _source;

        public VideoPlayer source {
            get {
                if (_source == null)
                    _source = GetComponent<VideoPlayer>();
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
                Refresh(GetCurrentVideo());
            }
        }
        #endregion

        #region Methods
        private void Reset()
        {
            if (translations == null || translations.Count <= 0)
            {
                if (translations == null)
                    translations = new List<SwitchVideo>();
                else translations.Clear();
                Language[] langs = (Language[])Enum.GetValues(typeof(Language));
                for (int i = 0; i < langs.Length; i++)
                {
                    translations.Add(new SwitchVideo(langs[i], null));
                }
            }
        }

        SwitchVideo GetCurrentVideo()
        {
            for (int i = 0; i < translations.Count; i++)
            {
                if (translations[i].lang == currentLanguage)
                {
                    return translations[i];
                }
            }
            return null;
        }

        protected virtual void Refresh(SwitchVideo video)
        {
            source.source = video.clip != null ? VideoSource.VideoClip : VideoSource.Url;
            source.clip = video.clip;
            source.url = video.url;
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