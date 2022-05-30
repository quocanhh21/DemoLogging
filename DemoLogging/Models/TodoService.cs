using NLog;
using NLog.Web;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoLogging.Models
{
    public class TodoService
    {
        public async Task<TodoModel> GetTodo(int todoId)
        {
            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
            logger.Debug("Start method GetTodo in TodoService class");
            Stopwatch timer = new Stopwatch();
            timer.Start();
            string urlApi = ApiHelper.UrlFromConfig();
            string url = "";

            if (todoId > 0)
            {
                logger.Debug("Id todo is valid");
                url = $"{urlApi}/todos/{todoId}";
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
                    logger.Debug($"Finish method GetTodo in TodoService class. Run time: {timer.Elapsed} ");
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
