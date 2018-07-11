/*SimpleLocalizator plugin
 * Developed by NightLord189 (nldevelop.com)*/

namespace SimpleLocalizator {
	[System.Serializable]
	public struct Translation {
		public Language key;
		public string value;

        public Translation(Language key)
        {
            this.key = key;
            this.value = string.Empty;
        }

		public Translation(Language key, string value) {
			this.key = key;
			this.value = value;
		}

		public override string ToString() {
			return (key.ToString () + ":" + value);
		}
	}
}