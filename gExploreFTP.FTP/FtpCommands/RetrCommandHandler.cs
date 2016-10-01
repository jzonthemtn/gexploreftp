using System;
using Google.Documents;
using System.IO;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
	/// <summary>
	/// Implements the RETR command
	/// </summary>
	class RetrCommandHandler : FtpCommandHandler
	{
		public RetrCommandHandler(FtpConnectionObject connectionObject)
			: base("RETR", connectionObject)
		{
			
		}

		protected override string OnProcess(string sMessage)
		{

            sMessage = "\\" + sMessage;

           // Console.WriteLine("RETR: " + sMessage);

			string sFilePath = GetPath(sMessage);
			
			if (!ConnectionObject.FileSystemObject.FileExists(sFilePath))
			{
				return GetMessage(550, "File doesn't exist");
			}

            gExploreFTP.FTP.General.GoogleDocs googleDocs = ConnectionObject.FileSystemObject.getGoogleDocs();

            Document d = googleDocs.FindObject(gExploreFTP.FTP.Utilities.ESENT.Get(googleDocs.getUserName(), sFilePath));

            string exportFormat = System.IO.Path.GetExtension(sFilePath).Replace(".", String.Empty); 
            if (exportFormat == String.Empty)
            {
                exportFormat = "pdf";
            }

            if (d != null)
            {

                // if (ConnectionObject.)
                //if (gExploreFTP.Common.Licensing.checkVerificationKey() == false)
                //{
                //    if (d.QuotaBytesUsed > 2048)
                //    {
                //        return GetMessage(425, "Unlicensed gExploreFTP supports max of 2 MB files.");
                //    }
                //}

                FtpReplySocket replySocket = new FtpReplySocket(ConnectionObject);

                if (!replySocket.Loaded)
                {
                    return GetMessage(550, "Unable to establish data connection");
                }

                gExploreFTP.FTP.General.SocketHelpers.Send(ConnectionObject.Socket, "150 Starting data transfer, please wait...\r\n");

                const int m_nBufferSize = 65536;

                Stream s = googleDocs.DownloadFile(d, exportFormat);

                if (d == null)
                {
                    return GetMessage(550, "Couldn't open file");
                }

                byte[] abBuffer = new byte[m_nBufferSize];

                int nRead = s.Read(abBuffer, 0, m_nBufferSize);

                while (nRead > 0 && replySocket.Send(abBuffer, nRead))
                {
                    nRead = s.Read(abBuffer, 0, m_nBufferSize);
                }

                s.Close();
                replySocket.Close();

                return GetMessage(226, "File download succeeded.");

            }
            else
            {
                return GetMessage(550, "File does not exist");
            }

		}

	}

}
