using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CommonException : Exception
    {
        public  string Source { get; }
        public  string Message { get; }

        public CommonException(string message, string source)
        {
            this.Message = message;
            this.Source = source;
            
        }

    }
}
