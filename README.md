# charmed

Collection of code written by me (Andrew Miller) during
my time as an undergraduate student at the University
of Cincinnati.

Inspiration drawn by wikipedia documentation, my
Computer Engineering coursework, and my professional
experience as a co-op web developer at Fortech LLC.

## Current contents:

### ADT

The ADT folder contains files that store self-written
Abstract Data Types. Some of the ADTs were written for
my classes at the University of Cincinnati. The files in
this directory are copied into other projects in this
repository as needed.

The mille5a9lib project is a C# implementation of the
types in this folder. They were written for the purpose
of studying for Microsoft's Exam 70-483 for
certification in C#.

### ATM

A C# Console Application that utilizes the DSharpPlus
library to create a bot for the popular communication
service Discord. The bot has numerous capabilities
that are taken advantage of by friends who are also
on the platform.

### BIND

A C# Console Application that implements a very
barebones keylogger, and checks a local configuration
file for settings. Can be configured to execute
programs with custom keybinds.

### BYTEPAIR

Implementation of the Byte-Pair Encoding concept. Takes an
input text file of bytes that are separated by newlines,
and compresses the bytes using byte-pair encoding.
Displays the new compressed number of bytes and the
compression ratio.

### HASH

Small C# Console Application that uses Microsoft\'s
System.Security.Cryptography Namespace to detect
the usage of one of five different hash functions.
The user inputs a string and the hash result that
came from the string, and the program informs the
user of which function the hash came from.

### MATH

Algebra Expression Solver. Original intention was to
support using variables, but the current state of
this project only supports solving numeric expressions.
Parenthesis parsing works, sometimes operators are a
bit fuzzy - especially when it comes to mistaking
binary operators for unary ones. This project includes
some extra functions that act as shortcuts for common
formulas used in the world of Finance.

### MAZE

New attempt of Lab 3 from a Data Structures course. Lab 3
presented the task to solve a maze using stacks. Now
with knowledge of graphs, a shortest path function
with breadthfirstsearch methodology proves very useful
to better solve mazes.

### NUM

Contains multiple attempts to implement a natural
language processor specifically for spelling out
numbers and recognizing numbers that are spelled out.
The C++ and Python implementations are poor, and the
new C# project to accomplish this is still in infancy.

### REACT

A collection of JSX files containing React.js components
for interactive web applications.

### SCRAPE

An initial setup for a web-scraping system using
HtmlAgilityPack. Currently has functions to support
scraping Links from a google search results page, and
such a function has been implemented into the ATMBot
discord bot.

### WAAM

An infantile cryptocurrency called Waamcoin, which has
fallen by the wayside because there is no server to
host it. Waamcoin utilizes the SHA256 hashing algorithm
for its security. Waamcoin is also used in the ATM
project with heavy modifications to its functionality.

## Planned additions:

In the future, other projects of interest may be added.