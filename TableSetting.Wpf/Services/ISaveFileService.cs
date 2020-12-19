using System.Collections.Generic;
using System.Linq;
using TableSetting.Wpf.Models;

namespace TableSetting.Wpf.Services
{
    public interface ISaveFileService
    {
        string? SelectSaveFile() => SelectSaveFile(Enumerable.Empty<FileFilter>());

        string? SelectSaveFile(IEnumerable<FileFilter> filters);
    }
}
