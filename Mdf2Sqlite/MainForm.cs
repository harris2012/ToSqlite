using Mdf2Sqlite.Mssql;
using Mdf2Sqlite.Sqlite;
using Mdf2Sqlite.Template;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mdf2Sqlite
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            string mdfFilePath = MdfFilePathTextBox.Text;
            string sqliteFilePath = SqliteFilePathTextBox.Text;

            try
            {
                Process(mdfFilePath, sqliteFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            this.Enabled = true;
        }

        private void Process(string mdfFilePath, string sqliteFilePath)
        {
            MssqlSchemaReader mssqlSchemaReader = new MssqlSchemaReader();
            MssqlDataReader mssqlDataReader = new MssqlDataReader();
            SqliteDataWriter sqliteDataWriter = new SqliteDataWriter();

            List<MssqlTable> mssqlTableList = mssqlSchemaReader.GetTableList(mdfFilePath);

            List<SqliteTable> sqliteTableList = DBBridge.Mdf2Sqlite(mssqlTableList);

            CreateSqliteTable(sqliteTableList, sqliteFilePath);

            foreach (var mssqlTable in mssqlTableList)
            {
                string mssqlSelectSql = null;
                {
                    SelectFromMssqlTemplate template = new SelectFromMssqlTemplate();
                    template.MssqlTable = mssqlTable;
                    mssqlSelectSql = template.TransformText();

                    System.Diagnostics.Debug.WriteLine(mssqlSelectSql);
                }

                string sqliteInsertSql = null;
                {
                    InsertMdf2SqliteTemplate template = new InsertMdf2SqliteTemplate();
                    template.MssqlTable = mssqlTable;
                    sqliteInsertSql = template.TransformText();

                    System.Diagnostics.Debug.WriteLine(sqliteInsertSql);
                }

                List<dynamic> mssqlEntityList = mssqlDataReader.ReadEntityList(mssqlSelectSql, ConnectionProvider.GetMssqlConn(mdfFilePath));

                sqliteDataWriter.WriteEntityList(sqliteInsertSql, mssqlEntityList, ConnectionProvider.GetSqliteConn(sqliteFilePath));
            }
        }

        private static void CreateSqliteTable(List<SqliteTable> sqliteTableList, string sqliteFilePath)
        {
            foreach (var sqliteTable in sqliteTableList)
            {
                var template = new CreateSqliteTableTemplate();
                template.SqliteTable = sqliteTable;

                var content = template.TransformText();

                System.Diagnostics.Debug.WriteLine(content);

                using (var sqliteConn = ConnectionProvider.GetSqliteConn(sqliteFilePath))
                {
                    SqliteExecutor.Execute(content, sqliteConn);
                }
            }
        }
    }
}
