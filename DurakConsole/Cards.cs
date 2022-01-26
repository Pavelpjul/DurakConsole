using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakConsole
{
    internal class Cards
    {
        int suit;
        int value;

        public void SetSuit(int suit) { this.suit = suit; } //sets suit value

        public int GetSuit() { return this.suit; } // returns value of suit

        public void SetValue(int value) { this.value = value;} // sets value of the card

        public int GetValue() { return this.value;} // returns value of this card

        public string DisplayCard()  // displays the card
        {
            string[] suits = { "♣", "♦", "♥", "♠" }; // clubs, diamonds, hearts, spades
            string[] values = { "6", "7", "8", "9", "10", "Jack", "Queen", "King", "ACE" };            
            return "|" + suits[suit] + values[value] + "|";
        }

        



    }
}
