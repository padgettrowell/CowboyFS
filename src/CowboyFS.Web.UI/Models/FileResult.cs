namespace CowboyFS.Web.UI.Models
{
    public class FileResult
    {
        public FileResult(string relativePathName, bool isFolder = false)
        {
            IsFolder = isFolder;
            RelativePathName = relativePathName;
            FileName = relativePathName.Substring(relativePathName.LastIndexOf(@"\") + 1);
        }

        public string RelativePathName { get; private set; }
        public string FileName { get; private set; }
        public bool IsFolder { get; private set; }

        public string IconPath
        {
            get
            {
                if (IsFolder)
                    return "folder.png";

                if (FileName.IndexOf(".") == -1)
                    return "file.png";

                var extension = FileName.Substring(FileName.LastIndexOf(".")).ToLower();
                switch (extension)
                {
                    case ".pdf":
                        return "pdf.png";
                    case ".exe":
                        return "application.png";
                    case ".xls":
                    case ".xlsx":
                    case ".csv":
                        return "excel.png";
                    case ".avi":
                    case ".mkv":
                    case ".mp4":
                    case ".mov": 
                        return "film.png";
                    case ".htm":
                    case ".html":
                    case ".mht":
                    case ".mxl":
                        return "html.png";
                    case ".bmp":
                    case ".jpg":
                    case ".jpeg":
                    case ".gif":
                    case ".png":
                        return "image.png";
                    case ".mp3":
                    case ".flac":
                    case ".wav":
                    case ".ape":
                        return "music.png";
                    case ".ppt":
                    case ".pptx": 
                        return "powerpoint.png";
                    case ".txt": 
                    case ".config":
                    case ".ini":
                    case ".rtf": 
                        return "text.png";
                    case ".doc":
                    case ".docx":
                        return "word.png";
                    case ".zip":
                    case ".7z":
                        return "zip.png";
                    default:
                        return "file.png";

                }
            }
        }
    }
}