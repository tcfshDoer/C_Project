資源管控 (Resource Management)：
「使用了 using 語句。這能保證不論程式執行成功或失敗，SqlConnection 都會被正確關閉。在 24 小時高頻率使用的環境下，能防止連線洩漏 (Connection Leak) 導致系統崩潰。」

資安防護 (Security)：
「沒有直接把參數拼接到 SQL 字串中，而是使用 Parameters.AddWithValue。這是為了防止 SQL 注入 (SQL Injection)。」

強健性 (Robustness)：
「 try-catch 分層處理。能區分資料庫層級的錯誤（如 SqlException）與一般程式錯誤。快速判斷是網路斷線還是程式邏輯出錯。」
