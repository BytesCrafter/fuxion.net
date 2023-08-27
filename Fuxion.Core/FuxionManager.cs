using Fuxion.Core.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fuxion.Core
{
    public class FuxionManager
    {
        public static List<User> objects = new List<User>();

        public static void AddObject(User _object)
        {
            FuxionManager.objects.Add(_object);
        }

        public static void RemoveObject(User _object)
        {
            FuxionManager.objects.ForEach((User current) =>
            {
                FuxionManager.objects.Remove(current);
            });
        }

        public static User GetById(string user_id)
        {
            User current = null;
            FuxionManager.objects.ForEach((User cur_user) =>
            {
                if ( cur_user.id == user_id )
                {
                    current = cur_user;
                }
            });
            return current;
        }
    }
}
