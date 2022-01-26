using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakConsole
{
    internal class Program
    {
        static List<Cards> deck = new List<Cards>();
        static Cards[,] cardsTable = new Cards[6, 6];
        public static Cards kozir;

        static void Main()
        {
            Console.Write("Welcome to the game Durak !!!\nPlease enter yout name :");
            Player one = new Player();
            one.SetName(Console.ReadLine());
            Player two = new Player();
            two.SetName("Computer");

            CreatingMixingCards();
            DealingCards(one,two, 6, 6);   
            
            int kozirPlace = deck.Count-1;  // sets kozir for the game
            kozir = deck[kozirPlace];
            Console.WriteLine($"Kozir is {kozir.DisplayCard()}");
            int whoAttacks = WhoStarts(one, two);
            Console.WriteLine($"Press any key to start playing");
            Console.ReadKey();
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Kozir is {kozir.DisplayCard()}");
                if (whoAttacks == 0)
                {
                    Console.WriteLine($"{one.GetName} is attaking , Please chose card to start.");
                    DisplayPlayerCards(one);
                    Console.ReadKey();
                }


            }
            

        }

        static int WhoStarts(Player one, Player two) // verifys cards on hand and tell who have the smallest kozir to start
        {
            Cards playerOneSmallestKozir = new Cards();
            Cards playerTwoSmallestKozir = new Cards();
            playerOneSmallestKozir.SetValue(10); // sets values high to be sure they get replaced by kozir in hand 
            playerOneSmallestKozir.SetSuit(5);
            playerTwoSmallestKozir.SetValue(10);
            playerTwoSmallestKozir.SetSuit(5);

            for (int i = 0; i < one.SeePlayerHand().Count; i++)
            {
                if (one.SeePlayerHand()[i].GetSuit() == kozir.GetSuit() && one.SeePlayerHand()[i].GetValue() < playerOneSmallestKozir.GetValue())
                {
                    playerOneSmallestKozir = one.SeePlayerHand()[i];
                }
            }
            for (int i = 0; i < two.SeePlayerHand().Count; i++)
            {
                if (two.SeePlayerHand()[i].GetSuit() == kozir.GetSuit() && two.SeePlayerHand()[i].GetValue() < playerTwoSmallestKozir.GetValue())
                {
                    playerTwoSmallestKozir = two.SeePlayerHand()[i];
                }
            }
            int whoStarts;
            if (playerOneSmallestKozir.GetValue() > playerTwoSmallestKozir.GetValue())
            {
                whoStarts = 1;
                Console.WriteLine($"{two.GetName} have the smallest kozir {playerTwoSmallestKozir.DisplayCard()}");
            }
            else
            {
                whoStarts = 0;
                Console.WriteLine($"{one.GetName} have the smallest kozir {playerOneSmallestKozir.DisplayCard()}");
            }
            return whoStarts;
        }

        static void DisplayPlayerCards(Player One) // Displays ALL the cards of player
        {
            Console.WriteLine($"--------------------------------\n" +
                $"{One.GetName} you cards are :");
            for (int i = 0; i < One.SeePlayerHand().Count; i++)
            {
                Console.WriteLine(One.SeePlayerHand()[i].DisplayCard() + " <--" + i );
            }
        }

        static void DealingCards(Player one, Player two , int oneQty, int twoQty) // Deals carts to players as needet
        {
            for (int i = 0; i < oneQty; i++)
            {
                one.SetCards(deck[0]);
                deck.RemoveAt(0);
                
            }
            for (int i = 0; i < twoQty; i++)
            {
                two.SetCards(deck[0]);
                deck.RemoveAt(0);
            }
        }

        static void CreatingMixingCards() // creates and mixes cards
        {
            int count = 0;
            Cards[] creatingMixing = new Cards[36];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Cards card = new Cards();
                    card.SetValue(j);
                    card.SetSuit(i);
                    creatingMixing[count] = card;
                    count++;
                }
            }
            Random random = new Random();
            
            while (true)
            {
                int rand = random.Next(creatingMixing.Length);
                if (creatingMixing[rand] != null)
                {
                    deck.Add(creatingMixing[rand]);
                    creatingMixing[rand] = null;
                } else if (deck.Count == 36)
                {
                    break;
                }
            }
            return;
        }

        

    }
}
