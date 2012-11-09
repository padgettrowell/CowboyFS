using System.Collections.Generic;
using System.Linq;
using CowboyFS.Web.UI.Models;

namespace CowboyFS.Web.UI.ViewModels
{
    public class BrowseViewModel
    {
        public string RelativePath { get; private set; }
        public Library CurrentLibrary { get; private set; }

        public IEnumerable<FileResult> Folders
        {
            get { return Results.Where(x => x.FileType == FileType.Folder); }
        }

        public IEnumerable<FileResult> Files
        {
            get { return Results.Where(x => x.FileType != FileType.Folder); }
        }

        public BrowseViewModel(Library currentLibrary, string relativePath, IEnumerable<FileResult> results)
        {
            CurrentLibrary = currentLibrary;
            RelativePath = relativePath;
            Results = results;
        }

        private IEnumerable<FileResult> Results { get; set; }
    }
}