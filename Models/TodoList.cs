using System;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{   //ModelClass with all our properties needed for the project. Linked with our database.
    public class TodoList
    {   //Id will work as a indicator for what item we want to Create/Edit/Delete or Check.
        public int Id { get; set; }
        //[Required] prompts the user to fill in the Content field.
        [Required]
        public string Content { get; set; }
        //AddedAt will show us when the items were Created och Edited.
        public DateTime AddedAt { get; set; }
        //IsDone will be our boolean property for our checkbox.
        public bool IsDone { get; set; }
    }
}
