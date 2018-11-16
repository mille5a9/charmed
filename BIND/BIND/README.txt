# BIND

The program runs a Windows Application in the background that acts as a keylogger (built by me, for personal use)
which enables global use of pre-designated macros from a file "Macros.txt" in the C drive and a folder at 
"C:\BINDshortcuts\" of shortcut .lnk files. To add new macros to the text file, follow the format:

key | shortcut name | extra argument | alt pressed? | ctrl pressed? | shift pressed?

- the first macro is for Google Chrome, which demonstrates that the extra argument can be used for visiting a
specific website.

- anything besides "true" in the alt/ctrl/shift fields will count as "false"

- planned additional support for other types of windows functions besides just launching programs
(think copy/paste, auto-shifting focus to specific programs, other previously unavailable power-user features)