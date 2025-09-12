using Microsoft.EntityFrameworkCore;
using Payments.Infrastructure.DataBase.EntityFramework.EntityConfig;

namespace Payments.Infrastructure.DataBase.EntityFramework.Context
{
    /// <summary>  
    /// Represents the application's database context, providing access to the database and its entities.  
    /// </summary>  
    public class ApplicationDbContext : DbContext
    {
        /// <summary>  
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class with the specified options.  
        /// </summary>  
        /// <param name="options">The options to configure the database context.</param>  
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new PaymentConfiguration());


        }
    }
}
