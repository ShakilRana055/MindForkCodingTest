using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindForkCodingTest.EntityModel
{
    public class Blog
    {
        public Blog()
        {
            Date = DateTime.Now.Date.ToShortDateString();
            Comments = new List<Comment>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostedBy { get; set; }
        public string Date { get; set; }
        public int NumberOfComment { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
