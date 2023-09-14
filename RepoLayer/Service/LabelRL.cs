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
    public class LabelRL: ILabelRL
    {
        private readonly FundooDBContext fundooDBContext;
        private readonly IConfiguration configuration;

        public LabelRL(FundooDBContext fundooDB, IConfiguration config)
        {
            this.fundooDBContext = fundooDB;
            this.configuration = config;
        }
        public LabelTable AddNewLabel(string LabelName, long UserId, long NoteId)
        {
            try
            {
                var result = this.fundooDBContext.Notes.FirstOrDefault(e => e.NoteId == NoteId);
                if (result != null)
                {
                    LabelTable label = new LabelTable();
                    label.userId = UserId;
                    label.NoteId = NoteId;
                    label.LabelName = LabelName;
                    this.fundooDBContext.LabelTable.Add(label);
                    this.fundooDBContext.SaveChanges();
                    return label;
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
        public List<LabelTable> GetAllLabels(long LabelId)
        {
            try
            {
                var result = this.fundooDBContext.LabelTable.Where(e => e.LabelId == LabelId).ToList();
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
        public bool UpdateLabel(long LabelId, string NewLabelName)
        {
            try
            {
                var result = fundooDBContext.LabelTable.FirstOrDefault(e => e.LabelId == LabelId);
                if (result != null)
                {
                    result.LabelName = NewLabelName;
                    fundooDBContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteLabel(long LabelId)
        {
            try
            {
                var result = fundooDBContext.LabelTable.FirstOrDefault(e => e.LabelId == LabelId);
                if (result != null)
                {
                    fundooDBContext.LabelTable.Remove(result);
                    fundooDBContext.SaveChanges();
                    return true;
                    
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
