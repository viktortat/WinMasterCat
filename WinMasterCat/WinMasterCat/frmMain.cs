using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using System.Configuration;
using MasterCatCore.Dto;

namespace WinMasterCat
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        string startDirectory = @"i:\____TBN-(fido)\_Залитые\WineTime\";
        DataTable srcDt = new DataTable();
        DataTable destDt = new DataTable();
        private bool InTrue(string pathFile)
        {
            using (var pac = new ExcelPackage(new FileInfo(pathFile)))
            {
                var sheet = pac.Workbook.Worksheets;
                //var o = from c in sheet select c;
                return (pac.Workbook.Worksheets.FirstOrDefault(w => w.Name == "Производители") != null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var countMatch = new Regex(@"\\").Matches(startDirectory).Count;

            var t = Directory.GetFiles(startDirectory, "*.xlsx", SearchOption.AllDirectories)
                .Select(d => new FileInfo(d))
                .Where(f1 => !f1.Name.Contains("$") &&
                             (new Regex(@"\\").Matches(f1.Directory.FullName).Count - countMatch) == 0
                )
                .Select(f => new
                {
                    f.Name,
                    fNameFile = f.FullName,
                    f.Directory.FullName,
                    FileSize = (f.Length / 1024)
                });

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("ID", typeof(int)));
            dt.Columns.Add(new DataColumn("FullLName", typeof(String)));
            dt.Columns.Add(new DataColumn("DicName", typeof(String)));
            dt.Columns.Add(new DataColumn("FileSize", typeof(long)));


            var i = 0;
            foreach (var tRow in t.ToList())
            {
                if (InTrue(tRow.fNameFile))
                {
                    i++;
                    DataRow row = dt.NewRow();
                    row["ID"] = i;
                    row["FullLName"] = tRow.fNameFile;
                    row["DicName"] = tRow.FullName;
                    row["FileSize"] = tRow.FileSize;
                    dt.Rows.Add(row);
                    //Console.WriteLine($"------Обработали {i} - {tRow.fNameFile}");

                    FillDestDtProp(tRow.fNameFile, destDt, "Товары");

                    dataGridView1.DataSource = destDt;

                    //if (i>10) break; 
                }
            }



            if (destDt.Rows.Count > 0)
            {
                using (var connection = new SqlConnection(Program.connectionStr))
                {
                    connection.Open();

                    //var tSQL = "truncate table tmp_ProductAll";
                    //var cmd = new SqlCommand(tSQL, connection);
                    //cmd.CommandType = CommandType.Text;
                    //cmd.Connection = connection;
                    //cmd.ExecuteNonQuery();
                    using (var bulkCopy = new SqlBulkCopy(connection))
                    {
                        //var properties = destDt.AsEnumerable().Select(column => column[2].ToString()).ToList().Distinct();
                        //var rows = destDt.AsEnumerable().ToList();
                        //var error = string.Empty;

                        //foreach (var property in properties)
                        //{
                        //    int numberRow;

                        //    var localError = string.Empty;
                        //    CheckRows(rows.Where(row => row.ItemArray[2].ToString() == property).ToList(), out numberRow, ref localError);
                        //    if (!string.IsNullOrEmpty(localError))
                        //    {
                        //        error += $"{property}:{Environment.NewLine}";
                        //        error += localError;
                        //        error += $"----------------------------------{Environment.NewLine}";
                        //    }
                        //}
                        //if (!string.IsNullOrEmpty(error))
                        //{
                        //    MessageBox.Show(error);
                        //    return;
                        //}


                        dataGridView1.DataSource = destDt;
                        dataGridView1.AutoResizeColumns();
                        dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                        dataGridView1.CurrentRow.Selected = false;

                        bulkCopy.ColumnMappings.Clear();
                        //FamilyCode
                        bulkCopy.DestinationTableName = destDt.TableName;
                        foreach (DataColumn col in destDt.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                        }

                        //bulkCopy.ColumnMappings.Add("ProductId", "ProductId");
                        //bulkCopy.ColumnMappings.Add("BarCode", "BarCode");



                        bulkCopy.BatchSize = 10000;
                        bulkCopy.BulkCopyTimeout = 60;

                        bulkCopy.WriteToServer(destDt.CreateDataReader());
                    }
                    MessageBox.Show("Все");
                    //lblTopCaption.Text = $"Свойства импортированны {destDt.Rows.Count} записи";
                }
            }
            //DataTable table = ToDataTable(t.ToList());
        }


        private void button2_Click(object sender, EventArgs e)
        {
            srcDt = new DataTable();
            string pathFile = startDirectory +
                              @"TM_Advini_Advini_WineTime_new_translate\КТ_ТМ_Advini_Advini_WineTime_2SKU_05042016_eng_rus_ukr.xlsx";
            var destDt1 = FillDestDtProp(pathFile, destDt, "Товары");
            dataGridView1.DataSource = destDt1;
        }

        private static bool FillDestDtProp(string pathFile,DataTable destDt,string tabStr)
        {
          
            DataTable srcDt;
            FileInfo fi = new FileInfo(pathFile);
            using (var pac = new ExcelPackage(fi))
            {
                var stProduct = pac.Workbook.Worksheets.FirstOrDefault(w => w.Name == tabStr);
                srcDt = Utils.GetDTNew(stProduct);
            }

            var propCols = new List<DataColumn>();
            foreach (DataColumn col in srcDt.Columns)
            {
                propCols.Add(col);
            }
            var TransId = Guid.NewGuid();
            foreach (DataRow srow in srcDt.Rows)
            {
                DataRow dRow = null;
                foreach (var propCol in propCols)
                {
                    dRow = destDt.NewRow();
                    dRow["idRow"] = srow["cid"];
                    dRow["PropertyName"] = propCol.ColumnName;
                    dRow["PropertyVal"] = srow[propCol];
                    dRow["FileName"] = pathFile;
                    dRow["DirName"] = Path.GetDirectoryName(pathFile);
                    dRow["FileDate"] = fi.CreationTime;
                    dRow["DateAdd"] = DateTime.Now;
                    dRow["TabName"] = tabStr;
                    dRow["TransId"] = TransId;
                    destDt.Rows.Add(dRow);
                }
            }
            Console.WriteLine("==================================================");
            Console.WriteLine($"Заполнили {pathFile}");
            Application.DoEvents();
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Program.connectionStr = ConfigurationManager.ConnectionStrings["TbnProd.Local"].ConnectionString;

            destDt = new DataTable("tmp_ProductAll");
            destDt.Columns.Add(new DataColumn("idRow", typeof(int)) {AllowDBNull = true});
            destDt.Columns.Add(new DataColumn("PropertyName", typeof(string)) {AllowDBNull = false});
            destDt.Columns.Add(new DataColumn("PropertyVal", typeof(string)) {AllowDBNull = true});
            destDt.Columns.Add(new DataColumn("FileName", typeof(string)) {AllowDBNull = true});
            destDt.Columns.Add(new DataColumn("DirName", typeof(string)) { AllowDBNull = true });
            destDt.Columns.Add(new DataColumn("FileDate", typeof(DateTime)) {AllowDBNull = true});
            destDt.Columns.Add(new DataColumn("DateAdd", typeof(DateTime)) {AllowDBNull = true});
            destDt.Columns.Add(new DataColumn("TabName", typeof(string)) { AllowDBNull = true });
            destDt.Columns.Add(new DataColumn("TransId", typeof(Guid)) {AllowDBNull = true});
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (ProdAllContext db = new ProdAllContext())
            {
                var PProps = db.Test13.ToList();
                foreach (TestUser pp in PProps)
                {
                    Console.WriteLine($"{pp.Name} ");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (ProdAllContext db = new ProdAllContext())
            {
                string Lang = "ru";
                var brns = from b in db.Brands.AsNoTracking().Where(x => x.State != EnumObjectsState.Deleted)
                    from bl in db.BrandLocs.Where(x => x.Id == b.id && x.Lang == Lang)
                    select new
                    {
                        Id = b.id,
                        Code = b.Code,
                        Name = bl.Name
                    };

                var i = 1;
                //Brand brand = null;
                foreach (var b in brns)
                {
                    Console.WriteLine($"[{i}] \t {b.Id} \t {b.Code} \t {b.Name} ");
                    i++;
                }
            }
        }
    }
}
