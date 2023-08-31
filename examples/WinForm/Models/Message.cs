using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fuxion.Desktop.Models
{
    public class Messages
    {
        public Messages New(string id, string msg)
        {
            this.user_id = id;
            this.message = msg;
            return this;
        }

        public string user_id { get; set; }
        public string message  { get; set; }
    }
}
