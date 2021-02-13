using System;
using System.ComponentModel;
using System.Data;

namespace TableSetting.Services
{
    public static class DbTypeUtil
    {
        /// <summary>
        /// 文字列を指定された DbType 型として解析し、対応するランタイム型に変換する
        /// </summary>
        /// <param name="source">変換元の文字列</param>
        /// <param name="type">解析する型形式を示す DbType</param>
        /// <returns>ランタイム型に変換後のインスタンス</returns>
        public static object Parse(string source, DbType type) => type switch
        {
            DbType.AnsiString => source,
            DbType.Binary => throw new NotImplementedException(),
            DbType.Byte => byte.Parse(source),
            DbType.Boolean => bool.Parse(source),
            DbType.Currency => decimal.Parse(source),
            DbType.Date => DateTime.Parse(source),
            DbType.DateTime => DateTime.Parse(source),
            DbType.Decimal => decimal.Parse(source),
            DbType.Double => double.Parse(source),
            DbType.Guid => new Guid(source),
            DbType.Int16 => short.Parse(source),
            DbType.Int32 => int.Parse(source),
            DbType.Int64 => long.Parse(source),
            DbType.Object => source,
            DbType.SByte => sbyte.Parse(source),
            DbType.Single => float.Parse(source),
            DbType.String => source,
            DbType.Time => TimeSpan.Parse(source),
            DbType.UInt16 => ushort.Parse(source),
            DbType.UInt32 => uint.Parse(source),
            DbType.UInt64 => ulong.Parse(source),
            DbType.VarNumeric => decimal.Parse(source),
            DbType.AnsiStringFixedLength => source,
            DbType.StringFixedLength => source,
            DbType.Xml => throw new NotImplementedException(),
            DbType.DateTime2 => DateTime.Parse(source),
            DbType.DateTimeOffset => DateTimeOffset.Parse(source),
            _ => throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(DbType))
        };
    }
}
