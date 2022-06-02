using System.Collections.Generic;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace HttpClientConsole
{
    public class GetHttpError
    {
        public static Dictionary<string, List<string>> ExeractErrorFromWebApiResponse(string body)
        {
            var response = new Dictionary<string, List<string>>();
            var deserilze = JsonSerializer.Deserialize<JsonElement>(body);
            var errorElementJson = deserilze.GetProperty("errors");
            foreach (var item in errorElementJson.EnumerateObject())
            {
                var field = item.Name;
                var errors = new List<string>();
                foreach (var item2 in item.Value.EnumerateArray())
                {
                    var error = item2.GetString();
                    errors.Add(error);
                }
                response.Add(field, errors);
            }

            return response;
        }
    }
}