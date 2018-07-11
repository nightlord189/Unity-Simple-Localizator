/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using UnityEngine;
using UnityEngine.UI;

namespace SimpleLocalizator {
	[RequireComponent(typeof(Text))]
	public class MultiLangTextUI : MultiLangTextBase {
		Text _text;
		public Text text {
			get {
				if (_text == null && gameObject!=null)
					_text = GetComponent<Text> ();
				return _text;
			}
		}

		protected override void VisualizeString(string str) {
			if (text!=null)
				text.text = str;
		}
	}
}