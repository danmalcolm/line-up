@ECHO off
SET verbose=0

SET modify_path_file=%TEMP%\LineUp\modify_path.bat
IF "%verbose%" == "1" ECHO Using temporary file "%modify_path_file%" for new path

lineup.exe %*

IF EXIST %modify_path_file% CALL %modify_path_file%

IF EXIST %modify_path_file% DEL %modify_path_file%