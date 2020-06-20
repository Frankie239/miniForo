using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace miniForo.Models.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException():
            base("The log in credentials aren't correct"){ }
            
        
    }
}