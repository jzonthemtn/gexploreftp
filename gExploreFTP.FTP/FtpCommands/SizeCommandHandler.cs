using System;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
	class SizeCommandHandler : FtpCommandHandler
	{
		public SizeCommandHandler(FtpConnectionObject connectionObject)
			: base("SIZE", connectionObject)
		{
			
		}

		protected override string OnProcess(string sMessage)
		{
			string sPath = GetPath(sMessage);

			if (!ConnectionObject.FileSystemObject.FileExists(sPath))
			{
				return GetMessage(550, string.Format("File doesn't exist ({0})", sPath));
			}

            // TODO: Need to use some internal mapping of Documents and ResourceIDs to common names.
            FileSystem.Interfaces.IFileInfo info = null;  //ConnectionObject.FileSystemObject.GetFileInfo(sPath);

			if (info == null)
			{
				return GetMessage(550, "Error in getting file information");
			}

			return GetMessage(220, info.GetSize().ToString());
		}
	}
}
