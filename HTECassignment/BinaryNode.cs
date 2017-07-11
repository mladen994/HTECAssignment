using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTECassignment {
    public class BinaryNode {
        public Player key;
        public BinaryNode right;
        public BinaryNode left;

        public BinaryNode() {
            left = right = null;
        }
        public BinaryNode(Player el) {
            key = el;
            left = right = null;
        }
        public BinaryNode(Player el, BinaryNode left, BinaryNode right) {
            key = el;
            this.left = left;
            this.right = right;
        }
        public void ReplaceNode(Player el) {
            key = el;
            left = right = null;
        }
        public void replaceNode(Player el, BinaryNode left, BinaryNode right) {
            key = el;
            this.left = left;
            this.right = right;
        }
        public void setKey(Player key) {
            this.key = key;
        }
        public Player getKey() {
            return key;
        }
        public bool isLeaf() {
            return left == right; //both should be null if it is a leaf
        }
        public void visit() {
            Console.WriteLine(key.toString());
        }
    }
}
