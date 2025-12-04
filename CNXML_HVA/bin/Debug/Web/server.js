// Simple HTTP Server for Football Field Management System
// Giải quyết CORS khi đọc XML files

const http = require("http");
const fs = require("fs");
const path = require("path");

const PORT = 3000;

// MIME types
const mimeTypes = {
  ".html": "text/html",
  ".css": "text/css",
  ".js": "application/javascript",
  ".json": "application/json",
  ".xml": "application/xml",
  ".png": "image/png",
  ".jpg": "image/jpeg",
  ".jpeg": "image/jpeg",
  ".gif": "image/gif",
  ".svg": "image/svg+xml",
  ".ico": "image/x-icon",
};

const server = http.createServer((req, res) => {
  console.log(`${new Date().toLocaleTimeString()} - ${req.method} ${req.url}`);

  // Default to index.html
  let filePath = req.url === "/" ? "/index.html" : req.url;

  // Remove query string
  filePath = filePath.split("?")[0];

  // Security: prevent directory traversal
  filePath = path.normalize(filePath).replace(/^(\.\.[\/\\])+/, "");

  // Build full path
  const fullPath = path.join(__dirname, filePath);
  const ext = path.extname(fullPath).toLowerCase();
  const contentType = mimeTypes[ext] || "application/octet-stream";

  // Read and serve file
  fs.readFile(fullPath, (err, content) => {
    if (err) {
      if (err.code === "ENOENT") {
        // File not found
        res.writeHead(404, { "Content-Type": "text/html; charset=utf-8" });
        res.end(`
          <!DOCTYPE html>
          <html>
          <head>
            <meta charset="utf-8">
            <title>404 - Không tìm thấy</title>
            <style>
              body { font-family: Arial, sans-serif; text-align: center; padding: 50px; background: #f5f5f5; }
              h1 { color: #f44336; font-size: 3em; }
              p { color: #666; font-size: 1.2em; }
              a { color: #4CAF50; text-decoration: none; font-weight: bold; }
            </style>
          </head>
          <body>
            <h1>404</h1>
            <p>Không tìm thấy file: ${filePath}</p>
            <a href="/">← Quay về trang chủ</a>
          </body>
          </html>
        `);
      } else {
        // Server error
        res.writeHead(500);
        res.end(`Server Error: ${err.code}`);
      }
    } else {
      // Success - serve file with CORS headers
      res.writeHead(200, {
        "Content-Type": `${contentType}; charset=utf-8`,
        "Access-Control-Allow-Origin": "*",
        "Access-Control-Allow-Methods": "GET, POST, OPTIONS",
        "Access-Control-Allow-Headers": "Content-Type",
        "Cache-Control": "no-cache",
      });
      res.end(content, "utf-8");
    }
  });
});

server.listen(PORT, () => {
  console.log("\n╔════════════════════════════════════════════════════════╗");
  console.log("║   🏟️  Football Field Management System Server        ║");
  console.log("╚════════════════════════════════════════════════════════╝\n");
  console.log(`✓ Server đang chạy tại: http://localhost:${PORT}`);
  console.log(`✓ Thư mục Web: ${__dirname}`);
  console.log(`✓ Nhấn Ctrl+C để dừng server\n`);
  console.log("📂 Các trang có sẵn:");
  console.log(`   → http://localhost:${PORT}/index.html`);
  console.log(`   → http://localhost:${PORT}/dashboard.html`);
  console.log(`   → http://localhost:${PORT}/branches.html`);
  console.log(`   → http://localhost:${PORT}/customers.html`);
  console.log(`   → http://localhost:${PORT}/fields.html`);
  console.log(`   → http://localhost:${PORT}/bookings.html`);
  console.log(`   → http://localhost:${PORT}/debug-xml.html`);
  console.log("\n🔥 Server sẵn sàng! Không còn CORS errors! 🎉\n");
});

// Handle errors
server.on("error", (err) => {
  if (err.code === "EADDRINUSE") {
    console.error(`\n❌ Lỗi: Port ${PORT} đã được sử dụng!`);
    console.error(
      `💡 Giải pháp: Đóng ứng dụng đang dùng port ${PORT} hoặc đổi PORT trong server.js\n`
    );
  } else {
    console.error("Server Error:", err);
  }
});

// Graceful shutdown
process.on("SIGINT", () => {
  console.log("\n\n👋 Đang tắt server...");
  server.close(() => {
    console.log("✓ Server đã tắt!\n");
    process.exit(0);
  });
});
