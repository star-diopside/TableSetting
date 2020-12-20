using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace TableSetting.Wpf.Views
{
    public class Enums
    {
        public Dictionary<DbType, string> DbTypes { get; } = Enum.GetValues<DbType>().ToDictionary(e => e, e => e.ToString());
    }
}
