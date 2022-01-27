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
        static Cards[,] cardsTable = new Cards[2, 6];
        public static Cards kozir;

        static void Main()
        {
            Console.Write("Welcome to the game Durak !!!\nPlease enter yout name :");
            Player one = new Player();
            one.SetName(Console.ReadLine());
            Player two = new Player();
            two.SetName("Computer");

            CreatingMixingCards();
            DealingCards(one, two, 6, 6);

            int kozirPlace = deck.Count - 1;  // sets kozir for the game
            kozir = deck[kozirPlace];
            Console.WriteLine($"Kozir is {kozir.DisplayCard()}");
            int whoAttacks = WhoStarts(one, two);
            Console.WriteLine($"Press any key to start playing");
            Console.ReadKey();
            while (true)
            {
                int loopsCount = 0;                
                Console.WriteLine($"Kozir is {kozir.DisplayCard()}");
                while (whoAttacks == 0)// player attacks
                {
                    Console.WriteLine($"{one.GetName} is attaking , Please chose card to start.");
                    DisplayPlayerCards(one);
                    int chosenCard = IfIntAndValidChoise(Console.ReadLine(),one);

                    if (loopsCount > 0) verifyTableCardsAtt(one, chosenCard);
                    if (chosenCard == 100)// if user wants to stop attacking after first card
                    {
                        whoAttacks = 1;
                        tableCardsPickUp(two, loopsCount);
                        addCards(one);
                        loopsCount = 0;
                        break;
                    }

                    cardsTable[0,loopsCount] = one.GetCard(chosenCard);
                    cardsTable[1,loopsCount] = two.ComputerDefending(cardsTable[0,loopsCount]);

                    if (cardsTable[1, loopsCount] != null)
                    {
                        Console.Write($"You attacked with {cardsTable[0, loopsCount].DisplayCard()} and {two.GetName} defendet with {cardsTable[1, loopsCount].DisplayCard()}\n" +
                            $"{one.GetName} do you wish to continue the attack ? 'y' for yes :");
                        string response = Console.ReadLine();
                        if (response.Equals("y"))// continues attack
                        {
                            loopsCount++;
                            break;
                        } else // stops atack
                        {
                            addCards(two);
                            addCards(one);
                            emptyTableCards(loopsCount);
                            loopsCount = 0;
                            whoAttacks = 1;
                            break;
                        }
                    } else if (cardsTable[1,loopsCount] == null)// didnt had card to deffend
                    {
                        tableCardsPickUp(two, loopsCount);
                        addCards(one);
                        loopsCount = 0;
                        break;
                    }

                    
                }


            }

        }

        static int verifyTableCardsAtt(Player player, int chosenCard)
        {
            bool sameValue = false;
            foreach ( Cards card in cardsTable)
            {
                if (card.GetValue() == player.SeePlayerHand()[chosenCard].GetValue()) { sameValue = true; }
            }
            if (!sameValue)
            {
                Console.Write("There no card like that on the table, please chose the right card or type 'stop' to finish attack:");
                string imput = Console.ReadLine();
                if (imput.Equals("stop")) return 100;
                chosenCard = IfIntAndValidChoise(imput, player);
                verifyTableCardsAtt(player, chosenCard);
            }
            return chosenCard;
        }

        static void tableCardsPickUp(Player player, int loopsCount)// Gives all the cards on the table to loser
        {
            Console.WriteLine($"{player.GetName} didnt have the cards to defend ! Take card/s if needet and attack again !");
            for (int i = 0; i <= loopsCount; i++)
            {
                if(cardsTable[0, i] != null) player.SetCards(cardsTable[0, i]);
                if(cardsTable[1, i] != null) player.SetCards(cardsTable[1, i]);
                cardsTable[0, i] = null;
                cardsTable[1, i] = null;
            }
        }

        static void emptyTableCards(int loopsCount)
        {
            for (int i = 0; i <= loopsCount; i++)
            {                
                cardsTable[0, i] = null;
                cardsTable[1, i] = null;
            }
        }

        static void addCards(Player player)// verify if winer have 6 cards , if less, adds from the deck
        {
            if (player.SeePlayerHand().Count() < 6)
            {
                int count = 6 - player.SeePlayerHand().Count();
                for (int i = 0; i < count; i++)
                {
                    player.SetCards(deck[0]);
                    deck.RemoveAt(0);
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
                Console.WriteLine(One.SeePlayerHand()[i].DisplayCard() + " <--" + i);
            }
        }

        static void DealingCards(Player one, Player two, int oneQty, int twoQty) // Deals carts to players as needet
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

        public static int IfIntAndValidChoise(string imput, Player player)
        {
            int imputInt;
            bool ifInt = false;
            while (true)
            {
                ifInt = int.TryParse(imput, out imputInt);
                if (ifInt) break;
                Console.Write("You didnt enter a number, please try again :");
                imput = Console.ReadLine();
            }
            if (imputInt >= player.SeePlayerHand().Count() || imputInt < 0)
            {
                Console.Write("You entered a to high number or to low, please enter a correct one :");
                imputInt = IfIntAndValidChoise(Console.ReadLine(), player);
            }
            return imputInt;
        } 
        

    }
}
