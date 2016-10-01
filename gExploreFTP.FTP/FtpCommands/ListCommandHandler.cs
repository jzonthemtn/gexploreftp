using System;
using System.Collections;
using System.Collections.Generic;
using Google.Documents;
using gExploreFTP.FTP.FtpCommands.Base;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
	class ListCommandHandler : ListCommandHandlerBase
	{
		public ListCommandHandler(FtpConnectionObject connectionObject)
			: base("LIST", connectionObject)
		{
			
		}

		protected override string BuildReply(string sMessage, List<Document> asFiles)
		{
			return BuildLongReply(asFiles);
		}
	}
}
