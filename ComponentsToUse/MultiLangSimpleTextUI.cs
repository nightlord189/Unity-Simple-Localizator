/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using UnityEngine;
using UnityEngine.UI;

namespace SimpleLocalizator
{
    [RequireComponent(typeof(Text))]
    public class MultiLangSimpleTextUI : MultiLangSimpleTextBase
    {
        Text _text;

        public Text text {
            get {
                if (_text == null)
                    _text = GetComponent<Text>();
                return _text;
            }
        }

        protected override void RefreshString(string str)
        {
            text.text = str;
        }
    }
}