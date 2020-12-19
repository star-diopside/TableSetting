using System.Collections.Generic;
using System.Linq;
using TableSetting.Wpf.Models;

namespace TableSetting.Wpf.Services
{
    public interface IOpenFileService
    {
        string? SelectOpenFile() => SelectOpenFile(Enumerable.Empty<FileFilter>());

        string? SelectOpenFile(IEnumerable<FileFilter> filters);
    }
}
