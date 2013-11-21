/* This program was written to practice writing to .csv files
 * and to complement the FriendBirthdayReader that reads .csv files
 * provided in class. */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;                 // (don't forget to include this when dealing with file reading / writing)

namespace FriendBirthdayWriter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FirstNameTextBox.Focus();           // give the first name text box focus on startup
        }

        private void CreateEntryButton_Click(object sender, EventArgs e)
        {
            const string PATH = "FriendRecord.csv";    // Specify the file to which you want to write.
            
            // Declare the variables where you will store the entries of the form
            string firstName, lastName, emailAddress, phoneNumber, birthMonth, birthDay;

            // Grab the entries of the form and store them in the variables we declared
            firstName =     FirstNameTextBox.Text;
            lastName =      LastNameTextBox.Text;
            emailAddress =  EmailAddressTextBox.Text;
            phoneNumber =   PhoneNumberTextBox.Text;
            birthMonth =    BirthMonthTextBox.Text;
            birthDay =      BirthDayTextBox.Text;

            // (I got to this point and thought, "Let's create a method for writing to our .csv")

            // Pack up the variables into an array because method expects a string array
            // Declare a string array of size 6 to contain all 6 variables we grabbed from the form
            string[] FriendData = new string[6];

            // Fill the entires of the array
            FriendData[0] = firstName;
            FriendData[1] = lastName;
            FriendData[2] = emailAddress;
            FriendData[3] = phoneNumber;
            FriendData[4] = birthMonth;
            FriendData[5] = birthDay;

            // Pass the path name and fields array to our WriteToCSV method
            WriteToCSV(PATH, FriendData);                   // (See below for how the method works)

// (This section is not necessary, you can compoletely remove it if you want and the whole application would still work)
            // This whole string builder idea is explained in the WriteToCSV method
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string field in FriendData)
            {
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(",");
                stringBuilder.Append(field);
            }

            MessageBox.Show("\" " + stringBuilder.ToString() + " \" was added to the .csv.");
// End of uneccessary section=========================================================================================

            // Prepare for the next friend entry
            // Clear all the text boxes
            FirstNameTextBox.Text = "";
            LastNameTextBox.Text = "";
            EmailAddressTextBox.Text = "";
            PhoneNumberTextBox.Text = "";
            BirthMonthTextBox.Text = "";
            BirthDayTextBox.Text = "";

            // Move the focus back to the first name text box
            FirstNameTextBox.Focus();
        }

        // Method for writing to our .csv
        void WriteToCSV(string path, string[] fields)
        {
            const string DELIM = ",";                       // Indicate the delimeter of the file

            // Create a stream writer to allow us to write to our .csv
            // Give it the specified path/file name for where to create or look for this .csv
            // The true parameter indicates that if the file exists, append to the file and if it doesn't create it
            StreamWriter streamWriter = new StreamWriter(path, true);

            // Create a string builder to build our comma delimited for our .csv
            StringBuilder stringBuilder = new StringBuilder();

            // Loop through each entry in the fields array and write it to this single string that we're building
            // each entry will be separated by the delimiter, which in our case is a ","
            foreach (string field in fields)
            {
                // this if statement checks to see if this is the first entry we're putting into our string builder
                // if it isn't the first string, we append a delimiter to our string before writing our next string
                if (stringBuilder.Length > 0)
                    stringBuilder.Append(DELIM);

                // append the current entry to the string builder
                stringBuilder.Append(field);
            }

            // (At this point our string similar "John,Smith,12345555,js@gmail.com,11,20" is written in stringBuilder)
            // Write stringBuilder it to .csv
            streamWriter.WriteLine(stringBuilder.ToString());

            // Close the stream
            streamWriter.Close();
        }
    }
}
