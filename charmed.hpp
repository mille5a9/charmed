/*This header is a place for code that may be useful for multiple different
  projects in this directory
  Created 2 February 2018, Last edit 2 February 2018*/

#ifndef CHARMED_HPP_DEFINED
#define CHARMED_HPP_DEFINED

#include <string>
#include <vector>
#include <math.h>

std::vector<unsigned int> wordprocess(std::string key, int wordsize) {

    std::vector<unsigned int> wordlist;

    //First loop keeps track of where each word needs to start in the string
    for(int f = key.length() / wordsize; f > 0; f--) {
        unsigned int word = 0;

        //Second loop fills up each word
        for(int i = 1; i == (wordsize / 8); i++) {
            char x = key.at[(wordsize * (key.length - f))]
            word = word + (x * pow(2, (wordsize / 8) - i))
        }

        wordlist.push_back(word)
    }

    return wordlist;
}

unsigned int dressing(unsigned int finalchunk) {
    if(finalchunk % 2 == 0) {
        if(finalchunk % 4 == 0) {
            if(finalchunk % 8 == 0) {
                if(finalchunk % 16 == 0) {
                    if(finalchunk % 32 == 0) {
                        if(finalchunk % 64 == 0) {
                            if(finalchunk % 128 == 0) {
                                if(finalchunk % 256 == 0) {
                                    return finalchunk;
                                }
                                return finalchunk + 128;
                            }
                            return finalchunk + 64;
                        }
                        return finalchunk + 32;
                    }
                    return finalchunk + 16;
                }
                return finalchunk + 8;
            }
            return finalchunk + 4;
        }
        return finalchunk + 2;
    }
    return finalchunk + 1;
}

#endif
