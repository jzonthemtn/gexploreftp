using System;
using System.IO;
using System.Collections;
using Google.Documents;
using System.Collections.Generic;
using gExploreFTP.FTP.FtpServer;
using gExploreFTP.FTP.Utilities;

namespace gExploreFTP.FTP.FtpCommands.Base
{
	/// <summary>
	/// Base class for list commands
	/// </summary>
	abstract class ListCommandHandlerBase : FtpCommandHandler
	{
		public ListCommandHandlerBase(string sCommand, FtpConnectionObject connectionObject)
			: base(sCommand, connectionObject)
		{
			
		}

		protected override string OnProcess(string sMessage)
		{
            gExploreFTP.FTP.General.SocketHelpers.Send(ConnectionObject.Socket, "150 Opening data connection for LIST\r\n");

			List<Document> asFiles = null;

			sMessage = sMessage.Trim();

			string sPath = GetPath("");
			
			asFiles = ConnectionObject.FileSystemObject.GetDirectoryContents(sPath);

			FtpReplySocket socketReply = new FtpReplySocket(ConnectionObject);

			if (!socketReply.Loaded)
			{
				return GetMessage(550, "LIST unable to establish return connection.");
			}

            if (asFiles == null)
            {
                // The directory requested does not exist.
                return GetMessage(550, "LIST unable to list files (no such directory).");
            }

            string sFileList = BuildReply(sMessage, asFiles);

			socketReply.Send(sFileList);
			socketReply.Close();

			return GetMessage(226, "LIST successful.");

		}

        protected abstract string BuildReply(string sMessage, List<Document> asFiles);

        protected string BuildShortReply(List<Document> asFiles)
		{

            string sFileList = "";

            foreach (Document d in asFiles)
            {
                if (System.IO.Path.GetExtension(d.Title) == String.Empty)
                {
                    // Show files without extensions as being PDFs.
                    sFileList += d.Title + ".pdf\r\n";
                }
                else
                {
                    sFileList += d.Title + "\r\n";
                }
                
            }

			//string sFileList = string.Join("\r\n", asFiles);
			sFileList += "\r\n";
			return sFileList;

		}

		protected string BuildLongReply(List<Document> asFiles)
		{

			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

            //for (int nIndex = 0 ; nIndex < asFiles.Count; nIndex++)
            foreach (Document d in asFiles)
            {

                FileSystem.Interfaces.IFileInfo info = ConnectionObject.FileSystemObject.GetFileInfo(d);

                if (info != null)
                {
                    string sAttributes = info.GetAttributeString();
                    stringBuilder.Append(sAttributes);
                    stringBuilder.Append(" 1 owner group ");
                    //stringBuilder.Append(" ");

                    if (info.IsDirectory())
                    {
                        stringBuilder.Append("            1 ");
                    }
                    else
                    {
                        string sFileSize = info.GetSize().ToString();
                        stringBuilder.Append(Utilities.TextHelpers.RightAlignString(sFileSize, 15, ' '));
                        stringBuilder.Append(" ");
                    }

                    System.DateTime fileDate = info.GetModifiedTime();

                    //string sDay = fileDate.Day.ToString();

                    //stringBuilder.Append(fileDate.Year.ToString());
                    //stringBuilder.Append(" ");
                    stringBuilder.Append(fileDate.Year.ToString());
                    //General.TextHelpers.Month(fileDate.Month));
                    stringBuilder.Append("-");
                    stringBuilder.Append(fileDate.Month.ToString("D2"));
                    stringBuilder.Append("-");
                    stringBuilder.Append(fileDate.Day.ToString("D2"));
                    stringBuilder.Append(" ");
                    stringBuilder.Append(string.Format("{0:hh}", fileDate));
                    stringBuilder.Append(":");
                    stringBuilder.Append(string.Format("{0:mm}", fileDate));
                    stringBuilder.Append(" ");

                    //stringBuilder.Append(d.Title); //asFiles[nIndex]);

                    if (System.IO.Path.GetExtension(d.Title) == String.Empty)
                    {

                        if (d.Type == Document.DocumentType.Folder)
                        {
                            stringBuilder.Append(d.Title); 
                        }
                        else
                        {
                            // Show files without extensions as being PDFs.
                            stringBuilder.Append(d.Title + ".pdf"); 
                        }

                    }
                    else
                    {
                        stringBuilder.Append(d.Title); //asFiles[nIndex]);
                    }

                    stringBuilder.Append("\r\n");

        }

            }

			return stringBuilder.ToString();
		}
	}
}
