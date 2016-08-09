setlocal
cd /d "%~dp0"
net stop HeddokoService
git pull origin master
net start HeddokoService
set /p ready=Done