using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class Article 
    {
        //class describes an article
        //The following fields (not properties) define an Author
        // these are accessed by the ArticleDataController
        public int ArticleId;
        public string ArticleTitle;
        public string ArticleBody;
        


        public string AuthorName;

        public DateTime ArticleDate { get; internal set; }
    }
}