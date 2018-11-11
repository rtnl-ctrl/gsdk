﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Playfab.Gaming.GSDK.CSharp
{
    class HeartbeatRequest
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public GameState CurrentGameState { get; set; }
        public string CurrentGameHealth { get; set; }
        public ConnectedPlayer[] CurrentPlayers { get; set; }
    }

    class HeartbeatResponse
    {
        [JsonProperty(PropertyName = "sessionConfig")]
        public IDictionary<string, string> SessionConfig { get; set; }

        [JsonProperty(PropertyName = "nextScheduledMaintenanceUtc")]
        public string NextScheduledMaintenanceUtc { get; set; }

        [JsonProperty(PropertyName = "operation", ItemConverterType = typeof(StringEnumConverter))]
        public GameOperation Operation { get; set; }
    }

    interface IHttpClient
    {
        Task<HeartbeatResponse> SendHeartbeatAsync(HeartbeatRequest request);
    }

    static class HttpClientFactory
    {
        public static IHttpClient Instance { get; set; }

        public static IHttpClient CreateInstance(string baseUrl)
        {
            if (Instance == null)
            {
                Instance = new HttpClientProxy(baseUrl);
            }

            return Instance;
        }
    }
}
