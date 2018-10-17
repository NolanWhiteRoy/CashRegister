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
/// Possible additions: Acurate time and date; Order number that goes up by 1 everytime a new order is placed; tryCatch_Label disapears after number is inputed.

namespace CashRegister
{
    public partial class Form1 : Form
    {
        //Declare all variables globally 
        const double TAXES_COST = 0.13;
        const double COFFEE_COST = 1.59;
        const double DOUGHNUT_COST = 0.99;
        const double HOCKEYCARD_COST = 1.99;
        double doughnutsPurchased;
        double coffeesPurchased;
        double hockeycardsPurchased;
        double totalCost;
        double taxTotal;
        double subTotal;
        double ammountTendered;
        double changeGiven;

        //Declare all graphics
        Graphics g;
        Pen blackPen = new Pen(Color.Black, 5);
        SolidBrush whiteBrush = new SolidBrush (Color.White);
        SolidBrush blackBrush = new SolidBrush(Color.Black);
        Font titleFont = new Font("Times New Roman", 15, FontStyle.Bold);
        Font normalFont = new Font("Times New Roman", 10);

        //Declare sounds
        SoundPlayer printerPlayer = new SoundPlayer(Properties.Resources.printerSound);

        public Form1()
        {
            InitializeComponent();

            //Declare Graphics for everything
            g = this.CreateGraphics();
            //Try catch lable is not visible
            tryCatch_Label.Visible = false;
            //New order button is not visible
            newOrder_Button.Visible = false;
        }

        private void calculateButton_Click(object sender, EventArgs e)  
        {
            try
            {   //Text box values are converted to variables.
                doughnutsPurchased = Convert.ToDouble(doughnutBox.Text);
                coffeesPurchased = Convert.ToDouble(coffeeBox.Text);
                hockeycardsPurchased = Convert.ToDouble(hockeycardBox.Text);

                //Totals are calculated
                subTotal = doughnutsPurchased * DOUGHNUT_COST + coffeesPurchased * COFFEE_COST + hockeycardsPurchased * HOCKEYCARD_COST;
                totalCost = subTotal * TAXES_COST + subTotal;
                taxTotal = subTotal * TAXES_COST;

                //Totals are inputed into the labels
                taxTotal_Label.Text = "Taxes: " + taxTotal.ToString("C");
                subTotal_Label.Text = "Sub Total: " + subTotal.ToString("C");
                totalCost_Label.Text = "Total Cost: " + totalCost.ToString("C");
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
             
                //Change is calculated 
                changeGiven = ammountTendered - totalCost;

                //Amount of change is displayed in the label
                changeGiven_Label.Text = "Change: " + changeGiven.ToString("C");
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
            //Logo is not visible
            timHortonsLogo_Label.Visible = false;
           
            //Try catch lable is invisible
            tryCatch_Label.Visible = false;
            
            //Printer Sound plays 
            printerPlayer.Play();

            //Create receipt with delay in timing for printing effect
            g.DrawRectangle(blackPen, 225, 60, 230, 310);
            Thread.Sleep(500);
            g.DrawString("TIM HORTONS Inc.", titleFont, blackBrush, 245, 70);
            Thread.Sleep(300);
            g.DrawString("October 17, 2018", normalFont, blackBrush, 285, 95);
            Thread.Sleep(300);
            g.DrawString("16:48", normalFont, blackBrush, 320, 110);
            Thread.Sleep(300);
            g.DrawString("Coffees x" + coffeeBox.Text + " @ 1.59" , normalFont, blackBrush, 230, 140);
            Thread.Sleep(300);

            //Printer sound replays
            printerPlayer.Play();

            //Receipt continues printing 
            g.DrawString("Doughnuts x" + doughnutBox.Text + " @ .99 ", normalFont, blackBrush, 230, 155);
            Thread.Sleep(300);
            g.DrawString("Hockey Cards x" + hockeycardBox.Text + " @ 1.99", normalFont, blackBrush, 230, 170);
            Thread.Sleep(300);
            g.DrawString("SubTotal " + subTotal.ToString("C"), normalFont, blackBrush, 230, 200);
            Thread.Sleep(300);
            g.DrawString("Tax " + taxTotal.ToString("C"), normalFont, blackBrush, 230, 215);
            Thread.Sleep(300);
            g.DrawString("Total " + totalCost.ToString("C"), normalFont, blackBrush, 230, 230);
            Thread.Sleep(300);
            g.DrawString("Tendered " + ammountTendered.ToString("C"), normalFont, blackBrush, 230, 245);
            Thread.Sleep(300);
            g.DrawString("Change " + changeGiven.ToString("C"), normalFont, blackBrush, 230, 260);
            Thread.Sleep(300);
            g.DrawString("Order #468", titleFont, blackBrush, 285, 290);
            Thread.Sleep(300);
            g.DrawString("Tim Hortons, Always Fresh", normalFont, blackBrush, 265, 330);
            Thread.Sleep(300);
            newOrder_Button.Visible = true;
        }

        private void newOrder_Button_Click(object sender, EventArgs e)
        {
            //Old receipt is covered 
            g.FillRectangle(whiteBrush, 220, 55, 240, 320);
           
            //New order button is not visible 
            newOrder_Button.Visible = false;
            
            //Reset all lables and text boxes 
            coffeeBox.Text = "";
            doughnutBox.Text = "";
            hockeycardBox.Text = "";
            tenderedBox.Text = "";
            taxTotal_Label.Text = "Taxes ";
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
        }
    }
}
