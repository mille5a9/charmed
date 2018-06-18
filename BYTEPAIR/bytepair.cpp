//Andrew Miller
//University of Cincinnati Computer Engineering '21

//Implementation of byte-pair encoding concept to
//compress a collection of bytes

#include <iostream>
#include <fstream>
#include <string>
#include <math.h>
#include "linkedlist.cpp"

int bstr_t_d(std::string input) {
    //values range from 0 to 255
    int total = 0;
    if (input[7] == 1) total++;
    for (int i = 1; i < 8; i++) {
        if (input[7 - i] == 1) {
            total += pow(2, i);
        }
    }
    return total;
}

std::string d_t_bstr(int input) {
    std::string out = "00000000";
    int accountedfor = 0;
    if (input % 2 == 1) {
        out[7] = '1';
        accountedfor++;
    }
    for (int i = 1; i < 8; i++) {
        if ((input - accountedfor) %
                static_cast<int>(pow(2, i + 1)) ==
                static_cast<int>(pow(2, i))) {
            accountedfor += pow(2, i);
            out[7 - i] = '1';
        }
    }
    return out;
}

int main() {
    std::fstream file;
    mille5a9::Linkedlist<std::string> data, pairs, uniquepairs;
    std::string currentbyte = "";
    file.open("bytes.txt");

    if (!file) throw "The data file is not there!";

    //parses text file with byte data
    int line = 0;
    while (std::getline(file, currentbyte)) {
        data.insert(line, currentbyte);
        line++;
    }
    file.close();

    std::cout << "Initial data consists of "
    << data.itemcount 
    << " bytes of data, uncompressed.\n";
    
    //populates list of pairs
    for (int i = 0; i < data.itemcount - 1; i++) {
        pairs.insert(pairs.itemcount, 
            data.getItem(i) + data.getItem(i + 1));
    }

    //find which pair appears the most
    mille5a9::Linkedlist<int> occurences;
    for (int i = 0; i < pairs.itemcount; i++) {
        for (int j = 0; j < uniquepairs.itemcount; j++) {
            if (uniquepairs.itemcount == 0) break;
            if (pairs.getItem(i) == 
                    uniquepairs.getItem(j)) {
                occurences.setItem(j, occurences.getItem(j) + 1);
                j = 0;
                i++;
                if (i >= pairs.itemcount) break;
            }
        }
        if (i >= pairs.itemcount) break;
        uniquepairs.insert(uniquepairs.itemcount,
            pairs.getItem(i));
        occurences.insert(occurences.itemcount, 1);
    }

    //find a byte that does not appear in the data

    return 0;
}
