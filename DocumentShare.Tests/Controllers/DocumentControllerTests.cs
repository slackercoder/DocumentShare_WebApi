using DocumentShare.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentShare.Tests.Controllers
{
    class DocumentControllerTests
    {
        [TestMethod]
        public void GetAllDocuments_Pass()
        {
            DocumentShare.Controllers.DocumentController doc = new DocumentShare.Controllers.DocumentController();

            List<Document> docs = (List<Document>)doc.GetAllDocuments();

            Assert.IsTrue(docs.Count() > 0);
        }

        [TestMethod]
        public void GetDocumentById_Pass()
        {
            DocumentShare.Controllers.DocumentController doc = new DocumentShare.Controllers.DocumentController();

            Document thisDoc = doc.GetDocumentbyId(1);

            Assert.IsTrue(doc != null);
        }
    }
}
