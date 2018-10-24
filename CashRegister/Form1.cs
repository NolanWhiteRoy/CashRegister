using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/// Created by: Nolan White-Roy
/// Date Completed: 2018-10-15
/// Summary: A basic Tim Hortons cash register capable of all requirements listed on the ICS3U assignment post.

namespace CashRegister
{
    public partial class Form1 : Form
    {
        //Declaring global constants
        //Tax cost 
        const double TAXES_COST = 0.13;
        //Item cost 
        const double COFFEE_COST = 1.59;
        const double DOUGHNUT_COST = 0.99;
        const double HOCKEYCARD_COST = 1.99;

        //Decalaring global variables and setting to 0 
        //Number of each item pruchased 
        double doughnutsPurchased = 0;
        double coffeesPurchased = 0;
        double hockeycardsPurchased = 0;
        //Costs
        double totalCost = 0;
        double taxTotal = 0;
        double subTotal = 0;
        //Amount given to pay
        double ammountTendered = 0;
        //Change given 
        double changeGiven = 0;
        // Order number on receipt
        int orderNum = 1;

        //Declare all graphics
        Graphics g;
        Pen blackPen = new Pen(Color.Black, 5);
        SolidBrush whiteBrush = new SolidBrush (Color.White);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        Font titleFont = new Font("Courier", 15, FontStyle.Bold);
        Font normalFont = new Font("Courier", 10);

        //Declare global sounds
        SoundPlayer printerPlayer = new SoundPlayer(Properties.Resources.printerSound);

        public Form1()
        {
            InitializeComponent();

            //Declare Graphic 
            g = this.CreateGraphics();

            //Try catch lable is not visible
            tryCatch_Label.Visible = false;
            //New order button is not visible
            newOrder_Button.Visible = false;
            //Change Error button is not visible
            changeError_Label.Visible = false;

            //Buttons are disabled to be subsequently enabled later 
            printReceipt_Button.Enabled = false;
            calculateChange_Button.Enabled = false;
        }

        private void calculateButton_Click(object sender, EventArgs e)
        {
            //Program assumes no value in text boxes = 0
            if (coffeeBox.Text == "")
            {
                coffeeBox.Text = "0";
            }

            if (doughnutBox.Text == "")
            {
                doughnutBox.Text = "0";
            }

            if (hockeycardBox.Text == "")
            {
                hockeycardBox.Text = "0";
            }    

                //Try-Catch in case value entered is not numerical
                try
                {   //Text box values are converted to variables.
                doughnutsPurchased = Convert.ToDouble(doughnutBox.Text);
                coffeesPurchased = Convert.ToDouble(coffeeBox.Text);
                hockeycardsPurchased = Convert.ToDouble(hockeycardBox.Text);

                /*
                 //Values cannot be less than 0
                if (hockeycardsPurchased < 0 || coffeesPurchased < 0 || doughnutsPurchased < 0);
                {
                    changeError_Label.Visible = true;
                }
                else
                */

                //Totals are calculated
                subTotal = doughnutsPurchased * DOUGHNUT_COST + coffeesPurchased * COFFEE_COST + hockeycardsPurchased * HOCKEYCARD_COST;
                totalCost = subTotal * TAXES_COST + subTotal;
                taxTotal = subTotal * TAXES_COST;

                //Totals are inputed into the labels
                taxTotal_Label.Text = "Taxes:                                  " + taxTotal.ToString("C");
                subTotal_Label.Text = "Sub Total:                             " + subTotal.ToString("C");
                totalCost_Label.Text = "Total Cost:                            " + totalCost.ToString("C");

                //Calculate change button enabled
                calculateChange_Button.Enabled = true;
            }

            catch
            {
                //Try catch label is shown if the entry is invalid
                tryCatch_Label.Visible = true;
                return;
            }

        }

        private void calculateChange_Button_Click(object sender, EventArgs e)
        {
            try
            {
                //Amount tendered is converted to a variable
                ammountTendered = Convert.ToDouble(tenderedBox.Text);

                //Amount tendered cannot be less than the total cost 

                if (ammountTendered < totalCost)

                {
                    changeError_Label.Visible = true;

                    //Won't allow you to print receipt
                    printReceipt_Button.Enabled = false;
                }

                else
                {
                    //Allowed to print receipt
                    printReceipt_Button.Enabled = true;

                    //Change is calculated 
                    changeGiven = ammountTendered - totalCost;

                    //Amount of change is displayed in the label
                    changeGiven_Label.Text = "Change:                              " + changeGiven.ToString("C");

                    //Print receipt button is enabled
                    printReceipt_Button.Enabled = true;
                }

            }

            catch
            {
                //Try catch label is shown if the entry is invalid 
                tryCatch_Label.Visible = true;
                return;
            }
        }

        private void printReceipt_Button_Click(object sender, EventArgs e)
        {
            //Cannot Print receipt multiple times (would not be a problem but with different order # and time included it causes the receipts to look different)
            printReceipt_Button.Enabled = false;

            //Cannot click other buttons again. Although unnecessary because text box values cannot be changed, it gives a visual queue to the user.
            calculateChange_Button.Enabled = false;
            calculateTotal_Button.Enabled = false;

            // Text box values cannot be changed
            tenderedBox.ReadOnly = true;
            coffeeBox.ReadOnly = true;
            doughnutBox.ReadOnly = true;
            hockeycardBox.ReadOnly = true;

            //Logo is not visible
            timHortonsLogo_Label.Visible = false;
           
            //Try catch lable is invisible
            tryCatch_Label.Visible = false;

            //Change error button is not visble
            changeError_Label.Visible = false;

            //Printer Sound plays 
            printerPlayer.Play();

            //Create receipt with delay in timing for printing effect
            g.DrawRectangle(blackPen, 225, 60, 230, 310);
            Thread.Sleep(500);
            g.DrawString("TIM HORTONS Inc. " , titleFont, blackBrush, 240, 70);
            Thread.Sleep(300);
            g.DrawString( DateTime.Now.ToString(), normalFont, blackBrush, 270, 110);
            Thread.Sleep(300);
            g.DrawString("Coffees                x " + coffeeBox.Text + " @ 1.59" , normalFont, blackBrush, 230, 140);
            Thread.Sleep(300);

            //Printer sound replays
            printerPlayer.Play();

            //Receipt continues printing 
            g.DrawString("Doughnuts           x " + doughnutBox.Text + " @ .99 ", normalFont, blackBrush, 230, 155);
            Thread.Sleep(300);
            g.DrawString("Hockey Cards     x " + hockeycardBox.Text + " @ 1.99", normalFont, blackBrush, 230, 170);
            Thread.Sleep(300);
            g.DrawString("Sub Total:            " + subTotal.ToString("C"), normalFont, blackBrush, 230, 200);
            Thread.Sleep(300);
            g.DrawString("Tax:                      " + taxTotal.ToString("C"), normalFont, blackBrush, 230, 215);
            Thread.Sleep(300);
            g.DrawString("Total:                    " + totalCost.ToString("C"), normalFont, blackBrush, 230, 230);
            Thread.Sleep(300);
            g.DrawString("Tendered:            " + ammountTendered.ToString("C"), normalFont, blackBrush, 230, 245);
            Thread.Sleep(300);
            g.DrawString("Change:               " + changeGiven.ToString("C"), normalFont, blackBrush, 230, 260);
            Thread.Sleep(300);
            g.DrawString("Order Number: " + orderNum++ , titleFont, blackBrush, 255, 290);
            Thread.Sleep(300);
            g.DrawString("Tim Hortons, Always Fresh", normalFont, blackBrush, 260, 330);
            Thread.Sleep(2000);

            //New order button is visble 
            newOrder_Button.Visible = true;
        }

        private void newOrder_Button_Click(object sender, EventArgs e)
        {
            //New order button is not visible 
            newOrder_Button.Visible = false;

            //Old receipt is covered 
            g.FillRectangle(whiteBrush, 220, 55, 240, 320);


            //Logo is visible
            timHortonsLogo_Label.Visible = true;

            //Buttons are disabled to be subsequently enabled later 
            printReceipt_Button.Enabled = false;
            calculateChange_Button.Enabled = false;

            //Calculate total button enabled
            calculateTotal_Button.Enabled = true;

            //Reset all lables and text boxes 
            coffeeBox.Text = "";
            doughnutBox.Text = "";
            hockeycardBox.Text = "";
            tenderedBox.Text = "";
            taxTotal_Label.Text = "Taxes: ";
            subTotal_Label.Text = "Sub Total:";
            totalCost_Label.Text = "Total Cost:";
            changeGiven_Label.Text = "Change:";

            //Reset all variables to 0 
            doughnutsPurchased = 0;
            coffeesPurchased = 0;
            hockeycardsPurchased = 0;
            totalCost = 0;
            taxTotal = 0;
            subTotal = 0;
            ammountTendered = 0;
            changeGiven = 0;

            //Text box values can be changed again
            tenderedBox.ReadOnly = false;
            coffeeBox.ReadOnly = false;
            doughnutBox.ReadOnly = false;
            hockeycardBox.ReadOnly = false;
        }
    }
}