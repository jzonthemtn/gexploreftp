using System;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
	class AlloCommandHandler : FtpCommandHandler
	{
		public AlloCommandHandler(FtpConnectionObject connectionObject)
			: base("ALLO", connectionObject)
		{
			
		}

		protected override string OnProcess(string sMessage)
		{
			return GetMessage(202, "Allo processed successfully (depreciated).");
		}

	}
}
