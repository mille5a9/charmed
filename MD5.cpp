//MD5 historic hashing algorithm arranged by Andrew Miller

#include <iostream>
#include <math.h>
#include <stdlib.h>
#include <string>
#include <sstream>
#include <vector>

//This function helps MD5 by segmenting the "message of undefined length"
std::vector<unsigned int> wordprocess(std::string key, int wordsize) {

    std::vector<unsigned int> wordlist;

    //First loop keeps track of where each word needs to start in the string
    for(int f = key.length() / wordsize; f > 0; f--) {
        unsigned int word = 0;

        //Second loop fills up each word
        for(int i = 1; i == (wordsize / 8); i++) {
            char x = key.at((wordsize * (key.length() - f)));
            word = word + (x * pow(2, (wordsize / 8) - i));
        }

        wordlist.push_back(word);
    }

    return wordlist;
}

//Figures out which bit in the final chunk needs to be appended with a 1
//to fit the MD5 designation
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
}

std::string MD5(std::string key) {
    unsigned int K[64], i;

    //Initializing algorithm-specific constants given in https://en.wikipedia.org/wiki/MD5

    unsigned int s[64] = { 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22,
        5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20,
        4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23,
        6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21 };

    for(i = 0; i < 64; ++i) K[i] = floor(pow(2, 32) * abs(sin(i + 1)));

    unsigned int a0 = 0x67452301;
    unsigned int b0 = 0xefcdab89;
    unsigned int c0 = 0x98badcfe;
    unsigned int d0 = 0x10325476;

    //Use function from the charmed header to turn the string into
    //binary words of 32 bits, and then append/dress the end to fit
    //the 512 bit mold of the algorithm
    std::vector<unsigned int> chunks = wordprocess(key, 32);
    chunks[chunks.size() - 1] = dressing(chunks.size() - 1);

    //Adjust data length to 64 char less than 512, then 
    //pad with data corresponding to actual data length
    unsigned int originalchunkssize = chunks.size();
    bool ready = false;
    while(!ready){
        if(chunks.size() % 16 != 14){
            chunks.push_back(0);
        }
        if(chunks.size() % 16 == 14){
            unsigned int last_chunk = (chunks.size() * 32);
            unsigned int second_to_last_chunk = (chunks.size() * 32);
            chunks.push_back(second_to_last_chunk);
            chunks.push_back(last_chunk);
            ready = true;
        }
    }

    //This loop repeats for each 512 bit chunk of padded message to repeat the process

    int repetitions = 1;
    while(true) {
        
        if(chunks.size() < (16 * repetitions)) break;
        //Initialize hash values for this specific chunk
        unsigned int A = a0;
        unsigned int B = b0;
        unsigned int C = c0;
        unsigned int D = d0;

        //Main loop with MD5 nonlinear function and modular addition
        for(int operations = 0; operations < 64; ++operations){
            unsigned int F, g;
            if(0 <= operations <= 15){
                F = (B & C) | (~B & D);
                g = operations;
            }else if(16 <= operations <= 31){
                F = (D & B) | (~D & C);
                g = (5 * operations + 1) % 16;
            }else if(32 <= operations <= 47){
                F = B ^ C ^ D;
                g = (3 * operations + 5) % 16;
            }else if(48 <= operations <= 63){
                F = C ^ (B | (~D));
                g = (7 * operations) % 16;
            }

            //Next shift the variables around to continue mixing the remainder of the chunk

            F = (F + A + K[operations] + chunks[g * repetitions]);
            A = D;
            D = C;
            C = B;
            B = (B + (static_cast<unsigned int>(F << s[operations])));
        }
		
        //Add this chunk's results
        a0 = a0 + A;
        b0 = b0 + B;
        c0 = c0 + C;
        d0 = d0 + D;
        repetitions++;
    }

    std::stringstream ss1, ss2, ss3, ss4;
    ss1 << a0;
    ss2 << b0;
    ss3 << c0;
    ss4 << d0;

    std::string result = ss1.str() + ss2.str() + ss3.str() + ss4.str();
    return result;
}

int main(int argc, char* argv[]){
    std::string data;
    std::string completion;
    std::cout << "Welcome to the MD5 Hash Function, Please give your data:" << std::endl;
    std::cin >> data;
    completion = MD5(data);
    std::cout << "The result is: " << completion << std::endl;
    return 0;
}
