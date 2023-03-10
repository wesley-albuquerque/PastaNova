using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Grupo6.Logistcs.Conta.SharedClass.Extensions

{

    public partial class RequestToken
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        
        public long ExpiresIn { get; set; }

        [JsonProperty("ext_expires_in")]
        
        public long ExtExpiresIn { get; set; }

        [JsonProperty("expires_on")]
        
        public long ExpiresOn { get; set; }

        [JsonProperty("not_before")]
   
        public long NotBefore { get; set; }

        [JsonProperty("resource")]
        public Uri Resource { get; set; }

        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

    }

    public partial class RequestOpp
    {
        [JsonProperty("@odata.context")]
        public Uri OdataContext { get; set; }

        [JsonProperty("value")]
        public List<Value> Value { get; set; }
    }

    public partial class Value
    {
        [JsonProperty("@odata.etag")]
        public string OdataEtag { get; set; }

        [JsonProperty("gp6_codopp")]
        public string Gp6Codopp { get; set; }

        [JsonProperty("opportunityid")]
        public Guid Opportunityid { get; set; }
    }


    //public partial class RequestToken
    //{
    //    public static RequestToken FromJson(string json) => JsonConvert.DeserializeObject<RequestToken>(json, Grupo6.Logistcs.Conta.SharedClass.Extensions.Converter.Settings);
    //}

    //public static class Serialize
    //{
    //    public static string ToJson(this RequestToken self) => JsonConvert.SerializeObject(self, Grupo6.Logistcs.Conta.SharedClass.Extensions.Converter.Settings);
    //}

    //internal static class Converter
    //{
    //    public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
    //    {
    //        MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
    //        DateParseHandling = DateParseHandling.None,
    //        Converters =
    //        {
    //            new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
    //        },
    //    };
    //}

    //internal class ParseStringConverter : JsonConverter
    //{
    //    public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

    //    public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
    //    {
    //        if (reader.TokenType == JsonToken.Null) return null;
    //        var value = serializer.Deserialize<string>(reader);
    //        long l;
    //        if (Int64.TryParse(value, out l))
    //        {
    //            return l;
    //        }
    //        throw new Exception("Cannot unmarshal type long");
    //    }

    //    public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
    //    {
    //        if (untypedValue == null)
    //        {
    //            serializer.Serialize(writer, null);
    //            return;
    //        }
    //        var value = (long)untypedValue;
    //        serializer.Serialize(writer, value.ToString());
    //        return;
    //    }

    //    public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    //}
}


