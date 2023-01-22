#include <iostream>
using namespace std;
#include <unistd.h>
#include <stdlib.h>
#include <time.h>
int c=0;
int g;
class Cards
{
    private:
        int number;
        int suit;
    public:
        Cards()
        {}
        Cards(int n , int s) : number(n), suit(s)
        {}
        void gen_random(int&g)
        {
            g = rand();
        }
        void shuffle(Cards& , Cards&, Cards&);
        void choice(Cards&,Cards& , Cards&, Cards&);
        void display();
};
void Cards :: shuffle(Cards& obj1 , Cards& obj2, Cards& obj3)
{   int nt ,st;
    cout << "The shuffling begins..." << endl;
    cout << "Interchanging 1st Card and 3rd Card..." << endl;
        nt = obj1.number;
        st = obj1.suit;
        obj1 = obj3;
        obj3.number = nt;
        obj3.suit = st;
        sleep(2);
    cout << "Interchanging 2nd Card and 1st Card..." << endl;
        nt = obj1.number;
        st = obj1.suit;
        obj1 = obj2;
        obj2.number = nt;
        obj2.suit = st;
        sleep(2);
    cout << "Moving 3rd Card to 1st position and 1st Card to 3rd Position..." <<endl;
        nt = obj1.number;
        st = obj1.suit;
        obj1 = obj3;
        obj3.number = nt;
        obj3.suit = st;
        sleep(2);
    cout << "Interchanging 2nd Card and 3rd Card..." << endl ;
        nt = obj3.number;
        st = obj3.suit;
        obj3 = obj2;
        obj2.number = nt;
        obj2.suit = st;
        sleep(2);

        if(g%2 == 0)
        {
        cout << "Interchanging 2nd Card and 1st Card..." << endl << endl;
        nt = obj1.number;
        st = obj1.suit;
        obj1 = obj2;
        obj2.number = nt;
        obj2.suit = st;
        sleep(2);
        }
        else
        {
        cout << "Replacing 3rd Card with 1st Card..." <<endl << endl;
        nt = obj1.number;
        st = obj1.suit;
        obj1 = obj3;
        obj3.number = nt;
        obj3.suit = st;
        sleep(2);
        }
        
    
}


void Cards :: choice(Cards& p, Cards& obj1 , Cards& obj2, Cards& obj3)
{   
    int n,s;
    g = rand();
    if(g%5==0)
    {   cout << "Enter the Card number and suit of the card positioned at 1st place" << endl;
        p = obj1;
        cin >> n >> s;
    }
    else if(g%2==0)
    {   cout << "Enter the Card number and suit of the card positioned at 2nd place" << endl;
        p = obj2;
        cin >> n >> s;
    }
    else
    {   cout << "Enter the Card number and suit of the card positioned at 3rd place" << endl;
        p = obj3;
        cin >> n >> s;
    }
    if(n == p.number && s == p.suit)
    {   c++;
        obj1.display();
    }
    else
    {
        obj1.display();
        cout << "Number = " << p.number << " Suit = " << p.suit << endl;
    }
    
    


}


void Cards :: display()
{   
    (c==1)?cout << "You Won !!!" << endl:cout << "You Lost... Nice try Dummy!!!" << endl;
}


int main ()
{   Cards jack_h(11,3),ace_s(12,2),king_d(13,1);
    srand(time(0));
    g=rand();
    cout <<endl<< "*** The Rules are Simple ***" << endl;
    cout << "There are total 3 cards in total" << endl;
    
    if(g%5==0)
    {
    cout <<"1. Jack of Hearts (3rd Suit of 11th number)" << endl << "2. Ace of Spades(2nd Suit of 12th number)" << endl << "3. King of Diamonds(1st Suit of 13th Number)" << endl << endl;
    }
    else if(g%2==0) 
    {
     cout <<"1. King of Diamonds(1st Suit of 13th Number)" << endl << "2.Jack of Hearts (3rd Suit of 11th number)" << endl << "3. Ace of Spades(2nd Suit of 12th number)" << endl << endl;
    }
    else
    {
        cout <<"1.  Ace of Spades(2nd Suit of 12th number)" << endl << "2. King of Diamonds(1st Suit of 13th Number)" << endl << "3. Jack of Hearts (3rd Suit of 11th number)" << endl << endl;
    }
    cout << "The Cards will be reshuffled" << endl;
    cout << "You have to guess the number and suit of the card at a certain position" << endl << endl;
    //cout << "Plus you will have only one minute to make your choice" << endl;
    cout << "You Ready ?(y/n)" << endl;
    char ch;
    cin >> ch;
    if(ch == 'y' || ch == 'Y')
    {
    Cards temp,prize;
    
    if(g%5==0)
    {
        temp.shuffle(jack_h , ace_s , king_d);
        temp.choice(prize, jack_h , ace_s , king_d);
    }
    else if(g%2==0) 
    {
        temp.shuffle(king_d , jack_h , ace_s);
        temp.choice(prize, king_d , jack_h , ace_s);
    }
    else
    {
        temp.shuffle(ace_s , king_d , jack_h);
        temp.choice(prize, ace_s , king_d , jack_h );
    }
    
    
    }
    else
    {
        cout << "No Problem next time then." << endl;
    }
    return 0;
}
