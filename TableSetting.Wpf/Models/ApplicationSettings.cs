using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TableSetting.Wpf.Models
{
    /// <summary>
    /// アプリケーションの設定値を表すクラス
    /// </summary>
    [Serializable]
    public class ApplicationSettings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _dbProviderName = string.Empty;
        private List<ConnectionSetting> _connectionSettings = new();

        /// <summary>
        /// データプロバイダ名を取得または設定する。
        /// </summary>
        public string DbProviderName
        {
            get => _dbProviderName;
            set
            {
                if (_dbProviderName != value)
                {
                    _dbProviderName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DbProviderName)));
                }
            }
        }

        /// <summary>
        /// データベース接続文字列に設定する値のリストを取得または設定する。
        /// </summary>
        public List<ConnectionSetting> ConnectionSettings
        {
            get => _connectionSettings;
            set
            {
                if (_connectionSettings != value)
                {
                    _connectionSettings = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ConnectionSettings)));
                }
            }
        }
    }
}
