//Assignment 4 for EECS 214
//Out: May 7th, 2014
//Due: May 16th, 2014

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment_4
{
    public class BST
    {
        // The BSTNode Class - The building block of the tree
        public class BSTNode
        {
            public int? Field { get; set; }
            public BSTNode Parent { get; set; }
            public BSTNode LChild { get; set; }
            public BSTNode RChild { get; set; }

            public BSTNode(int? FieldIn, BSTNode ParentIn)
            {
                Field = FieldIn;
                Parent = ParentIn;
                LChild = null;
                RChild = null;
            }
        }

        // Store the tree's 'root' and highlighted 'hlNode' Nodes
        // Store the tree's inorder traversal in a Queue 'BST_DB'
        public BSTNode root;
        public BSTNode hlNode;
        public Queue<BST.BSTNode> BST_DB;

        // Start by building a pre-defined tree
        public BST()
        {
            root = new BST.BSTNode(null, null);
        }

        // Add an int 'value' to the BST using a recursive loop
        public BSTNode Insert(BSTNode node, BST tree)
        {
            // We use 'temp' and 'tempParent' to help us traverse through the tree
            BSTNode temp = tree.root;
            BSTNode tempParent = null;

            // Traverse the tree until you find the spot to insert stuff at         
            while (temp.Field != null)
            {
                if (node.Field == temp.Field)
                {
                    System.Console.WriteLine("Tried to add an existing value");
                    return null;
                }
                else if (node.Field < temp.Field)
                {
                    tempParent = temp;
                    temp = temp.LChild;
                }
                else if (node.Field > temp.Field)
                {
                    tempParent = temp;
                    temp = temp.RChild;
                }
            }

            // Now insert it into the right spot - special case for empty tree
            if (tree.root.Field == null)
            {
                tree.root = node;
            }
            else if (node.Field < tempParent.Field)
            {
                tempParent.LChild = node;
                node.Parent = tempParent;
            }
            else
            {
                tempParent.RChild = node;
                node.Parent = tempParent;
            }

            return node;
        }

        // ROTATION FUNCTIONS
        public void RightRotate(BSTNode b)
        {
            // Set a to keep track of it
            BSTNode a = b.LChild;

            // Move a's right child to b's left child
            b.LChild = a.RChild;
            if (a.RChild != null)
            {
                a.RChild.Parent = b;
            }

            // Make b's parent a's parent
            a.Parent = b.Parent;

            // Reset the root if b was the root, otherwise reset a to the top
            if (b.Parent.Field == null)
            {
                root = a;
            }
            else if (b == a.Parent.RChild)
            {
                a.Parent.RChild = a;
            }
            else
            {
                a.Parent.LChild = a;
            }

            // Set the right child of a as b
            a.RChild = b;
            b.Parent = a;
        }

        public void LeftRotate(BSTNode a)
        {
            // Keep track of B, the RChild of A
            BSTNode b = a.RChild;

            // Move the LChild of B to be the RChild of A
            // Complete the relationship both ways
            a.RChild = b.LChild;
            if (b.LChild != null)
            {
                b.LChild.Parent = a;
            }

            // Give both A and B the parents of A
            b.Parent = a.Parent;

            // Make B either the root or the child of A's parent
            if (a.Parent.Field == null)
            {
                root = b;
            }

            // If A is an LChild, make B that LChild
            else if (a == a.Parent.LChild)
            {
                a.Parent.LChild = b;
            }

            // If A is an RChild, make B that RChild
            else
            {
                a.Parent.RChild = b;
            }

            // Move A to B's left
            b.LChild = a;
            a.Parent = b;
        }

        // Update the queue 'treeDB' with the results of the inorder traversal
        public void inOrder()
        {
            // Clear treeDB and replace it (not the most efficient way, but...)
            BST_DB = new Queue<BST.BSTNode>();
            inOrderLoop(root, BST_DB);
        }

        // Inner private loop contains inner workings of 'inOrder()'
        private void inOrderLoop(BSTNode n, Queue<BST.BSTNode> BST_DB)
        {
            if (n != null)
            {
                inOrderLoop(n.LChild, BST_DB);
                if (n.Field != null)
                {
                    BST_DB.Enqueue(n);
                }
                inOrderLoop(n.RChild, BST_DB);
            }
        }

        // Search for a value - if you find it, mark it and return TRUE, otherwise return FALSE
        public BSTNode search(int value)
        {
            BSTNode temp = root;

            while (temp.Field != null)
            {
                if (value == temp.Field)
                {
                    hlNode = temp;
                    return temp;
                }
                else if (value > temp.Field)
                {
                    temp = temp.RChild;
                }
                else if (value < temp.Field)
                {
                    temp = temp.LChild;
                }
            }

            // If you cannot find the BSTNode, return null
            return null;
        }

        public void findMin()
        {
            // Find the smallest value in the tree by looking at left children
            BSTNode temp = root;
            while (temp.LChild != null)
            {
                temp = temp.LChild;
            }

            hlNode = temp;
        }

        public void findMax()
        {
            // Find the largest value in the tree by looking at right children
            BSTNode temp = root;
            while (temp.RChild != null)
            {
                temp = temp.RChild;
            }

            hlNode = temp;
        }

        // Find the successor of the currently highlighted node - kind of hacky but it works and conforms to theory
        public void findSuccessor()
        {
            // We have reached the end of the tree and cannot traverse further
            if (hlNode == null)
            {
                return;
            }

            // This is the previously highlighted node, unhighlight it
            BSTNode temp = hlNode;
            hlNode = null;

            // Highlight the activeNode if you can
            if (temp.RChild != null)
            {
                temp = temp.RChild;
                while (temp.LChild != null)
                {
                    temp = temp.LChild;
                }
            }
            else
            {
                BSTNode otherNode = temp;
                temp = temp.Parent;
                while (temp != null && otherNode == temp.RChild)
                {
                    otherNode = temp;
                    temp = temp.Parent;
                }
            }

            // Highlight the activeNode if you can
            if (temp != null)
            {
                hlNode = temp;
            }
        }
    }
}
