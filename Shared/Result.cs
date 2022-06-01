using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models
{
    public class Result<T>
    {
        public T Data { get; set; }

        public string StatusCode { get; set; }

        public string Message { get; set; }
    }
}
