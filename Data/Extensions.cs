/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using UnityEngine;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

namespace SimpleLocalizator {
	public static class Extensions{
		public static int LinesCount(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return 0;
			int len = str.Length;
			int c = 0;
			for (int i = 0; i < len; i++) {
				if (str [i] == '\n')
					c++;
			}
			return c + 1;
		}

        #if UNITY_EDITOR
        public static void WriteAsset (Object asset)
	    {
			string resPath = Path.Combine (Application.dataPath, "Resources");
			if (!Directory.Exists (resPath)) {
				Directory.CreateDirectory(resPath);
			}
	        string path = "Assets\\Resources";
	        string assetPathAndName = Path.Combine(path, "LabelsData.asset"); 
	        AssetDatabase.CreateAsset(asset, assetPathAndName);
	        AssetDatabase.SaveAssets();
	        AssetDatabase.Refresh();
	        EditorUtility.FocusProjectWindow();
	        Selection.activeObject = asset;
	    }
        #endif
    }
}