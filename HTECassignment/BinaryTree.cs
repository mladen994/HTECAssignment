using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTECassignment {
    public class BinaryTree {
        public BinaryNode root;
        public uint numOfElements;
        Queue<BinaryNode> holder;
        public BinaryTree() {
            root = null;
            numOfElements = 0;
            holder = new Queue<BinaryNode>();
        }
        public void initializeRoot(Player finals) {
            if (root == null) {
                root = new BinaryNode(finals);
                ++numOfElements;
            }
        }
        public void setNode(BinaryNode here, Player data) {
            here = new BinaryNode(data);
            ++numOfElements;
        }
        public void insertLeftAt(BinaryNode position) {
            position.left = new BinaryNode();
        }
        public void insertRightAt(BinaryNode position) {
            position.right = new BinaryNode();
        }
        public void populateDefault() {
            initializeRoot(new Player("1/1", "finals", 1));
            insertLeftAt(root);
            insertRightAt(root);
            holder.Enqueue(root.left);
            holder.Enqueue(root.right);

            while (holder.Count < 32) {
                var current = holder.Dequeue();

                switch (numOfElements) {
                    case 1:
                    case 2:
                        current.setKey(new Player("1/2", "finals", numOfElements));
                        ++numOfElements;
                        break;
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                        current.setKey(new Player("1/4", "finals", numOfElements - 2));
                        ++numOfElements;
                        break;
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                        current.setKey(new Player("1/8", "finals", numOfElements - 6));
                        ++numOfElements;
                        break;
                    default:
                        current.setKey(new Player("1/16", "finals", numOfElements - 14));
                        ++numOfElements;
                        break;
                }
                insertLeftAt(current);
                insertRightAt(current);
                holder.Enqueue(current.left);
                holder.Enqueue(current.right);

            }
        }
        public void populateWithPlayers(IList<Player> players) {
            foreach (Player player in players) {
                holder.Dequeue().setKey(player);
            }
        }
        public List<Player> getAsList() {
            var p = root;
            var listOfAllOpponnents = new List<Player>();
            var queue = new Queue<BinaryNode>((int)numOfElements);
            queue.Enqueue(p);
            while (queue.Count != 32) {
                p = queue.Dequeue();
                if (p.left != null) {
                    queue.Enqueue(p.left);
                }
                if (p.right != null) {
                    queue.Enqueue(p.right);
                }
            }
            while (queue.Count > 0) {
                listOfAllOpponnents.Add(queue.Dequeue().getKey());
            }
            return listOfAllOpponnents;
        }
        public void breadthFirst() {
            BinaryNode p = root;
            Queue<BinaryNode> queue = new Queue<BinaryNode>((int)numOfElements);
            queue.Enqueue(p);
            while (queue.Count > 0) {
                p = queue.Dequeue();
                p.visit();
                if (p.left != null) {
                    queue.Enqueue(p.left);
                }
                if (p.right != null) {
                    queue.Enqueue(p.right);
                }
            }
        }
        public void printPairs() {
            BinaryNode p = root;
            Queue<BinaryNode> queue = new Queue<BinaryNode>((int)numOfElements);
            queue.Enqueue(p);
            while (queue.Count != 32) {
                p = queue.Dequeue();
                //p.visit();
                if (p.left != null) {
                    queue.Enqueue(p.left);
                }
                if (p.right != null) {
                    queue.Enqueue(p.right);
                }
            }
            int counter = 0;
            while (queue.Count > 0) {
                queue.Dequeue().visit();
                ++counter;
                if (counter % 2 == 0) {
                    Console.WriteLine();
                    if (counter % 4 == 0) {
                        Console.WriteLine();
                        if (counter % 8 == 0) {
                            Console.WriteLine();
                            if (counter % 16 == 0) {
                                Console.WriteLine();
                            }
                        }
                    }
                }
            }
        }
        
    }

}
