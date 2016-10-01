using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Google.Documents;
using Google.GData.Documents;

namespace gExploreFTP.FTP.FileSystem.Interfaces.GoogleDocs
{
	class GoogleDocsFileSystemObject : IFileSystem
	{
		#region Member Variables
        
		private string m_sStartDirectory = "";
        private gExploreFTP.FTP.General.GoogleDocs m_GoogleGocs;

        private Document m_LastListedDirectory = null;

		#endregion

		#region Construction

        public gExploreFTP.FTP.General.GoogleDocs getGoogleDocs()
        {
            return m_GoogleGocs;
        }

        public GoogleDocsFileSystemObject(string sStartDirectory, gExploreFTP.FTP.General.GoogleDocs gd)
		{
			m_sStartDirectory = sStartDirectory;
            m_GoogleGocs = gd;
		}

		#endregion

		#region IFileSystem Members

        public Document LastListedDirectory
        {
            get
            {
                return m_LastListedDirectory;
            }
            set
            {
                m_LastListedDirectory = value;
            }
        }

		public Document OpenFile(string sPath)
		{

            Document d = m_GoogleGocs.FindObject(gExploreFTP.FTP.Utilities.ESENT.Get(m_GoogleGocs.getUserName(), sPath));

            return d;

		}

        public IFileInfo GetFileInfo(Document d)
		{

            bool isDocument = false;

            if(d.Type == Document.DocumentType.Folder)
                isDocument = true;

            GoogleDocsFileInfoObject info = new GoogleDocsFileInfoObject(d, d.ResourceId, d.Title, (long)d.QuotaBytesUsed, isDocument);

            return info;

		}

        public List<Document> GetDirectoryContents(string sPath)
		{

            //Console.WriteLine("GetDirectoryContents sPath = " + sPath + ".");

            List<Document> asFiles = new List<Document>();

            // Get a directory listing from Google Docs.

            if (sPath == "\\")
            {
                
                m_GoogleGocs.Request = new DocumentsRequest(m_GoogleGocs.GetRequestSettings());
                Google.GData.Client.Feed<Document> folders = m_GoogleGocs.Request.GetEverything();

                foreach (Document d in folders.Entries)
                {

                    // Only get top-level folders.
                    if (d.ParentFolders.Count == 0)
                    {
                            
                        // Add it to the list to return.
                        asFiles.Add(d);

                        // Add it to the collection of title <--> resource IDs.
                        if (System.IO.Path.GetExtension(d.Title) == String.Empty)
                        {
                            
                            if (d.Type == Document.DocumentType.Folder)
                            {
                                gExploreFTP.FTP.Utilities.ESENT.Add(m_GoogleGocs.getUserName(), sPath + d.Title, d.ResourceId);
                            }
                            else
                            {
                                // Show files without extensions as being PDFs.
                                gExploreFTP.FTP.Utilities.ESENT.Add(m_GoogleGocs.getUserName(), sPath + d.Title + ".pdf", d.ResourceId);
                            }
                            
                        }
                        else
                        {
                            gExploreFTP.FTP.Utilities.ESENT.Add(m_GoogleGocs.getUserName(), sPath + d.Title, d.ResourceId);
                        }

                    }

                }

            }

            else
            {

                // The path may be deep.

                string[] path = sPath.Split('\\');

                // The current directory to look in (starting at the root).
                Document current = null;

                // For each directory, find it.

                foreach (string s in path)
                {

                    // Find the document in Google Docs in this folder that has this same name.
                    Document d = m_GoogleGocs.FindObjectByTitle(s, current);

                    // Make current be the next directory.
                    current = d;

                }

                // current is the directory. If null then it does not exist.

                // We want to find the files in a specific folder.
                //Document d = m_GoogleGocs.FindObject(gExploreFTP.FTP.Utilities.ESENT.Get(sPath));

                if (current == null)
                {

                    // The directory was not found.
                    //return GetMessage(550, "LIST unable to establish return connection.");
                    return null;

                }
                else
                {

                    // Remember the last directory we looked at. This is used when uploading to a directory.
                    m_LastListedDirectory = current; // d;

                    //Console.WriteLine("ResourceID: " + current.ResourceId);

                    m_GoogleGocs.Request = new DocumentsRequest(m_GoogleGocs.GetRequestSettings());
                    Google.GData.Client.Feed<Document> folders = m_GoogleGocs.Request.GetFolderContent(current);

                    foreach (Document doc in folders.Entries)
                    {

                        // Add it to the list to return.
                        asFiles.Add(doc);

                        // Add it to the collection of title <--> resource IDs.
                        if (System.IO.Path.GetExtension(doc.Title) == String.Empty)
                        {

                            if (doc.Type == Document.DocumentType.Folder)
                            {
                                gExploreFTP.FTP.Utilities.ESENT.Add(m_GoogleGocs.getUserName(), sPath + "\\" + doc.Title, doc.ResourceId);
                            }
                            else
                            {
                                // Show files without extensions as being PDFs.
                                gExploreFTP.FTP.Utilities.ESENT.Add(m_GoogleGocs.getUserName(), sPath + doc.Title + ".pdf", doc.ResourceId);
                            }

                        }
                        else
                        {
                            gExploreFTP.FTP.Utilities.ESENT.Add(m_GoogleGocs.getUserName(), sPath + "\\" + doc.Title, doc.ResourceId);
                        }


                    }

                }

            }


            return asFiles;

		}

        public bool DirectoryExists(string sFile)
		{

            return FileExists(sFile);

		}

        public bool FileExists(string sFile)
		{

            Document doc = m_GoogleGocs.FindObject(gExploreFTP.FTP.Utilities.ESENT.Get(m_GoogleGocs.getUserName(), sFile));

            if (doc != null)
            {
                return true;
            }
            else
            {
                return false;
            }

		}

		public bool Move(string sOldPath, string sNewPath)
		{

            // TODO

			return true;

		}

		public bool Delete(string sFile)
		{

            try
            {

                Document doc = m_GoogleGocs.FindObject(gExploreFTP.FTP.Utilities.ESENT.Get(m_GoogleGocs.getUserName(), sFile));

                //Console.WriteLine("sFile = " + sFile);
                //Console.WriteLine("Deleting: " + doc.Title);
                m_GoogleGocs.Request.Delete(new Uri(String.Format("http://docs.google.com/feeds/default/private/full/{0}?delete=false", doc.ResourceId)), doc.ETag);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }

		}

		public bool CreateDirectory(string sPath)
		{

            try
            {

                // We only need the last directory and not the whole path.
                string[] fullPath = sPath.Split('\\');
                sPath = fullPath[fullPath.Length - 1];

                //Console.WriteLine("MKDIR " + sPath);

                Uri feedUri;

                if (m_LastListedDirectory == null)
                {
                    feedUri = new Uri("https://docs.google.com/feeds/default/private/full/?convert=false");
                }
                else
                {
                    feedUri = new Uri(m_LastListedDirectory.DocumentEntry.SelfUri.ToString() + "/contents");
                }

                DocumentEntry d = new DocumentEntry();
                d.Title = new Google.GData.Client.AtomTextConstruct(Google.GData.Client.AtomTextConstructElementType.Title, sPath);
                d.Categories.Add(DocumentEntry.FOLDER_CATEGORY);

                m_GoogleGocs.Service.Insert(feedUri, d);

                return true;

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error creating directory." + ex);
                return false;
            }

		}

		#endregion

		private void RemovePath(string [] asFiles, string sPath)
		{
			int nIndex = 0;

			string sPathLowerCase = sPath.ToLower();

			foreach (string sString in asFiles)
			{
				if (sString.Substring(0, sPath.Length).ToLower() == sPathLowerCase)
				{
					string sFileName = sString.Substring(sPath.Length);

					if (sFileName.Length > 0 && sFileName[0] == '\\')
					{
						sFileName = sFileName.Substring(1);
					}

					asFiles[nIndex] = sFileName;
				}

				nIndex += 1;
			}
		}
	}
}
