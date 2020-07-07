REM Installer Chronome CapsLock general Driver 2020
REM ChronomeDev 2020
REM ========== HARAP TUNGGU ========================
@echo off
PowerShell -Command "Add-Type -AssemblyName PresentationFramework;[System.Windows.MessageBox]::Show('Selamat Datang pada instalasi Shell ini', 'ChronomeDriver')"
cls
xcopy %~dp0chrservice.exe "C:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp"
cls
PowerShell -Command "Add-Type -AssemblyName PresentationFramework;[System.Windows.MessageBox]::Show('Pastikan setelah halaman shell close, restart komputer. Apabila driver tidak berfungsi, coba kembali dengan menjalankan instalasi ini dengan hak Administrator', 'ChronomeDriver')"
exit