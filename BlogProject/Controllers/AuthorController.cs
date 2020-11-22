﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;

namespace BlogProject.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Author
        public ActionResult Index()
        {
            return View();
        }

        //GET: /Author/List
        //method that links to specific page:  list.cshtml 
        public ActionResult List(string SearchKey = null)
        {
            AuthorDataController controller = new AuthorDataController();
            IEnumerable<Author> Authors = controller.ListAuthors(SearchKey);
            
            return View(Authors);
        }

        //GET : /Author/Show/{id}
        public ActionResult Show(int id)
        {
            
            AuthorDataController controller = new AuthorDataController();
            Author NewAuthor = controller.FindAuthor(id);

            return View(NewAuthor);
        }

        //GET: /Author/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            AuthorDataController controller = new AuthorDataController();
            Author NewAuthor = controller.FindAuthor(id);

            return View(NewAuthor);
        }

        //POST: /Author/Delete{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            AuthorDataController controller = new AuthorDataController();
            controller.DeleteAuthor(id);
            return RedirectToAction("List");
        }
    }
}