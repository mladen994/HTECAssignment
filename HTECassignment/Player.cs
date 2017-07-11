using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace HTECassignment {
    public class Player {
        [JsonProperty("first_name")]
        private string first_name;
        [JsonProperty("last_name")]
        private string last_name;
        [JsonProperty("points")]
        private uint points;
        public Player() {
            first_name = "";
            last_name = "";
            points = 0;
        }
        public Player(Player x) {
            first_name = x.first_name;
            last_name = x.last_name;
            points = x.points;
        }
        public Player(string first_name, string last_name, uint points) {
            this.first_name = first_name;
            this.last_name = last_name;
            this.points = points;
        }
        public string getName() {
            return first_name + " " + last_name;
        }
        public string getFirstName() {
            return first_name;
        }
        public string getLastName() {
            return last_name;
        }
        public uint getPoints() {
            return points;
        }
        public bool isEQ(Player x) {
            if (first_name.Equals(x.first_name) &&
                last_name.Equals(x.last_name) &&
                points == x.points)
                return true;
            return false;
        }
        public string toString() {
            return first_name + " " + last_name + ": " + points;
        }
        public void setEverything(Player x) {
            first_name = x.first_name;
            last_name = x.last_name;
            points = x.points;
        }
    }
}
