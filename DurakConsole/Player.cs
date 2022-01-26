using System.Collections.Generic;

namespace DurakConsole
{
    internal class Player
    {
        string playerName;
        List<Cards> playerCards = new List<Cards>();



        public string GetName { get { return playerName; } } // Returns player name

        public void SetName(string name) // Sets player name
        {
            this.playerName = name;
        }

        public void SetCards(Cards card) // add card to player hand
        {
            playerCards.Add(card);
        }

        public List<Cards> SeePlayerHand() // returns list to see
        {
            return playerCards;
        }

        public Cards GetCard(int place) // retunrs card and removes it from the list
        {
            Cards card = playerCards[place];
            playerCards.RemoveAt(place);
            return card;
        }

        public Cards ComputerDefending(Cards att)// Computer choses defending card
        {
            Cards def = null;

            for (int i = 0; i < this.playerCards.Count; i++)
            {
                if (this.playerCards[i].GetSuit() == att.GetSuit() && this.playerCards[i].GetValue() > att.GetValue())// to see if computer have higher card in same suit
                {
                    if (def == null || def.GetValue() > this.playerCards[i].GetValue()) def = this.playerCards[i];
                }
            }
            if (def == null)
            {
                for (int i = 0; i < this.playerCards.Count; i++)// if computer didnt have same suit, choses lowest kozir to beat the card
                {
                    if (this.playerCards[i].GetSuit() == Program.kozir.GetSuit() && att.GetSuit() != Program.kozir.GetSuit())
                    {
                        if (def == null || def.GetValue() > this.playerCards[i].GetValue()) def = this.playerCards[i];
                    }
                }
            }
            if (def != null)this.playerCards.Remove(def);
            
            return def;// return null if didnt have cards to beat the attacking card
        }
    }
}
