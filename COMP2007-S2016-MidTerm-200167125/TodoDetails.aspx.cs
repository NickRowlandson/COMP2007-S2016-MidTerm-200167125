using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// Using statements required for EF DB access
using COMP2007_S2016_MidTerm_200167125.Models;
using System.Web.ModelBinding;

/**
 * @author: Nick Rowlandson
 * @date: June 23 2016
 * @version: 0.0.1
 */

namespace COMP2007_S2016_MidTerm_200167125
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetTodo();
            }
        }

        /**
         * <summary>
         * This method gets the specified todo from the db fo editing.
         * </summary>
         * 
         * @method GetTodos
         * @return {void}
         */
        protected void GetTodo()
        {
            // populate the form with existing data from the database
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

            // connect to the EF DB
            using (TodoConnection db = new TodoConnection())
            {
                // populate a todo object instance with the TodoID from the URL parameter
                Todo updatedTodo = (from todo in db.Todos
                                    where todo.TodoID == TodoID
                                    select todo).FirstOrDefault();

                // map the todo properties to the form controls
                if (updatedTodo != null)
                {
                    TodoNameTextBox.Text = updatedTodo.TodoName;
                    TodoNotesTextBox.Text = updatedTodo.TodoNotes;
                }
            }
        }

        /**
         * <summary>
         * This method 
         * </summary>
         * 
         * @method CancelButton_Click
         * @return {void}
         */
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to todo list page
            Response.Redirect("~/TodoList.aspx");
        }

        /**
         * <summary>
         * This method 
         * </summary>
         * 
         * @method SaveButton_Click
         * @return {void}
         */
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use Ef to connect to the server
            using (TodoConnection db = new TodoConnection())
            {
                // Use the student model to create a new todo object and also save a new record
                Todo newTodo = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0) // our URL has a TodoID in it
                {
                    // get the id from the URL
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    // get the current todo from the EF database
                    newTodo = (from todo in db.Todos
                                  where todo.TodoID == TodoID
                                  select todo).FirstOrDefault();
                }


                // add data to the new todo record
                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = TodoNotesTextBox.Text;

                // use LINQ to ADO.NET to add / insert new todo into the database

                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }

                // save our changes
                db.SaveChanges();

                // redirect back to the updated todo list page
                Response.Redirect("~/TodoList.aspx");
            }
        }
    }
}