using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ToDoList.Models;

namespace ToDoList.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection") { }

        public DbSet<TaskItemModel> TaskItems { get; set; }

        public DbSet<UserModel> Users { get; set; }
    }
}