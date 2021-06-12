using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindForkCodingTest.EntityModel
{
    public class Comment
    {
        public Comment()
        {
            Date = DateTime.Now.Date.ToShortDateString();
        }
        public int Id { get; set; }
        public int? BlogId { get; set; }
        public Blog Blog { get; set; }
        public string CommenterName { get; set; }
        public string Date { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }
    }
}
