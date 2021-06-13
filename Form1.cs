using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Speech.AudioFormat;
using System.IO;

namespace speech_recognition_test_2
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine Sre = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        SpeechRecognitionEngine SreAsleep = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

        SpeechSynthesizer synth = new SpeechSynthesizer();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //test();
            timer1.Start();

            Console.WriteLine("recognizable words: \n\n\n" + File.ReadAllText(@"dictionary.txt"));
            Console.WriteLine(File.ReadAllText(@"AsleepDictionary.txt"));

            GrammarBuilder Gb = new GrammarBuilder(new Choices(File.ReadAllLines(@"dictionary.txt")));
            GrammarBuilder GbAsleep = new GrammarBuilder(new Choices(File.ReadAllLines(@"AsleepDictionary.txt")));

            Gb.Culture = new System.Globalization.CultureInfo("en-US");
            GbAsleep.Culture = new System.Globalization.CultureInfo("en-US");
            //synth.SelectVoice("David");


            Sre.LoadGrammar(new Grammar(Gb));
            SreAsleep.LoadGrammar(new Grammar(GbAsleep));

            synth.SelectVoice("Microsoft David Desktop");

            Sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Sre_SpeechRecognized);
            SreAsleep.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SreAsleep_SpeechRecognized);

            Sre.SetInputToDefaultAudioDevice();
            SreAsleep.SetInputToDefaultAudioDevice();
            synth.SetOutputToDefaultAudioDevice();

            SreAsleep.RecognizeAsync(RecognizeMode.Multiple);
        }


       private void SreAsleep_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
       {
            if (e.Result.Text == "jarvis")
            {
                listBox1.Items.Add("<< jarvis");
                synth.SpeakAsync("Yes Sir? I'm Listening!");
                listBox1.Items.Add(">> Yes sir? I'm Listening.");
                //MessageBox.Show("YesSir?");

                SreAsleep.RecognizeAsyncStop();
                Sre.RecognizeAsync(RecognizeMode.Multiple);
            }
            else if (e.Result.Text == "hi")
            {
                synth.SpeakAsync("hi!");
                //MessageBox.Show("Hi!");
                
            }
            else
            {
                synth.SpeakAsync("I don't understand!");
                MessageBox.Show("I don't understand!");
                SreAsleep.RecognizeAsync(RecognizeMode.Single);
            }

            
            
            Console.WriteLine(e.Result.Text);
       }

       private void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string text = e.Result.Text;

            if (text == "hello computer")
            {
                //MessageBox.Show("hello user");
                listBox1.Items.Add("<< hello computer");
                listBox1.Items.Add(">> hello user");
                
                synth.SpeakAsync("hello user!");
            }
            if(text == "hello")
            {
                listBox1.Items.Add("<< hello");
                listBox1.Items.Add(">> hello!");
                synth.SpeakAsync("hello!");
            }
            if(text == "never gonna give you up")
            {
                synth.SpeakAsync("never gonna let you down!");
                listBox1.Items.Add("<< never gonna give you up");
                listBox1.Items.Add(">> never gonna let you down!");
                System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
            }
            if(text == "don't stop me now")
            {
                synth.SpeakAsync("i'm haveing such a good time");
                listBox1.Items.Add("<< don't stop me now");
                listBox1.Items.Add(">> i'm haveing such a good time!");
                System.Diagnostics.Process.Start("https://youtu.be/HgzGwKwLmgM?t=37");
            }
            if(text == "stop listening")
            {
                synth.SpeakAsync("Ok, just say J A R V I S if you want to wake me up");
                listBox1.Items.Add("<< stop listening");
                listBox1.Items.Add(">> Ok, just say J.A.R.V.I.S if you want to wake me up");

                Sre.RecognizeAsyncStop();
                SreAsleep.RecognizeAsync(RecognizeMode.Multiple);
            }
            if(text == "clear" || text == "clear screen")
            {
                listBox1.Items.Clear();
                synth.SpeakAsync("done!");
            }
            if(text == "stop application" || text == "end")
            {
                listBox1.Items.Add("<< " + text);
                listBox1.Items.Add(">> goodbye!");
                synth.Speak("Goodbye!");
                Application.Exit();
            }

            //Console.WriteLine(e.Result.Text);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Sre.RecognizeAsync(RecognizeMode.Single);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sre.RecognizeAsyncStop();
        }


        private void test()
        {
            using (SpeechSynthesizer synth = new SpeechSynthesizer())
            {

                // Get information about supported audio formats.  
                string AudioFormats = "";
                foreach (SpeechAudioFormatInfo fmt in synth.Voice.SupportedAudioFormats)
                {
                    AudioFormats += String.Format("{0}\n",
                    fmt.EncodingFormat.ToString());
                }

                // Write information about the voice to the console.  
                Console.WriteLine(" Name:          " + synth.Voice.Name);
                Console.WriteLine(" Culture:       " + synth.Voice.Culture);
                Console.WriteLine(" Age:           " + synth.Voice.Age);
                Console.WriteLine(" Gender:        " + synth.Voice.Gender);
                Console.WriteLine(" Description:   " + synth.Voice.Description);
                Console.WriteLine(" ID:            " + synth.Voice.Id);
                if (synth.Voice.SupportedAudioFormats.Count != 0)
                {
                    Console.WriteLine(" Audio formats: " + AudioFormats);
                }
                else
                {
                    Console.WriteLine(" No supported audio formats found");
                }

                // Get additional information about the voice.  
                string AdditionalInfo = "";
                foreach (string key in synth.Voice.AdditionalInfo.Keys)
                {
                    AdditionalInfo += String.Format("  {0}: {1}\n",
                      key, synth.Voice.AdditionalInfo[key]);
                }

                Console.WriteLine(" Additional Info - " + AdditionalInfo);
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.SelectedIndex = -1;
            }
        }
    }
}
