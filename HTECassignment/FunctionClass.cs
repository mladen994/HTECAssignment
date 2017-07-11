using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;


namespace HTECassignment {
    public static class FunctionClass {

        public static void Shuffle<T>(this IList<T> list) { //thanks StackOverflow!
            int n = list.Count;
            while (n > 1) {
                n--;
                int k = getRandom(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
        public static int getRandom(int min, int max) {
            var rnd = new Random();
            return rnd.Next(min, max);
        }
        public static BinaryTree generateTree(List<Player> listPlayers) {
            var stablo = new BinaryTree();
            stablo.populateDefault();
            var fairlyRandomized = new List<Player>(listPlayers.Count);
            for (int i = 0; i < listPlayers.Count; i++) {//initializing fairly randomized
                fairlyRandomized.Add(new Player());
            }

            var privilegedIndices = generateRandomPrivileged();
            var second8Indices = randomHalfOf(privilegedIndices);
            foreach (int index in second8Indices) {
                privilegedIndices.Remove(index);
            }
            second8Indices.Shuffle();

            var second4Indices = randomHalfOf(privilegedIndices);
            foreach (int index in second4Indices) {
                privilegedIndices.Remove(index);
            }
            second4Indices.Shuffle();

            var second2Indices = randomHalfOf(privilegedIndices);
            foreach (int index in second2Indices) {
                privilegedIndices.Remove(index);
            }
            second2Indices.Shuffle();

            privilegedIndices.Shuffle();
            privilegedIndices.AddRange(second2Indices);
            privilegedIndices.AddRange(second4Indices);
            privilegedIndices.AddRange(second8Indices);
            int counter = 0;
            foreach (int index in privilegedIndices) {
                fairlyRandomized[index] = listPlayers[counter++];
            }

            counter = 0;
            listPlayers.RemoveRange(0, 16);
            listPlayers.Shuffle();
            foreach (Player player in fairlyRandomized) {
                if (player.getPoints() == 0) {
                    player.setEverything(listPlayers[counter++]);
                }
            }

            stablo.populateWithPlayers(fairlyRandomized);
            return stablo;
        }

        public static List<int> generateRandomPrivileged() {
            List<int> returner = new List<int>();
            for (int i = 0; i < 16; i++) {
                returner.Add(i * 2 + getRandom(0, 10000) % 2);
            }
            return returner;
        }

        public static List<int> randomHalfOf(List<int> source) {
            List<int> returner = new List<int>();
            for (int i = 0; i < source.Count() / 2; i++) {
                returner.Add(source[i * 2 + getRandom(0, 10000) % 2]);
            }
            return returner;
        }

        public static List<Player> getPlayersFromJSON() {
            List<Player> listPlayers;
            string filePath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\Data\Data\players.json";
            string serializedListofPlayers = File.ReadAllText(filePath);
            listPlayers = JsonConvert.DeserializeObject<List<Player>>(serializedListofPlayers);
            listPlayers = listPlayers.OrderBy(o => o.getPoints()).ToList();
            listPlayers.Reverse();
            return listPlayers;
        }

        public static Player prettify(Player target, List<Player> playerList) { //unexisting word
            string name = target.getFirstName();
            name = new System.Globalization.CultureInfo("en-US", false).TextInfo.ToTitleCase(name.ToLower());

            return playerList.Find(o => o.getFirstName().ToLower().Equals(target.getFirstName().ToLower())
                                         && o.getLastName().ToLower().Equals(target.getLastName().ToLower()));
        }

        public static bool doesExist(Player target, List<Player> source) {
            target = prettify(target, source);
            return (target != null);
        }

        public static List<Player> findOpponnentsForRound(Player target, BinaryTree zreb, int round) {
            var returner = new List<Player>();
            var listOpponents = zreb.getAsList();
            int index = listOpponents.FindIndex(o => o.isEQ(target));
            for (int i = 16; index != 0; i /= 2) {
                if (index > i) {
                    listOpponents.Reverse(0, 2 * i);
                    index = listOpponents.FindIndex(o => o.isEQ(target));
                }
            }
            var cap = 1 << (round - 1);
            for (int i = 0; i < cap; i++) {
                returner.Add(listOpponents[cap + i]);
            }
            return returner;
        }
    }
}
