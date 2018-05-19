using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReviewMe.Exceptions
{
    public class VisitorNotFoundException : Exception
    {
        public VisitorNotFoundException(string userName)
            : base("Visitor not found: " + userName)
        {

        }
    }
}