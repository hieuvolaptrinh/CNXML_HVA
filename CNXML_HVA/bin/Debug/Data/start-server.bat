@echo off
chcp 65001 >nul
echo ================================================
echo     FOOTBALL FIELD MANAGEMENT - WEB SERVER
echo ================================================
echo.
echo Đang khởi động server tại http://localhost:3000
echo.
echo Nhấn Ctrl+C để dừng server
echo ================================================
echo.

node server.js

pause
