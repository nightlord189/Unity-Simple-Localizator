/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using System.Collections.Generic;
using UnityEngine;
using System;

namespace SimpleLocalizator {
	public class LanguageManager : MonoBehaviour {
		#region Data
		[SerializeField] bool autoDetectLanguage=true;
		[SerializeField] Language defaultLanguage = Language.Russian;
		[SerializeField] Language _currentLang;
		static string _loc = string.Empty;
		static LanguageManager _instance;
		static LanguageManager instance {
			get {
				if (_instance == null) {
					_instance = FindObjectOfType<LanguageManager> ();
					if (_instance == null) {
						GameObject obj = new GameObject ("LanguageManager");
						DontDestroyOnLoad (obj);
						_instance = obj.AddComponent<LanguageManager> ();
					}
				}
				return _instance;
			}
			set {
				_instance = value;
			}
		}
		static List<Label> labels {
			get {
				return LabelsData.labels;
			}
		}

        static bool initLang = false;
		#endregion

		#region Interface
		public static string loc {
			get {
				return _loc;
			}
		}

		public static Language currentLang {
			get {
				return instance._currentLang;
			}
			set {
                Debug.Log("LanguageManager: language " + value);
				instance._currentLang = value;
                initLang = true;
				if (onLanguageChanged != null)
					onLanguageChanged ();
			}
		}

		public static Action onLanguageChanged;

		public static Label GetLabel (int labelID) {
			for (int i = 0; i < labels.Count; i++) {
				if (labels [i].id == labelID) {
					return labels [i];
				}
			}
			return LabelsData.defaultLabel;
		}

		public static string GetString(int labelID) {
			for (int i = 0; i < labels.Count; i++) {
				if (labels [i].id == labelID) {
					return labels [i].Get (currentLang);
				}
			}
			return LabelsData.defaultLabel.Get (currentLang);
		}

        public static string GetString(int labelID, Language lang)
        {
            for (int i = 0; i < labels.Count; i++)
            {
                if (labels[i].id == labelID)
                {
                    return labels[i].Get(lang);
                }
            }
            return LabelsData.defaultLabel.Get(lang);
        }

        public static Language GetSystemLanguage() {
			switch (Application.systemLanguage) {
			case SystemLanguage.Ukrainian:
				return Language.Russian;
			case SystemLanguage.Belarusian:
				return Language.Russian;
			case SystemLanguage.Russian:
				return Language.Russian;
			case SystemLanguage.English:
				return Language.English;
			default:
				return instance.defaultLanguage;
			}
		}
		#endregion

		#region Methods
		void OnEnable() {
			if (_instance == null) {
				instance = this;
			} else if (instance != this) {
				Destroy (this);
			}
		}

		void Start() {
			if (autoDetectLanguage && !initLang) {
				currentLang = GetSystemLanguage ();
			} else {
				currentLang = currentLang;
			}
			Debug.Log ("LanguageManager: initialized. Current language: " + currentLang);
		}
		#endregion
	}
}