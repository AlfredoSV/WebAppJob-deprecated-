using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DtoResponse<T>
    {
        public T? Data { set; get; }
        public int Count { get; set; }
    }
}
