using System;
using System.IO;
using gExploreFTP.FTP.FtpCommands.Base;
using gExploreFTP.FTP.FtpServer;
using Google.Documents;

namespace gExploreFTP.FTP.FtpCommands
{
	/// <summary>
	/// Present working directory command handler
	/// </summary>
	class CDUPCommandHandler : PwdCommandHandlerBase
	{
        public CDUPCommandHandler(FtpConnectionObject connectionObject)
			: base("CDUP", connectionObject)
		{
			
		}

        // TODO: Move up a directory.
        protected override string OnProcess(string sMessage)
        {

            /*sMessage = sMessage.Replace('/', '\\');

            if (!Utilities.FileNameHelpers.IsValid(sMessage))
            {
                return GetMessage(550, "Not a valid directory string.");
            }

            string sDirectory = GetPath(sMessage);

            if (!ConnectionObject.FileSystemObject.DirectoryExists(sDirectory))
            {
                return GetMessage(550, "Not a valid directory.");
            }

            if (ConnectionObject.FileSystemObject.LastListedDirectory.ParentFolders.Count > 0)
            {
               
                string parentFolderResoureId = ConnectionObject.FileSystemObject.LastListedDirectory.ParentFolders[0];
                Document parent = ConnectionObject.FileSystemObject.getGoogleDocs().FindObject(parentFolderResoureId);
                ConnectionObject.FileSystemObject.LastListedDirectory = parent;

            }
            else
            {

                // The parent is the root folder.
                //string parentFolderResoureId = ConnectionObject.FileSystemObject.LastListedDirectory.ParentFolders[0];
                //Document parent = ConnectionObject.FileSystemObject.getGoogleDocs().FindObject(parentFolderResoureId);
                ConnectionObject.FileSystemObject.LastListedDirectory = null;

            }

            //ConnectionObject.CurrentDirectory = Path.Combine(ConnectionObject.CurrentDirectory, sMessage);
            return GetMessage(250, string.Format("CWD Successful ({0})", ConnectionObject.CurrentDirectory.Replace("\\", "/")));
            */

            return GetMessage(550, "CDUP command is not implemented. Paths must be navigated to directly.");

        }

	}

}
