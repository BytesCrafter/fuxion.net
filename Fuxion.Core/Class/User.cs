using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fuxion.Core.Class
{
    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }

        public string ToJson()
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{");
            jsonBuilder.AppendFormat("\"id\": \"{0}\",", this.id);
            jsonBuilder.AppendFormat("\"name\": \"{0}\",", this.name);
            jsonBuilder.AppendFormat("\"type\": \"{0}\"", this.type);
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
    }
}
