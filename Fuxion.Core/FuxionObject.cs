using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;
using Fuxion.Core;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using Fuxion.Core.Class;

namespace Fuxion
{
    public class FuxionObject : WebSocketBehavior
    {
        private string _name = "";
        private static int _number = 0;
        private string _prefix;

        public FuxionObject()
        {
            _prefix = "object#";
        }

        public string Prefix
        {
            get
            {
                return _prefix;
            }

            set
            {
                _prefix = !value.IsNullOrEmpty() ? value : "object#";
            }
        }

        public string getName()
        {
            var name = QueryString["name"];

            return !name.IsNullOrEmpty() ? name : _prefix + getNumber();
        }

        private static int getNumber()
        {
            return Interlocked.Increment(ref _number);
        }

        protected string getUserObjectString(string _id, string _name, string _type = "")
        {
            Core.Class.User userString = new Core.Class.User()
            {
                id = _id,
                name = _name,
                type = _type
            };
            return JsonConvert.SerializeObject(userString);
        }

        protected string getMessageString(string _id, string _data)
        {
            Core.Class.Message msg = JsonConvert
                .DeserializeObject<Core.Class.Message>(_data);
            msg.id = this.ID;
            return JsonConvert.SerializeObject(msg);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            if (_name == null)
                return;

            //Remove the user.
            FuxionManager.RemoveObject(new Core.Class.User()
            {
                id = this.ID,
                name = _name,
            });

            Console.WriteLine($"Disconnected: {_name} with id of #{this.ID}");
            string stringjson = this.getUserObjectString(this.ID, _name, "close");
            Sessions.Broadcast(stringjson);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.Data == "users-list")
            {
                string userList = this.ListUser();
                Sessions.SendTo(userList, this.ID);
            }

            else if ( e.Data.Contains((String)"message-all") )
            {
                string stringjson = this.getMessageString(this.ID, e.Data);
                
                this.ActiveUsers().ForEach((User user) =>
                {
                    Sessions.SendToAsync(stringjson, user.id, null);
                });
                Sessions.SendToAsync(stringjson, this.ID, null);
            }

            else
            {
                string stringjson = this.getMessageString(this.ID, e.Data);
                Sessions.Broadcast(stringjson);
            }
        }

        private List<User> ActiveUsers()
        {
            //Get all active users.
            List<Core.Class.User> users = new List<Core.Class.User>();

            List<string> activeID = Sessions.ActiveIDs.ToList<string>();
            activeID.ForEach((string curId) =>
            {
                if (curId != this.ID)
                {
                    Core.Class.User cuser = FuxionManager.GetById(curId);
                    if (cuser != null)
                    {
                        users.Add(cuser);
                    }
                }
            });

            return users;
        }

        private string ListUser()
        {
            //Get all active users.
            List<Core.Class.User> users = this.ActiveUsers();
            Core.Class.Actives dataObj = new Core.Class.Actives()
            {
                users = users,
                type = "users"
            };

            return JsonConvert.SerializeObject(dataObj);
        }

        protected override void OnOpen()
        {
            _name = getName();

            if (_name == null)
                return;

            //Add the user to the list
            FuxionManager.AddObject(new Core.Class.User()
            {
                id = this.ID,
                name = _name,
            });

            Console.WriteLine($"Connected: {_name} with id of #{this.ID}");
            string stringjson = this.getUserObjectString(this.ID, _name, "open");
            Sessions.Broadcast(stringjson);

            string stringSelf = this.getUserObjectString(this.ID, _name, "open-self");
            Sessions.SendTo(stringSelf, this.ID);
        }
    }
}
