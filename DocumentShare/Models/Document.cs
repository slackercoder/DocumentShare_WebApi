using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocumentShare.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public String DocumentName { get; set; }
        public String DocumentDescription { get; set; }
        public String DocumentLocation { get; set; }
    }
}