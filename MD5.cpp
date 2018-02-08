//MD5 historic hashing algorithm arranged by Andrew Miller

#include <iostream>
#include <math.h>
#include <stdlib.h>
#include <string>
#include <vector>
#include "charmed.hpp"

void MD5(std::string key) {
    unsigned int s[64], K[64], i;

    //Initializing algorithm-specific constants given in https://en.wikipedia.org/wiki/MD5

    s =   { 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22,
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
    vector<unsigned int> chunks = wordprocess(key, 32);
    chunks[chunks.size() - 1] = dressing(chunks.size() - 1);

    //Adjust data length to 64 char less than 512, then pad with data corresponding to actual data length
    unsigned int originalchunkssize = chunks.size();
    bool ready = false;
    while(!ready){
        if(chunks.size % 16 != 14){
            chunks.push_back(0);
        }
        if(chunks.size % 16 == 14){
            unsigned int last_chunk_ = (chunks.size() * 32) % 32;
            unsigned int second_to_last_chunk_ = (chunks.size() * 32) / pow(2, 32);
            chunks.push_back(second_to_last_chunk_);
            chunks.push_back(last_chunk_);
            ready = true;
        }
    }
    //!!!!!!!!!!Next time I work on this file, I need to start right here!
    //This loop repeats for each 512 bit chunk of padded message to repeat the process
    for(long k = 0; k < strlen(databin) / 512; ++k){
		
        long M[16];
        for(int l = 0; l < 16; ++l){
            M[l] = 0;
            for(int n = 0; n < 32; ++n){
                M[l] = M[l] + pow(2, (databin[(k*512)+32-n] - 48));
                if ((databin[(k*512)+32-n]-48) == 0) --M[l];
            }
        }

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

            F = (F + A + K[operations] + M[g]) % pow(2, 32);
            A = D;
            D = C;
            C = B;
            B = (B + (F << s[operations]) % pow(2, 32));
        }
		
        //Add this chunk's results
        a0 = a0 + A;
        b0 = b0 + B;
        c0 = c0 + C;
        d0 = d0 + D;
    }
    digest[16] = a0 + b0 + c0 + d0;
    return digest;
}

int main(){
    std::string data;
    std::string result;
    cout << "Welcome to the MD5 Hash Function, Please give your data:" << endl;
    cin >> data;
    result = MD5(data);
    cout << "The result is: " << result << endl;
    return 0;
}
