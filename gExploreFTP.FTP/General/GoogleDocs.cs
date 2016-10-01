using System;
using System.Collections.Generic;
using System.Text;
using Google.GData.Documents;
using Google.GData.Client;
using Google.Documents;
using Google.GData.Client.ResumableUpload;
using System.IO;
using System.Net;

namespace gExploreFTP.FTP.General
{
    public class GoogleDocs
    {

        // A connection with the DocList API.
        DocumentsService p_Service;
        DocumentsRequest p_Request;

        string p_UserName;
        string p_Password;

        public DocumentsService Service
        {
            get { return p_Service; }
        }

        /// <remarks></remarks>
        public DocumentsRequest Request
        {
            get { return p_Request; }
            set { p_Request = value; }
        }

        public string getUserName()
        {
            return p_UserName;
        }

        public string getPassword()
        {
            return p_Password;
        }

        public bool doLogin()
        {

            try
            {

                // force the service to authenticate
                DocumentsListQuery query = new DocumentsListQuery();
                query.NumberToRetrieve = 1;
                Service.Query(query);

                return true;

            }
            catch (Exception e)
            {

                return false;

            }

        }

        public GoogleDocs(string UserName, string Password)
        {

	        // NOTE: This is using version 3 of the Google Docs API.

	        p_Service = new DocumentsService("gExploreFTP");

	        ((GDataRequestFactory)Service.RequestFactory).KeepAlive = false;
	        ((GDataRequestFactory)Service.RequestFactory).UseSSL = true;

	        Service.RequestFactory.UseSSL = true;
	        Service.setUserCredentials(UserName, Password);

	        p_UserName = UserName;
	        p_Password = Password;

        }

        public RequestSettings GetRequestSettings()
        {

            GDataCredentials credentials = new GDataCredentials(p_UserName, p_Password);

            RequestSettings settings = new RequestSettings("gExploreFTP", credentials);

            settings.UseSSL = true;
            settings.AutoPaging = false;

            return settings;

        }

        public Stream DownloadFile(Document d, string exportFormat)
        {

            Request = new DocumentsRequest(GetRequestSettings());
            Stream s = Request.Download(d, exportFormat);
            return s;

        }

        public Document FindObject(string ResourceID)
        {


            try
            {
                Uri DocumentUri = new Uri("https://docs.google.com/feeds/default/private/full/" + ResourceID);

                Document Result = null;

                DocumentsRequest documentsRequest = new DocumentsRequest(GetRequestSettings());
                Feed<Document> documentFeed = documentsRequest.Get<Google.Documents.Document>(DocumentUri);


                foreach (Document d in documentFeed.Entries)
                {

                    if (d.ResourceId == ResourceID)
                    {
                        Result = d;

                        break;

                    }

                }

                return Result;

            }
            catch (GDataRequestException ex)
            {
                //Dim resp As HttpWebResponse = TryCast(ex.Response, HttpWebResponse)
                //resp.StatusCode = HttpStatusCode.NotFound

                return null;

            }

        }

        public Document FindObjectByTitle(string title, Document folder)
        {

            try
            {

                Document Result = null;

                DocumentsRequest documentsRequest = new DocumentsRequest(GetRequestSettings());
           
                Feed<Document> documentFeed;
              
                if(folder != null)  
                    documentFeed = documentsRequest.GetFolderContent(folder);
                else
                    documentFeed = documentsRequest.GetEverything();

                foreach (Document d in documentFeed.Entries)
                {

                    if (d.Title == title)
                    {
                        Result = d;

                        break;

                    }

                }

                return Result;

            }
            catch (GDataRequestException ex)
            {

                return null;

            }

        }

    }

}
