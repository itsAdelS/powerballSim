//Program Name:     PowerBallWinForms.
//
//Description:      This program is a windows forms application that simulates play of the
//                  multi-state Powerball lottery game => http://www.powerball.com/pb_home.asp
//                  It randomly picks the "winning" white ball numbers (5) and the winning red
//                  ball (1).  It then randomly draws 5 white balls and 1 red ball and
//                  checks to see if these match the "winning" numbers.  It continues drawing
//                  until the draw matches the winning numbers.
//
//Date Written:     9/26/2016
//
//Programmer:       Adel Sliman
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerBallWinForms
{
    public partial class PowerballForm : Form
    {       
        //69 white balls, 26 red balls, 5 white balls drawn, 1 red ball drawn.
        //We use const because we never want to use "magic numbers."
        public const int whiteBallHopperCount = 69;

        public const int redBallHopperCount = 26;

        public const int numOfWhiteBallsToBeDrawn = 5;

        public PowerballForm()
        {
            InitializeComponent();
        }

        //This event fires when the user clicks the button to simulate the game.
        private void simulateGameButton_Click(object sender, EventArgs e)
        {
            int NumOfDraws = 0;

            int WBP = 0;
            int RBP = 0;
            int RBW = 0;

            int[] A = new int[69];

            for(int i = 0; i < A.Length; i++)
            {
                A[i] = i + 1;
            }

            Random WhiteBall = new Random();
            Random RedBall = new Random();

            bool whiteBallsMatch;

            for (int i = 0; i < A.Length; i++)
            {
                A[i] = i + 1;
            }

            WBP = A[WhiteBall.Next(0, 69)];

            RBW = RedBall.Next(1, 26);


            
            int[] wbWinners = new int[5];
            int workIndex;
            int LastIndex = 68;

           

            int[] wbUserArray = new int[5];
            int workUserIndex;
            int LastUserIndex = 68;

            //winner Pick Numbers
            for (int i = 0; i < wbWinners.Length; i++)
            {
                workIndex = WhiteBall.Next(0, (LastIndex + 1));
                wbWinners[i] = A[workIndex];

                for (int k = workIndex; k < LastIndex; k++)
                {
                    A[k] = A[k + 1];
                }
                LastIndex--;
            }
            Array.Sort(wbWinners);

            while (true)
            {
                //the Counter.
                NumOfDraws++;

                //reset use index
                LastUserIndex = 68;
                RBP = RedBall.Next(1, 26);

                //compares the winning redball to the current player redball.
                if (RBW != RBP)
                {
                    continue;
                }
                //Reload whiteball Array "A"
                for (int i = 0; i < A.Length; i++)
                {
                    A[i] = i + 1;
                }

                LastUserIndex = 68;

                //user Pick Numbers
                for (int i = 0; i < wbUserArray.Length; i++)
                {
                  workUserIndex = WhiteBall.Next(0, (LastUserIndex + 1));
                  wbUserArray[i] = A[workUserIndex];

                  for (int k = workUserIndex; k < LastUserIndex; k++)
                  {
                        A[k] = A[k + 1];
                  }
                  LastUserIndex--;
                }
                Array.Sort(wbUserArray);

                //Matches the whiteballs
                whiteBallsMatch = true;

                //checks the current whiteball winners against our current whiteball picks.
                for (int j = 0; j < wbUserArray.Length; j++)
                {
                    if (wbWinners[j] != wbUserArray[j])
                    {
                        whiteBallsMatch = false;
                        break;
                    }
                }

                //if none of the white balls match then continue.
                if (!whiteBallsMatch)
                {
                    continue;
                }

                //displays the winning and current information!
                whiteWinnerLabel01.Text = wbWinners[0].ToString();
                whiteWinnerLabel02.Text = wbWinners[1].ToString();
                whiteWinnerLabel03.Text = wbWinners[2].ToString();
                whiteWinnerLabel04.Text = wbWinners[3].ToString();
                whiteWinnerLabel05.Text = wbWinners[4].ToString();
                redWinnerLabel.Text = RBW.ToString();

                redPickLabel.Text = RBP.ToString();
                whitePickLabel01.Text = wbUserArray[0].ToString();
                whitePickLabel02.Text = wbUserArray[1].ToString();
                whitePickLabel03.Text = wbUserArray[2].ToString();
                whitePickLabel04.Text = wbUserArray[3].ToString();
                whitePickLabel05.Text = wbUserArray[4].ToString();

                drawCountLabel.Text = NumOfDraws.ToString("n0");

                int cost = 2;

                cost = cost * NumOfDraws;

                lblCost.Text = cost.ToString("C");
                lblCost.Visible = true;
                lblCost.BackColor = Color.LimeGreen;
                //Breaks out of the loop
                break;

            }

           


        }

        private void displayOddsButton_Click(object sender, EventArgs e)
        {
            int workWhiteBalls = whiteBallHopperCount;
            
            int workRedBalls = redBallHopperCount;

            int workWhiteBallPicks = numOfWhiteBallsToBeDrawn;

            double probabilityOfWinning = 1;

            int oddsOfWinning = 0;

            string s = "The odds of winning the PowerBall jackpot are calculated as follows:\n\n";

            for (int i = workWhiteBallPicks; i >= 1; i--)
            {
                probabilityOfWinning = probabilityOfWinning * ((double)i / (workWhiteBalls - (5 - i)));

                s = s + i + "/" + (workWhiteBalls - (5 - i)) + " * ";
            }

            probabilityOfWinning = probabilityOfWinning * ((double)1 / workRedBalls);

            s = s + 1 + "/" + workRedBalls + " = " + probabilityOfWinning.ToString("n20") + "!\n\n";

            oddsOfWinning = (int)(1 / probabilityOfWinning);

            s = s + "Or, expressed in odds format, we have 1 chance in every " + oddsOfWinning.ToString("n0") + " draws of picking the winning numbers!!!";

            MessageBox.Show(s,"Odds of Winning",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            simulateGameButton.Enabled = true;

            simulateGameButton.Focus();

            foreach (var ctrl in Controls)
            {
                if (ctrl.GetType() == typeof(GroupBox))
                {
                    GroupBox gbx = ctrl as GroupBox;

                    foreach (var gbxCtrl in gbx.Controls)
                    {
                        Label lbl = (Label)gbxCtrl;

                        if (lbl.BackColor == Color.Yellow || lbl.BackColor == Color.Red)
                        {
                            lbl.Text = "";
                        }        
                    }                   
                }      
            }

            drawCountLabel.Text = "";
            lblCost.Text = "";
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
