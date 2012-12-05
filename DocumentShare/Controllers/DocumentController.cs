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
        public IEnumerable<Document> GetAllDocuments(int userId)
        {
            using (DocumentShareEntities db = new DocumentShareEntities())
            {
                IEnumerable<Document> docs;

                docs = from doc in db.Documents
                       join udc in db.UserDocuments on doc.DocumentId equals udc.DocumentId
                       where udc.UserId == userId
                       select doc;

                return docs;
            }
        }

        public Document GetDocumentbyId(int userId, int documentId)
        {
            using (DocumentShareEntities db = new DocumentShareEntities())
            {
                return db.Documents.Where(o => o.DocumentId == documentId).FirstOrDefault();
            }
        }
    }
}
