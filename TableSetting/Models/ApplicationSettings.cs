using System;
using System.Collections.Generic;

namespace TableSetting.Models
{
    /// <summary>
    /// アプリケーションの設定値を表すクラス
    /// </summary>
    [Serializable]
    public class ApplicationSettings
    {
        /// <summary>
        /// データプロバイダ名を取得または設定する。
        /// </summary>
        public string DbProviderName
        {
            get;
            set;
        }

        /// <summary>
        /// データベース接続文字列に設定する値のリストを取得または設定する。
        /// </summary>
        public List<ConnectionSetting> ConnectionSettings
        {
            get;
            set;
        }
    }
}
