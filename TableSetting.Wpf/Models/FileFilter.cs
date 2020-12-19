using System.Collections.Generic;
using System.Linq;

namespace TableSetting.Wpf.Models
{
    public class FileFilter
    {
        public string Description { get; set; } = string.Empty;

        public IEnumerable<string> Extensions { get; set; } = Enumerable.Empty<string>();
    }
}
