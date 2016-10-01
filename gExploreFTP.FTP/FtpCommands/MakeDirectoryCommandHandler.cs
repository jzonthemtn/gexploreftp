using System;
using gExploreFTP.FTP.FtpCommands.Base;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
	class MakeDirectoryCommandHandler : MakeDirectoryCommandHandlerBase
	{
		public MakeDirectoryCommandHandler(FtpConnectionObject connectionObject)
			: base("MKD", connectionObject)
		{
			
		}
	}
}
