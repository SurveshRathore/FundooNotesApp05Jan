using ManagerLayer.Interface;
using RepoLayer.Entity;
using RepoLayer.Interface;
using RepoLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Service
{
    public class CollabBL: ICollabBL
    {
        private readonly ICollabRL CollabInterfaceRL;

        public CollabBL(ICollabRL collabInterfaceRL)
        {
            this.CollabInterfaceRL = collabInterfaceRL;
        }
        public CollabTable AddNewCollab(long UserId, long NoteId, string Email)
        {
            try
            {
                return this.CollabInterfaceRL.AddNewCollab(UserId, NoteId, Email);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<CollabTable> GetAllCollab(long NoteId)
        {
            try
            {
                return this.CollabInterfaceRL.GetAllCollab(NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool RemoveCollab(long CollabId)
        {
            try
            {
                return this.CollabInterfaceRL.RemoveCollab(CollabId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
