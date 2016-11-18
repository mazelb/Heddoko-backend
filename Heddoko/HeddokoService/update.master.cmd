setlocal
cd /d "%~dp0"
net stop HeddokoService
git pull origin master
HeddokoService.exe -m
net start HeddokoService
set /p ready=Done