using System.Collections.Generic;
using System.IO;
using System.Linq;
using CowboyFS.Web.UI.Models;

namespace CowboyFS.Web.UI.Services
{
    public class FileRepository
    {

        public bool TryGetLibrary(int? libraryId, out Library library)
        {
            if (!libraryId.HasValue)
            {
                library = null;
                return false;
            }

            library = FetchLibraries().First(x => x.LibraryId == libraryId);
            return (library != null);
        }

        public IEnumerable<Library> FetchLibraries()
        {
            yield return new Library(1, @"D:\Dropbox","Dropbox");
            yield return new Library(2, @"D:\Documents", "My Documents");
            yield return new Library(3, @"D:\Sql", "Databases");
            yield return new Library(4, @"D:\Temp", "Temp");
        }

        public IEnumerable<FileResult> FetchResultsForPath(Library library, string relativePath)
         {
             string fullPath = string.Format("{0}{1}",library.FullPathName,relativePath);

             var folders = FileSystemEnumerator.GetDirectories(fullPath, "*.*");
             foreach (string r in folders)
             {

                 yield return new FileResult(library.MakePathRelativeToLibrary(r), FileType.Folder);
             }

             var files = FileSystemEnumerator.GetFiles(fullPath, "*.*");
             foreach (string r in files)
             {
                 yield return new FileResult(library.MakePathRelativeToLibrary(r), FileType.UnknownFile);
             }

         }

    }
}