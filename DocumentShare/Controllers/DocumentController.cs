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
        private List<Document> documents = new List<Document>()
        {
            new Document() {DocumentId = 1, DocumentName="Doc1", DocumentDescription="Doc1Desc", DocumentLocation="Doc1Loc"},
            new Document() {DocumentId = 2, DocumentName="Doc2", DocumentDescription="Doc2Desc", DocumentLocation="Doc2Loc"},
            new Document() {DocumentId = 3, DocumentName="Doc3", DocumentDescription="Doc3Desc", DocumentLocation="Doc3Loc"},
            new Document() {DocumentId = 4, DocumentName="Doc4", DocumentDescription="Doc4Desc", DocumentLocation="Doc4Loc"}
        };

        public IEnumerable<Document> GetAllDocuments()
        {
            return documents;
        }

        public Document GetDocumentbyId(int id)
        {
            return documents.Where(o => o.DocumentId == id).First();
        }
    }
}
