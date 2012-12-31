using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
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
            NameValueCollection configuredLibries = (NameValueCollection)ConfigurationManager.GetSection("libraries");

            for (int i = 0; i < configuredLibries.Count; i++)
            {
                yield return new Library(i + 1, configuredLibries.GetValues(i)[0], configuredLibries.Keys[i]);
            }
        }

        public IEnumerable<FileResult> FetchResultsForPath(Library library, string relativePath)
        {
            string fullPath = string.Format("{0}{1}", library.FullPathName, relativePath);

            if (!library.IsDescendant(fullPath))
                throw new HttpException(403,"No no no!");

            var folders = FileSystemEnumerator.GetDirectories(fullPath, "*.*");
            foreach (string r in folders)
            {

                yield return new FileResult(library.MakePathRelativeToLibrary(r), true);
            }

            var files = FileSystemEnumerator.GetFiles(fullPath, "*.*");
            foreach (string r in files)
            {
                yield return new FileResult(library.MakePathRelativeToLibrary(r), false);
            }

        }

       
    }
}