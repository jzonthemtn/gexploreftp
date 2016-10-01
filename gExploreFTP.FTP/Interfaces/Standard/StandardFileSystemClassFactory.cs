using System;

namespace Assemblies.Ftp.FileSystem.Interfaces.Standard
{
	public class StandardFileSystemClassFactory : IFileSystemClassFactory
	{
		public StandardFileSystemClassFactory()
		{
			
		}

		#region IFileSystemClassFactory Members

		public IFileSystem Create(string sUser, string sPassword)
		{
			if (UserData.Get().HasUser(sUser) && UserData.Get().GetUserPassword(sUser) == sPassword)
			{
                return new AWS.S3FileSystemObject(UserData.Get().GetUserStartingDirectory(sUser));
			}
			else
			{
				return null;
			}
		}

		#endregion
	}
}
