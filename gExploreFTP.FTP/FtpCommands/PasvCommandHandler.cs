using System;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
	class PasvCommandHandler : FtpCommandHandler
	{
		const int m_nPort = 30;

		public PasvCommandHandler(FtpConnectionObject connectionObject)
			: base("PASV", connectionObject)
		{
            //Console.WriteLine("Received PASV");
		}

		protected override string OnProcess(string sMessage)
		{
			/*if (ConnectionObject.PasvSocket == null)
			{

                System.Net.Sockets.TcpListener listener = gExploreFTP.FTP.General.SocketHelpers.CreateTcpListener(m_nPort);

				if (listener == null)
				{
					return GetMessage(550, string.Format("Couldn't start listener on port {0}", m_nPort));
				}
                
				SendPasvReply();
				listener.Start();

                //Console.WriteLine("Started listender on port " + m_nPort.ToString());
                
				ConnectionObject.PasvSocket = listener.AcceptTcpClient();

                //Console.WriteLine("4");

				listener.Stop();

                //Console.WriteLine("5");

				return "";

			}
			else
			{
              
				SendPasvReply();
				return "";
			}*/

            return GetMessage(550, "PASV is not yet supported.");

		}

		private void SendPasvReply()
		{

            string sIpAddress = gExploreFTP.FTP.General.SocketHelpers.GetLocalAddress().ToString();
			sIpAddress = sIpAddress.Replace('.', ',');
			sIpAddress += ',';
			sIpAddress += "0";
			sIpAddress += ',';
			sIpAddress += m_nPort.ToString();
			//Assemblies.General.SocketHelpers.Send(ConnectionObject.Socket, string.Format("227 ={0}\r\n", sIpAddress));

            string response = string.Format("227 Entering Passive Mode ({0})\r\n", sIpAddress);

            gExploreFTP.FTP.General.SocketHelpers.Send(ConnectionObject.Socket, response);

            //Console.WriteLine("Passive reply: " + response);

		}

	}
}
