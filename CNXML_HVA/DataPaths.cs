using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace CNXML_HVA
{
     
    public static class DataPaths
    {
         
        public static string DataFolderName => "Data";
 
        public static string GetDataFolder()
        {
            // Lưu dữ liệu trong thư mục Data bên cạnh file exe
            string exeFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var folder = Path.Combine(exeFolder, DataFolderName);
            
            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            
            return folder;
        }
 
        public static string GetXmlFilePath(string filename)
        {
            return Path.Combine(GetDataFolder(), filename);
        }

        
        public static void EnsureXmlFileExists(string filename)
        {
            string filePath = GetXmlFilePath(filename);
            
            // Nếu file đã tồn tại thì không làm gì
            if (File.Exists(filePath))
            {
                return;
            }

            // Tạo file XML rỗng mới
            string rootElement = GetRootElementName(filename);
            string xmlContent = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<{rootElement}>\n</{rootElement}>";
            
            File.WriteAllText(filePath, xmlContent, System.Text.Encoding.UTF8);
        }

        
        private static string GetRootElementName(string filename)
        {
            string name = Path.GetFileNameWithoutExtension(filename);
            
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
                case "Bookings": return "bookings";
                default: return "data";
            }
        }
    }
}
