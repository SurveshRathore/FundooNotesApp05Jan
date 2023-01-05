using CommonLayer.Models;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interface
{
    public interface ILabelRL
    {
        public LabelTable AddNewLabel(string LabelName, long UserId, long NoteId);
        public List<LabelTable> GetAllLabels(long LabelId);
        public bool UpdateLabel(long LabelId, string NewLabelName);
        public bool DeleteLabel(long LabelId);
    }
}
