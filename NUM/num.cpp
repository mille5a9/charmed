//C++ implementation of machine-learning
//number reader
#include <iostream>
#include <fstream>
#include <string>
#include "linkedlist.h"

int main() {
    Linkedlist<std::pair<int, std::string>> *rawdata
        = new Linkedlist<std::pair<int, std::string>>();

    ifstream f;
    f.open("classroom.txt");
    for(int i = 0; i < 30; i++) {
        std::pair<int, std::string> temp;
        getline(f, temp.first);
        getline(f, temp.second);
        rawdata->insert(rawdata->getSize(), temp);
    }

    Linkedlist<std::string> *output
        = new Linkedlist<std::string>();
    while (true) {
        int choice = 0;
        std::cout << "Please provide a number to translate (99999 max): ";
        std::cin >> choice;

        //time to analyze the input
        int thou = choice / rawdata.getItem(29).first;
        if (thou && thou < 20) {
            output->insert(0,
                rawdata.getItem(thou).second);
            output->insert(1, "thousand");
        } else if (thou > 19) {
            int carry = thou - (thou % 10);
            output->insert(0,
                rawdata.getItem(carry / 10 + 18).second);
            if (thou % 10 > 0) {
                output->insert(1,
                    rawdata.getItem(thou % 10).second);
            }
            output->insert(output.getSize(), "thousand");
        }
    }
    return 0;
}
