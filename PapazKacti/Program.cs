using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace PapazKacti
{
    internal class Program
    {
        public int papazCount;

        static int Kontrol(string search, List<string> namesList)
        {
            int count = 0;

            for (int i = 0; i < namesList.Count(); i++)
            {
                if (namesList[i] == search)
                {
                    count++;
                }
            }
            return count;
        }
        static void Round (List<string> player,List<string> otherPlayer, string randomz,List<int>ValueExists1,List<int>ValueExists2,List<string>ValuesList,int papazCount)
        {
          

            if (player.Count != 0)
            {
                
                randomz = player[Random(player)];
                if (randomz == "King")
                {
                    papazCount++;
                }
                player.Remove(randomz);
                otherPlayer.Add(randomz);
                ValueExists1.Clear();
                ValueExists2.Clear();
                foreach (var i in ValuesList)
                {
                    ValueExists1.Add(Kontrol(i, player));
                }
                foreach (var i in ValuesList)
                {
                    ValueExists2.Add(Kontrol(i, otherPlayer));
                }
                Remove(ValueExists1, ValuesList, player);
                Remove(ValueExists2, ValuesList, otherPlayer);
                
            }
        }
        public void getRound(int papazCount)
        {
            this.papazCount = papazCount;
            Console.WriteLine(papazCount);
        }
        static void Remove(List<int> ValueExists, List<string> ValuesList, List<string> player)
        {
            
            for (int i = 0; i < ValuesList.Count; i++)
            {
                if (ValueExists[i] == 2 || ValueExists[i] == 3)
                {
                    player.Remove(ValuesList[i]);
                    player.Remove(ValuesList[i]);
                }
                else if (ValueExists[i] == 4)
                {
                    player.Remove(ValuesList[i]);
                    player.Remove(ValuesList[i]);
                    player.Remove(ValuesList[i]);
                    player.Remove(ValuesList[i]);
                }
            }
        }
        public static int Random(List<string> b)
        {
            if (b.Count == 0)
            {
                return 0;
            }
            else
            {
                int max;
                Random rast = new Random();
                max = rast.Next(0, b.Count);

                return max;
            }
        }

        static void Main(string[] args)
        {
            
            int papazCount = 0;
            int elCount = 0;
            int ortCount = 0;
            int ortPapazCount = 0;
            
            for (int x = 0; x < 1000; x++)
            {
                DeckEntities1 db = new DeckEntities1();
                List<Deck> deck = new List<Deck>();
                deck = db.Deck.ToList();

                string random1=null;
                string random2 = null;
                string random3 = null;
                string random4 = null;
                List<string> player1 = new List<string>();
                List<string> player2 = new List<string>();
                List<string> player3 = new List<string>();
                List<string> player4 = new List<string>();
                List<string> ValuesList = new List<string>();
                ValuesList.Add("Ace");
                ValuesList.Add("2");
                ValuesList.Add("3");
                ValuesList.Add("4");
                ValuesList.Add("5");
                ValuesList.Add("6");
                ValuesList.Add("7");
                ValuesList.Add("8");
                ValuesList.Add("9");
                ValuesList.Add("10");
                ValuesList.Add("Jack");
                ValuesList.Add("Queen");
                List<string> DeckValuesOnly = new List<string>();

                foreach (var i in deck)
                {
                    DeckValuesOnly.Add(i.Value);

                }
                //removing 3 Kings from deck to leave only one King
                DeckValuesOnly.RemoveAt(51);
                DeckValuesOnly.RemoveAt(38);
                DeckValuesOnly.RemoveAt(25);

                //Creating random numbers for deck order
                HashSet<int> random = new HashSet<int>();
                Random rst = new Random();
                do
                {
                    int sayi = rst.Next(0, 49);
                    random.Add(sayi);

                } while (random.Count < 49);
                List<int> randoms = new List<int>();
                randoms = random.ToList();



                //lets deal cards
                for (int i = 0; i < 48; i++)
                {
                    player2.Add(DeckValuesOnly[randoms[i]]);
                    i++;
                    player3.Add(DeckValuesOnly[randoms[i]]);
                    i++;
                    player4.Add(DeckValuesOnly[randoms[i]]);
                    i++;
                    player1.Add(DeckValuesOnly[randoms[i]]);
                }
                player2.Add(DeckValuesOnly[randoms[48]]);



                //Before Rounds Begin Get rid of the same value cards
                List<int> ValueExists1 = new List<int>();
                List<int> ValueExists2 = new List<int>();
                List<int> ValueExists3 = new List<int>();
                List<int> ValueExists4 = new List<int>();

                foreach (var i in ValuesList)
                {
                    ValueExists1.Add(Kontrol(i, player1));
                }

                foreach (var i in ValuesList)
                {
                    ValueExists2.Add(Kontrol(i, player2));
                }

                foreach (var i in ValuesList)
                {
                    ValueExists3.Add(Kontrol(i, player3));
                }

                foreach (var i in ValuesList)
                {
                    ValueExists4.Add(Kontrol(i, player4));
                }
                //Control and Remove if there is two of same value
                Remove(ValueExists1, ValuesList, player1);
                Remove(ValueExists2, ValuesList, player2);
                Remove(ValueExists3, ValuesList, player3);
                Remove(ValueExists4, ValuesList, player4);



                do
                {
                    if (player1.Count != 0)
                    {

                        random1 = player1[Random(player1)];
                        if (random1 == "King")
                        {
                            papazCount++;
                        }
                        player1.Remove(random1);
                        player2.Add(random1);
                        ValueExists1.Clear();
                        ValueExists2.Clear();
                        foreach (var i in ValuesList)
                        {
                            ValueExists1.Add(Kontrol(i, player1));
                        }
                        foreach (var i in ValuesList)
                        {
                            ValueExists2.Add(Kontrol(i, player2));
                        }
                        Remove(ValueExists1, ValuesList, player1);
                        Remove(ValueExists2, ValuesList, player2);

                    }
                    elCount++;
                    if (player1.Count + player2.Count + player3.Count + player4.Count == 1) { break; }

                    if (player2.Count != 0)
                    {

                        random2 = player2[Random(player2)];
                        if (random2 == "King")
                        {
                            papazCount++;
                        }
                        player2.Remove(random2);
                        player3.Add(random2);
                        ValueExists2.Clear();
                        ValueExists3.Clear();
                        foreach (var i in ValuesList)
                        {
                            ValueExists2.Add(Kontrol(i, player2));
                        }
                        foreach (var i in ValuesList)
                        {
                            ValueExists3.Add(Kontrol(i, player3));
                        }
                        Remove(ValueExists2, ValuesList, player2);
                        Remove(ValueExists3, ValuesList, player3);

                    }

                    elCount++;
                   
                    if (player1.Count + player2.Count + player3.Count + player4.Count == 1) { break; }

                    if (player3.Count != 0)
                    {

                        random3 = player3[Random(player3)];
                        if (random3 == "King")
                        {
                            papazCount++;
                        }
                        player3.Remove(random3);
                        player4.Add(random3);
                        ValueExists3.Clear();
                        ValueExists4.Clear();
                        foreach (var i in ValuesList)
                        {
                            ValueExists3.Add(Kontrol(i, player3));
                        }
                        foreach (var i in ValuesList)
                        {
                            ValueExists4.Add(Kontrol(i, player4));
                        }
                        Remove(ValueExists3, ValuesList, player3);
                        Remove(ValueExists4, ValuesList, player4);

                    }

                    elCount++; 
                    if (player1.Count + player2.Count + player3.Count + player4.Count == 1) { break; }

                    if (player4.Count != 0)
                    {

                        random4 = player4[Random(player4)];
                        if (random4 == "King")
                        {
                            papazCount++;
                        }
                        player4.Remove(random4);
                        player1.Add(random4);
                        ValueExists4.Clear();
                        ValueExists1.Clear();
                        foreach (var i in ValuesList)
                        {
                            ValueExists4.Add(Kontrol(i, player4));
                        }
                        foreach (var i in ValuesList)
                        {
                            ValueExists1.Add(Kontrol(i, player1));
                        }
                        Remove(ValueExists4, ValuesList, player4);
                        Remove(ValueExists1, ValuesList, player1);

                    }

                    elCount++;
                    if (player1.Count + player2.Count + player3.Count + player4.Count == 1) { break; }


                } while (true);









            }
            Console.WriteLine("toplam el sayisi "+elCount);
            ortCount = elCount / 1000;
            Console.WriteLine("ortalama el sayisi "+ortCount);
            Console.WriteLine("papaz "+papazCount );
            ortPapazCount = papazCount / 1000;
            Console.WriteLine("Ortalama papazın el değiştirmesi: "+ortPapazCount);
            
            Console.ReadLine();
        }
    }
}
