using System;
using System.Collections;
using System.Collections.Generic;
using Google.Documents;
using gExploreFTP.FTP.FtpCommands.Base;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
	class NlstCommandHandler : ListCommandHandlerBase
	{
		public NlstCommandHandler(FtpConnectionObject connectionObject)
			: base("NLST", connectionObject)
		{
			
		}

        protected override string BuildReply(string sMessage, List<Document> asFiles)
		{
			if (sMessage == "-L" || sMessage == "-l")
			{
				return BuildLongReply(asFiles);
			}
			else
			{
				return BuildShortReply(asFiles);
			}
		}
	}
}
