using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

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

        SpeechRecognitionEngine Sre = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
        SpeechRecognitionEngine SreAsleep = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

        SpeechSynthesizer synth = new SpeechSynthesizer();

        private string[] preferences;
        //[0] - webbrowser
        //[1] - wiadomość podczas pierwszego uruchomienia

        bool IntroduceEnd = false;

        private int frame = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
                    "reset",
                    "show commands",
                    "open hot milf",
                    "step sister im stuck",
                    "hot milfs near you",
                    "japanese scissors",
                    "clear my history"
                };

                File.WriteAllLines("dictionary.txt",DictionaryData);
            }

            if (!File.Exists("AsleepDictionary.txt"))
            {
                File.WriteAllText("AsleepDictionary.txt","jarvis\nhi");
            }

            preferences = PreferenceFilesLoad();

            timer1.Start(); // AutoScroll

            GrammarBuilder Gb = new GrammarBuilder(new Choices(File.ReadAllLines(@"dictionary.txt")));
            GrammarBuilder GbAsleep = new GrammarBuilder(new Choices(File.ReadAllLines(@"AsleepDictionary.txt")));

            Gb.Culture = new System.Globalization.CultureInfo("en-US");
            GbAsleep.Culture = new System.Globalization.CultureInfo("en-US");

            Sre.LoadGrammar(new Grammar(Gb));
            SreAsleep.LoadGrammar(new Grammar(GbAsleep));

            synth.SelectVoice("Microsoft David Desktop");

            Sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Sre_SpeechRecognized);
            SreAsleep.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(SreAsleep_SpeechRecognized);

            Sre.SetInputToDefaultAudioDevice();
            SreAsleep.SetInputToDefaultAudioDevice();
            synth.SetOutputToDefaultAudioDevice();

            MainTimer.Start();

            if (preferences[1] == "0")
            {
                SreAsleep.RecognizeAsync(RecognizeMode.Multiple);
            }
        }


       private void SreAsleep_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
       {
            switch (e.Result.Text) 
            {
                case "jarvis":
                    listBox1.Items.Add("<< jarvis");
                    synth.SpeakAsync("Yes Sir? I'm Listening!");
                    listBox1.Items.Add(">> Yes sir? I'm Listening.");

                    SreAsleep.RecognizeAsyncStop();
                    Sre.RecognizeAsync(RecognizeMode.Multiple);
                    break;
                case "hi":
                    synth.SpeakAsync("hi!");
                    break;
            }
       }

       private void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string text = e.Result.Text;

            string[] CommandsFromFile = File.ReadAllLines(@"dictionary.txt");
            int pos = Array.IndexOf(CommandsFromFile, text);
            if (pos > -1)
            {
                listBox1.Items.Add("<< " + text);
            }


                switch (text)
            {
                case "hello computer":
                    Say("hello user!");
                    break;

                case "hello":
                    Say("hello!");
                    break;
                case "never gonna give you up":
                    Say("never gonna let you down!");
                    System.Diagnostics.Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
                    break;
                case "don't stop me now":
                    Say("i'm having such a good time");
                    System.Diagnostics.Process.Start("https://youtu.be/HgzGwKwLmgM?t=37");
                    break;
                case "stop listening":
                    Say("Ok, just say J A R V I S if you want to wake me up");

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
                    Say("Goodbye!");
                    Application.Exit();
                    break;
                #region przeglądarki internetowe
                case "open web browser":
                    listBox1.Items.Add("<< " + text);
                    listBox1.Items.Add(">> ok");
                    synth.SpeakAsync("ok");
                    switch (preferences[0])
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
                    if(preferences[0] == "chrome")
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
                        UpdatePreferences("chrome", 0);
                    }
                    break;

                case "change default web browser to edge":
                    if (preferences[0] == "edge")
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
                        UpdatePreferences("edge", 0);
                    }
                    break;
                #endregion

                case "show commands":
                    listBox1.Items.Add(">> Available commands:");

                    for (int i  = 0; i < CommandsFromFile.Length; i++)
                    {
                        listBox1.Items.Add(">>  -" + CommandsFromFile[i]);
                    }
                    synth.SpeakAsync("done!");

                    break;

                #region pornhub functions

                case "open hot milf":
                    string[] hot_milf_links = { 
                        "https://www.pornhub.com/view_video.php?viewkey=ph5be0cc66abcd8", 
                        "https://www.pornhub.com/view_video.php?viewkey=ph5be0cc66abcd8", 
                        "https://www.pornhub.com/view_video.php?viewkey=ph5cc37c614b1fa", 
                        "https://www.pornhub.com/view_video.php?viewkey=ph5c75b526c629d" 
                    };

                    Random hot_milf_rand = new Random();
                    int hot_milf_index = hot_milf_rand.Next(hot_milf_links.Length);
                    String hot_milf = hot_milf_links[hot_milf_index];

                    System.Diagnostics.Process.Start(hot_milf);
                    Say("done!");
                    break;

                case "step sister im stuck":
                    string[] im_stuck_links = {
                        "https://www.pornhub.com/view_video.php?viewkey=ph5f48d30f84acf",
                        "https://www.pornhub.com/view_video.php?viewkey=ph5d3744e991306",
                        "https://www.pornhub.com/view_video.php?viewkey=ph606b1f6b4526c"
                    };

                    Random im_stuck_rand = new Random();
                    int im_stuck_index = im_stuck_rand.Next(im_stuck_links.Length);
                    String im_stuck = im_stuck_links[im_stuck_index];

                    System.Diagnostics.Process.Start(im_stuck);
                    Say("done!");
                    break;

                case "hot milfs near you":
                    string[] near_you_links = { };
                    Say("Function not implemented.");
                    break;

                case "japanese scissors":
                    string[] japanese_scissors_links = {
                        "https://www.pornhub.com/view_video.php?viewkey=ph5f76447ae7d69",
                        "https://www.pornhub.com/view_video.php?viewkey=ph5f06eb5b43a33",
                        "https://www.pornhub.com/view_video.php?viewkey=ph590cb2ac974db",
                        "https://www.pornhub.com/view_video.php?viewkey=ph5b10497b5910e"
                    };


                    Random japanese_scissors_rand = new Random();
                    int japanese_scissors_index = japanese_scissors_rand.Next(japanese_scissors_links.Length);
                    String japanese_scissors = japanese_scissors_links[japanese_scissors_index];

                    System.Diagnostics.Process.Start(japanese_scissors);
                    Say("done!");
                    break;

                case "clear my history":
                    Say("Function not implemented.");
                    break;

                #endregion

                #region kontrola dźwięku

                case "play pause":
                    keybd_event(VK_MEDIA_PLAY_PAUSE, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
                    Say("done!");
                    break;

                case "next":
                    keybd_event(VK_MEDIA_NEXT_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
                    Say("ok!");
                    break;
                case "previous":
                    keybd_event(VK_MEDIA_PREV_TRACK, 0, KEYEVENTF_EXTENTEDKEY, IntPtr.Zero);
                    Say("ok!");
                    break;
                #endregion

                case "reset":
                    UpdatePreferences("1", 1);
                    UpdatePreferences("chrome", 0);
                    Say("Done! To see results, restart app");
                    MainTimer.Stop();
                    break;
            }
        }



        private void Button1_Click(object sender, EventArgs e)
        {
            Sre.RecognizeAsync(RecognizeMode.Single);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Sre.RecognizeAsyncStop();
        }

        // AutoScroll
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.SelectedIndex = -1;
            }
        }

        private string[] PreferenceFilesLoad()
        {
            if (!Directory.Exists(@"JarvisData\"))
            {
                Directory.CreateDirectory(@"JarvisData\");
            }

            if (!File.Exists(@"JarvisData\preferences.txt"))
            {
                File.WriteAllText(@"JarvisData\preferences.txt","chrome\n1");
            }

            bool waitForLoad = true;
            string[] fileContent = null;

            while (waitForLoad)
            {
                try
                {
                    fileContent = File.ReadAllLines(@"JarvisData\preferences.txt");
                    waitForLoad = false;
                catch
                {
                    continue;
                }
            }
            return fileContent;
        }

        private void UpdatePreferences(string data, int number)
        {
            List<string> InputFromFile = new List<string>();
            InputFromFile = File.ReadAllLines(@"JarvisData\preferences.txt").ToList<string>();
            if (number < InputFromFile.Count) {
                InputFromFile[number] = data;
            }
            File.Delete(@"JarvisData\preferences.txt");
            File.WriteAllLines(@"JarvisData\preferences.txt", InputFromFile.ToArray<string>());
            preferences = InputFromFile.ToArray<string>();
        }

        private void Animation_Tick(object sender, EventArgs e)
        {
            // Animation
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
            }
            else
            {
                button1.BackgroundImage = Properties.Resources.frame1;
                frame = 0;
            }
        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Button2_Click_1(object sender, EventArgs e)
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

        private void Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Introduction()
        {
            Thread.CurrentThread.Name = "Main";

            Task task = new Task(() => {
                Thread.Sleep(2000);

                ThreadSay("Good Morning, User!");

                Thread.Sleep(1000);

                listBox1.Invoke(new Action(() => listBox1.Items.Add(">> My name is J.A.R.V.I.S (Just A Rather Very Intelligent System)")));
                listBox1.Invoke(new Action(() => listBox1.Update()));

                synth.Speak("My name is jarvis , Just A Rather Very Intelligent System");

                Thread.Sleep(1000);

                ThreadSay("Every time you open me, I will be in sleeping mode.");

                Thread.Sleep(1000);

                ThreadSay("To wake me up, say my name.");

                Thread.Sleep(1000);

                ThreadSay("To get all commands you can use, just say \"show commands\" and everything will appear on screen!");

                Thread.Sleep(1000);

                ThreadSay("Warning:");

                Thread.Sleep(500);

                ThreadSay("This is not a full version of the software, it could have some bugs.");

                Thread.Sleep(1000);

                ThreadSay("Everything wrong please report on discord or social media");

                Thread.Sleep(1000);

                ThreadSay("Have Fun!");

                Invoke(new Action(() => IntroduceEnd = true));
            });

            task.Start();
        }

        private void Say(String text)
        {
            listBox1.Items.Add(">> " + text);
            synth.Speak(text);
        }

        private void ThreadSay(String text)
        {
            listBox1.Invoke(new Action(() => listBox1.Items.Add(">> " + text)));
            listBox1.Invoke(new Action(() => listBox1.Update()));
            synth.Speak(text);
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            if (preferences[1] == "1")
            {
                UpdatePreferences("0", 1);
                Introduction();
            }
            if (IntroduceEnd)
            {
                Sre.RecognizeAsync(RecognizeMode.Multiple);
                IntroduceEnd = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
