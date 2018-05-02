﻿using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HaloSharp.Extension
{
    internal static class StringExtensions
    {
        public static Task<T> ParsedAsJson<T>(this string json)
        {
            var settings = new JsonSerializerSettings();
            return Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(json, settings));
        }
    }
}