using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace CowboyFS.Web.UI.Services
{

    // Original code for this stolen from:  http://codepaste.net/msm8b1

    internal class FileSystemEnumerator
    {
        private static string makePath(string path, string searchPattern)
        {
            if (!path.EndsWith("\\"))
                path += "\\";
            return Path.Combine(path, searchPattern);
        }

        private static IEnumerable<string> getInternal(string path, string searchPattern, bool isGetDirs)
        {
            WIN32_FIND_DATA findData;

            IntPtr findHandle = FindFirstFile(makePath(path, searchPattern), out findData);

            if (findHandle == INVALID_HANDLE_VALUE)
                yield break;
            try
            {
                do
                {
                    if (findData.cFileName == "." || findData.cFileName == "..")
                        continue;
                    if (findData.dwFileAttributes.HasFlag(FileAttributes.Hidden) || findData.dwFileAttributes.HasFlag(FileAttributes.System))
                        continue;
                    if (isGetDirs
                            ? (findData.dwFileAttributes & FileAttributes.Directory) != 0
                            : (findData.dwFileAttributes & FileAttributes.Directory) == 0)
                        yield return Path.Combine(path, findData.cFileName);
                } while (FindNextFile(findHandle, out findData));
            }
            finally
            {
                FindClose(findHandle);
            }
        }


        public static IEnumerable<string> GetFiles(string path, string searchPattern)
        {
            return getInternal(path, searchPattern, false);
        }


        public static IEnumerable<string> GetDirectories(string path, string searchPattern)
        {
            return getInternal(path, searchPattern, true);
        }


        public static IEnumerable<string> GetAllDirectories(string path)
        {
            foreach (string subDir in GetDirectories(path, "*"))
            {
                if (subDir == ".." || subDir == ".")
                    continue;
                string relativePath = Path.Combine(path, subDir);
                yield return relativePath;
                foreach (string subDir2 in GetAllDirectories(relativePath))
                    yield return subDir2;
            }
        }

        #region Import from kernel32

        private const int MAX_PATH = 260;

        [Serializable]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        [BestFitMapping(false)]
        private struct WIN32_FIND_DATA
        {
            public FileAttributes dwFileAttributes;
            public FILETIME ftCreationTime;
            public FILETIME ftLastAccessTime;
            public FILETIME ftLastWriteTime;
            public int nFileSizeHigh;
            public int nFileSizeLow;
            public int dwReserved0;
            public int dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATH)]
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternate;
        }

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr FindFirstFile(string lpFileName,
                                                   out WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool FindNextFile(IntPtr hFindFile,
                                                out WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FindClose(IntPtr hFindFile);

        private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        #endregion
    }
}