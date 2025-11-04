using System;
using System.IO;
using System.Reflection;

namespace CNXML_HVA
{
    /// <summary>
    /// Quản lý đường dẫn file XML trong AppData để ứng dụng có thể triển khai độc lập
    /// </summary>
    public static class DataPaths
    {
        // Tên thư mục trong AppData - Sửa theo tên công ty/dự án của bạn
        public static string AppFolderName => "CNXML_HVA\\Data";

        /// <summary>
        /// Lấy đường dẫn thư mục AppData của ứng dụng
        /// Ví dụ: C:\Users\YourName\AppData\Roaming\CNXML_HVA\Data
        /// </summary>
        public static string GetAppDataFolder()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var folder = Path.Combine(appData, AppFolderName);
            
            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            
            return folder;
        }

        /// <summary>
        /// Lấy đường dẫn đầy đủ của file XML trong AppData
        /// </summary>
        public static string GetXmlFilePath(string filename)
        {
            return Path.Combine(GetAppDataFolder(), filename);
        }

        /// <summary>
        /// Sao chép file XML mẫu từ thư mục Templates vào AppData khi lần đầu chạy
        /// </summary>
        public static void InitializeXmlFile(string filename)
        {
            string targetPath = GetXmlFilePath(filename);
            
            // Nếu file đã tồn tại trong AppData, bỏ qua
            if (File.Exists(targetPath))
            {
                return;
            }

            // 1. Thử lấy từ thư mục Templates (trong thư mục exe)
            string exeFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string templatePath = Path.Combine(exeFolder, "Templates", filename);

            if (File.Exists(templatePath))
            {
                File.Copy(templatePath, targetPath);
                return;
            }

            // 2. Thử lấy từ thư mục gốc (compatibility với phát triển)
            string devPath = Path.Combine(exeFolder, filename);
            if (File.Exists(devPath))
            {
                File.Copy(devPath, targetPath);
                return;
            }

            // 3. Nếu không có file nào, tạo file XML rỗng mới
            CreateEmptyXmlFile(filename, targetPath);
        }

        /// <summary>
        /// Tạo file XML rỗng với cấu trúc cơ bản
        /// </summary>
        private static void CreateEmptyXmlFile(string filename, string targetPath)
        {
            string rootElement = GetRootElementName(filename);
            string xmlContent = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<{rootElement}>\n</{rootElement}>";
            
            File.WriteAllText(targetPath, xmlContent, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// Xác định tên root element dựa trên tên file
        /// </summary>
        private static string GetRootElementName(string filename)
        {
            string name = Path.GetFileNameWithoutExtension(filename);
            
            // Mapping tên file sang tên root element
            switch (name)
            {
                case "Fields": return "fields";
                case "FieldTypes": return "field_types";
                case "Branches": return "branches";
                case "Customers": return "customers";
                case "Equipments": return "equipments";
                case "EquipmentMatch": return "equipment_matches";
                case "FieldEquipment": return "field_equipments";
                case "Matches": return "matches";
                case "Orders": return "orders";
                case "Users": return "users";
                default: return "data";
            }
        }

        /// <summary>
        /// Khởi tạo tất cả file XML cần thiết khi ứng dụng chạy lần đầu
        /// </summary>
        public static void InitializeAllXmlFiles()
        {
            string[] xmlFiles = new[]
            {
                "Fields.xml",
                "FieldTypes.xml",
                "Branches.xml",
                "Customers.xml",
                "Equipments.xml",
                "EquipmentMatch.xml",
                "FieldEquipment.xml",
                "Matches.xml",
                "Orders.xml",
                "Users.xml"
            };

            foreach (var file in xmlFiles)
            {
                InitializeXmlFile(file);
            }
        }
    }
}
