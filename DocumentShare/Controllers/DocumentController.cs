﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DocumentShare.Models;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Text;

namespace DocumentShare.Controllers
{
    public class DocumentController : ApiController
    {
        public List<Document> GetAllDocuments(int userId)
        {
            try
            {
                using (DocumentShareEntities db = new DocumentShareEntities())
                {
                    List<Document> docs;

                    docs = (from doc in db.Documents
                            join udc in db.UserDocuments on doc.DocumentId equals udc.DocumentId
                            where udc.UserId == userId
                            select doc).ToList();

                    return docs;
                }
            }
            catch (Exception e)
            {
                LogError(e);
                return null;
            }
        }

        public Document GetDocumentbyId(int userId, int documentId)
        {
            try
            {
                using (DocumentShareEntities db = new DocumentShareEntities())
                {
                    return db.Documents.Where(o => o.DocumentId == documentId).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                LogError(e);
                return null;
            }
        }

        public bool SendDocumentToUser(int fromUserId, int toUserId, int documentId)
        {
            try
            {
                using (DocumentShareEntities db = new DocumentShareEntities())
                {
                    User user = db.Users.Where(o => o.Id == fromUserId).FirstOrDefault();
                    
                    if (user == null)
                        throw new Exception("User was not found");

                    Document doc = db.Documents.Where(o => o.DocumentId == documentId).FirstOrDefault();

                    if (doc == null)
                        throw new Exception("Document was not found");

                    UserDocument userDoc = new UserDocument();
                    userDoc.DocumentId = documentId;
                    userDoc.UserId = toUserId;

                    db.UserDocuments.Add(userDoc);

                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception e)
            {
                LogError(e);
                return false;
            }
        }

        public bool AddUnknownDocumentByName(int fromUserId, int toUserId, String documentName)
        {
            return true;
        }

        public async Task<HttpResponseMessage> PostFile()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                StringBuilder sb = new StringBuilder(); // Holds the response body

                // Read the form data and return an async task.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the form data.
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        sb.Append(string.Format("{0}: {1}\n", key, val));
                    }
                }

                // This illustrates how to get the file names for uploaded files.
                foreach (var file in provider.FileData)
                {
                    FileInfo fileInfo = new FileInfo(file.LocalFileName);
                    sb.Append(string.Format("Uploaded file: {0} ({1} bytes)\n", fileInfo.Name, fileInfo.Length));
                }
                return new HttpResponseMessage()
                {
                    Content = new StringContent(sb.ToString())
                };
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }

        public bool AddNewDocument()
        {
            /*
            //Ftp Creds:
            //ftp64366917-0
            //d0cumentsh4re
            //mfstudios.ca

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://www.mfstudios.ca/");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential("ftp64366917-0", "d0cumentsh4re");

            // Copy the contents of the file to the request stream.
            StreamReader sourceStream = new StreamReader(
            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            request.ContentLength = fileContents.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

            response.Close();

            return true;
             * */
            return true;
        }

        internal void LogError(Exception e, String notes = "")
        {
            try
            {
                using (DocumentShareEntities db = new DocumentShareEntities())
                {
                    ErrorLog err = new ErrorLog();
                    err.Message = String.Format("{0} {1}", e.Message, e.InnerException == null ? String.Empty : "(" + e.InnerException.Message + ")").Substring(0, 500);
                    err.Notes = notes;

                    db.ErrorLogs.Add(err);
                }
            }
            catch (Exception e)
            {
                //hmmm...
            }
        }
    }
}
