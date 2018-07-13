/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleLocalizator
{
    [System.Serializable]
    public struct SwitchMesh
    {
        public Language lang;
        public Mesh mesh;

        public SwitchMesh(Language l, Mesh m)
        {
            this.lang = l;
            this.mesh = m;
        }
    }

    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class MultiLangMesh : MonoBehaviour
    {
        #region Unity scene settings
        [SerializeField] List<SwitchMesh> translations = new List<SwitchMesh>();
        #endregion

        #region Data
        MeshFilter _meshFilter;

        MeshFilter meshFilter {
            get {
                if (_meshFilter == null)
                    _meshFilter = GetComponent<MeshFilter>();
                return _meshFilter;
            }
        }

        [SerializeField] Language _currentLanguage = Language.Russian;

        Language currentLanguage {
            get {
                return _currentLanguage;
            }
            set {
                _currentLanguage = value;
                Refresh(GetCurrentMesh());
            }
        }
        #endregion

        #region Methods
        private void Reset()
        {
            if (translations == null || translations.Count <= 0)
            {
                if (translations == null)
                    translations = new List<SwitchMesh>();
                else translations.Clear();
                Language[] langs = (Language[])Enum.GetValues(typeof(Language));
                for (int i = 0; i < langs.Length; i++)
                {
                    translations.Add(new SwitchMesh(langs[i], null));
                }
            }
        }

        Mesh GetCurrentMesh()
        {
            for (int i = 0; i < translations.Count; i++)
            {
                if (translations[i].lang == currentLanguage)
                {
                    return translations[i].mesh;
                }
            }
            return null;
        }

        protected virtual void Refresh(Mesh m)
        {
            meshFilter.mesh = m;
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