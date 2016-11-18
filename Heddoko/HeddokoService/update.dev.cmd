setlocal
cd /d "%~dp0"
net stop HeddokoServiceDev
git pull origin dev
HeddokoService.exe -m
net start HeddokoServiceDev
set /p ready=Done