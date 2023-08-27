using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fuxion.Core.Class
{
    class Actives
    {
        public List<User> users = new List<User>();
        public string type { get; set; }

        public string ToJson()
        {
            int total = users.Count;
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("[");
            this.users.ForEach((User user) =>
            {
                jsonBuilder.Append("{");
                jsonBuilder.AppendFormat("\"id\": \"{0}\",", user.id);
                jsonBuilder.AppendFormat("\"name\": \"{0}\",", user.name);
                jsonBuilder.AppendFormat("\"type\": \"{0}\"", user.type);

                if(users.Count == total)
                {
                    jsonBuilder.Append("}");
                }

                else
                {
                    jsonBuilder.Append("},");
                }
            });
            jsonBuilder.Append("]");

            string usersString = jsonBuilder.ToString();
            StringBuilder jsonBuilder2 = new StringBuilder();
            jsonBuilder2.Append("{");
            jsonBuilder2.AppendFormat("\"users\": {0},", usersString);
            jsonBuilder2.AppendFormat("\"type\": \"{0}\"", this.type);
            jsonBuilder2.Append("}");
            return jsonBuilder2.ToString();
        }
    }
}
