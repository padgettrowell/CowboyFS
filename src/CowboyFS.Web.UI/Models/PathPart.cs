namespace CowboyFS.Web.UI.Models
{
    public class PathPart
    {
        public string RelativePath { get; private set; }
        public string Display { get; private set; }

        public PathPart(string relativePath, string display)
        {
            RelativePath = relativePath;
            Display = display;
        }
    }
}