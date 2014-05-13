using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;

namespace auto_mechanic.Models
{
    public class Tools
    {
        public static HttpResponseMessage JsonResponse(object obj)
        {

            string res = JsonSerialize(obj);

            return new HttpResponseMessage()
            {
                Content = new StringContent(res),
            };
        }

        public static String JsonSerialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.None,
                 NullValueHandling = NullValueHandling.Ignore,
                 MissingMemberHandling = MissingMemberHandling.Ignore,
                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore

            });
        }
    }
}