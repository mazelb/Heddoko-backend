SETLOCAL
cd /d "%~dp0"
SET HeddokoOutput="C:\Users\knopa\Dropbox\Work\Heddoko\heddoko-backend-service"
SET HeddokoInput="C:\Users\knopa\Dropbox\Work\Heddoko\heddoko-backend\Heddoko\HeddokoService"

DEL /F /Q /S %HeddokoOutput%\*.exe
DEL /F /Q /S %HeddokoOutput%\*.dll
DEL /F /Q /S %HeddokoOutput%\*.pdb
DEL /F /Q /S %HeddokoOutput%\*.xml
DEL /F /Q /S %HeddokoOutput%\*.txt
DEL /F /Q /S %HeddokoOutput%\*.md
DEL /F /Q /S %HeddokoOutput%\*.cmd
DEL /F /Q /S %HeddokoOutput%\*.config

XCOPY %HeddokoInput%\bin\x64\Dev %HeddokoOutput%

DEL /F /Q /S %HeddokoOutput%\*.vshost.*
DEL /F /Q /S %HeddokoOutput%\*.manifest
DEL /F /Q /S %HeddokoOutput%\*.InstallLog
DEL /F /Q /S %HeddokoOutput%\*.application
DEL /F /Q /S %HeddokoOutput%\*.InstallState

XCOPY %HeddokoInput%\update.dev.cmd %HeddokoOutput%
XCOPY %HeddokoInput%\readme.md %HeddokoOutput%
set /p ready=Done