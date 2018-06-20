//Andrew Miller
//University of Cincinnati Computer Engineering '21

//Implementation of byte-pair encoding concept to
//compress a collection of bytes

#include <iostream>
#include <fstream>
#include <string>
#include <math.h>
#include "linkedstack.h"
#include "linkedlist.cpp"

//decimal to binary string
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
    mille5a9::Linkedlist<std::string> data,
        pairs, uniquepairs;
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

    int initialcount = data.itemcount;
    std::cout << "Initial data consists of "
    << initialcount
    << " bytes of data, uncompressed.\n";
    
    mille5a9::Linkedlist<int> occurences;
    int maxindex = 0;
    //overarching loop to repeat algorithm until
    //more iterations wont shrink data

    do {
        pairs.clear();
        uniquepairs.clear();
        occurences.clear();
        maxindex = 0;

        //populates list of pairs
        for (int i = 0; i < data.itemcount - 1; i++) {
            pairs.insert(pairs.itemcount, 
                data.getItem(i) + data.getItem(i + 1));
            i++;
        }

        mille5a9::Linkedlist<std::string> pairsdummy;
        for(int l = 0; l < pairs.itemcount; l++) {
            pairsdummy.insert(l, pairs.getItem(l));
        }

        while (pairsdummy.itemcount > 0) {

            int j = 0;
            uniquepairs.insert(uniquepairs.itemcount, 
                pairsdummy.getItem(0));
            occurences.insert(uniquepairs.itemcount - 1, 1);
            for (int k = 0; k - j < pairsdummy.itemcount; k++) {
                if (pairsdummy.getItem(k - j) ==
                    uniquepairs.getItem(
                    uniquepairs.itemcount - 1)) {
                    pairsdummy.remove(k - j);
                    j++;
                    occurences.setItem(uniquepairs.itemcount - 1,
                        occurences.getItem(uniquepairs.itemcount - 1) + 1);
                }
            }
        }
        for (int i = 0; i < occurences.itemcount; i++) {
            if(occurences.getItem(i) >
                occurences.getItem(maxindex))
                maxindex = i;
        }
        //max appearing pair is now stored in uniquepairs.
        //getItem(maxindex) and  occurences.getItem(maxindex)
        //is the number of times it appears

        //now find a byte that does not appear in the data
        std::string freebie = "";
        for (int i = 0; i < 256; i++) {
            freebie = d_t_bstr(i);
            for (int j = 0; j < data.itemcount; j++) {
                if (data.getItem(j) == freebie) {
                    break;
                } else if (j == data.itemcount - 1) {
                    i = 256;
                }
            }
        }

        //condition to catch if the system didn't find
        //an appropriately unused byte
        if (freebie == "11111111")
            throw "Probable Error: Data has no unused bytes";

        //instantiaing stack to hold the substitute byte,
        //and the two bytes it replaces
        mille5a9::Stack<std::string> keybyte,
            pairbyte1, pairbyte2;
        std::string one, two;
        for (int i = 0; i < 8; i++) {
            if (i < 4) one +=
                uniquepairs.getItem(maxindex)[i];
            else two += uniquepairs.getItem(maxindex)[i];
        }
        keybyte.push(freebie);
        pairbyte1.push(one);
        pairbyte2.push(two);

        //iterate through the pairs list, and edit
        //the data list accordingly
        int a = 0;
        for (int i = 0; i + a < pairs.itemcount; i++) {
            if (pairs.getItem(i + a) == 
                uniquepairs.getItem(maxindex)) {
                data.remove(i);
                data.setItem(i, freebie);
                a++;
            }
        }

        std::cout << "." << std::flush;
    } while (occurences.getItem(maxindex) > 3);

    float compercent = (1 -  
        static_cast<float>(data.itemcount) /
        static_cast<float>(initialcount)) * 100;
    std::cout << "\nCompression complete!" << std::endl
        << "The data is now of length " <<
        data.itemcount << "!\n"
        << "The data has been compressed by "
        << compercent << "%\n";
    return 0;
}
