using System;
using System.IO;

namespace CowboyFS.Web.UI.Models
{
    public class Library
    {
        public int LibraryId { get; private set; }
        public string DisplayName { get; private set; }
        public string FullPathName { get; private set; }

        public Library(int id, string fullPathName, string displayName)
        {
            FullPathName = fullPathName;
            DisplayName = displayName;
            LibraryId = id;
        }

        public string MakePathRelativeToLibrary(string fullPathName)
        {
            return fullPathName.Replace(FullPathName, "");
        }

        public string MakePathAbsoluteFromLibrary(string relPath)
        {
            return FullPathName + relPath;
        }

        public bool IsDescendant(string file)
        {
            if (file.Equals(FullPathName, StringComparison.InvariantCultureIgnoreCase))
                return true;

            var parent = Directory.GetParent(file);
            
            if (parent == null)
                return false;
            if (parent.FullName.Equals(FullPathName, StringComparison.InvariantCultureIgnoreCase))
                return true;
            return IsDescendant(parent.FullName);
        }
    }
}