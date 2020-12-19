using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using TableSetting.Wpf.Models;

namespace TableSetting.Wpf.Services
{
    public class SaveFileService : ISaveFileService
    {
        public string? SelectSaveFile(IEnumerable<FileFilter> filters)
        {
            var dialog = new SaveFileDialog
            {
                Filter = string.Join('|', from f in filters
                                          let e = string.Join(';', f.Extensions)
                                          select $"{f.Description} ({e})|{e}")
            };

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            else
            {
                return null;
            }
        }
    }
}
