using System;
using gExploreFTP.FTP.FtpCommands.Base;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
	class XMkdCommandHandler : MakeDirectoryCommandHandlerBase
	{
		public XMkdCommandHandler(FtpConnectionObject connectionObject)
			: base("XMKD", connectionObject)
		{
			
		}
	}
}
