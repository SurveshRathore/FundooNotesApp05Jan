using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface ICollabBL
    {
        public CollabTable AddNewCollab(long UserId, long NoteId, string Email);
        public List<CollabTable> GetAllCollab(long NoteId);
        public bool RemoveCollab(long CollabId);
    }
}
