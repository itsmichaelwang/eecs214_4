//Assignment 4 for EECS 214
//Out: May 7th, 2014
//Due: May 16th, 2014

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///Don't worry about this being defined as a partial class, a partial class just allows you to split up code in 
    ///different files while telling the compiler to splice them together during compilation
    public partial class MainWindow : Window
    {
        //The top, left and margin variables are used in the
        //drawStructure function. Their only purpose is to specify where to draw
        //the shapes
        int top = 200, left = 50, margin = 10;

        // Create RBTree!
        RBTree birch = new RBTree();

        public MainWindow()
        {          
            InitializeComponent();
            
            //Registering callbacks for the 2 buttons
            searchBtn.Click += searchBtn_Click;
            insertBtn.Click += insertBtn_Click;
            bhBtn.Click += bhBtn_Click;
            
            // Build a tree from the bottom up, using a from the lab guide
            birch.Insert(new RBTree.RBNode(11, null), birch);
            birch.Insert(new RBTree.RBNode(2, null), birch);
            birch.Insert(new RBTree.RBNode(14, null), birch);
            birch.Insert(new RBTree.RBNode(1, null), birch);
            birch.Insert(new RBTree.RBNode(7, null), birch);
            birch.Insert(new RBTree.RBNode(15, null), birch);
            birch.Insert(new RBTree.RBNode(5, null), birch);
            birch.Insert(new RBTree.RBNode(8, null), birch);

            // Populate BST_DB with the inOrder traversal of the tree, then draw it
            birch.inOrder();
            drawStructure();
        }

        //Search for an element in the BST
        void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            if (input.Text != "")
            {
                int number;
                bool result = Int32.TryParse(input.Text, out number);

                if (result)
                {
                    BST.BSTNode resultNode = birch.search(number);
                    if (resultNode == null)
                    {
                        MessageBoxResult message = MessageBox.Show("Unable to find node");
                    }
                    else
                    {
                        drawStructure();
                    }
                }
                else
                {
                    MessageBoxResult message = MessageBox.Show("Unable to parse integer value");
                }
            }
            else
            {
                MessageBoxResult message = MessageBox.Show("Type value in first");
            }
        }

        // Insert an element in the RBTree
        void insertBtn_Click(object sender, RoutedEventArgs e)
        {
            if (input.Text != "")
            {
                int number;
                bool result = Int32.TryParse(input.Text, out number);

                if (result)
                {
                    birch.Insert(new RBTree.RBNode(number, null), birch);
                    birch.inOrder();
                    drawStructure();
                }
                else
                {
                    MessageBoxResult message = MessageBox.Show("Unable to parse integer value");
                }
            }
            else
            {
                MessageBoxResult message = MessageBox.Show("Type value in first");
            }
        }

        // Show the black height
        void bhBtn_Click(object sender, RoutedEventArgs e)
        {
            if (input.Text != "")
            {
                int number;
                bool result = Int32.TryParse(input.Text, out number);

                if (result)
                {
                    BST.BSTNode resultNode = birch.search(number);
                    if (resultNode == null)
                    {
                        MessageBoxResult message = MessageBox.Show("Unable to find node");
                    }
                    else
                    {
                        input.Text = (birch.getBlackHeight(number)).ToString();
                    }
                }
                else
                {
                    MessageBoxResult message = MessageBox.Show("Unable to parse integer value");
                }
            }
            else
            {
                MessageBoxResult message = MessageBox.Show("Type value in first");
            }
        }

        //Draw the inorder traversal (i.e. sorted list) of BST you've defined
        private void drawStructure()
        {
            canvas.Children.Clear(); //You need to call this function to redraw the canvas after each click

            //Brush for Red nodes
            SolidColorBrush redBrush = new SolidColorBrush(Color.FromArgb(90, 201, 56, 76));
            //Brush for Black nodes
            SolidColorBrush blackBrush = new SolidColorBrush(Color.FromArgb(90, 11, 32, 56));

            // HL Brush
            SolidColorBrush hlBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            SolidColorBrush rStrBrush = new SolidColorBrush(Color.FromArgb(100, 46, 59, 74));

            // Unused Brushes?
            SolidColorBrush rBrush = new SolidColorBrush(Color.FromArgb(90, 101, 135, 178));
            SolidColorBrush lBrush = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            //We are drawing an inOrder traversal of our tree, starting with 0 to keep track of displacement
            int i = 0;
            foreach (BST.BSTNode n in birch.BST_DB)
            {
                // Re-cast as a colorful node
                RBTree.RBNode temp = (RBTree.RBNode) n;

                //Think about this drawing exercise as your having brushes dipped in different types of paint
                //And you use those brushes to paint a rectangle
                Ellipse r = new Ellipse();
                r.Width = 40;
                r.Height = 40;

                // Red vs. Black Nodes
                if (temp.NodeColor == RBTree.COLOR.BLACK)
                {
                    r.Fill = blackBrush;
                }
                else
                {
                    r.Fill = redBrush;
                }
                r.StrokeThickness = 2; //This defines the thickness of the outline of each rectangular block

                // Highlighted vs. Un-Highlighted Nodes
                if (n == birch.hlNode)
                {
                    r.Stroke = hlBrush;
                } else {
                    r.Stroke = rStrBrush;
                }

                Label value = new Label();//It's all about objects! A label is an object that can contain text
                value.Width = r.Width;  //We have to define how wide the label can go, otherwise the text can overflow from the rectangle
                value.Height = r.Height;
                value.Content = temp.Field;      //Read the i-th element in the stack
                value.FontSize = 12;
                value.Foreground = lBrush; //Again, consider that text is also painted on, the paint color is specified in line 84
                value.HorizontalContentAlignment = HorizontalAlignment.Center; //We are just centering the text horizontally and vertically
                value.VerticalContentAlignment = VerticalAlignment.Center;

                canvas.Children.Add(r); //Add the rectangle 
                Canvas.SetLeft(r, left + i * r.Width); //Set the left (i.e. x-coordinate of the rectangle)
                Canvas.SetTop(r, top + margin); //set the position of the rectangle in the canvas

                //Add the text. Note, if you've added the text before the rectangle, the text would have
                //been occluded by the rectangle. 
                //So the order of drawing things matter. The things you draw later, are the ones that are layered on top of the previous ones
                canvas.Children.Add(value);
                Canvas.SetTop(value, Canvas.GetTop(r));
                Canvas.SetLeft(value, left + i * r.Width);

                // Increment by 1 to right shift the blocks when necessary
                i++;
            }
        }
    }
}
