using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogProject.Models;
using System.Diagnostics;

namespace BlogProject.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        //GET: /Article/List   <====== many articles

        public ActionResult List(string ArticleSearchKey)
        {
            //need to add using System Diagnostics to use this debug line
            Debug.WriteLine("The search is " + ArticleSearchKey);

            ArticleDataController Controller = new ArticleDataController();
            

            IEnumerable<Article> Articles = Controller.ListArticles(ArticleSearchKey);

            return View(Articles);
        }

        //GET: /Article/Show/{id}    <==== one article 
        public ActionResult Show(int id) 
        {
            ArticleDataController Controller = new ArticleDataController();


            Article SelectedArticle = Controller.FindArticle(id);

            return View(SelectedArticle);
        }



    }
}