using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;

namespace MyFormApp
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        

        public Form1()
        {
            InitializeComponent();
            
        }

        private void EnableButton_Click(object sender, EventArgs e)
        {
            //recEngine.RecognizeAsync(RecognizeMode.Multiple);
            //DisableButton.Enabled = true;
            foreach (RecognizerInfo ri in SpeechRecognitionEngine.InstalledRecognizers())
            {
                System.Diagnostics.Debug.WriteLine(ri.Culture.Name);
                Console.WriteLine(ri.Culture.Name);
                richTextBox1.Text += ri.Culture.Name;
            }
            Console.WriteLine("test passed");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*
            Choices commands = new Choices();
            commands.Add(new string[] { "say hello", "print my name" });
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammar = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += recEngine_SpeechRecognized;
            */
        }

        private void recEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "say hello":
                    MessageBox.Show("hello!");
                    break;
                case "print my name":
                    richTextBox1.Text += "\nAntek";
                    break;
            }
        }

        private void DisableButton_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
            DisableButton.Enabled = false;
        }
    }
}
