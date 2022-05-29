using NLog;
using NLog.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoLogging.Models
{
    public class TodoService
    {
        public static async Task<TodoModel> LoadTodo(int? todoNumber)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            string urlApi = ApiHelper.UrlFromConfig();
            string url = "";

            if (todoNumber>0)
            {
                logger.Debug("Id todo is valid");
                url = $"{urlApi}/todos/{todoNumber}";
            }
            else
            {
                url = $"{urlApi}/todos";
            }

            HttpClient client = new HttpClient();
            using (HttpResponseMessage response = await client.GetAsync(url))
            {
                TodoModel todo = null;
                if (response.IsSuccessStatusCode)
                {
                    todo = await response.Content.ReadAsAsync<TodoModel>();
                    return todo;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
