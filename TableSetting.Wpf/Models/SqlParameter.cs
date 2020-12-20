using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace TableSetting.Wpf.Models
{
    public class SqlParameter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _name = string.Empty;
        private DbType _type = DbType.String;
        private string _value = string.Empty;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new(nameof(Name)));
                }
            }
        }

        public DbType Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    PropertyChanged?.Invoke(this, new(nameof(Type)));
                }
            }
        }

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

        public DbParameter ToDbParameter(DbProviderFactory factory)
        {
            var parameter = factory.CreateParameter() ?? throw new NotImplementedException();

            parameter.ParameterName = Name;
            parameter.DbType = Type;
            parameter.Value = DbTypeUtil.Parse(Value, Type);

            return parameter;
        }
    }
}
