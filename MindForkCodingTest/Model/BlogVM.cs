using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindForkCodingTest.Model
{
    public class BlogVM
    {
        public BlogVM()
        {
            Date = DateTime.Now.Date.ToShortDateString();
            Comments = new List<CommentVM>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostedBy { get; set; }
        public string Date { get; set; }
        public string NumberOfComment { get; set; }
        public List<CommentVM> Comments { get; set; }
    }
}
