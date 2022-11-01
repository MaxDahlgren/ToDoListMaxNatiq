using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ToDoController : Controller
    {   //Connects the Controller with the Data folder
        private readonly ToDoContext context;
        public ToDoController(ToDoContext context)
        {
                this.context = context;
        }
        //GET /
        public async Task<ActionResult> Index()
        {   //IndexView which reads the data from the Database
            IQueryable<TodoList> items = from i in context.ToDoList orderby i.Id select i;
            List<TodoList> todolist = await items.ToListAsync();
            return View(todolist);
        }
        //GET /todo/create
        public IActionResult Create() => View();

        // POST /todo/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TodoList item)
        {
            if (ModelState.IsValid)
            {   /*If valid, the input will be added to the list 
		          with the Date it was added at with DateTime.Now.*/
                
                context.Add(item);
                item.AddedAt = DateTime.Now;
                await context.SaveChangesAsync();
                //With a success message and redirecting back to IndexView
                TempData["Success"] = "The item has been added!";
                return RedirectToAction("Index");
            }

            return View(item);
        }
       
        //GET /todo/edit/id
        public async Task<ActionResult> Edit(int id)
        {   
            //Looking for chosen id in the list of items
            TodoList item = await context.ToDoList.FindAsync(id);
            if (item == null)
            {   //If not found, return Status 404
                return NotFound(item);
            }
            //If found, return item in editView
            return View(item);
        }
        // POST /todo/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TodoList item)
        {
            if (ModelState.IsValid)
            {   /*If valid, the input will be added to the list
		            with the new Date of when it was last Edited. */
                context.Update(item);
                item.AddedAt = DateTime.Now;
                await context.SaveChangesAsync();
                //With a success message and redirecting back to IndexView
                TempData["Success"] = "The item has been updated!";
                return RedirectToAction("Index");
            }

            return View(item);
        }
        //GET /todo/delete/id
        public async Task<ActionResult> Delete(int id)
        {
            //Looking for chosen id in the list of items
            TodoList item = await context.ToDoList.FindAsync(id);
            if (item == null)
            {   //If not found, return Error message
                TempData["Error"] = "The item does not exist!";
            }
            else
            {
                //If found Remove item from List
                context.ToDoList.Remove(item);
                await context.SaveChangesAsync();
                //Success message
                TempData["Success"] = "The item has been removed!";
            }
            //Redirect to IndexView
            return RedirectToAction("Index");
        }
        //POST /todo/toggleisdone/id
        public async Task<ActionResult> ToggleIsDone(int id)
        {   /*The javascript in the View will fetch the ToDoListItem specified by the id parameter,
                using the context.ToDoList.FindAsync(id) from the Database. */

            TodoList item = await context.ToDoList.FindAsync(id);
            /*When we have the item, we change it to be False if its True, and vice versa by checking the box,
	            and then instantly update the database and refreshing the page. */
            if (item != null)
            {
                item.IsDone = !item.IsDone;
                context.Update(item);
                await context.SaveChangesAsync();
            }
           
            return RedirectToAction("Index");
        }
    }

}
