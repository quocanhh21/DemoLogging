using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DemoLogging.Models
{
    public class TodoModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool Completed { get; set; }
    }
}
