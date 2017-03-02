using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCorrida.Model
{
    [DataTable("Atividade")]
    public class Atividade
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Data")]
        public DateTime Data { get; set; }

        [JsonProperty("Voltas")]
        public decimal Voltas { get; set; }

        [JsonProperty("Tempo")]
        public DateTime Tempo { get; set; }

        [Version]
        public string Version { get; set; }

        //public TimeSpan Media
        //{
        //    get
        //    {
        //        return new TimeSpan((long)(Tempo.Ticks / Voltas));
        //    }
        //}
    }
}
