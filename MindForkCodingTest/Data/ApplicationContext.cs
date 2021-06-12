using Microsoft.EntityFrameworkCore;
using MindForkCodingTest.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MindForkCodingTest.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext( DbContextOptions<AppDbContext> options ): base(options)
        {

        }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<Comment> Comment { get; set; }
    }
}
