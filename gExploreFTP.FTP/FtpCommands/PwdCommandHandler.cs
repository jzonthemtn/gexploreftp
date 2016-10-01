using System;
using gExploreFTP.FTP.FtpCommands.Base;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
	/// <summary>
	/// Present working directory command handler
	/// </summary>
	class PwdCommandHandler : PwdCommandHandlerBase
	{
		public PwdCommandHandler(FtpConnectionObject connectionObject)
			: base("PWD", connectionObject)
		{
			
		}
	}
}
