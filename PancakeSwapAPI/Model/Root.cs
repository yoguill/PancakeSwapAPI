using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PancakeSwapAPI.Model
{
    public class Root
    {
        public long updated_at { get; set; }
        public Data data { get; set; }
    }
}
