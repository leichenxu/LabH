using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZOR
{
    class Setting
    {
        /// <summary>
        /// Name of setting.
        /// </summary>
        private string name;
        /// <summary>
        /// Main Urls.
        /// </summary>
        private string[] mainUrl;
        /// <summary>
        /// Secondary urls.
        /// </summary>
        private string[] secondaryUrl;
        /// <summary>
        /// Default constructor for json.
        /// </summary>
        public Setting()
        {
        }
        public Setting(string name, string[] mainUrl, string[] secondaryUrl)
        {
            this.name = name;
            this.mainUrl = mainUrl;
            this.secondaryUrl = secondaryUrl;
        }

        [JsonProperty("name")]
        public string Name { get => name; set => name = value; }
        [JsonProperty("main_url")]
        public string[] MainUrl { get => mainUrl; set => mainUrl = value; }
        [JsonProperty("secondary_url")]
        public string[] SecondaryUrl { get => secondaryUrl; set => secondaryUrl = value; }
        /// <summary>
        /// Override toString for show name only.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return name;
        }
    }
}
