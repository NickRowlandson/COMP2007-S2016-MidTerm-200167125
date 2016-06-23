using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements that are required to connnect to EF DB
using COMP2007_S2016_MidTerm_200167125.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

/**
 * @author: Nick Rowlandson
 * @date: June 23 2016
 * @version: 0.0.1
 */

namespace COMP2007_S2016_MidTerm_200167125
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // if loading page for first time, populate the todo grid
            if (!IsPostBack)
            {
                // default sort column
                Session["SortColumn"] = "TodoID";
                Session["SortDirection"] = "ASC";

                // Get the todo data
                this.GetTodos();
            }
        }

        /**
         * <summary>
         * This method gets all todos form the db and binds them to the todo gridview.
         * </summary>
         * 
         * @method GetTodos
         * @return {void}
         */
        protected void GetTodos()
        {
            // connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                // query the todos table using EF and LINQ
                var Todos = (from allTodo in db.Todos
                             select allTodo);

                // bind the result to the GridView
                TodoGridview.DataSource = Todos.AsQueryable().OrderBy(SortString).ToList();
                TodoGridview.DataBind();
            }
        }

        /**
         * <summary>
         * This method 
         * </summary>
         * 
         * @method TodoGridview_RowDeleting
         * @return {void}
         */
        protected void TodoGridview_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

            // get the selected todo id using the grids data key collection
            int TodoID = Convert.ToInt32(TodoGridview.DataKeys[selectedRow].Values["TodoID"]);

            // use EF to find the selected todo in the DB and remove it
            using (TodoConnection db = new TodoConnection())
            {
                // create object of the todo class and store the query string inside of it
                Todo deletedTodo = (from todoRecords in db.Todos
                                    where todoRecords.TodoID == TodoID
                                    select todoRecords).FirstOrDefault();

                // remove the selected todo from the db
                db.Todos.Remove(deletedTodo);

                // save my changes back to the db
                db.SaveChanges();

                // refresh the grid
                this.GetTodos();
            }
        }

        /**
         * <summary>
         * This method 
         * </summary>
         * 
         * @method TodoGridview_PageIndexChanging
         * @return {void}
         */
        protected void TodoGridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // set the new page number
            TodoGridview.PageIndex = e.NewPageIndex;

            // refresh the grid
            this.GetTodos();
        }

        /**
         * <summary>
         * This method 
         * </summary>
         * 
         * @method TodoGridview_Sorting
         * @return {void}
         */
        protected void TodoGridview_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            // refresh the grid
            this.GetTodos();

            // toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        /**
         * <summary>
         * This method 
         * </summary>
         * 
         * @method TodoGridview_RowDataBound
         * @return {void}
         */
        protected void TodoGridview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) // if header has been clicked
                {
                    LinkButton linkButton = new LinkButton();

                    for (int index = 0; index < TodoGridview.Columns.Count - 1; index++)
                    {
                        if (TodoGridview.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkButton.Text = " <i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkButton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkButton);
                        }
                    }
                }
            }
        }

        /**
         * <summary>
         * This method 
         * </summary>
         * 
         * @method PageSizeDropDownList_SelectedIndexChanged
         * @return {void}
         */
        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set the new list size
            TodoGridview.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the grid
            this.GetTodos();
        }

    }
}