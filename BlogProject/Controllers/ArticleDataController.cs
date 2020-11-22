using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlogProject.Models;
using MySql.Data.MySqlClient;

namespace BlogProject.Controllers
{
    public class ArticleDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private BlogDbContext Blog = new BlogDbContext(); //1. new instance of a class as blog object


        //This Controller will access the articles table of our blog database
        /// <summary>
        /// Returns a list of Articles in the system
        /// </summary>
        /// <example>GET api/ArticleData/ListArticles</example>
        /// <returns>A list of articles</returns>
        /// 

        ///<summary>
        ///
        /// </summary>
        /// <param name = "ArticleSearchKey"></param>
        /// <returns></returns>
 

        [HttpGet]
        //Need to configure the route so that the article search key can be accessed by the dataaccess level 
        [Route("api/ArticleData/ListArticles/{ArticleSearchKey}")]
        public IEnumerable<Article> ListArticles(string ArticleSearchKey)  //Article is a class from models.article.cs
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the conn or connection between the web server and database
            Conn.Open();

            //Establish a new command(query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            string query = "select * from articles where articletitle like  @searchkey or articlebody like @searchkey";

            cmd.CommandText = query;
            cmd.Parameters.AddWithValue("@SearchKey", "%"+ArticleSearchKey+"%"); // partial query: "%"+ C# parameter +"%"
            cmd.Prepare();

            //accesses info from articles table

            //Gather REsult Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader(); //cmd.executereader is a result set

            //Create an empty list of Articles List 
            List<Article> Articles = new List<Article>();

            //Loop through each row the result set
            while (ResultSet.Read())
            {

                //Access Column informaiton by the DB column name as an index
                //create new article
                // Object of type article,  variable name = class of the article 
                Article NewArticle = new Article();

                //new articles has properties associated to it from database
                string ArticleTitle = ResultSet["articletitle"].ToString();
                //Access Column info by the DB column name as an index
                //DateTime ArticleDate = ResultSet["articledate"];

                string ArticleBody = ResultSet["articlebody"].ToString();

                int ArticleID = Convert.ToInt32(ResultSet["articleid"]);

                NewArticle.ArticleTitle = ArticleTitle;
                NewArticle.ArticleDate = ArticleDate;
                NewArticle.ArticleBody = ArticleBody;
                NewArticle.ArticleId = ArticleID;


                //article objects added to NewArticle
                Articles.Add(NewArticle);
            }

            //Close the connetion between MySQL Database and the WebServer
            Conn.Close();

            //Return final list of article titles
            return Articles; //variable name

        }

        [HttpGet]

        //Need to specify route or there will be an error. 
        [Route("api/ArticleData/FindArticle/{articleid}")]
        public Article FindArticle(int articleid)  //Article is a class from models.article.cs
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the conn or connection between the web server and database
            Conn.Open();

            //Establish a new command(query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "select * from articles WHERE articleid=" + articleid; //accesses info from articles table

            //Gather REsult Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader(); //cmd.executereader is a result set

            // Variable declaration has to be outside the loop 
            Article NewArticle = new Article();

            //Defining the article needs to be OUTSIDE the loop  
            while (ResultSet.Read())
            {
                //new articles has properties associated to it from database
                string ArticleTitle = ResultSet["articletitle"].ToString();
                //Access Column info by the DB column name as an index
                DateTime ArticleDate = (DateTime)ResultSet["articledate"];

                string ArticleBody = ResultSet["articlebody"].ToString();

                int ArticleID = Convert.ToInt32(ResultSet["articleid"]);

                NewArticle.ArticleTitle = ArticleTitle;
                NewArticle.ArticleDate = ArticleDate;
                NewArticle.ArticleBody = ArticleBody;
                NewArticle.ArticleId = ArticleID;
               
            }

            return NewArticle;
        }
    }

}