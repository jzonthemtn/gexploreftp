using System;
using Google.Documents;

namespace gExploreFTP.FTP.FileSystem.Interfaces.GoogleDocs
{
	class GoogleDocsFileInfoObject : gExploreFTP.FTP.General.LoadedClass, IFileInfo
	{
		#region Member Variables

        private long m_size = 0;
        private string m_title;
        private string m_documentResourceID;
        private bool m_isDirectory = false;
        private Document p_d;

		#endregion

		#region Construction

        public GoogleDocsFileInfoObject(Document d, string documentResourceID, string title, long size, bool isDirectory)
		{

            m_documentResourceID = documentResourceID;
            m_title = title;
            m_size = size;
            m_isDirectory = isDirectory;

		}

		#endregion

		#region IFileInfo Members

		public bool IsDirectory()
		{
            return m_isDirectory;
		}

		public DateTime GetModifiedTime()
		{
            // TODO:
			//return m_theInfo.LastWriteTime;
            return DateTime.Now;
		}

		public long GetSize()
		{
            return m_size;
		}

		public string GetAttributeString()
		{

			System.Text.StringBuilder builder = new System.Text.StringBuilder();

            if (m_isDirectory)
			{
				builder.Append("d");
			}
			else
			{
				builder.Append("-");
			}

			builder.Append("r");
			
			/*if (fReadOnly)
			{
				builder.Append("-");
			}
			else
			{*/
				builder.Append("w");
			//}

            if (m_isDirectory)
			{
				builder.Append("x");
			}
			else
			{
				builder.Append("-");
			}

            if (m_isDirectory)
			{
				builder.Append("r-xr-x");
			}
			else
			{
				builder.Append("r--r--");
			}

			return builder.ToString();
		}

		#endregion
	}
}
