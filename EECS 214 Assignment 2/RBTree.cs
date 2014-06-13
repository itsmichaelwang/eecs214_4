//Assignment 4 for EECS 214
//Out: May 7th, 2014
//Due: May 16th, 2014

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment_4
{
    public class RBTree:BST 
    {
        public enum COLOR { RED, BLACK };

        // The RBNode Class - The building block of the RBTree
        public class RBNode:BSTNode
        {
            // Use an int to keep track of the Node's color (0 is BLACK, 1 is RED)
            public COLOR NodeColor { get; set; }

            public RBNode(int? FieldIn, BSTNode ParentIn)
                : base(FieldIn, ParentIn)
            {
            }

            public RBNode(int? FieldIn, BSTNode ParentIn, COLOR NodeColorIn)
                : base(FieldIn, ParentIn)
            {
                NodeColor = NodeColorIn;
            }
        }
        
        // RBTree Constructor
        public RBTree()
        {
        }

        // Nil Node
        RBNode NilNode = new RBNode(null, null, COLOR.BLACK);
        
        // Basic Insert function - Uses polymorphism
        public void Insert(RBNode node, RBTree tree)
        {   
            node = (RBNode)base.Insert(node, tree);

            if (node == null)
            {
                return;
            }

            // Add the NilNodes as leaves
            node.LChild = NilNode;
            node.RChild = NilNode;

            // If we are inserting the root, make it black, otherwise, make it red
            if (node.Parent == null) {
                node.Parent = NilNode;
                node.NodeColor = COLOR.BLACK;
            }
            else
            {
                node.NodeColor = COLOR.RED;
            }

            // Fix-Up
            FixUpRB(tree, node);
        }

        public int getBlackHeight(int searchValue) {

            // Find the Node in question
            RBNode subTreeNode = (RBNode) search(searchValue);

            // Note that black height starts at 0 - It should start at -1, but we compensate because this algorithm always skips the leaf
            int blackHeight = 0;

            // Traverse down the tree and collect the black height
            while (subTreeNode.Field != null)
            {
                subTreeNode = (RBNode)subTreeNode.LChild;

                if (subTreeNode.NodeColor == COLOR.BLACK)
                {
                    blackHeight++;
                }
            }

            return blackHeight;
        }


        // Functions to find the ancestors of nodes
        public RBNode parent(RBNode N)
        {
            return (RBNode) N.Parent;
        }
        public RBNode grandparent(RBNode N)
        {
            return (RBNode) N.Parent.Parent;
        }

        // Fix nodes 
        public void FixUpRB(RBTree tree, RBNode N)
        {
            while (parent(N).NodeColor == COLOR.RED)
            {
                // If parent(N) is an LChild of grandparent(N)
                if (parent(N) == grandparent(N).LChild)
                {
                    // Y is the uncle of Node N
                    RBNode Y = (RBNode)grandparent(N).RChild;

                    // Case 1: RED Uncle, 
                    if (Y.NodeColor == COLOR.RED)
                    {
                        // Make the parent and uncle black
                        parent(N).NodeColor = COLOR.BLACK;
                        Y.NodeColor = COLOR.BLACK;
                        grandparent(N).NodeColor = COLOR.RED;
                        N = grandparent(N);
                    }
                    
                    // Case 2: BLACK Uncle
                    else
                    {
                        if (N == parent(N).RChild)
                        {
                            N = parent(N);
                            LeftRotate(N);
                        }
                        parent(N).NodeColor = COLOR.BLACK;
                        grandparent(N).NodeColor = COLOR.RED;
                        RightRotate(grandparent(N));
                    }
                }
                else
                {
                    // Y is the uncle of Node N
                    RBNode Y = (RBNode)grandparent(N).LChild;

                    // Case 1: RED Uncle, 
                    if (Y.NodeColor == COLOR.RED)
                    {
                        // Make the parent and uncle black
                        parent(N).NodeColor = COLOR.BLACK;
                        Y.NodeColor = COLOR.BLACK;
                        grandparent(N).NodeColor = COLOR.RED;
                        N = grandparent(N);
                    }

                    // Case 2: BLACK Uncle
                    else
                    {
                        if (N == parent(N).LChild)
                        {
                            N = parent(N);
                            RightRotate(N);
                        }
                        parent(N).NodeColor = COLOR.BLACK;
                        grandparent(N).NodeColor = COLOR.RED;
                        LeftRotate(grandparent(N));
                    }
                }
            }

            // Color it black
            ((RBNode)tree.root).NodeColor = COLOR.BLACK;
        }

        //Ensure the following
        //a. Class called RBNode that derives from the BSTNode 
        //b. Have a Fix-up function that fixes up the tree after each insertion
        //c. Ensure that you know what you are doing with the Nil nodes.
        //d. Have a function that calculates and returns the black height in a MessageBox
        //The syntax for showing a messagebox is: MessageBoxResult m = MessageBox.Show("hello folks");
        //Instead of the messagebox, if you want to show the black height as a label inside the visualizer, feel free to add it to the
        //xaml file
    
    }
}
