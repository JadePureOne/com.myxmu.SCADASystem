using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// TulingRead.xlsx 表结构
    /// </summary>
    public class ReadEntity
    {
        public string Module { get; set; }
        public string Cn { get; set; }
        public string En { get; set; }
        public string Address { get; set; }
        public bool Save { get; set; }
    }
}
