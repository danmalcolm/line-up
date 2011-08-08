@ECHO off
SET verbose=0

SET temp_path_file=%USERPROFILE%\LineUp.NewPath.temp
IF "%verbose%" == "1" ECHO Using temporary file "%temp_path_file%" for new path

IF EXIST %temp_path_file% DEL %temp_path_file%

lineup.exe %*

IF EXIST %temp_path_file% set /p new_path=<%temp_path_file%

IF ("%new_path%") == ("") GOTO END

IF "%verbose%" == "1" ECHO Setting path from file: %new_path%
SET PATH=%new_path%

:END
IF EXIST %temp_path_file% DEL %temp_path_file%