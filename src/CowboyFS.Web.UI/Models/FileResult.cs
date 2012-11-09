namespace CowboyFS.Web.UI.Models
{
    public class FileResult
    {
        public FileResult(string relativePathName, FileType fileType)
        {
            FileType = fileType;
            RelativePathName = relativePathName;
            FileName = relativePathName.Substring(relativePathName.LastIndexOf(@"\") + 1);
        }

        public string RelativePathName { get; private set; }
        public string FileName { get; private set; }
        public FileType FileType { get; private set; }
    }
}