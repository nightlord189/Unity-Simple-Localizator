/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using UnityEngine;
using System.Collections.Generic;
using System;

namespace SimpleLocalizator {
	[System.Serializable]
	public class Label{
		#region Data
		[SerializeField] int _id;
	    [SerializeField] List<Translation> translations = new List<Translation>();
	    private const string defaultText = "not translated ";
        #endregion

        #region Interface
        public int id {
			get {
				return _id;
			}
			set {
				_id = value;
			}
		}

		public Label(int id) {
			this.id = id;
            InitTranslations();
		}

        public Label()
        {
            InitTranslations();
        }

        public void InitTranslations()
        {
            if (translations == null || translations.Count <= 0)
            {
                if (translations == null)
                    translations = new List<Translation>();
                else translations.Clear();
                Language[] langs = (Language[])Enum.GetValues(typeof(Language));
                for (int i = 0; i < langs.Length; i++)
                {
                    translations.Add(new Translation(langs[i], string.Empty));
                }
            }
        }

		public string Get(Language language) {
		    for (int i = 0; i < translations.Count; i++)
		    {
		        if (translations[i].key == language)
		        {
		            return translations[i].value;
		        }
		    }
		    translations.Add(new Translation(language, defaultText));
		    return translations[translations.Count - 1].value;
		}

		public void Set(Language language, string str) {
		    for (int i = 0; i < translations.Count; i++)
		    {
		        if (translations[i].key == language)
		        {
		            translations[i] = new Translation(language, str);
		            return;
		        }
		    }
		    translations.Add(new Translation(language, str));
		}

	    public List<Language> Keys()
	    {
	        List<Language> keys = new List<Language>();
	        for (int i = 0; i < translations.Count; i++)
	        {
	            if (!keys.Contains(translations[i].key))
                    keys.Add(translations[i].key);
	        }
	        return keys;
	    }

        public Label CopyWithOtherId(int newId)
        {
            Label result = new Label(newId);
            for (int i=0; i<translations.Count; i++)
            {
                result.Set(translations[i].key, translations[i].value);
            }
            return result;
        }
		#endregion

		#region Methods
		public override string ToString ()
		{
			return "Label (id:" + id.ToString () + ")."+translations.ToString();
		}
		#endregion
	}
}