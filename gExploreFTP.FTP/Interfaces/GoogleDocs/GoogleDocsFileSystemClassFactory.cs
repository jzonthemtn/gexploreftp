using System;

namespace gExploreFTP.FTP.FileSystem.Interfaces.GoogleDocs
{
	public class GoogleDocsFilesClassFactory : IFileSystemClassFactory
	{

        public GoogleDocsFilesClassFactory()
		{

		}

		#region IFileSystemClassFactory Members

		public IFileSystem Create(string sUser, string sPassword)
		{

            // Validate the username and password by sending it to Google Docs.

            gExploreFTP.FTP.General.GoogleDocs gd = new gExploreFTP.FTP.General.GoogleDocs(sUser, sPassword);
            bool retval = gd.doLogin();

            if (retval == true)
            {
                return new GoogleDocs.GoogleDocsFileSystemObject("\\", gd);
            }
            else
            {
                return null;
            }

		}

		#endregion
	}
}
