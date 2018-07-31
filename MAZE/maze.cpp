//solves the maze in maze.txt
#include <fstream>
#include <iostream>
#include "graph.h"

class Tile {
public:
    Tile() = default;
    Tile(int x, int y)
        : x(x), y(y) {};
    int x = 0, y = 0;

    bool operator==(Tile a) {
        if (this->x == a.x && this->y == a.y) return true;
        else return false;
    }
};

int main() {
    mille5a9::Graph<Tile>* board = 
        new mille5a9::Graph<Tile>();
    std::ifstream file;
    char printout[20][20];

    std::cout << "Building Maze...\n";
    file.open("maze.txt");
    if (!file) std::cout << "Error: the file did not open!\n";
    char temp = '\0';
    while (!file.eof()) {//hardcoded for 20x20 here
        std::string line = "";
        for (int y = 0; getline(file, line); y++) {
            for (int x = 0; x < 20; x++) {
                printout[y][x] = line[x];
                if (line[x] == '2') continue;
                if (y > 0)  board->addEdge(//north
                       Tile(x, y),
                       Tile(x, y - 1));
                if (x < 19) board->addEdge(//east
                        Tile(x, y),
                        Tile(x + 1, y));
                if (y < 19) board->addEdge(//south
                        Tile(x, y),
                        Tile(x, y + 1));
                if (x > 0) board->addEdge(//west
                        Tile(x, y),
                        Tile(x - 1, y));
            }
        }
    }
    file.close();

    std::cout << "Solving Maze...\n";
    mille5a9::LinkedList<Tile>* ans = 
        board->shortestPath(Tile(0, 0), Tile(19, 19));
    int size = ans->getSize();
    for (int i = 0; i < size; i++) {
        printout[ans->getItem(i).y][ans->getItem(i).x] = 'x';
    }
    for (int i = 0; i < 400; i++) {
        char out = '\0';
        if (printout[(int)(i / 20)][i % 20] == '1') out = ' ';
        else if (printout[(int)(i / 20)][i % 20] == '2') out = 'T';
        else out = 'x';
        std::cout << out << std::flush;
        if (i % 20 == 19) std::cout << std::endl;
    }
    return 0;
}
