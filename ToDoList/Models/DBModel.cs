namespace ToDoList.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using MySql.Data.Entity;

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DBModel : DbContext
    {
        // Your context has been configured to use a 'DBModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ToDoList.Models.DBModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DBModel' 
        // connection string in the application configuration file.
        public DBModel()
            : base("name=DBModel")

        {
            this.Database.CreateIfNotExists();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Password).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Email).IsRequired();

            modelBuilder.Entity<Category>().HasKey(x => x.Id);
            modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Category>().Property(x => x.UserId).IsRequired();

            modelBuilder.Entity<Task>().HasKey(x => x.Id);
            modelBuilder.Entity<Task>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Task>().Property(x => x.UserId).IsRequired();
            modelBuilder.Entity<Task>().Property(x => x.CategoryId).IsRequired();
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
    }
}