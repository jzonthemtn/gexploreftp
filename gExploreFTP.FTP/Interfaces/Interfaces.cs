using System;
using System.Collections;
using System.Collections.Generic;
using Google.Documents;
using Google.GData.Documents;

namespace gExploreFTP.FTP.FileSystem.Interfaces
{

	public interface IFile
	{
		int Read(byte [] abData, int nDataSize);
		int Write(byte [] abData, int nDataSize);
		void Close();
	}

	public interface IFileInfo
	{
		System.DateTime GetModifiedTime();
		long GetSize();
		string GetAttributeString();
		bool IsDirectory();
	}

	public interface IFileSystem
	{

		Document OpenFile(string sPath);
        IFileInfo GetFileInfo(Document d);

        List<Document> GetDirectoryContents(string sPath);

		bool DirectoryExists(string sPath);
		bool FileExists(string sPath);
        
		bool CreateDirectory(string sPath);
		bool Move(string sOldPath, string sNewPath);
		bool Delete(string sPath);

        gExploreFTP.FTP.General.GoogleDocs getGoogleDocs();

        Document LastListedDirectory { get; set; }

	}

	public interface IFileSystemClassFactory
	{
		IFileSystem Create(string sUser, string sPassword);
	} 	
}
