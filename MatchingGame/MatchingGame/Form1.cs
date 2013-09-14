using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();
        }


        Random random = new Random();

        //Stores what was clicked for comparison reasons (to see if there is a match)
        Label firstClicked = null;

        Label secondClicked = null;


        //These letters are a neat wingding b/c i guess this
        //tutorial doesnt want to go into loading graphics :(

        //Hopefully it will cover it in some way. b/c I feel like these
        //forms are pretty neat.

        //Pretty sure my suffle bag would of done this better.
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",",",", "k","k",
            "b","b", "v","v","w","w","z","z"
        };

        private void AssignIconsToSquares()
        { 
            //Pulls an icon at random from the list and added to the label
            //So this is a simplified version of my suffle bag after all.
            //I could make this reusable, i wonder if the performance is better.
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }//Assign Icons method

        private void label_Click(object sender, EventArgs e)
        {
        
            //not sure about this one.
            if (timer1.Enabled)
            {
                return;
            }  

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            { 
                //If its black it has already been clicked so ignore the click
                if (clickedLabel.ForeColor == Color.Black)
                    return;


                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;
                }
               
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                //See if they won
                CheckForWinner();

                //Check and see if they are a pair
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;

                    return;
                }


                timer1.Start();
            }
        } //Label click handler
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        { 
            //Goes through the labels in the TableLayoutPanel
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                //If something is not matched then we wont get further.
                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            //Since the loop finished that means we have a winner.
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();

        }


    }
}
