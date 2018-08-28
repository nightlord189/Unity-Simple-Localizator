/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using UnityEngine;

namespace SimpleLocalizator
{
    [RequireComponent(typeof(TextMesh))]
    public class MultiLangSimpleTextMesh : MultiLangSimpleTextBase
    {
        TextMesh _text;

        TextMesh text {
            get {
                if (_text == null)
                    _text = GetComponent<TextMesh>();
                return _text;
            }
        }

        protected override void RefreshString(string str)
        {
            text.text = str;
        }
    }
}