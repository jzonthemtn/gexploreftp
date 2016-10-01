using System;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
	/// <summary>
	/// Starts a rename file operation
	/// </summary>
	class RenameStartCommandHandler : FtpCommandHandler
	{
		public RenameStartCommandHandler(FtpConnectionObject connectionObject)
			: base("RNFR", connectionObject)
		{
			
		}

		protected override string OnProcess(string sMessage)
		{
			string sFile = GetPath(sMessage);

			ConnectionObject.FileToRename = sFile;

            // TODO: Need to use some internal mapping of Documents and ResourceIDs to common names.
            FileSystem.Interfaces.IFileInfo info = null; // ConnectionObject.FileSystemObject.GetFileInfo(sFile);

			if (info == null)
			{
				return GetMessage(550, string.Format("File does not exist ({0}).", sFile));
			}
			
			ConnectionObject.RenameDirectory = info.IsDirectory();
			return GetMessage(350, string.Format("Rename file started ({0}).", sFile));
		} 
	}
}
