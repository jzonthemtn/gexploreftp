using System;
using Google.Documents;
using System.IO;
using Google.GData.Client.ResumableUpload;
using Google.GData.Client;
using gExploreFTP.FTP.FtpServer;

namespace gExploreFTP.FTP.FtpCommands
{
    class StoreCommandHandler : FtpCommandHandler
    {
        private const int m_nBufferSize = 65536;

        public StoreCommandHandler(FtpConnectionObject connectionObject)
            : base("STOR", connectionObject)
        {

        }

        protected override string OnProcess(string sMessage)
        {

            //Console.WriteLine("STOR sMessage = " + sMessage);

            string sFile = sMessage; // GetPath(sMessage);//.Replace("\\", "");

            // TODO:
            /*if (ConnectionObject.FileSystemObject.FileExists(sFile))
            {
                return GetMessage(553, "File already exists.");
            }*/

            FtpReplySocket socketReply = new FtpReplySocket(ConnectionObject);

            if (!socketReply.Loaded)
            {
                return GetMessage(425, "Error in establishing data connection.");
            }

            string tempFileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), sFile);
            FileStream fs = new FileStream(tempFileName, FileMode.Create);

            //Console.WriteLine("Uploading " + sFile + " to " + tempFileName);

            byte[] abData = new byte[m_nBufferSize];

            gExploreFTP.FTP.General.SocketHelpers.Send(ConnectionObject.Socket, GetMessage(150, "Opening connection for data transfer."));

            int nReceived = socketReply.Receive(abData);

            while (nReceived > 0)
            {
                fs.Write(abData, 0, nReceived);
                nReceived = socketReply.Receive(abData);
            }

            fs.Flush();
            fs.Close();
            fs.Dispose();

            // Upload it.

            try
            {

                Uri createUploadUrl;

                if (ConnectionObject.FileSystemObject.LastListedDirectory == null)
                {
                    //Console.WriteLine("Uploading to root directory.");
                    createUploadUrl = new Uri("https://docs.google.com/feeds/upload/create-session/default/private/full?convert=false");
                }
                else
                {
                    //Console.WriteLine("Uploading to directory: " + ConnectionObject.FileSystemObject.LastListedDirectory.Title);
                    createUploadUrl = new Uri("https://docs.google.com/feeds/upload/create-session/default/private/full/" + ConnectionObject.FileSystemObject.LastListedDirectory.ResourceId + "/contents?convert=false");
                }

                Document entry = new Document();
                entry.Title = Path.GetFileName(sFile);
                entry.MediaSource = new MediaFileSource(tempFileName, "application/octet-stream");

                //Console.WriteLine("Uploading " + entry.Title + "   "  + sFile);

                AtomLink link = new AtomLink(createUploadUrl.ToString());
                link.Rel = ResumableUploader.CreateMediaRelation;
                entry.DocumentEntry.Links.Add(link);

                // TODO: Don't hardcode the product name.
                ClientLoginAuthenticator cla = new ClientLoginAuthenticator("gExploreFTP", ServiceNames.Documents, ConnectionObject.FileSystemObject.getGoogleDocs().getUserName(), ConnectionObject.FileSystemObject.getGoogleDocs().getPassword());

                ResumableUploader ru = new ResumableUploader(1);
                ru.InsertAsync(cla, entry.DocumentEntry, new object());

                socketReply.Close();

                // Return success.

                return GetMessage(226, "Uploaded file successfully.");

            }
            catch (Exception ex)
            {
                return GetMessage(425, "Error uploading file.");
            }

        }

    }

}
