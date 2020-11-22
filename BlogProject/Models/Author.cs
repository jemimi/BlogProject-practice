using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class Author
    {

        //The following fields (not properties) define an Author 
        // these definitions are accessed by the authordata controller
        public int AuthorId;
        public string AuthorFname;
        public string AuthorLname;
        public string AuthorBio;
        public DateTime AuthorJoinDate;
    }
}