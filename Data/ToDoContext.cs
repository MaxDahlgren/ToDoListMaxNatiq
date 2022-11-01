using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class ToDoContext:DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options )
            : base( options )
        {
        }
     //Sets the databse with the properties from Models.TodoList
        public DbSet<TodoList> ToDoList { get; set; }
    }
}
