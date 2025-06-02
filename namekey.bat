echo Off
REM
REM  namekey.bat
REM
REM  This batch creates the the example_scripts.html file used
REM  by ASAP as an index to all current ASAP example scripts 
REM  as released from BRO TCS.
REM
REM  System Requirements:
REM  ActivePerl 5.8 or equivalent with Perl Package Tie-TxHash
REM  installed on host machine.
REM
REM  All files operate and/or reside within a single directory. 
REM  Input:
REM  command_macro.txt
REM  *.inr
REM
REM  Output:
REM  example_scripts.html
echo Starting example_scripts.html creation...
echo .
echo .
echo Verify that example_scripts.html is checked out
echo .
pause
echo Verify that command_macro.txt is up to date.
echo .
pause
namekey.pl -f *.inr -o example_scripts.html
echo New example_scripts.html creation complete!
echo .
echo After verification of example_scripts.html, 
echo please check the file in.
echo .
echo .

