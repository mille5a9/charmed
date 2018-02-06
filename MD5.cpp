//MD5 historic hashing algorithm arranged by Andrew Miller

#include <math.h>
#include <stdlib.h>
#include <cstring>
#include <bitset>
#include <string>

using namespace std;

void MD5(char data[], char *&digest[16]){

	long s[64], K[64], i;

	//Initializing algorithm-specific constants given in https://en.wikipedia.org/wiki/MD5

	s =   { 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22,
		5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20,
		4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23,
		6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21 };

	for(i = 0; i < 64; ++i) K[i] = floor(pow(2, 32) * abs(sin(i + 1)));

	long a0 = 0x67452301;
	long b0 = 0xefcdab89;
	long c0 = 0x98badcfe;
	long d0 = 0x10325476;

	//Convert data string from arg to raw binary data

	char databin[8 * strlen(data)];
	for(long j = 0; j < strlen(data); j++){
		strcat(databin, (bitset<8>(data[j]));
	}

	//Adjust data length to 64 char less than 512, then pad with data corresponding to actual data length

	long originalstrlen = strlen(databin) % pow(2, 64);
	strcat(databin, "1");
	while((strlen(databin) % 512) < 448) strcat(databin, "0");
	strcat(databin, bitset<64> (originalstrlen));
	
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
		long A = a0;
		long B = b0;
		long C = c0;
		long D = d0;

		//Main loop with MD5 nonlinear function and modular addition
		for(int operations = 0; operations < 64; ++operations){
			long F, g;
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
	return;
}

int main(){
	char data[];
	string result;
	cout << "Welcome to the MD5 Hash Function, Please give your data:" << endl;
	cin >> data;
	MD5(data, char digest[16]);
	cout << "The result is: " << digest << endl;
	return 0;
}
