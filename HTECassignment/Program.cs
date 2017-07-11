using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace HTECassignment {
    public class Program {
        static void Main(string[] args) {
            List<Player> listPlayers = FunctionClass.getPlayersFromJSON();
            BinaryTree zreb = null;

            Console.WriteLine("Dobrodošli u superTenis aplikaciju za pravljenje žreba na osnovu najboljih igrača.");
            while (true) {
                Console.WriteLine("\"first_name last_name round\" - prikazati sve moguće protivnike u toj rundi za izabranog igrača na gorenavedenom takmicenju.");
                Console.WriteLine("\"Zreb\" - Nasumično generisan žreb na osnovu podataka u .json-u.");
                Console.WriteLine("\"Leaderboard\" - Napustiti aplikaciju.");
                Console.WriteLine("\"Quit\" - Napustiti aplikaciju.");
                string command = Console.ReadLine();
                command.ToLower();
                if (command.Split(' ').Length == 3) {
                    if (zreb == null) {
                        Console.WriteLine("Molimo izgenerišite nasumični žreb pa pokušajte ponovo!");
                    } else {
                        string[] niz = command.Split(' ');
                        int round = Int32.Parse(niz[2]);
                        Player target = new Player(niz[0], niz[1], 42);
                        target = FunctionClass.prettify(target, listPlayers);
                        if (FunctionClass.doesExist(target, listPlayers)) {
                            List<Player> opponents = FunctionClass.findOpponnentsForRound(target, zreb, round);
                            Console.WriteLine("Mogući protivnici za " + target.getName() + " u " + round + ". fazi su:");
                            foreach (Player opponent in opponents) {
                                Console.WriteLine("\t" + opponent.getName());
                            }
                        } else {
                            Console.WriteLine("Gospodin " + niz[0] + " " + niz[1] + " ne postoji u bazi podataka.");
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                } else if (command.Equals("zreb")) {
                    Console.WriteLine();
                    Console.WriteLine();

                    zreb = FunctionClass.generateTree(listPlayers);
                    zreb.printPairs();

                    listPlayers = FunctionClass.getPlayersFromJSON();

                } else if (command.Equals("leaderboard")) {
                    int counter = 0;
                    foreach (Player player in listPlayers) {
                        Console.WriteLine(++counter + " " + player.toString());
                    }
                    Console.WriteLine();
                } else if (command.Equals("quit")) {
                    Console.WriteLine("Hvala vam što ste koristili našu aplikaciju!");
                    break;
                } else {
                    Console.WriteLine("Neispravna komanda!");
                }
            }
            Console.ReadLine();
        }
    }
}
