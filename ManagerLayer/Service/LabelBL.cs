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
    public class LabelBL: ILabelBL
    {
        private readonly ILabelRL LabelInterfaceRL;

        public LabelBL(ILabelRL labelInterfaceRL)
        {
            this.LabelInterfaceRL = labelInterfaceRL;
        }
        public LabelTable AddNewLabel(string LabelName, long UserId, long NoteId)
        {
            try
            {
                return this.LabelInterfaceRL.AddNewLabel(LabelName, UserId, NoteId);
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
                return this.LabelInterfaceRL.GetAllLabels(LabelId);
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
                return this.LabelInterfaceRL.UpdateLabel(LabelId, NewLabelName);
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
                return this.LabelInterfaceRL.DeleteLabel(LabelId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
