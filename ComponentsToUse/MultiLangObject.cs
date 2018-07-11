/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using System.Collections.Generic;
using UnityEngine;
using System;

namespace SimpleLocalizator {
	public class MultiLangObject : MultiLangComponent {
		[System.Serializable]
		public struct SwitchObject {
			public Language lang;
			public GameObject obj;

			public SwitchObject (Language lang, GameObject obj) {
				this.lang=lang;
				this.obj=obj;
			}
		}

		[SerializeField] List<SwitchObject> translations = new List<SwitchObject>();

		protected override void Refresh() {
			for (int i = 0; i < translations.Count; i++) {
				if (translations[i].obj!=null)
					translations [i].obj.SetActive(translations[i].lang == currentLanguage);
			}
		}


		private void Reset()
		{
			if (translations == null || translations.Count <= 0)
			{
				if (translations == null)
					translations = new List<SwitchObject>();
				else translations.Clear();
				Language[] langs = (Language[])Enum.GetValues(typeof(Language));
				for (int i = 0; i < langs.Length; i++)
				{
					translations.Add(new SwitchObject(langs[i], null));
				}
			}
		}
	}
}