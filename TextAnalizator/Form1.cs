using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextAnalizator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            {
                // Read the text from the input TextBox control and split it into words
                string text = txtInput.Text;
                string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Create a dictionary to store the words and their frequency
                Dictionary<string, int> wordFreq = new Dictionary<string, int>();

                // Update the dictionary with the words from the input text
                foreach (string word in words)
                {
                    // Convert the word to lowercase and remove any non-letter characters
                    string cleanedWord = new string(word.Where(char.IsLetter).ToArray()).ToLowerInvariant();

                    // If the cleaned word is already in the dictionary, increase its frequency by 1
                    if (wordFreq.ContainsKey(cleanedWord))
                    {
                        wordFreq[cleanedWord]++;
                    }
                    // Otherwise, add the cleaned word to the dictionary with a frequency of 1
                    else
                    {
                        wordFreq.Add(cleanedWord, 1);
                    }
                }

                // Determine the length of the longest word and the highest frequency
                int maxWordLength = 0;
                int maxFrequency = 0;
                foreach (KeyValuePair<string, int> entry in wordFreq.OrderByDescending(pair => pair.Value))
                {
                    maxWordLength = Math.Max(maxWordLength, entry.Key.Length);
                    maxFrequency = Math.Max(maxFrequency, entry.Value);
                }

                // Calculate the width of the frequency column based on the number of digits in the highest frequency
                int frequencyWidth = maxFrequency.ToString().Length;

                // Generate a string containing the words and their frequency, padded with spaces to align the columns
                string results = "";
                foreach (KeyValuePair<string, int> entry in wordFreq.OrderByDescending(pair => pair.Value))
                {
                    string word = entry.Key;
                    string freq = entry.Value.ToString();

                    // Pad the word and frequency with spaces to make them the same width as the longest word and the frequency column width, respectively
                    word = word.PadRight(maxWordLength + 2);
                    freq = freq.PadLeft(frequencyWidth + 1);

                    // Add the word and frequency to the output with a newline character between them
                    results += word + freq + Environment.NewLine;
                }

                // Display the results in the output TextBox control
                txtFreq.Text = results;

                // Set the font of the output TextBox control to a fixed-width font like Courier New
                txtFreq.Font = new Font("Courier New", txtFreq.Font.Size);
            }
        }
    }
}