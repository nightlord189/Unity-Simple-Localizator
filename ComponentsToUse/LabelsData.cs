/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

using System.Collections.Generic;
using UnityEngine;
using System.Text;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SimpleLocalizator {
	#if UNITY_EDITOR
	[InitializeOnLoad]
	#endif
	[CreateAssetMenu(fileName="LabelsData", menuName="SimpleLocalizator/LabelsData")]
	public class LabelsData : ScriptableObject {
		#if UNITY_EDITOR
		public enum EncodingType {UTF8, ANSI};
		[SerializeField] EncodingType encoding;
		#endif
		[SerializeField] List<Label> _labels=new List<Label>();
		Label _defaultLabel = new Label (-1);

		public static Label defaultLabel {
			get {
				return instance._defaultLabel;
			}
		}

		public static List<Label> labels {
			get {
				return instance._labels;
			}
			private set {
				instance._labels = value;
			}
		}

		#if UNITY_EDITOR
		static LabelsData() {
			//InitInstance ();
		}

        public void Load() {
			labels = TranslationsSaver.LoadFromCSV ();
			MultiLangTextBase[] texts = FindObjectsOfType<MultiLangTextBase> ();
			for (int i = 0; i < texts.Length; i++) {
				texts [i].OnValidate ();
			}
		}

		public void Save() {
			TranslationsSaver.SaveToCSV (labels, encoding == EncodingType.UTF8 ? Encoding.UTF8 : Encoding.Default);
		}

		public void ClearLabels() {
			labels.Clear ();
		}

        void OnValidate()
        {
            for (int i=0; i<labels.Count; i++)
            {
                labels[i].InitTranslations();
            }
	        for (int i = 0; i < labels.Count; i++)
	        {
		        if (i > 0 && labels[i].id<=labels[i-1].id)
		        {
			        labels[i].id = labels[i - 1].id + 1;
		        }
	        }
        }
		#endif

		static LabelsData _instance;
		public static LabelsData instance {
			get {
				InitInstance();
				return _instance;
			}
		}

		private static void InitInstance() {
			if (_instance==null) {
				_instance = (LabelsData)Resources.Load ("LabelsData");
				if (_instance == null) {
					_instance = CreateInstance<LabelsData> ();
					#if UNITY_EDITOR
					Extensions.WriteAsset(_instance);
					CreateDefaultLabels();
					#endif
					Debug.Log ("LabelsData: loaded instance from resources is null, created instance");
				}
			}
		}

		#if UNITY_EDITOR
		private static void CreateDefaultLabels() {
			if (_instance._labels.Count==0) {
				Label label = new Label(0);
				label.Set(Language.English, "Switch language");
				label.Set(Language.Russian, "Переключить язык");
				_instance._labels.Add(label);
				label = new Label(1);
				label.Set(Language.English, "Test text on English language");
				label.Set(Language.Russian, "Тестовый текст на русском языке");
				_instance._labels.Add(label);
			}
		}
		#endif
	}

	#if UNITY_EDITOR
	[CustomEditor(typeof(LabelsData))]
	public class LabelsDataEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			LabelsData myScript = (LabelsData)target;
			if(GUILayout.Button("Load from CSV"))
			{
				myScript.Load ();
			}
			if(GUILayout.Button("Save to CSV"))
			{
				myScript.Save ();
			}
			if(GUILayout.Button("Clear labels"))
			{
				myScript.ClearLabels ();
			}
			EditorUtility.SetDirty (target);
		}
	}
	#endif
}