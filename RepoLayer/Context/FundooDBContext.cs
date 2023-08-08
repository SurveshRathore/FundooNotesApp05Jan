using Microsoft.EntityFrameworkCore;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Context
{
    public class FundooDBContext:DbContext
    {
        public FundooDBContext(DbContextOptions options):base(options) { }

        public DbSet<UserTable> UserTable { get; set; }
        public DbSet<NoteTable> Notes { get; set; }
        public DbSet<LabelTable> LabelTable { get; set; }
        public DbSet<CollabTable> CollabTables { get; set; }

        public DbSet<ReviewTable> ReviewTable { get; set; }
        public DbSet<StudentTable> StudentTables { get; set; }
        public DbSet<StudentTable> StudentTablesm { get; set; }

        public DbSet<OrdeTable> ordeTables { get; set; }
    }
}
