#!/bin/bash

#Program name: Blinking_exit

#Author: Ethan Jones
#Author contact: ejonest@csu.fullerton.edu

#System requirements:
#  A Linux system with BASH shell (in a terminal window).
#  The mono compiler must be installed.  If not installed run the command "sudo apt install mono-complete" without quotes.
#  The source files and this script file must be in the same folder.
#  This file, run.sh, must have execute permission.  Go to the properties window of build.sh and put a check in the
#  permission to execute box.

#To compile and run this project use sh run.sh

echo "Remove old binary files"
rm *.dll
rm *.exe

echo "List files"
ls -l

echo "Compile UI.cs to create the file: UI.dll"
mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:UI.dll ExitUI.cs

echo "Compile ExitMain.cs and link it with UI.dll. Create Exit.exe from this"
mcs -r:System -r:System.Windows.Forms -r:UI.dll -out:Exit.exe ExitMain.cs

echo "List files again"
ls -l

echo "Run generated executable"
./Exit.exe

echo "Program terminated."