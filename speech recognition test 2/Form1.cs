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
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace speech_recognition_test_2
{
    public partial class Form1 : Form
    {
        #region obsługa klawiatury czy coś //pomijanie piosenek i video (kod ze strony: https://ourcodeworld.com/articles/read/128/how-to-play-pause-music-or-go-to-next-and-previous-track-from-windows-using-c-valid-for-all-windows-music-players )


        public const int KEYEVENTF_EXTENTEDKEY = 1;
        public const int KEYEVENTF_KEYUP = 0;
        public const int VK_MEDIA_NEXT_TRACK = 0xB0;
        public const int VK_MEDIA_PLAY_PAUSE = 0xB3;
        public const int VK_MEDIA_PREV_TRACK = 0xB1;

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte virtualKey, byte scanCode, uint flags, IntPtr extraInfo);

        #endregion

        #region stuff thai i need to move the window around
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        //po prostu tworzymy obiekty
        SpeechRecognitionEngine Sre = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US")); //rozpoznaje mowę jak wywołasz jarvisa
        SpeechRecognitionEngine SreAsleep = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US")); //rozpoznaje mowę jak jarvis jest uśpiony

        SpeechSynthesizer synth = new SpeechSynthesizer(); //syntezator mowy

        private string[] preferencje;
        //[0] - webbrowser
        //[1] - wiadomość podczas pierwszego uruchomienia

        private int frame = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //uruchamiamy animację
            Animation.Start();


            if (!File.Exists("dictionary.txt"))
            {
                string[] DictionaryData = {
                    "never gonna give you up",
                    "don\'t stop me now",
                    "hello",
                    "hello computer",
                    "stop listening",
                    "clear",
                    "clear screen",
                    "stop application",
                    "end",
                    "open web browser",
                    "change default web browser to chrome",
                    "change default web browser to edge",
                    "play pause",
                    "next",
                    "previous",
                    "reset"
                };

                File.WriteAllLines("dictionary.txt",DictionaryData);
            }

            if (!File.Exists("AsleepDictionary.txt"))
            {
                File.WriteAllText("AsleepDictionary.txt","jarvis\nhi");
            }

            preferencje = PreferenceFilesLoad();

            //test();
            timer1.Start(); //auto scroll

            //Console.WriteLine("recognizable words: \n\n\n" + File.ReadAllText(@"dictionary.txt")); //do debugowania
            //Console.WriteLine(File.ReadAllText(@"AsleepDictionary.txt")); //to też

            GrammarBuilder Gb = new GrammarBuilder(new Choices(File.ReadAllLines(@"dictionary.txt"))); //wszystkie komendy
            GrammarBuilder GbAsleep = new GrammarBuilder(new Choices(File.ReadAllLines(@"AsleepDictionary.txt"))); //komendy do wywołania jarvisa

            //z tym miałem spory problem bo mam windowsa po polsku a język polski nie ma rozpoznawania mowy, te dwie linijki naprawiły to :)
            Gb.Culture = new System.Globalization.CultureInfo("en-US");
            GbAsleep.Culture = new System.Globalization.CultureInfo("en-US");
            //synth.SelectVoice("David");

            //wczytywanie komend
            Sre.LoadGrammar(new Grammar(Gb));
            SreAsleep.LoadGrammar(new Grammar(GbAsleep));

            synth.SelectVoice("Microsoft David Desktop"); //może sprawiać drobne prblemy jak ktoś nie ma zainstalowanego

            //dodawanie eventów kiedy rozposnawanie mowy coś rozpozna
            Sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Sre_SpeechRecognized);
            SreAsleep.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SreAsleep_SpeechRecognized);

            //ustawianie wejść i wyjścia
            Sre.SetInputToDefaultAudioDevice();
            SreAsleep.SetInputToDefaultAudioDevice();
            synth.SetOutputToDefaultAudioDevice();

            //uruchomienie głównego timera (czas: 1 sekunda)
            MainTimer.Start();

            //tutaj rozpoczyna się słuchanie (zareaguje na słowo "jarvis")
            SreAsleep.RecognizeAsync(RecognizeMode.Multiple);

        }


       private void SreAsleep_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
       {
            switch (e.Result.Text) 
            {
                case "jarvis":
                    listBox1.Items.Add("<< jarvis");
                    synth.SpeakAsync("Yes Sir? I'm Listening!");
                    listBox1.Items.Add(">> Yes sir? I'm Listening.");
                    //MessageBox.Show("YesSir?");

                    SreAsleep.RecognizeAsyncStop();
                    Sre.RecognizeAsync(RecognizeMode.Multiple);
                    break;
                case "hi":
                    synth.SpeakAsync("hi!");
                    //MessageBox.Show("Hi!");
                    break;
            }
            
            //Console.WriteLine(e.Result.Text); //do debugowania
       }

       private void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string text = e.Result.Text;

            switch (text)
            {
                case "hello computer":
                    listBox1.Items.Add("<< hello computer");
                    listBox1.Items.Add(">> hello user");

                    synth.SpeakAsync("hello user!");
                    break;

                case "hello":
                    listBox1.Items.Add("<< hello");
                    listBox1.Items.Add(">> hello!");
                    synth.SpeakAsync("hello!");
                    break;
                case "never gonna give you up":      //klasyczny rickroll 
                    synth.SpeakAsync("never gonna let you down!");
                    listBox1.Items.Add("<< never gonna give you up");
                    listBox1.Items.Add(">> never gonna let you down!");
                    System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
                    break;
                case "don't stop me now":           //po porsu lubię tę piosenkę, więc czemu nie?
                    synth.SpeakAsync("i'm having such a good time");
                    listBox1.Items.Add("<< don't stop me now");
                    listBox1.Items.Add(">> i'm having such a good time!");
                    System.Diagnostics.Process.Start("https://youtu.be/HgzGwKwLmgM?t=37");
                    break;
                case "stop listening":
                    synth.SpeakAsync("Ok, just say J A R V I S if you want to wake me up");
                    listBox1.Items.Add("<< stop listening");
                    listBox1.Items.Add(">> Ok, just say J.A.R.V.I.S if you want to wake me up");

                    Sre.RecognizeAsyncStop();
                    SreAsleep.RecognizeAsync(RecognizeMode.Multiple);
                    break;
                case "clear":
                case "clear screen":
                    listBox1.Items.Clear();
                    synth.SpeakAsync("done!");
                    break;
                case "stop application":
                case "end":
                    listBox1.Items.Add("<< " + text);
                    listBox1.Items.Add(">> goodbye!");
                    synth.Speak("Goodbye!");
                    Application.Exit();
                    break;
                #region przeglądarki internetowe
                case "open web browser":
                    listBox1.Items.Add("<< " + text);
                    listBox1.Items.Add(">> ok");
                    synth.SpeakAsync("ok");
                    switch (preferencje[0])
                    {
                        case "chrome":
                            System.Diagnostics.Process.Start("chrome.exe");
                            break;
                        case "edge":
                            System.Diagnostics.Process.Start("msedge.exe");
                            break;
                    }
                    break;
                case "change default web browser to chrome":
                    if(preferencje[0] == "chrome")
                    {
                        listBox1.Items.Add("<< " + text);
                        listBox1.Items.Add(">> your default web browser is chrome already!");
                        synth.SpeakAsync("your default web browser is chrome already");
                    }
                    else
                    {
                        listBox1.Items.Add("<< " + text);
                        listBox1.Items.Add(">> default browser changed sucessfully!");
                        synth.SpeakAsync("default browser changed sucessfully");
                        UpDatePreferences("chrome", 0);
                    }
                    break;

                case "change default web browser to edge":
                    if (preferencje[0] == "edge")
                    {
                        listBox1.Items.Add("<< " + text);
                        listBox1.Items.Add(">> your default web browser is edge already!");
                        synth.SpeakAsync("your default web browser is chrome already");
                    }
                    else
                    {
                        listBox1.Items.Add("<< " + text);
                        listBox1.Items.Add(">> default browser changed sucessfully!");
                        synth.SpeakAsync("default browser changed sucessfully");
                        UpDatePreferences("edge", 0);
                    }
                    break;
                #endregion

                case "show commands":
                    string[] CommandsFromFile = File.ReadAllLines(@"dictionary.txt");

                    listBox1.Items.Add(">> Available commands:");

                    for (int i  = 0; i < CommandsFromFile.Length; i++)
                    {
                        listBox1.Items.Add(">>  -" + CommandsFromFile[i]);
                    }
                    synth.SpeakAsync("done!");

                    break;
                #region kontrola dźwięku

                case "play pause":
                    keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
                    synth.SpeakAsync("done!");
                    listBox1.Items.Add("<< " + text);
                    listBox1.Items.Add(">> done!");
                    break;

                case "next":
                    keybd_event(VK_MEDIA_NEXT_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
                    synth.SpeakAsync("ok!");
                    listBox1.Items.Add("<< " + text);
                    listBox1.Items.Add(">> ok!");
                    break;
                case "previous":
                    keybd_event(VK_MEDIA_PREV_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
                    synth.SpeakAsync("ok!");
                    listBox1.Items.Add("<< " + text);
                    listBox1.Items.Add(">> ok!");
                    break;
                #endregion

                case "reset":
                    UpDatePreferences("1", 1);
                    UpDatePreferences("chrome", 0);
                    listBox1.Items.Add(">> Done! To see results, restart app");
                    synth.SpeakAsync("Done! To see results, restart app");
                    MainTimer.Stop();
                    break;
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

        /*
                private void test() //skopiowane z dokumentacji, tylko do debugowania!
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
        */

        //auto scroll
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.SelectedIndex = -1;
            }
        }

        private string[] PreferenceFilesLoad()
        {
            if (!Directory.Exists(@"JarvisData\")) //tworzenie folderu jeśli nie istnieje
            {
                Directory.CreateDirectory(@"JarvisData\");
            }

            if (!File.Exists(@"JarvisData\preferences.txt")) //tworzenie pliku jeśli nie istnieje
            {
                File.WriteAllText(@"JarvisData\preferences.txt","chrome\n1");
            }

            bool NieMamPomyslu = true;
            string[] InputFromFile = null;

            while (NieMamPomyslu) //czekanie aż system stworzy plik jeśli nie istnieje
            {
                try
                {
                    InputFromFile = File.ReadAllLines(@"JarvisData\preferences.txt");
                    NieMamPomyslu = false;
                }
                catch
                {
                    continue;
                }
            }
            return InputFromFile;
        }

        private void UpDatePreferences(string data, int number)
        {
            List<string> InputFromFile = new List<string>();
            InputFromFile = File.ReadAllLines(@"JarvisData\preferences.txt").ToList<string>();
            if (number < InputFromFile.Count) {
                InputFromFile[number] = data;
            }
            File.Delete(@"JarvisData\preferences.txt");
            File.WriteAllLines(@"JarvisData\preferences.txt", InputFromFile.ToArray<string>());
            preferencje = InputFromFile.ToArray<string>();
        }

        private void Animation_Tick(object sender, EventArgs e)
        {
            //animacja
            if (synth.State == SynthesizerState.Speaking)
            {
                frame++;
                if (frame > 3) { frame = 1; }
                switch (frame)
                {
                    case 1:
                        button1.BackgroundImage = Properties.Resources.frame2;
                        break;
                    case 2:
                        button1.BackgroundImage = Properties.Resources.frame3;
                        break;
                    case 3:
                        button1.BackgroundImage = Properties.Resources.frame4;
                        break;
                }

                //MessageBox.Show("it soould work");
            }
            else
            {
                button1.BackgroundImage = Properties.Resources.frame1;
                frame = 0;
            }

            //Console.WriteLine(synth.Volume);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region przyciski

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            button2.ForeColor = Color.Red;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.DeepSkyBlue;
        }

        private void button3_MouseMove(object sender, MouseEventArgs e)
        {
            button3.ForeColor = Color.Blue;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.ForeColor = Color.DeepSkyBlue;
        }

        private void button4_MouseMove(object sender, MouseEventArgs e)
        {
            button4.ForeColor = Color.Blue;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.ForeColor = Color.DeepSkyBlue;
        }

        #endregion

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void introduction()
        {
            Thread.CurrentThread.Name = "Main"; // zmiana aktualnego zadania na Main

            Task task = new Task(() => {
                System.Threading.Thread.Sleep(2000);
                listBox1.Invoke(new Action(() => listBox1.Items.Add(">> Good Morning, User!")));
                listBox1.Invoke(new Action(() => listBox1.Update()));
                synth.Speak("Good Morning User!");
                System.Threading.Thread.Sleep(1000);
                listBox1.Invoke(new Action(() => listBox1.Items.Add(">> My name is J.A.R.V.I.S (Just A Rather Very Intelligent System)")));

                listBox1.Invoke(new Action(() => listBox1.Update()));
                synth.Speak("My name is jarvis , Just A Rather Very Intelligent System");
                System.Threading.Thread.Sleep(1000);
                listBox1.Invoke(new Action(() => listBox1.Items.Add(">> Every time you open me, I will be in sleeping mode.")));

                listBox1.Invoke(new Action(() => listBox1.Update()));
                synth.Speak("Every time you open me, I will be in sleeping mode.");
                System.Threading.Thread.Sleep(1000);
                listBox1.Invoke(new Action(() => listBox1.Items.Add(">> To wake me up, say my name.")));

                listBox1.Invoke(new Action(() => listBox1.Update()));
                synth.Speak("To wake me up, say my name.");
                System.Threading.Thread.Sleep(1000);
                listBox1.Invoke(new Action(() => listBox1.Items.Add(">> To get all commands you can use, just say \"show commands\" and everything will appear on screen! ")));
                listBox1.Invoke(new Action(() => listBox1.Update()));
                synth.Speak("To get all commands you can use, just say \"show commands\" and everything will appear on screen! ");
                System.Threading.Thread.Sleep(1000);
                listBox1.Invoke(new Action(() => listBox1.Items.Add(">> Warning:")));
                listBox1.Invoke(new Action(() => listBox1.Items.Add(">> This is not a full version of the software, it could have some bugs.")));
                listBox1.Invoke(new Action(() => listBox1.Update()));
                synth.Speak("Warning!");
                System.Threading.Thread.Sleep(500);
                synth.Speak("This is not a full version of the software, it could have some bugs.");
                System.Threading.Thread.Sleep(1000);
                listBox1.Invoke(new Action(() => listBox1.Items.Add(">> Everything wrong please report on discord or social media")));
                listBox1.Invoke(new Action(() => listBox1.Update()));
                synth.Speak("Everything wrong please report on discord or social media");
                System.Threading.Thread.Sleep(1000);

                listBox1.Invoke(new Action(() => listBox1.Items.Add(">> Have Fun!")));
                listBox1.Invoke(new Action(() => listBox1.Update()));
                synth.Speak("Have Fun!");
            });

            // Start osobnego zadania asynchronicznego
            task.Start();
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            if (preferencje[1] == "1")
            {
                UpDatePreferences("0", 1);
                introduction();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
