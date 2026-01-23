using System;
using System.Data;
using System.Data.SqlClient;

namespace HospitalApp
{
    class Program
    {
        // 1. 設定資料庫連線字串 (指向醫院伺服器)
        static string connString = "Server=localhost;Database=HospitalDB;Integrated Security=True;";

        static void Main(string[] args)
        {
            Console.WriteLine("=== 醫院掛號系統查詢介面 ===");
            Console.Write("請輸入掛號編號 (AppID): ");
            string inputID = Console.ReadLine();

            // 執行查詢邏輯
            QueryAppointment(inputID);

            Console.WriteLine("\n查詢結束，按任意鍵退出...");
            Console.ReadKey();
        }

        static void QueryAppointment(string appId)
        {
            // 2. 使用 using 確保連線在結束後立即關閉，避免占用醫院資料庫資源
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try
                {
                    // 3. 準備 SQL 指令 (使用參數化查詢 @ID 防止 SQL 注入攻擊)
                    string sql = "SELECT PatientName, DoctorName, AppTime FROM Appointment WHERE AppID = @ID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ID", appId);

                    conn.Open(); // 開啟連線

                    // 4. 執行讀取器
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Console.WriteLine("\n----------------------------");
                            Console.WriteLine($"病患姓名：{reader["PatientName"]}");
                            Console.WriteLine($"主治醫師：{reader["DoctorName"]}");
                            Console.WriteLine($"看診時間：{reader["AppTime"]}");
                            Console.WriteLine("----------------------------");
                        }
                        else
                        {
                            Console.WriteLine("\n[提示] 找不到該掛號紀錄。");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // 針對資料庫錯誤的處理
                    Console.WriteLine("\n[錯誤] 無法連線至醫院資料庫。錯誤代碼：" + ex.Number);
                }
                catch (Exception ex)
                {
                    // 針對一般程式錯誤的處理
                    Console.WriteLine("\n[錯誤] 系統發生異常：" + ex.Message);
                }
            }
        }
    }
}
