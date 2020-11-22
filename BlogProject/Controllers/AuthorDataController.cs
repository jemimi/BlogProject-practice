
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlogProject.Models;//new reference to projects in the model folder to access blogdatabase context
using MySql.Data.MySqlClient;

namespace BlogProject.Controllers
{
    public class AuthorDataController : ApiController 
        //type of controller
    {
        // The database context class which allows us to access our MySQL Database.
        private BlogDbContext Blog = new BlogDbContext(); //1. new instance of a class as blog object

        //This Controller will access the authors table of our blog database
        /// <summary>
        /// Returns a list of Authors in the system
        /// </summary>
        /// <example>GET api/AuthorData/ListAuthors</example>
        /// <returns>A list of authors (first names and last names)
        /// </returns>

        [HttpGet]
        //configure the route attribute for the searchkey for form
        //? means that information may or may not be included
        [Route("api/AuthorData/ListAuthors/{SearchKey?}")]
        public IEnumerable<Author> ListAuthors(string SearchKey = null) //2. method = ListAuthors as a string
        {
            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //OPen the conn or connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database 
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY = parameterized query
            cmd.CommandText = "Select * from Authors where lower(authorfname) like lower(@key) or lower(authorlname) like lower(@key) or lower(concat(authorfname, ' ', authorlname)) like lower(@key)";
            //lower: means lowercase
            //commandtext is public This is s command object. represents a string

            //security: anything that is included - the @key is the search key, don't have to 
            //worry about tampering. It is a find and replace. Any strange characters
            //get stripped out with following: 
            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();
            //preparing the SQL query with cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader(); //cmd.executereader is a result set 


            //Create an empty list of Authors
            //what it does:  finds the author names and adds them to a new list
            List<Author> Authors = new List<Author>{};

            //Loop Through Each Row the Result Set 
            //read method will proceed through list via rows. result set will return a dual data type
            // 1 loop will result in one result set. ex. if 300 authors - will loop 300 times
            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int AuthorId = Convert.ToInt32(ResultSet["authorid"]);
                string AuthorFname = ResultSet["Authorfname"].ToString();
                string AuthorLname = ResultSet["authorlname"].ToString();
                string AuthorBio = ResultSet["authorbio"].ToString();

                //Access the Author Join Date - safer way by parsing the result into a string
                DateTime AuthorJoinDate;
                DateTime.TryParse(ResultSet["authorjoindate"].ToString(), out AuthorJoinDate);
                

                //Create a new author object
                Author NewAuthor = new Author(); 
                NewAuthor.AuthorId = AuthorId; //AuthorId on left refers to author.cs class
                NewAuthor.AuthorFname = AuthorFname;
                NewAuthor.AuthorLname = AuthorLname;
                NewAuthor.AuthorBio = AuthorBio;

                //Add the Author  to the List of Authors
                Authors.Add(NewAuthor);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of authors 
            return Authors;
        }



        /// <summary>
        /// Returns an individual author from the database by specifying the primary key authorid
        /// </summary>
        /// <param name="id">the author's ID in the database</param>
        /// <returns>An author object</returns>
        [HttpGet]
        public Author FindAuthor(int id)
        {
            Author NewAuthor = new Author();

            //Create an instance of a connection
            MySqlConnection Conn = Blog.AccessDatabase();

            //OPen the conn or connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database 
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Select * from Authors where authorid = @id"; //commandtext is public This is s command object. represents a string
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader(); //cmd.executereader is a result set 

            //loops through the database
            while(ResultSet.Read())
            {
                //Access Column info by the DB column name as an index
                int AuthorId = Convert.ToInt32(ResultSet["authorid"]);
                string AuthorFname = ResultSet["authorfname"].ToString();
                string AuthorLname = ResultSet["authorlname"].ToString();
                string AuthorBio = ResultSet["authorbio"].ToString();

                //Date 
                DateTime AuthorJoinDate;
                DateTime.TryParse(ResultSet["authorjoindate"].ToString(), out AuthorJoinDate);

                NewAuthor.AuthorId = AuthorId; //AuthorId on left refers to author.cs class
                NewAuthor.AuthorFname = AuthorFname;
                NewAuthor.AuthorLname = AuthorLname;
                NewAuthor.AuthorBio = AuthorBio;
                NewAuthor.AuthorJoinDate = AuthorJoinDate;


            }
            Conn.Close();

            return NewAuthor;
        }

        ///<summary>
        /// Removes an Author from the database
        /// </summary>
        /// <param name="id"> The ID of the author to remove </param>
        /// <example>POST: /api/AuthorData/DeleteAuthor/3</example>
        /// <returns>Does not return anything</returns>
        
        [HttpPost]
        public void DeleteAuthor(int id)
        {
            //Create an instance of a conne tion
            MySqlConnection Conn = Blog.AccessDatabase();

            //Open the conection between web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL Query
            cmd.CommandText = "Delete from authros where authorid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();
           
        }

            


    }
}
