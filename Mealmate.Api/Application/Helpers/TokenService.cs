using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mealmate.Api.Helpers
{
    public class TokenService
    {
        public static JToken CreateJToken(object obj, string props)
        {
            string _serializedTracks = JsonConvert.SerializeObject(obj, Formatting.None,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            JToken jtoken = JToken.Parse(_serializedTracks);
            if (!string.IsNullOrEmpty(props))
                Utils.FilterProperties(jtoken, props.ToLower().Split(',').ToList());

            return jtoken;
        }
    }
}
