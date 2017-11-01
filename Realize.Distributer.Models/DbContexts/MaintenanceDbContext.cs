using Realize.Distributer.Models.Entities.MaintenanceDb;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realize.Distributer.Models.DbContexts
{
    /// <summary>
    /// 
    /// </summary>
    public class MaintenanceDbContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public MaintenanceDbContext()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public MaintenanceDbContext(string connectionString)
            : base(connectionString)
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            this.Database.Log = (x) => { Trace.TraceInformation(x); };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToOneConstraintIntroductionConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public DbSet<DistributionTask> DistributionTasks { get; set; }
    }
}
