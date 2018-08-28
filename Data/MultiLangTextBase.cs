/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using UnityEngine;

namespace SimpleLocalizator {
	public abstract class MultiLangTextBase : MultiLangComponent{
		#region Unity scene settings
		[SerializeField] int _labelID;
		[SerializeField] bool toUpper=false;
		#endregion

		#region Interface
		public int labelID {
			get {
				return _labelID;
			}
			set {
				_labelID = value;
			    Refresh();
			}
		}

		public void SetLabelId(int id)
		{
			labelID = id;
		}
        #endregion

        #region Methods
        protected override void Refresh()
        {
            bool local = (Application.isEditor && !Application.isPlaying);
            string str = local ? LanguageManager.GetString(labelID, currentLanguage) : LanguageManager.GetString(labelID);
		    if (toUpper)
                str = str.ToUpper();
		    VisualizeString(str);
		}

	    protected abstract void VisualizeString(string str);
	    #endregion
	}
}