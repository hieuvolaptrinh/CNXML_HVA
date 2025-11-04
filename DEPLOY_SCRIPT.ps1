# =====================================
# SCRIPT TỰ ĐỘNG ĐÓNG GÓI CNXML_HVA
# =====================================
# Cách dùng: Click chuột phải → Run with PowerShell

$baseDir = "f:\new\OneDrive - University of Technology and Education\Dai_hoc\2025-2026\XML\CNXML_HVA"
$sourceDir = "$baseDir\CNXML_HVA\bin\Debug"
$deployDir = "$baseDir\CNXML_HVA_Deploy"
$zipFile = "$baseDir\CNXML_HVA_v1.0.zip"

Write-Host "`n🚀 BẮT ĐẦU ĐÓNG GÓI..." -ForegroundColor Cyan
Write-Host "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━`n" -ForegroundColor Gray

# Kiểm tra file EXE có tồn tại không
if (-not (Test-Path "$sourceDir\CNXML_HVA.exe")) {
    Write-Host "❌ LỖI: Không tìm thấy file CNXML_HVA.exe" -ForegroundColor Red
    Write-Host "   Vui lòng build project trước (Ctrl+Shift+B trong Visual Studio)" -ForegroundColor Yellow
    Write-Host "`nNhấn phím bất kỳ để thoát..."
    $null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
    exit
}

# Xóa thư mục cũ nếu có
if (Test-Path $deployDir) {
    Remove-Item $deployDir -Recurse -Force
    Write-Host "✓ Đã xóa thư mục Deploy cũ" -ForegroundColor Yellow
}

# Tạo thư mục mới
New-Item -ItemType Directory -Path $deployDir -Force | Out-Null
Write-Host "✓ Đã tạo thư mục Deploy" -ForegroundColor Green

# Copy file EXE
Copy-Item "$sourceDir\CNXML_HVA.exe" -Destination $deployDir
$exeSize = (Get-Item "$deployDir\CNXML_HVA.exe").Length / 1KB
Write-Host "✓ Đã copy CNXML_HVA.exe ($([math]::Round($exeSize, 2)) KB)" -ForegroundColor Green

# Copy file config
Copy-Item "$sourceDir\CNXML_HVA.exe.config" -Destination $deployDir
Write-Host "✓ Đã copy CNXML_HVA.exe.config" -ForegroundColor Green

# Copy thư mục Templates
if (Test-Path "$sourceDir\Templates") {
    Copy-Item "$sourceDir\Templates" -Destination $deployDir -Recurse
    $xmlCount = (Get-ChildItem "$deployDir\Templates\*.xml").Count
    Write-Host "✓ Đã copy thư mục Templates ($xmlCount file XML)" -ForegroundColor Green
} else {
    Write-Host "⚠️  CẢNH BÁO: Không tìm thấy thư mục Templates" -ForegroundColor Yellow
}

# Tạo file README
$readme = @"
╔══════════════════════════════════════════════════════════╗
║   CNXML_HVA - HỆ THỐNG QUẢN LÝ SÂN BÓNG ĐÁ            ║
╚══════════════════════════════════════════════════════════╝

📋 HƯỚNG DẪN SỬ DỤNG:

1. Giải nén file ZIP này ra thư mục bất kỳ
2. Chạy file CNXML_HVA.exe
3. Lần đầu chạy sẽ tự động tạo dữ liệu mẫu

📁 CẤU TRÚC FILE:
   ├── CNXML_HVA.exe          → File chính để chạy
   ├── CNXML_HVA.exe.config   → File cấu hình
   ├── Templates\             → Dữ liệu XML mẫu (10 files)
   └── README.txt             → File này

💾 DỮ LIỆU LƯU Ở ĐÂU?
   C:\Users\[TênBạn]\AppData\Roaming\CNXML_HVA\Data\
   → Bạn có thể backup thư mục này để lưu dữ liệu

⚙️ CÁC CHỨC NĂNG CHÍNH:
   ✓ Quản lý sân bóng (Form San.cs)
   ✓ Quản lý loại sân (Form LoaiSan.cs)
   ✓ Thêm/Sửa/Xóa dữ liệu
   ✓ Tìm kiếm và lọc
   ✓ Import/Export XML

⚠️ YÊU CẦU HỆ THỐNG:
   - Windows 7 trở lên
   - .NET Framework 4.7.2
     (Tải tại: https://dotnet.microsoft.com/download/dotnet-framework/net472)

🔧 XỬ LÝ SỰ CỐ:
   - Nếu gặp lỗi khi mở ứng dụng:
     1. Xóa thư mục: C:\Users\[TênBạn]\AppData\Roaming\CNXML_HVA
     2. Chạy lại CNXML_HVA.exe
   
   - Nếu thiếu .NET Framework:
     1. Download từ link trên
     2. Cài đặt và khởi động lại máy
     3. Chạy lại ứng dụng

📞 HỖ TRỢ:
   - Email: [Điền email của bạn]
   - GitHub: https://github.com/hieuvolaptrinh/CNXML_HVA

Phiên bản: 1.0
Ngày phát hành: $(Get-Date -Format "dd/MM/yyyy")
Copyright © 2025 - CNXML_HVA Team
"@

$readme | Out-File -FilePath "$deployDir\README.txt" -Encoding UTF8
Write-Host "✓ Đã tạo file README.txt" -ForegroundColor Green

# Tạo file ZIP
if (Test-Path $zipFile) {
    Remove-Item $zipFile -Force
}
Compress-Archive -Path "$deployDir\*" -DestinationPath $zipFile -Force
Write-Host "✓ Đã tạo file ZIP" -ForegroundColor Green

# Hiển thị kết quả
Write-Host "`n✅ HOÀN TẤT!" -ForegroundColor Green
Write-Host "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" -ForegroundColor Gray
Write-Host "`n📁 Thư mục Deploy:" -ForegroundColor Cyan
Write-Host "   $deployDir" -ForegroundColor White

Write-Host "`n📦 File ZIP để gửi:" -ForegroundColor Cyan
Write-Host "   $zipFile" -ForegroundColor White

Write-Host "`n📊 Kích thước file:" -ForegroundColor Cyan
$zipSize = (Get-Item $zipFile).Length / 1KB
Write-Host "   $([math]::Round($zipSize, 2)) KB" -ForegroundColor White

Write-Host "`n📋 Nội dung gói:" -ForegroundColor Cyan
Get-ChildItem $deployDir -Recurse -File | ForEach-Object {
    $size = [math]::Round($_.Length / 1KB, 2)
    Write-Host "   - $($_.Name) ($size KB)" -ForegroundColor White
}

Write-Host "`n━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━" -ForegroundColor Gray
Write-Host "`n🎯 BƯỚC TIẾP THEO:" -ForegroundColor Yellow
Write-Host "   1. Gửi file ZIP cho người dùng" -ForegroundColor White
Write-Host "   2. Họ giải nén và chạy CNXML_HVA.exe" -ForegroundColor White
Write-Host "   3. Lần đầu chạy sẽ tự động tạo dữ liệu`n" -ForegroundColor White

# Mở thư mục
Write-Host "🚀 Đang mở thư mục Deploy..." -ForegroundColor Cyan
Start-Process $baseDir

Write-Host "`nNhấn phím bất kỳ để thoát..."
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
