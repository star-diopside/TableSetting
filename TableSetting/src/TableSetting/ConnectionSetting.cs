using System;

namespace TableSetting
{
    /// <summary>
    /// データベース接続文字列に設定する各項目の値を表すクラス
    /// </summary>
    [Serializable]
    public class ConnectionSetting
    {
        /// <summary>
        /// 設定項目のキー名を取得または設定する
        /// </summary>
        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// 設定項目の値を取得または設定する
        /// </summary>
        public string Value
        {
            get;
            set;
        }

        /// <summary>
        /// 設定項目が有効であるかどうかを示す値を取得または設定する
        /// </summary>
        public bool Enable
        {
            get;
            set;
        }
    }
}
