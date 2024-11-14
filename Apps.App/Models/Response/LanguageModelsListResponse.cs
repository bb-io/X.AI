﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.X.AI.Models.Response
{
    public class LanguageModelsListResponse
    {
        [JsonProperty("models")]
        public List<LanguageModelResponse> Models { get; set; }
    }  
}
