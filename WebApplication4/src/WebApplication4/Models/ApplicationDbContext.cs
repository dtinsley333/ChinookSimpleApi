using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;




namespace WebApplication4.Models
{
    public class ApplicationDbContext : DbContext
    {
 
        public DbSet<Album> Album { get; set; }
        public DbSet<Album> Artist { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceLine> InvoiceLine { get; set; }
        public DbSet<Track> Track { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

           //builder.Entity<Album>()  /looks like this is no longer needed but left there for now
           //             .ToTable("Album")
           //            .HasKey(c => c.AlbumId);

        }
       
    }
}
