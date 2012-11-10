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

    }
}