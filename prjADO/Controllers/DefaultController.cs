using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace prjADO.Controllers
{
    public class DefaultController : Controller
    {

        // 資料庫連線物件
        SqlConnection conn = new SqlConnection();

        string connectionStr =
            @"Data Source=(LocalDB)\MSSQLLocalDB;" +
            "AttachDbFilename=|DataDirectory|Northwind.mdf;" +
            "Integrated Security=True";

        /// <summary>
        /// 查詢所有員工記錄
        /// </summary>
        /// <returns> 所有員工資料 </returns>
        public string ShowEmployee()
        {
            // 設定資料庫連線
            conn.ConnectionString = connectionStr;

            // SQL 查詢語法
            string sql = "SELECT 員工編號, 姓名, 稱呼, 職稱 FROM 員工";

            // 建立 DataAdapter 物件取得資料庫的資料表
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, conn);

            // 建立 DataSet 物件作為本機暫存，方便離線存取
            DataSet dataSet = new DataSet();

            // 將資料庫資料置入本機暫存區
            dataAdapter.Fill(dataSet, "員工");

            // 建立 DataTable 物件讀取暫存表格
            DataTable dataTable = dataSet.Tables["員工"];

            string str = "";

            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                str += "編號：" + dataTable.Rows[i]["員工編號"] + "<br />";
                str += "姓名：" + dataTable.Rows[i]["姓名"] + dataTable.Rows[i]["稱呼"] + "<br />";
                str += "職稱：" + dataTable.Rows[i]["職稱"] + "<hr>";
            }

            return str;
        }

        /// <summary>
        /// 查詢單價大於 30 
        /// 依單價遞增排序
        /// 依庫存遞減排序
        /// 顯示產品、單價、庫存
        /// </summary>
        /// <returns></returns>
        public string ShowProduct()
        {
            // 設定資料庫連線
            conn.ConnectionString = connectionStr;

            // SQL 查詢字串
            string sqlStr = "SELECT 產品, 單價, 庫存量 FROM 產品資料 " +
                "WHERE 單價>30 ORDER BY 單價 ASC, 庫存量 DESC";

            // 建立 SqlAdapter 物件讀取資料庫
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlStr, conn);

            // 建立 DataSet 物件作為本機離線存取物件
            DataSet dataSet = new DataSet();

            // 將資料庫資料存入離線區
            dataAdapter.Fill(dataSet, "產品資料");

            // 建立 DataTable 物件
            DataTable dataTable = dataSet.Tables["產品資料"];

            string str = "";
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                str += "產品：" + dataTable.Rows[i]["產品"] + "<br />";
                str += "單價：" + dataTable.Rows[i]["單價"] + "<br />";
                str += "庫存量：" + dataTable.Rows[i]["庫存量"] + "<hr>";
            }

            return str;
        }

        /// <summary>
        /// 找出客戶地址中含有 keyword 關鍵字的客戶記錄
        /// </summary>
        /// <param name="keyword"> 關鍵字 </param>
        /// <returns> 地址含有 keyword 的客戶 </returns>
        public string ShowCustomerByAddress(string keyword)
        {
            // 設定資料庫連線
            conn.ConnectionString = connectionStr;

            // SQL 查詢字串
            string sqlStr = "SELECT 公司名稱, 連絡人, 連絡人職稱, 地址 FROM 客戶 " +
                "WHERE 地址 LIKE '%" + keyword.Replace("'", "''") + "%'";

            // 建立 SqlDataAdapter 物件讀取資料庫資料
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlStr, conn);

            // 建立 DataSet 物件作為離線存取
            DataSet dataSet = new DataSet();

            // 置入離線存取資料
            dataAdapter.Fill(dataSet, "客戶");

            // 建立 DataTable 物件讀取離線資料
            DataTable dataTable = dataSet.Tables["客戶"];

            string str = "";
            for(int i = 0; i < dataTable.Rows.Count; i++)
            {
                str += "公司：" + dataTable.Rows[i]["公司名稱"] + "<br />";
                str += "姓名：" + dataTable.Rows[i]["連絡人"] + dataTable.Rows[i]["連絡人職稱"] + "<br />";
                str += "地址：" + dataTable.Rows[i]["地址"] + "<hr>";
            }

            return str;
        }
    }
}