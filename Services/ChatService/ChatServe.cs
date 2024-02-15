using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ChatService
{
    public class ChatServe
    {
        private readonly Dictionary<string, string?> Users;

        public ChatServe()
        {
            Users=new Dictionary<string, string?>();
        }

        public bool AddUser(string name)
        {
            lock (Users)
            {
                if (Users.ContainsKey(name.ToLower()))
                {
                    return false;
                }
                Users.Add(name.ToLower(), null);
                return true;
            }

        }

       public void AddConnectionId(string name, string connectionId)
        {
            lock (Users)
            {
                if(Users.ContainsKey(name.ToLower())) {
                    Users[name.ToLower()] = connectionId;
                
                }
            }
        }
      
        public string GetUserNameByConnectionId(string connectionId)
        {
            lock (Users)
            {
                string? id =connectionId;
                return Users.FirstOrDefault(x => x.Value == id).Key;
                
            }
        }

        public string? GetUserIdByConnectionName(string Name)
        {
            lock (Users)
            {
                
                return Users.FirstOrDefault(x => x.Key == Name).Value;

            }
        }

        public bool RemoveUserByName(string name)
        {
            lock (Users)
            {
               var i=Users.FirstOrDefault(x => x.Key == name).Key;
                if (!(i == null))
                {
                    Users.Remove(i);
                    return true;
                }
                return false;
            }
        }

        public string[] GetUsers()
        {
            lock (Users)
            {
                return Users.OrderBy(x => x.Key).Select(x => x.Key).ToArray();
            }
        }
    }
}
