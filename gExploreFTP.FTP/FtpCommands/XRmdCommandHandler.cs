using System;
using gExploreFTP.FTP.FtpCommands.Base;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
    class XRmdCommandHandler : FtpCommandHandler
    {

        public XRmdCommandHandler(FtpConnectionObject connectionObject)
            : base("XRMD", connectionObject)
        {

        }

        protected override string OnProcess(string sMessage)
        {

            string sFile = "\\" + sMessage; // GetPath(sMessage);
            //Console.WriteLine("Deleting directory: " + sFile);

            if (!ConnectionObject.FileSystemObject.FileExists(sFile))
            {
                return GetMessage(550, "File does not exist.");
            }

            if (!ConnectionObject.FileSystemObject.Delete(sFile))
            {
                return GetMessage(550, "Couldn't delete file.");
            }

            return GetMessage(250, "File deleted successfully");
        }

    }

}
