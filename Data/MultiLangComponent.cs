/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using UnityEngine;


namespace SimpleLocalizator {
	public class MultiLangComponent : MonoBehaviour {
		[SerializeField] Language _currentLanguage=Language.Russian;

		protected Language currentLanguage {
			get {
				return _currentLanguage;
			}
			set {
				_currentLanguage = value;
				Refresh ();
			}
		}

		public void OnValidate() {
			currentLanguage = _currentLanguage;
		}

		void OnEnable() {
			OnLanguageRefresh ();
			LanguageManager.onLanguageChanged += OnLanguageRefresh;
		}

		void OnDisable() {
			LanguageManager.onLanguageChanged -= OnLanguageRefresh;
		}

		void OnLanguageRefresh() {
			currentLanguage = LanguageManager.currentLang;
		}

		protected virtual void Refresh() {
		}
	}
}