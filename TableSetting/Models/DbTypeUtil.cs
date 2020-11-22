using System;
using System.Collections.Generic;
using System.Data;

namespace TableSetting.Models
{
    public static class DbTypeUtil
    {
        /// <summary>
        /// DbType とランタイム Type の対応表
        /// </summary>
        private static readonly Dictionary<DbType, Type> TypePatternMatrix = new Dictionary<DbType, Type>()
        {
            { DbType.AnsiString, typeof(string) },
            { DbType.Binary, typeof(byte[]) },
            { DbType.Byte, typeof(byte) },
            { DbType.Boolean, typeof(bool) },
            { DbType.Currency, null },
            { DbType.Date, null },
            { DbType.DateTime, typeof(DateTime) },
            { DbType.Decimal, typeof(decimal) },
            { DbType.Double, typeof(double) },
            { DbType.Guid, typeof(Guid) },
            { DbType.Int16, typeof(short) },
            { DbType.Int32, typeof(int) },
            { DbType.Int64, typeof(long) },
            { DbType.Object, typeof(object) },
            { DbType.SByte, typeof(sbyte) },
            { DbType.Single, typeof(float) },
            { DbType.String, typeof(string) },
            { DbType.Time, typeof(TimeSpan) },
            { DbType.UInt16, typeof(ushort) },
            { DbType.UInt32, typeof(uint) },
            { DbType.UInt64, typeof(ulong) },
            { DbType.VarNumeric, null },
            { DbType.AnsiStringFixedLength, typeof(string) },
            { DbType.StringFixedLength, typeof(string) },
            { DbType.Xml, null },
            { DbType.DateTime2, null },
            { DbType.DateTimeOffset, null }
        };

        /// <summary>
        /// 文字列から Type 型へ変換するデリゲート
        /// </summary>
        private static readonly Dictionary<Type, Func<string, object>> TypeParseDelegate = new Dictionary<Type, Func<string, object>>()
        {
            { typeof(object), s => s },
            { typeof(string), s => s },
            { typeof(bool), s => bool.Parse(s) },
            { typeof(sbyte), s => sbyte.Parse(s) },
            { typeof(byte), s => byte.Parse(s) },
            { typeof(byte[]), delegate { throw new NotImplementedException(); } },
            { typeof(short), s => short.Parse(s) },
            { typeof(int), s => int.Parse(s) },
            { typeof(long), s => long.Parse(s) },
            { typeof(ushort), s => ushort.Parse(s) },
            { typeof(uint), s => uint.Parse(s) },
            { typeof(ulong), s => ulong.Parse(s) },
            { typeof(float), s => float.Parse(s) },
            { typeof(double), s => double.Parse(s) },
            { typeof(decimal), s => decimal.Parse(s) },
            { typeof(Guid), s => new Guid(s) },
            { typeof(DateTime), s => DateTime.Parse(s) },
            { typeof(TimeSpan), s => TimeSpan.Parse(s) }
        };

        /// <summary>
        /// DbType に対応するランタイム Type を取得する
        /// </summary>
        /// <param name="type">変換元の DbType</param>
        /// <returns>type に対応するランタイム Type</returns>
        public static Type GetRuntimeType(DbType type)
        {
            if (TypePatternMatrix.ContainsKey(type))
            {
                Type runtimeType = TypePatternMatrix[type];

                if (runtimeType == null)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    return runtimeType;
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// 文字列を指定された DbType 型として解析し、対応するランタイム型に変換する
        /// </summary>
        /// <param name="source">変換元の文字列</param>
        /// <param name="type">解析する型形式を示す DbType</param>
        /// <returns>ランタイム型に変換後のインスタンス</returns>
        public static object Parse(string source, DbType type)
        {
            Type runtimeType = GetRuntimeType(type);

            if (TypeParseDelegate.ContainsKey(runtimeType))
            {
                return TypeParseDelegate[runtimeType](source);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
