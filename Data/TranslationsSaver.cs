/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using System;

namespace SimpleLocalizator {
	public static class TranslationsSaver{
		#region Data
		static string fileName = "translations.csv";
		static string filePath {
			get {
				return Path.Combine (Path.Combine(Application.dataPath, "Resources"), fileName);
			}
		}
		#endregion

		#region Methods
		static List<Language> ParseHeader(string header) {
			List<Language> result = new List<Language> ();
		    string[] strings = SplitCSVString(header);
			object current = null;
			for (int i = 1; i < strings.Length; i++) {
				try {
					current = Enum.Parse (typeof(Language), strings [i], true);
					if (current != null) {
						result.Add ((Language)current);
					}
				}
				catch {}
			}
			return result;
		}

		static string[] SplitCSVString(string str, int count=0) {
			char[] delimiter = { ';' };
			string[] result = count>0 ? str.Split (delimiter, count) : str.Split(delimiter);
			for (int i = 0; i < result.Length; i++) {
				if (result[i].Contains(";"))
					result[i]=result[i].Replace(";", string.Empty);
			}
			return result;
		}

		static Label ParseLabel(string str, List<Language> langs) {
			string[] strings = SplitCSVString (str, langs.Count + 1);
			int id = 0;
			if (Int32.TryParse(strings[0], out id)) {
				Label result = new Label (id);
				int langCounter = 0;
				for (int i = 1; i < strings.Length; i++) {
					result.Set (langs [langCounter], strings [i]);
					langCounter++;
					if (langCounter >= langs.Count)
						break;
				}
				//Debug.Log ("Parse label: success. Label:" + result.ToString ());
				return result;
			}
			return null;
		}

	    static List<Language> GetUsedLangs(List<Label> labels)
	    {
	        List<Language> res = new List<Language>();
	        for (int i = 0; i < labels.Count; i++)
	        {
	            var keys = labels[i].Keys();
	            for (int j = 0; j < keys.Count; j++)
	            {
	                if (!res.Contains(keys[j]))
                        res.Add(keys[j]);
	            }
	        }
	        return res;
	    }

		static StringBuilder ListToCSV(List<Label> labels) {
			StringBuilder result = new StringBuilder ();
		    var langs = GetUsedLangs(labels);
			string current = "id;";
			for (int i = 0; i < langs.Count; i++) {
				current += langs[i].ToString().ToLower() + ";"; //adding header
			}
			result.AppendLine (current);
		    if (labels != null)
		    {
		        for (int i = 0; i < labels.Count; i++)
		        {
		            current = labels[i].id.ToString() + ";";
		            for (int j = 0; j < langs.Count; j++)
		            {
		                current += labels[i].Get(langs[j]) + ";"; //adding fields
		            }
		            //Debug.Log (labels[i].ToString());
		            result.AppendLine(current);
		        }
            }
			return result;
		}

		static List<Label> StringToList(string str) {
			StringReader r = new StringReader (str);
			int linesCount = str.LinesCount ();
			string currentStr = r.ReadLine ();
			List<Language> langs = ParseHeader (currentStr);
			List<Label> result = new List<Label> ();
			Label currentLabel=null;
			for (int i = 1; i < linesCount; i++) {
				currentStr = r.ReadLine ();
				if (currentStr != null) {
					currentLabel = ParseLabel (currentStr, langs);
					if (currentLabel != null)
						result.Add (currentLabel);
				}
			}
			return result;
		}
		#endregion

		#region Interface
	    public static void SaveNewCSV(Encoding encoding)
	    {
	        if (!File.Exists(filePath))
	        {
	            SaveToCSV(null, encoding);
	        }
	    }

		public static void SaveToCSV(List<Label> labels, Encoding encoding) {
			try {
				#if !UNITY_WEBPLAYER
				File.WriteAllText(filePath, ListToCSV (labels).ToString(), encoding);
				Debug.Log("Translation Saver: csv saved successfully!");
				#else 
				Debug.LogWarning("Translation Saver: cannot save csv on WebPlayer!");
				#endif
			}
			catch (Exception ex) {
				Debug.LogError("Translation Saver: can't save csv file:"+ex.ToString());
			}
		}

		public static List<Label> LoadFromCSV() {
			try {
				#if !UNITY_WEBPLAYER
				string str = File.ReadAllText (filePath);
				List<Label> result = StringToList (str);
				Debug.Log("Translation Saver: csv loaded successfully!");
				return result;
				#else
				Debug.LogWarning("Translation Saver: cannot read csv on WebPlayer!");
				return new List<Label>();
				#endif
			}
			catch (Exception ex) {
				Debug.LogError("Translation Saver: can't read csv file:"+ex.ToString());
				return new List<Label> ();
			}
		}
		#endregion
	}
}
#endif