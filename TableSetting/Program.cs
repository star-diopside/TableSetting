using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
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
            Application.ThreadException += (_, e) =>
            {
                MessageBox.Show(e.Exception.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            DbProviderFactories.RegisterFactory("Odbc", OdbcFactory.Instance);
            DbProviderFactories.RegisterFactory("OleDb", OleDbFactory.Instance);
            DbProviderFactories.RegisterFactory("SqlClient", SqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("Npgsql", NpgsqlFactory.Instance);
            DbProviderFactories.RegisterFactory("MySqlClient", MySqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("Sqlite", SqliteFactory.Instance);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}