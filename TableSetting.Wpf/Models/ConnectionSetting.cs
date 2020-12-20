using System;
using System.ComponentModel;

namespace TableSetting.Wpf.Models
{
    /// <summary>
    /// データベース接続文字列に設定する各項目の値を表すクラス
    /// </summary>
    [Serializable]
    public class ConnectionSetting : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _key = string.Empty;
        private string _value = string.Empty;
        private bool _enable;

        /// <summary>
        /// 設定項目のキー名を取得または設定する
        /// </summary>
        public string Key
        {
            get => _key;
            set
            {
                if (_key != value)
                {
                    _key = value;
                    PropertyChanged?.Invoke(this, new(nameof(Key)));
                }
            }
        }

        /// <summary>
        /// 設定項目の値を取得または設定する
        /// </summary>
        public string Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    PropertyChanged?.Invoke(this, new(nameof(Value)));
                }
            }
        }

        /// <summary>
        /// 設定項目が有効であるかどうかを示す値を取得または設定する
        /// </summary>
        public bool Enable
        {
            get => _enable;
            set
            {
                if (_enable != value)
                {
                    _enable = value;
                    PropertyChanged?.Invoke(this, new(nameof(Enable)));
                }
            }
        }
    }
}
