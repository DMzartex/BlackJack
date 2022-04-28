using System;

namespace BlackJackGame
{
    class Program
    {

        static void distribCartesJoueurs(string[] cartes, int[] valCartes,ref int[,] cartesJoueurs, int nbrJoueurs, int n)
        {
           
            int valCarteJoueur;
            string TypeCarteJoueur;
            string play = "Y";
            Random rnd = new Random();

            int sommeCartes = 0;

            Console.WriteLine("joueur" + " " + (n + 1) + "\n");

            for (int y = 0; y < cartesJoueurs.GetLength(1) && play == "Y" && sommeCartes <= 21; y++)
            {
                string carteJoueur = "";
                int nbrRandom = rnd.Next(0, 9);
                int carteRandom = rnd.Next(0, 4);

                valCarteJoueur = valCartes[nbrRandom];
                TypeCarteJoueur = cartes[carteRandom];

                sommeCartes = sommeCartes + valCarteJoueur;

                carteJoueur = valCarteJoueur + " " + TypeCarteJoueur;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Votre carte est " + " " + carteJoueur);

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Votre total est de : " + " " + sommeCartes);

                

                cartesJoueurs[n, y] = valCarteJoueur;

                Console.ForegroundColor = ConsoleColor.White;

                if(sommeCartes <= 21)
                {
                    Console.WriteLine("Voulez vous tirer à nouveau une carte joueur" + " " + (n + 1) + " ?" + "\n");
                    play = Console.ReadLine();
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

        static void balance(ref int[,] balanceJoueurs, int nbrJoueurs)
        {
            // parcourir les lignes de la matrice ( les joueurs)
            for (int x = 0; x < nbrJoueurs; x++)
            {
                // parcourir les colonnes de la matrice ( les sommes de la balance)
                for (int y = 0; y < balanceJoueurs.GetLength(1); y++)
                {
                    balanceJoueurs[x, y] = 10000;

                }
            }

        }

        static void mise(ref int[,] balanceJoueurs, out int[] miseTab, int nbrJoueurs)
        {
            miseTab = new int[4];
            

            for(int i = 0; i < nbrJoueurs; i++)
            {
                Console.WriteLine("Joueur " + " " + (i + 1) + " " + "Entrez votre mise ( max 10 000 euros ) :");
                int mise = int.Parse(Console.ReadLine());
                balanceJoueurs[i, 0] = balanceJoueurs[i, 0] - mise;
                miseTab[i] = mise;
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

        static void calcScoreJoueurs(int[,] cartesJoueurs, out int scoreJoueur1, out int scoreJoueur2, out int scoreJoueur3, out int scoreJoueur4)
        {
            scoreJoueur1 = 0;
            scoreJoueur2 = 0;
            scoreJoueur3 = 0;
            scoreJoueur4 = 0;

            for(int y = 0; y < cartesJoueurs.GetLength(0); y++)
            {
                for(int x = 0; x < cartesJoueurs.GetLength(1); x++)
                {
                    switch (y)
                    {
                        case 0:
                            scoreJoueur1 = scoreJoueur1 + cartesJoueurs[y, x];
                            break;
                        case 1:
                            scoreJoueur2 = scoreJoueur2 + cartesJoueurs[y, x];
                            break;
                        case 2:
                            scoreJoueur3 = scoreJoueur3 + cartesJoueurs[y, x];
                            break;
                        case 3:
                            scoreJoueur4 = scoreJoueur4 + cartesJoueurs[y, x];
                            break;
                    }
                }
            }

        }

        static void DistribGains(int[] scoreJoueur, int valCartesIA, int nbrJoueurs, ref int[,] balanceJoueurs, int[] miseTab)
        {
            for(int i = 0; i < nbrJoueurs; i++)
            {
                int scoreJoueur = scoreJoueur[i];
                if (valCartesIA > 21)
                {
                    if (scoreJoueur1 == 21)
                    {
                        miseTab[i] = miseTab[i] * 4;
                        balanceJoueurs[0, 0] = balanceJoueurs[0, 0] + miseTab[0];
                    }
                    else
                    {
                        miseTab[0] = miseTab[0] * 2;
                        balanceJoueurs[0, 0] = balanceJoueurs[0, 0] + miseTab[0];
                    }
                }
                else if (scoreJoueur1 > valCartesIA && scoreJoueur1 <= 21)
                {
                    if (scoreJoueur1 == 21)
                    {
                        miseTab[0] = miseTab[0] * 4;
                        balanceJoueurs[0, 0] = balanceJoueurs[0, 0] + miseTab[0];
                    }
                    else
                    {
                        miseTab[0] = miseTab[0] * 2;
                        balanceJoueurs[0, 0] = balanceJoueurs[0, 0] + miseTab[0];
                    }
                }
            }

            if (nbrJoueurs > 0)
            {
                if(valCartesIA > 21)
                {
                    if (scoreJoueur1 == 21)
                    {
                        miseTab[0] = miseTab[0] * 4;
                        balanceJoueurs[0, 0] = balanceJoueurs[0, 0] + miseTab[0];
                    }
                    else
                    {
                        miseTab[0] = miseTab[0] * 2;
                        balanceJoueurs[0, 0] = balanceJoueurs[0, 0] + miseTab[0];
                    }
                }
                else if(scoreJoueur1 > valCartesIA && scoreJoueur1 <= 21)
                {
                        if (scoreJoueur1 == 21)
                        {
                            miseTab[0] = miseTab[0] * 4;
                            balanceJoueurs[0, 0] = balanceJoueurs[0, 0] + miseTab[0];
                        }
                        else
                        {
                            miseTab[0] = miseTab[0] * 2;
                            balanceJoueurs[0, 0] = balanceJoueurs[0, 0] + miseTab[0];
                        }
                }
            }
            if (nbrJoueurs > 1)
            {
                if (valCartesIA > 21)
                {
                    if (scoreJoueur2 == 21)
                    {
                        miseTab[1] = miseTab[1] * 4;
                        balanceJoueurs[1, 0] = balanceJoueurs[1, 0] + miseTab[1];
                    }
                    else
                    {
                        miseTab[1] = miseTab[1] * 2;
                        balanceJoueurs[1, 0] = balanceJoueurs[1, 0] + miseTab[1];
                    }
                }
                else if (scoreJoueur2 > valCartesIA && scoreJoueur2 <= 21)
                {
                    if (scoreJoueur2 == 21)
                    {
                        miseTab[1] = miseTab[1] * 4;
                        balanceJoueurs[1, 0] = balanceJoueurs[1, 0] + miseTab[1];
                    }
                    else
                    {
                        miseTab[1] = miseTab[1] * 2;
                        balanceJoueurs[1, 0] = balanceJoueurs[1, 0] + miseTab[1];
                    }
                }
            }
            if (nbrJoueurs > 2)
            {

                if (valCartesIA > 21)
                {
                    if (scoreJoueur3 == 21)
                    {
                        miseTab[2] = miseTab[2] * 4;
                        balanceJoueurs[2, 0] = balanceJoueurs[2, 0] + miseTab[2];
                    }
                    else
                    {
                        miseTab[2] = miseTab[2] * 2;
                        balanceJoueurs[2, 0] = balanceJoueurs[2, 0] + miseTab[2];
                    }
                }else if(scoreJoueur3 > valCartesIA && scoreJoueur3 <= 21)
                {
                    if (scoreJoueur3 == 21)
                    {
                        miseTab[2] = miseTab[2] * 4;
                        balanceJoueurs[2, 0] = balanceJoueurs[2, 0] + miseTab[2];
                    }
                    else
                    {
                        miseTab[2] = miseTab[2] * 2;
                        balanceJoueurs[2, 0] = balanceJoueurs[2, 0] + miseTab[2];
                    }
                }
            }
            if (nbrJoueurs > 3)
            {
                if (valCartesIA > 21)
                {
                    if (scoreJoueur4 == 21)
                    {
                        miseTab[3] = miseTab[3] * 4;
                        balanceJoueurs[3, 0] = balanceJoueurs[3, 0] + miseTab[3];
                    }
                    else
                    {
                        miseTab[3] = miseTab[3] * 2;
                        balanceJoueurs[3, 0] = balanceJoueurs[3, 0] + miseTab[3];
                    }
                }else if(scoreJoueur4 > valCartesIA && scoreJoueur2 <= 21)
                {
                    if (scoreJoueur4 == 21)
                    {
                        miseTab[3] = miseTab[3] * 4;
                        balanceJoueurs[3, 0] = balanceJoueurs[3, 0] + miseTab[3];
                    }
                    else
                    {
                        miseTab[3] = miseTab[3] * 2;
                        balanceJoueurs[3, 0] = balanceJoueurs[3, 0] + miseTab[3];
                    }
                }
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
            int nbrJoueurs;
            string play;
            int n = 0;
            int tourAddBalance = 0;
            int scoreJoueur1;
            int scoreJoueur2;
            int scoreJoueur3;
            int scoreJoueur4;
            int tourIA = 0;
            string cartesIA = "";
            int valCartesIA = 0;
            string[] cartes = { "Trèfles", "Piques", "Coeurs", "Carreaux" };
            int[] valCartes = { 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[] miseTab;
            string restart = "Y";
            do
            {

                Console.WriteLine("Entrer le nombre de joueurs ( max 4 joueurs ) : ");
                nbrJoueurs = int.Parse(Console.ReadLine());
            }
            while (nbrJoueurs > 4);
            

            int[,] cartesJoueurs = new int[nbrJoueurs, 6];
            int[,] balanceJoueurs = new int[nbrJoueurs,1];

            while (tourAddBalance < nbrJoueurs)
            {
                if (n == 0)
                {
                    balance(ref balanceJoueurs, nbrJoueurs);
                }

                tourAddBalance = tourAddBalance + 1;
            }

            while (restart == "Y" && )
            {
                cartesIA = "";
                valCartesIA = 0;
                n = 0;

                affichageMatriceBalance(balanceJoueurs);

                mise(ref balanceJoueurs, out miseTab, nbrJoueurs);
                while (n < nbrJoueurs)
                {
                    Console.WriteLine(n);

                    affichageMatriceBalance(balanceJoueurs);
                    distribCartesJoueurs(cartes, valCartes, ref cartesJoueurs, nbrJoueurs, n);

                    n = n + 1;
                    play = "Y";
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

                calcScoreJoueurs(cartesJoueurs, out scoreJoueur1, out scoreJoueur2, out scoreJoueur3, out scoreJoueur4);

                if (nbrJoueurs > 0)
                {
                    Console.WriteLine("Le score du joueur 1 est de :" + " " + scoreJoueur1);
                }
                if (nbrJoueurs > 1)
                {
                    Console.WriteLine("Le score du joueur 2 est de :" + " " + scoreJoueur2);
                }
                if (nbrJoueurs > 2)
                {
                    Console.WriteLine("Le score du joueur 3 est de :" + " " + scoreJoueur3);

                }
                if (nbrJoueurs > 3)
                {
                    Console.WriteLine("Le score du joueur 4 est de :" + " " + scoreJoueur4);
                }

                DistribGains(scoreJoueur1, scoreJoueur2, scoreJoueur3, scoreJoueur4, valCartesIA, nbrJoueurs, ref balanceJoueurs, miseTab);

                if (nbrJoueurs > 0)
                {
                   if(balanceJoueurs[0,0] > 0)
                    {
                        // ok
                    }
                }
                if (nbrJoueurs > 1)
                {
                    if (balanceJoueurs[1, 0] > 0)
                    {
                        // ok
                    }
                }
                if (nbrJoueurs > 2)
                {
                    if (balanceJoueurs[2, 0] > 0)
                    {
                        // ok
                    }

                }
                if (nbrJoueurs > 3)
                {
                    if (balanceJoueurs[3, 0] > 0)
                    {
                        // ok
                    }
                }

                affichageMatriceBalance(balanceJoueurs);


                Console.WriteLine("Voulez vous rejouez ? Y/N");
                restart = Console.ReadLine();

                
            }

            
        }
    }
}
