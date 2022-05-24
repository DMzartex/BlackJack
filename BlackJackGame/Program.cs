using System;

namespace BlackJackGame
{
    class Program
    {

        static void distribCartesJoueurs(string[] cartes, int[] valCartes,ref int[,] cartesJoueurs, int bJoueurs, int n, out string play)
        {
           
            int valCarteJoueur;
            string typeCarteJoueur;
            play = "Y";
            int bplay;
            Random rnd = new Random();

            int sommeCartes = 0;

            Console.WriteLine("joueur" + " " + (n + 1) + "\n");

            for (int y = 0; y < cartesJoueurs.GetLength(1) && play == "Y" && sommeCartes <= 21; y++)
            {
                string carteJoueur = "";
                int nbrRandom = rnd.Next(0, 9);
                int carteRandom = rnd.Next(0, 4);

                valCarteJoueur = valCartes[nbrRandom];
                typeCarteJoueur = cartes[carteRandom];

                sommeCartes = sommeCartes + valCarteJoueur;

                carteJoueur = valCarteJoueur + " " + typeCarteJoueur;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Votre carte est " + " " + carteJoueur);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Votre total est de : " + " " + sommeCartes);

                
               
                
                cartesJoueurs[n, y] = valCarteJoueur;

                Console.ForegroundColor = ConsoleColor.White;

                if(sommeCartes <= 21)
                {
                    do
                    {
                        do
                        {
                            Console.WriteLine("Voulez vous tirer à nouveau une carte joueur" + " " + (n + 1) + " ? (Y/N)" + "\n");
                            play = Console.ReadLine();

                        } while (play != "Y" && play != "N");

                    } while (int.TryParse(play, out bplay));
                    
                }
                
            }
        }

      static void remiseAZeroMatriceCarte(string restart, ref int[,] cartesJoueurs)
        {
            for(int y = 0; y < cartesJoueurs.GetLength(0); y++)
            {
                for(int x = 0; x < cartesJoueurs.GetLength(1); x++)
                {
                    cartesJoueurs[y, x] = 0;
                }
            }
        }

        

        static void distribCartesIA(string[] cartes, int[] valCartes, ref string cartesIA, ref int valCartesIA, int n)
        {
           
            
            Random rnd = new Random();

            int nbrRandom = rnd.Next(0, 9);
            int carteRandom = rnd.Next(0, 4);

            int nbrCarteIA = valCartes[nbrRandom];
            string typeCarteIA = cartes[carteRandom];

            cartesIA = cartesIA + " " + nbrCarteIA + " " + typeCarteIA;
            valCartesIA = valCartesIA + nbrCarteIA;
           
        }

        static void balance(ref int[,] balanceJoueurs, int bJoueurs)
        {
            // parcourir les lignes de la matrice ( les joueurs)
            for (int x = 0; x < bJoueurs; x++)
            {
                // parcourir les colonnes de la matrice ( les sommes de la balance)
                for (int y = 0; y < balanceJoueurs.GetLength(1); y++)
                {
                    balanceJoueurs[x, y] = 10000;

                }
            }

        }

        static void mise(ref int[,] balanceJoueurs, out int[] miseTab, int bJoueurs)
        {
            miseTab = new int[4];
            

            for(int i = 0; i < bJoueurs; i++)
            {
                string mise;
                int bMise;

                do
                {
                    do
                    {

                        Console.WriteLine("Joueur " + " " + (i + 1) + " " + "Entrez votre mise ( max 10 000 euros ) :");
                        mise = Console.ReadLine();
                    
                    } while (!int.TryParse(mise, out bMise));

                } while (bMise > balanceJoueurs[i, 0]);


                if (balanceJoueurs[i, 0] != 0)
                {
                    balanceJoueurs[i, 0] = balanceJoueurs[i, 0] - bMise;
                    miseTab[i] = bMise;
                }
                else
                {
                    Console.WriteLine("Vous n'avez plus assez d'argent pour pouvoir jouer !");
                }
            }


        }

      
        static void affichageMatrice(int[,] cartesJoueurs)
        {
            for (int n = 0; n < cartesJoueurs.GetLength(0); n++)
            {
                for (int i = 0; i < cartesJoueurs.GetLength(1); i++)
                {
                    Console.Write("  " + cartesJoueurs[n, i]);
                }
                Console.WriteLine("");
            }
        }

        static void affichageMatriceBalance(int[,] balanceJoueurs)
        {
            for (int n = 0; n < balanceJoueurs.GetLength(0); n++)
            {
                for (int i = 0; i < balanceJoueurs.GetLength(1); i++)
                {
                    Console.Write("Joueur" + " " + (n + 1 ) + "  " + balanceJoueurs[n, i] + " " + "euros");
                }
                Console.WriteLine("\n");
            }
        }

        static void calcScoreJoueurs(int bJoueurs, int[,] cartesJoueurs, ref int[] scoreJoueurs)
        {
            scoreJoueurs = new int[bJoueurs];

          
                for (int y = 0; y < cartesJoueurs.GetLength(0); y++)
                {
                    for (int x = 0; x < cartesJoueurs.GetLength(1); x++)
                    {

                        scoreJoueurs[y] =scoreJoueurs[y] + cartesJoueurs[y, x];
                    }
                }

            
        }

            

       

        static void DistribGains(int valCartesIA, int bJoueurs, ref int[,] balanceJoueurs, int[] miseTab, ref int[] scoreJoueurs)
        {
           
            for(int n = 0; n < bJoueurs; n++)
            {
                if (valCartesIA > 21 && scoreJoueurs[n] <= 21)
                {
                    if (scoreJoueurs[n] == 21)
                    {
                        miseTab[n] = miseTab[n] * 4;
                        balanceJoueurs[n, 0] = balanceJoueurs[n, 0] + miseTab[n];
                    }
                    else
                    {
                        miseTab[n] = miseTab[n] * 2;
                        balanceJoueurs[n, 0] = balanceJoueurs[n, 0] + miseTab[n];
                    }
                }
                else if (scoreJoueurs[n] > valCartesIA && scoreJoueurs[n] <= 21)
                {
                    if (scoreJoueurs[n] == 21)
                    {
                        miseTab[n] = miseTab[n] * 4;
                        balanceJoueurs[n, 0] = balanceJoueurs[n, 0] + miseTab[n];
                    }
                    else
                    {
                        miseTab[n] = miseTab[n] * 2;
                        balanceJoueurs[n, 0] = balanceJoueurs[n, 0] + miseTab[n];
                    }
                }

            }
        }

        static void affichageScore(int bJoueurs,ref int[] scoreJoueurs)
        { 
            for (int n = 0; n < bJoueurs; n++)
            {
                Console.WriteLine("Le score du joueur" + " " + n + " est " + scoreJoueurs[n]);
            }
        }


        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"                                                                                                                                                           
BBBBBBBBBBBBBBBBB   lllllll                                      kkkkkkkk                  JJJJJJJJJJJ                                   kkkkkkkk           
B::::::::::::::::B  l:::::l                                      k::::::k                  J:::::::::J                                   k::::::k           
B::::::BBBBBB:::::B l:::::l                                      k::::::k                  J:::::::::J                                   k::::::k           
BB:::::B     B:::::Bl:::::l                                      k::::::k                  JJ:::::::JJ                                   k::::::k           
  B::::B     B:::::B l::::l   aaaaaaaaaaaaa      cccccccccccccccc k:::::k    kkkkkkk         J:::::J  aaaaaaaaaaaaa      cccccccccccccccc k:::::k    kkkkkkk
  B::::B     B:::::B l::::l   a::::::::::::a   cc:::::::::::::::c k:::::k   k:::::k          J:::::J  a::::::::::::a   cc:::::::::::::::c k:::::k   k:::::k 
  B::::BBBBBB:::::B  l::::l   aaaaaaaaa:::::a c:::::::::::::::::c k:::::k  k:::::k           J:::::J  aaaaaaaaa:::::a c:::::::::::::::::c k:::::k  k:::::k  
  B:::::::::::::BB   l::::l            a::::ac:::::::cccccc:::::c k:::::k k:::::k            J:::::j           a::::ac:::::::cccccc:::::c k:::::k k:::::k   
  B::::BBBBBB:::::B  l::::l     aaaaaaa:::::ac::::::c     ccccccc k::::::k:::::k             J:::::J    aaaaaaa:::::ac::::::c     ccccccc k::::::k:::::k    
  B::::B     B:::::B l::::l   aa::::::::::::ac:::::c              k:::::::::::k  JJJJJJJ     J:::::J  aa::::::::::::ac:::::c              k:::::::::::k     
  B::::B     B:::::B l::::l  a::::aaaa::::::ac:::::c              k:::::::::::k  J:::::J     J:::::J a::::aaaa::::::ac:::::c              k:::::::::::k     
  B::::B     B:::::B l::::l a::::a    a:::::ac::::::c     ccccccc k::::::k:::::k J::::::J   J::::::Ja::::a    a:::::ac::::::c     ccccccc k::::::k:::::k    
BB:::::BBBBBB::::::Bl::::::la::::a    a:::::ac:::::::cccccc:::::ck::::::k k:::::kJ:::::::JJJ:::::::Ja::::a    a:::::ac:::::::cccccc:::::ck::::::k k:::::k   
B:::::::::::::::::B l::::::la:::::aaaa::::::a c:::::::::::::::::ck::::::k  k:::::kJJ:::::::::::::JJ a:::::aaaa::::::a c:::::::::::::::::ck::::::k  k:::::k  
B::::::::::::::::B  l::::::l a::::::::::aa:::a cc:::::::::::::::ck::::::k   k:::::k JJ:::::::::JJ    a::::::::::aa:::a cc:::::::::::::::ck::::::k   k:::::k 
BBBBBBBBBBBBBBBBB   llllllll  aaaaaaaaaa  aaaa   cccccccccccccccckkkkkkkk    kkkkkkk  JJJJJJJJJ       aaaaaaaaaa  aaaa   cccccccccccccccckkkkkkkk    kkkkkkk
                                                                                                 ");
            string nbrJoueurs;
            string play;
            int n = 0;
            int tourAddBalance = 0;
            int[] scoreJoueurs = new int[4];
            int tourIA = 0;
            string cartesIA = "";
            int valCartesIA = 0;
            string[] cartes = { "Trèfles", "Piques", "Coeurs", "Carreaux" };
            int[] valCartes = { 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[] miseTab;
            string restart = "y";
            int bJoueurs;
            float brestart;
            do
            {

                Console.WriteLine("Entrer le nombre de joueurs ( max 4 joueurs ) : ");
                nbrJoueurs = Console.ReadLine();
            }
            while (!int.TryParse(nbrJoueurs, out bJoueurs));
            

            int[,] cartesJoueurs = new int[bJoueurs, 6];
            int[,] balanceJoueurs = new int[bJoueurs,1];

            while (tourAddBalance < bJoueurs)
            {
                if (n == 0)
                {
                    balance(ref balanceJoueurs, bJoueurs);
                }

                tourAddBalance = tourAddBalance + 1;
            }

            while (restart == "y" || restart == "Y")
            {
                restart = "";
                cartesIA = "";
                valCartesIA = 0;
                n = 0;

                affichageMatriceBalance(balanceJoueurs);

                mise(ref balanceJoueurs, out miseTab, bJoueurs);
                
                while (n < bJoueurs)
                {
                    Console.WriteLine(n);

                    affichageMatriceBalance(balanceJoueurs);
                    distribCartesJoueurs(cartes, valCartes, ref cartesJoueurs, bJoueurs, n,out play);

                    n = n + 1;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\nIA : \n");

                while (valCartesIA <= 16)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("tour : " + " " + (tourIA + 1) + "\n");
                    distribCartesIA(cartes, valCartes, ref cartesIA, ref valCartesIA, n);
                    Console.WriteLine(valCartesIA + "\n");
                    Console.WriteLine(cartesIA + "\n");
                    tourIA = tourIA + 1;
                }

                Console.ForegroundColor = ConsoleColor.Green;

                affichageMatrice(cartesJoueurs);

                calcScoreJoueurs(bJoueurs, cartesJoueurs, ref scoreJoueurs);

                DistribGains(valCartesIA, bJoueurs, ref balanceJoueurs, miseTab, ref scoreJoueurs);

                affichageScore(bJoueurs, ref scoreJoueurs);

                remiseAZeroMatriceCarte(restart, ref cartesJoueurs);


                affichageMatriceBalance(balanceJoueurs);


                while (restart != "Y" && restart != "N")
                {
                    do
                    {

                        Console.WriteLine("Voulez vous rejouez ? Y/N");
                        restart = Console.ReadLine();

                    } while (float.TryParse(restart, out brestart));
                }
              
                

                
            }

            
        }
    }
}
