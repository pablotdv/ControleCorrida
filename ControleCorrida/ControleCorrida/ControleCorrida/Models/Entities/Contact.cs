﻿using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleCorrida.Models.Entities
{
    [DataTable("Contacts")]
    public class Contact
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [Version]
        public string Version { get; set; }
    }
}
