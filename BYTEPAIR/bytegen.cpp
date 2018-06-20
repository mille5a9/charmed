//This code generates a large set of data to be compressed
#include <fstream>
#include <stdlib.h>

int main() {
    srand(time(NULL));
    std::ofstream file;

    file.open("bytes.txt");

    for(int i = 0; i < 2000; i++) {
        file << "0000";
        for(int j = 0; j < 4; j++) {
            file << rand() % 2;
        }
        file << "\n";
    }

    file.close();

    return 0;
}
