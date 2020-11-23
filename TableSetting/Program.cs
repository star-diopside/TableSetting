using Npgsql;
using System;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Windows.Forms;
using TableSetting.Forms;

namespace TableSetting
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            DbProviderFactories.RegisterFactory("Odbc", OdbcFactory.Instance);
            DbProviderFactories.RegisterFactory("OleDb", OleDbFactory.Instance);
            DbProviderFactories.RegisterFactory("SqlClient", SqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}