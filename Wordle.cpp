#include <iostream>
#include <fstream>
#include <time.h>
#include <iomanip>
#include <string>
#include <vector>
#include <algorithm>
#include <windows.h>
using namespace std;

unsigned int chances = 5;


#pragma region Declarations



void Generate_Word(string& str);
string validate(string& res, string& str);
void Rule_Description();
void The_Game();
void PrintColor(string& str);
bool BackCheck(string& str, int pos);



#pragma endregion Declartions




int main()
{

    cout << "*** Welcome to Wordle ***" << endl;
    Rule_Description();
    The_Game();
    return 0;
}


#pragma region Definations
void Generate_Word(string& str)
{
    srand(time(0) + 999999999);
    int n = 1 + (rand() % 12972);
    // cout << n << endl;
    ifstream in("valid-wordle-words.txt");
    for (int i = 1; i <= n; i++)
    {
        in >> str;
    }

}

void Rule_Description()
{

    cout << "Rules of the game are: " << endl;
    cout << setw(10) << "1. You have to find the correct 5 letter word" << endl;
    cout << setw(10) << "2. Characters which are in the actual word but in the wrong position will be colored YELLOW" << endl;
    cout << setw(10) << "3. Characters which are NOT in the actual word will colored WHITE." << endl;
    cout << setw(10) << "4. Characters which are in the actual word and in their correct position will be colored GREEN" << endl;
    cout << setw(10) << "5. Words should be entered in LOWERCASE ONLY." << endl;
    cout << setw(10) << "6. Note you will be given only 5 CHANCES to win" << endl << "Good Luck" << endl;


}

bool BackCheck(string& str, int pos)
{
    if (pos != 0)
    {
        for (int i = 0; i < pos; i++)
        {
            if (str[i] == str[pos])
            {
                return true;
            }
        }
    }
    return false;
}

string validate(string& res, string& str)
{
    string str1, temp = res;
    for (int i = 0; i < 5; i++)
    {

        if (temp[i] == str[i])
        {
            str1.push_back(str[i] - 32);
            temp[i] = '.';
        }
        else
        {
            unsigned int t = temp.find(str[i]);

            if (t < 0 || t > 4)
            {
                str1.push_back('|');
                str1.push_back(str[i]);
                str1.push_back('|');
            }
            else
            {
                temp[temp.find(str[i])] = '.';
                t = temp.find(str[i]);
                if (BackCheck(str, i) && !(t < 0 || t > 4))
                {
                    str1.push_back('|');
                    str1.push_back(str[i]);
                    str1.push_back('|');
                }
                else
                {
                    str1.push_back(str[i]);
                }

            }
        }
    }
    return str1;
}


void PrintColor(string& str)
{
    HANDLE h = GetStdHandle(STD_OUTPUT_HANDLE);
    for (int i = 0; i < str.length(); i++)
    {
        if (65 <= str[i] && str[i] <= 90)
        {
            SetConsoleTextAttribute(h, 2);
            cout << (char)(str[i] + 32);
            SetConsoleTextAttribute(h, 15);
        }
        else if (97 <= str[i] && str[i] <= 122)
        {
            SetConsoleTextAttribute(h, 14);
            cout << str[i];
            SetConsoleTextAttribute(h, 15);
        }
        else if (str[i] == '|')
        {
            //cout << "| detected" << endl;
            SetConsoleTextAttribute(h, 15);
            i++;
            cout << str[i];
            i += 1;
            //cout << "After increment :" << endl << "i = " << i << endl << "str[i] = " << str[i] << endl;
            continue;
        }

    }
    cout << endl;
}


void The_Game()
{
    string result, str;
    Generate_Word(result);
    cout << "Chances : 5/5" << endl;
    cout << result << endl;
    while (chances != 0)
    {
        cout << "Enter the word" << endl;
        getline(cin, str);
        if (result == str)
        {
            if (chances == 5)
            {
                cout << "DAMN !! at once" << endl << "You are the Winner !!!" << endl;
            }
            else if (chances == 4)
            {
                cout << "Not bad !!! You are the Winner !!!" << endl;
            }
            else
            {
                cout << "Correct !!! You are the Winner !!!" << endl;
            }
            cout << "Word was'" << result << "'" << endl;
            return;
        }
        else
        {
            cout << "Chances : " << --chances << " / 5" << endl;
            string temp = validate(result, str);
            //cout << "Temp = " << temp << endl;
            PrintColor(temp);
        }
    }
    cout << "You Lost, Game Over !!!" << endl;
    cout << "Word was' " << result << " '" << endl;
}
#pragma endregion Definations
