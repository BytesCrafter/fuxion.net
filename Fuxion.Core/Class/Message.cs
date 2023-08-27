using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fuxion.Core.Class
{
    class Message
    {
        public string id { get; set; }
        public string data { get; set; }
        public string type { get; set; }

        public string ToJson()
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{");
            jsonBuilder.AppendFormat("\"id\": \"{0}\",", this.id);
            jsonBuilder.AppendFormat("\"data\": \"{0}\",", this.data);
            jsonBuilder.AppendFormat("\"type\": \"{0}\"", this.type);
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }

        public static Message fromJson(string msgJson)
        {
            Message msg2 = new Message();

            int idStartIndex = msgJson.IndexOf("\"id\":\"") + "\"id\":\"".Length;
            int idEndIndex = msgJson.IndexOf("\",", idStartIndex);
            msg2.id = msgJson.Substring(idStartIndex, idEndIndex - idStartIndex);

            int dataStartIndex = msgJson.IndexOf("\"data\":\"") + "\"data\":\"".Length;
            int dataEndIndex = msgJson.IndexOf("\",", dataStartIndex);
            msg2.data = msgJson.Substring(dataStartIndex, dataEndIndex - dataStartIndex);

            int typeStartIndex = msgJson.IndexOf("\"type\":\"") + "\"type\":\"".Length;
            int typeEndIndex = msgJson.IndexOf("\"}", typeStartIndex);
            msg2.type = msgJson.Substring(typeStartIndex, typeEndIndex - typeStartIndex);

            return msg2;
        }
    }
}
