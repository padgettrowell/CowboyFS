using System;
using System.Collections.Generic;
using System.Linq;
using CowboyFS.Web.UI.Models;

namespace CowboyFS.Web.UI.ViewModels
{
    public class BrowseViewModel
    {
        public string RelativePath { get; private set; }
        public Library CurrentLibrary { get; private set; }
        public IList<PathPart> PathCrumbs { get; private set; }
        private IEnumerable<FileResult> Results { get; set; }

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
            PathCrumbs = GetAllSubPathsContainedWithinRelativePath(relativePath);
        }

        private List<PathPart> GetAllSubPathsContainedWithinRelativePath(string relativePath)
        {
            List<PathPart> paths = new List<PathPart>();

            if (string.IsNullOrEmpty(relativePath) || relativePath.Length < 1)
                return paths;

            var parts = relativePath.Split(new char[] {'\\'}, StringSplitOptions.RemoveEmptyEntries);
            string lastPath = string.Empty;

            for (int i = 0; i < parts.Length; i++)
            {
                string thisDisplay = parts[i];
                string thisPath = lastPath + "\\" + thisDisplay;

                paths.Add(new PathPart(thisPath,thisDisplay));

                lastPath = thisPath;
            }
            return paths;
        }
        
    }
}