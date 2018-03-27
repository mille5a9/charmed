/*  A second attempt to create the Message Digest 5 historic hashing algorithm
    by Andrew Miller, University of Cincinnati Computer Engineering Sophomore   */

#include <iostream>
#include <string>
#include <tclap/ValueArg.h>

class Md5 {

public:
    Md5(std::string x) {
        working_string = x;
    }

private:
    std::string working_string;
};

int main (int argc, const char ** argv) {

    std::string data_input = "";

    /* Parse the input string as a command line arguement */        
    TCLAP::ValueArg<std::string>::ValueArg (const std::string &flag = "data",
        const std::string &name = "data_input",
        const std::string &desc = "The string that is to be converted to a hash.",
        bool req = true, std::string data_input, "");

    return 0;
}
