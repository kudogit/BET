using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Elect.Data.IO;

namespace BETSTS.Models
{
    public class StaticFileModel
    {
        public string AreaFolderName { get; set; }

        public string AreaName { get; set; }

        /// <summary>
        ///     Use exactly folder name case in explorer 
        /// </summary>
        /// <remarks> Relative path from <see cref="AreaName" /> </remarks>
        public string FolderRelativePath { get; set; }

        /// <summary>
        ///     Use lower case for http request 
        /// </summary>
        public string HttpRequestPath { get; set; }

        /// <summary>
        ///     Max Age in Cache Control Header 
        /// </summary>
        /// <remarks> Use the . separator between days and hours, see more: https://msdn.microsoft.com/en-us/library/system.timespan.aspx </remarks>
        public TimeSpan? MaxAgeResponseHeader { get; set; } = new TimeSpan(365, 0, 0, 0);

        public List<string> GetListRelativeUrl()
        {
            List<string> listRelativeUrl = new List<string>();

            // Build Relative Path

            var folderRelativePath = FolderRelativePath;

            if (!string.IsNullOrWhiteSpace(AreaName))
            {
                folderRelativePath = Path.Combine(AreaName, folderRelativePath);
            }

            if (!string.IsNullOrWhiteSpace(AreaFolderName))
            {
                folderRelativePath = Path.Combine(AreaFolderName, folderRelativePath);
            }

            var folderAbsolutePath = PathHelper.GetFullPath(folderRelativePath);

            // Scan Files

            var folder = new DirectoryInfo(folderAbsolutePath);

            var listFile = folder.GetFiles("*", SearchOption.AllDirectories);

            // Build Urls

            var httpRequestUrl = HttpRequestPath.Trim('\\', '/');

            listRelativeUrl.AddRange(
                listFile.Select(file =>
                    httpRequestUrl + Path.DirectorySeparatorChar + file.FullName
                        .Replace(folderAbsolutePath, string.Empty)
                        .Trim(Path.DirectorySeparatorChar)
                ));

            listRelativeUrl = listRelativeUrl.Select(x => x.Replace(Path.DirectorySeparatorChar, '/').Trim('/')).Distinct().ToList();

            return listRelativeUrl;
        }
    }
}