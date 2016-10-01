using System;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
	/// <summary>
	/// Delete command handler
	/// </summary>
	class DeleCommandHandler : FtpCommandHandler
	{
		public DeleCommandHandler(FtpConnectionObject connectionObject)
			: base("DELE", connectionObject)
		{
			
		}

		protected override string OnProcess(string sMessage)
		{
           
            string sFile;

            if(ConnectionObject.FileSystemObject.LastListedDirectory == null)
                sFile = "\\" + sMessage; 
            else
                sFile = "\\" + ConnectionObject.FileSystemObject.LastListedDirectory.Title + "\\" + sMessage; 

            //Console.WriteLine("Deleting file: " + sFile);

			if (!ConnectionObject.FileSystemObject.FileExists(sFile))
			{
				return GetMessage(550, "File does not exist.");
			}

			if (!ConnectionObject.FileSystemObject.Delete(sFile))
			{
				return GetMessage(550, "Couldn't delete file.");
			}

			return GetMessage(250, "File deleted successfully");

		}

	}

}
