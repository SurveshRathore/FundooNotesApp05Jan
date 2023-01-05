using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Service
{
    public class CollabRL: ICollabRL
    {
        private readonly FundooDBContext fundooDBContext;
        

        public CollabRL(FundooDBContext fundooDB)
        {
            this.fundooDBContext = fundooDB;
            
        }
        public CollabTable AddNewCollab(long UserId, long NoteId, string Email)
        {
            try
            {
                CollabTable collab = new CollabTable();
                collab.CollabEmail = Email;
                collab.userId = UserId;
                collab.NoteId = NoteId;
                collab.CollabModifiedDate = DateTime.Now;
                fundooDBContext.CollabTables.Add(collab);
                int result = fundooDBContext.SaveChanges();
                if (result != null)
                {
                    return collab;
                }
                else
                {
                    return null;
                }
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
                var result = this.fundooDBContext.CollabTables.Where(e => e.NoteId == NoteId).ToList();
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
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
                var result = fundooDBContext.CollabTables.Where(e => e.CollabId == CollabId).FirstOrDefault();
                if (result != null)
                {
                    fundooDBContext.Remove(result);
                    fundooDBContext.SaveChanges();
                    return true;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
