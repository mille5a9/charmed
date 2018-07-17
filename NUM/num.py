""" Script takes data from a text file and uses it
    to learn how numbers should be spelled. May be
    incorrect if the data provided does not cover
    a certain edge case, but such is life. """

#!/usr/bin/python
import math
import sys
import os
import string

def main():
    data_line = []
    i = 0

    f = open("classroom.txt", "r")
    for line in f:
        data_line.append(line)
        i += 1
    f.close()

    data_int = []
    data_str = []
    for x in range(i):
        if x % 2 == 0:
            data_int.append(int(data_line[x]))
        else:
            data_str.append(data_line[x])

    #begin learning with small numbers
    machinemem = dict()
    for x in range(int(i / 2)):
        machinemem[data_int[x]] = data_str[x]

    f = open("test.txt", "w")
    for x in range(1000):
        out = machinemem[x]
        f.write(out)
    f.close()
    return

if __name__ == '__main__':
    main()
