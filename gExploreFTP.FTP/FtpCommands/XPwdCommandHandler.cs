using System;
using gExploreFTP.FTP.FtpCommands.Base;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
	/// <summary>
	/// Present working directory command handler
	/// </summary>
	class XPwdCommandHandler : PwdCommandHandlerBase
	{
		public XPwdCommandHandler(FtpConnectionObject connectionObject)
			: base("XPWD", connectionObject)
		{
			
		}
	}
}
