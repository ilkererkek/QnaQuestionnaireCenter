using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class QnaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=QNADB;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Questionnaire>().HasMany(x => x.Users).WithMany(x => x.Questionnaires);
            modelBuilder.Entity<MultiSelection>().HasOne(x => x.TakenQuestionnaire).WithMany(x => x.MultiSelections).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<NumericalSelection>().HasOne(x => x.TakenQuestionnaire).WithMany(x => x.NumericalSelections).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OptionSelection>().HasOne(x => x.TakenQuestionnaire).WithMany(x => x.OptionSelections).OnDelete(DeleteBehavior.Restrict);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Questionnaire> Questionnaires { get; set; }
        public virtual DbSet<TakenQuestionnaire> TakenQuestionnaires { get; set; }
        public virtual DbSet<Question> Questions { get; set; }



        public virtual DbSet<MultiAnswer> MultiAnswers { get; set; }
        public virtual DbSet<NumericalAnswer> NumericalAnswers { get; set; }
        public virtual DbSet<OpenAnswer> OpenAnswers { get; set; }
        public virtual DbSet<OptionAnswer> OptionAnswers { get; set; }



        public virtual DbSet<MultiSelection> MultiSelections { get; set; }
        public virtual DbSet<NumericalSelection> NumericalSelections { get; set; }
        public virtual DbSet<OptionSelection> OptionSelections { get; set; }


    }
}
