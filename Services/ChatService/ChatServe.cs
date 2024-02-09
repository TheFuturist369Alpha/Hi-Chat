using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ChatService
{
    public class ChatServe
    {
        private readonly Dictionary<string, Guid?> Users;

        public ChatServe()
        {
            Users=new Dictionary<string, Guid?>();
        }

        public bool AddUser(string name)
        {
            lock (Users)
            {
                if (Users.ContainsKey(name.ToLower()))
                {
                    return false;
                }
                Users.Add(name.ToLower(), new Guid());
                return true;
            }

        }

      /*  public void AddConnectionId(string name, Guid connectionId)
        {
            lock (Users)
            {
                if(Users.ContainsKey(name.ToLower())) {
                    Users[name.ToLower()] = connectionId;
                
                }
            }
        }
      */
        public string GetUserNameByConnectionId(string connectionId)
        {
            lock (Users)
            {
                Guid? id = Guid.Parse(connectionId);
                return Users.FirstOrDefault(x => x.Value == id).Key;
                
            }
        }

        public string? GetUserIdByConnectionName(string Name)
        {
            lock (Users)
            {
                
                return Users.FirstOrDefault(x => x.Key == Name).Value.ToString();

            }
        }

        public bool RemoveUserById(Guid id)
        {
            lock (Users)
            {
               var i=Users.FirstOrDefault(x => x.Value == id).Key;
                if (!(i == null))
                {
                    Users.Remove(i);
                    return true;
                }
                return false;
            }
        }

        public List<string> GetUsers()
        {
            lock (Users)
            {
                return Users.OrderBy(x => x.Key).Select(x => x.Key).ToList();
            }
        }
    }
}
