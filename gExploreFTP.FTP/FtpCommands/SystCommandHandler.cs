using System;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
    class SYSTCommandHandler : FtpCommandHandler
    {
        public SYSTCommandHandler(FtpConnectionObject connectionObject)
            : base("SYST", connectionObject)
        {

        }

        protected override string OnProcess(string sMessage)
        {
            return GetMessage(220, "gExploreFTP Type: L8");
        }
    }
}
