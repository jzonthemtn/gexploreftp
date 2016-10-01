using System;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
	class NoopCommandHandler : FtpCommandHandler
	{
		public NoopCommandHandler(FtpConnectionObject connectionObject)
			: base("NOOP", connectionObject)
		{
			
		}

		protected override string OnProcess(string sMessage)
		{
			return GetMessage(200, "");
		}
	}
}
