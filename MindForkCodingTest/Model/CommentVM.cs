using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindForkCodingTest.Model
{
    public class CommentVM
    {
        public int Id { get; set; }
        public int? BlogId { get; set; }
        public string CommenterName { get; set; }
        public string Date { get; set; }
        public string LikeDislike { get; set; }
    }
}
