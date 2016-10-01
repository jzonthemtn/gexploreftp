using System;
using gExploreFTP.FTP.General;
using Google.Documents;
using System.IO;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
	class AppendCommandHandler : FtpCommandHandler
	{
		private const int m_nBufferSize = 65536;

        public AppendCommandHandler(FtpConnectionObject connectionObject)
			: base("APPE", connectionObject)
		{

		}

		protected override string OnProcess(string sMessage)
		{

            return GetMessage(425, "Not implemented.");

			/*string sFile = GetPath(sMessage);

            Document d = ConnectionObject.FileSystemObject.OpenFile(sFile); //, true);

			if (d == null)
			{
				return GetMessage(425, "Couldn't open file");
			}
			
			FtpReplySocket socketReply = new FtpReplySocket(ConnectionObject);

			if (!socketReply.Loaded)
			{
				return GetMessage(425, "Error in establishing data connection.");
			}

			byte [] abData = new byte[m_nBufferSize];

			Assemblies.General.SocketHelpers.Send(ConnectionObject.Socket, GetMessage(150, "Opening connection for data transfer."));

			int nReceived = socketReply.Receive(abData);





			while (nReceived > 0)
			{
				nReceived = socketReply.Receive(abData);
				//file.Write(abData, nReceived);
                //d
			}

			//dfile.Close();
            //d
			socketReply.Close();

            Assemblies.General.GoogleDocs googleDocs = ConnectionObject.FileSystemObject.getGoogleDocs();

            Stream s = googleDocs.DownloadFile(d);

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
			
			return GetMessage(226, string.Format("Appended file successfully. ({0})", sFile));*/
		}
	}
}
