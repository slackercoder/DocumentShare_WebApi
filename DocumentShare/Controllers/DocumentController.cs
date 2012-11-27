using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DocumentShare.Models;

namespace DocumentShare.Controllers
{
    public class DocumentController : ApiController
    {
        public IEnumerable<Document> GetAllDocuments()
        {
            using (DocumentShareEntities data = new DocumentShareEntities())
            {
                return data.Documents.ToList();
            }
        }

        public Document GetDocumentbyId(int id)
        {
            using (DocumentShareEntities data = new DocumentShareEntities())
            {
                return data.Documents.Where(o => o.DocumentId == id).FirstOrDefault();
            }
        }
    }
}
