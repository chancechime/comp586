/*
    * Project 1: Remote Control
    * Student: Chance Chime
*/
using System;

namespace Project1
{
    public enum MuteStatus { Muted, Unmuted } // Mute Status

    public enum PowerStatus { On, Off } // Power Status

    public enum ChannelDirectory { News, Sports, Movies, Music, Kids, Food, Shopping, Travel, Education, Lifestyle, Technology, Gaming, Fitness, Religion, Weather, Documentary, History, Science, Nature, Fashion, Comedy, Drama, Reality, TalkShow, GameShow, SoapOpera, Sitcom, Horror, Thriller, Action, Adventure, Mystery, Romance, Western, War, Crime, Legal, Medical, Police, Military, Espionage, Superhero, Fantasy, ScienceFiction, Animation, Family, Musical, Dance, Theatre, Variety, Awards, Cooking, Canada, Overseas, Northridge }

    public class Delay
    {
        public static void delay(int ms = 550)
        {
            System.Threading.Thread.Sleep(ms);
        }
    }

    public class Remote // Remote Control Class
    {
        private Screen screen; // Screen Field
        public Remote(Screen screen) // Remote Constructor
        {
            this.screen = screen;
        }
        public static string DisplayChannelList(int listedchannel) // Display Channel List
        {
            var channelName = Enum.GetName(typeof(ChannelDirectory), listedchannel - 1);
            string theChannel = $"Channel {listedchannel} - {channelName}";
            return theChannel ?? "Unknown Channel";
        }
        public void Power() // Power On/Off Method
        {
            screen.TVStatus();
            this.screen.Power = this.screen.Power == PowerStatus.On ? PowerStatus.Off : PowerStatus.On; // Toggles Power
            Console.WriteLine($"TV Power: {this.screen.Power}");
        }
        public void Volume(int vol)// Volume Adjustment Method
        {
            screen.TVStatus();
            if (this.screen.Power == PowerStatus.On) // Check if TV is on
            {
                switch (vol) // Volume Adjustment
                {
                    case 1 when this.screen.Volume < 20: // Increase Volume
                        this.screen.Volume++;
                        Console.WriteLine($"Volume: {this.screen.Volume}|20 {(this.screen.Mute == MuteStatus.Muted ? " (MUTED)" : "")}");
                        break;
                    case 1 when this.screen.Volume >= 20: // Volume is at max level
                        Console.WriteLine($"Volume is at max level. (20|20) {(this.screen.Mute == MuteStatus.Muted ? " (MUTED)" : "")}");
                        break;

                    case 0 when this.screen.Volume > 0: // Decrease Volume
                        this.screen.Volume--;
                        Console.WriteLine($"Volume: {this.screen.Volume}|20 {(this.screen.Mute == MuteStatus.Muted ? " (MUTED)" : "")}");
                        break;
                    case 0 when this.screen.Volume <= 0: // Volume is at min level
                        Console.WriteLine($"Volume is at min level. (0|20) {(this.screen.Mute == MuteStatus.Muted ? " (MUTED)" : "")}");
                        break;

                    default: // Invalid Volume Adjustment
                        Console.WriteLine("Invalid volume adjustment.");
                        break;
                }
            }
            else { Console.WriteLine("TV is Off. Unable to adjust Volume."); }
        }
        public void Channel(int chn) // Change Channel Method
        {
            screen.TVStatus();
            switch (chn) // Change Channel
            {
                case 0: // Increase Channel
                    if (this.screen.Power == PowerStatus.On)
                    {
                        this.screen.Channel++;
                        if (this.screen.Channel == 56) { this.screen.Channel = 1; }
                        Console.WriteLine($"{DisplayChannelList(this.screen.Channel)}");
                    }
                    else
                    {
                        Console.WriteLine("TV is Off. Unable to change Channel.");
                    }
                    break;
                case -1: // Decrease Channel
                    if (this.screen.Power == PowerStatus.On) // Check if TV is on
                    {
                        this.screen.Channel--;
                        if (this.screen.Channel == 0) { this.screen.Channel = 55; }
                        Console.WriteLine($"{DisplayChannelList(this.screen.Channel)}"); // Display Channel
                    }
                    else
                    {
                        Console.WriteLine("TV is Off. Unable to change Channel.");
                    }
                    break;
                default: // Change to specific channel
                    if (this.screen.Power == PowerStatus.On) // Check if TV is on
                    {
                        this.screen.Channel = chn;
                        Console.WriteLine($"{DisplayChannelList(this.screen.Channel)}"); //  Display Channel
                    }
                    else
                    {
                        Console.WriteLine("TV is Off. Unable to change Channel.");
                    }
                    break;
            }
        }
        public void Mute() // Mute/Unmute
        {
            screen.TVStatus();
            if (this.screen.Power == PowerStatus.On) // Check if TV is on
            {
                this.screen.Mute = this.screen.Mute == MuteStatus.Muted ? MuteStatus.Unmuted : MuteStatus.Muted; // Toggles Mute
                Console.WriteLine($"Mute: {this.screen.Mute}");
            }
            else
            {
                Console.WriteLine("TV is Off. Unable to mute.");
            }
        }
        public void SmartMenu() // Smart Menu
        {
            screen.TVStatus();
            Console.WriteLine("Smart Menu"); // Smart Menu
            Console.WriteLine("   1 - Apps");
            Console.WriteLine("   2 - Gallery");
            Console.WriteLine("   3 - Web Browser");
            Console.WriteLine("   4 - Screen Mirroring");
            Console.WriteLine("   5 - Voice Assistant");
            Console.WriteLine("   0 - Exit");
            Console.WriteLine("\nEnter a command:");

            string smartcmd = Console.ReadLine()!;
            Console.WriteLine();

            switch (smartcmd) // Smart Menu
            {
                case "1": // Apps
                    Console.WriteLine("Opening Apps ...");
                    screen.TVStatus();
                    Console.WriteLine("Apps Menu"); // Apps Menu
                    Console.WriteLine("   1 - Netflix");
                    Console.WriteLine("   2 - Hulu");
                    Console.WriteLine("   3 - Amazon Prime Video");
                    Console.WriteLine("   4 - Disney+");
                    Console.WriteLine("   5 - YouTube");
                    Console.WriteLine("   0 - Exit");
                    Console.WriteLine("\nEnter a command:");
                    string appcmd = Console.ReadLine()!;
                    Console.WriteLine();

                    switch (appcmd) // Apps Menu
                    {
                        case "1": // Netflix
                            Console.WriteLine("Opening Netflix ...");
                            Delay.delay();
                            screen.TVStatus();
                            Console.WriteLine("Netflix Opened\n");
                            Console.WriteLine("Press any key to return to Smart Menu ...");
                            Console.ReadKey();
                            SmartMenu();
                            break;
                        case "2": // Hulu
                            Console.WriteLine("Opening Hulu ...");
                            Delay.delay();
                            screen.TVStatus();
                            Console.WriteLine("Hulu Opened\n");
                            Console.WriteLine("Press any key to return to Smart Menu ...");
                            Console.ReadKey();
                            SmartMenu();
                            break;
                        case "3": // Amazon Prime Video
                            Console.WriteLine("Opening Amazon Prime Video ...");
                            Delay.delay();
                            screen.TVStatus();
                            Console.WriteLine("Amazon Prime Video Opened\n");
                            Console.WriteLine("Press any key to return to Smart Menu ...");
                            Console.ReadKey();
                            SmartMenu();
                            break;
                        case "4": // Disney+
                            Console.WriteLine("Opening Disney+ ...");
                            Delay.delay();
                            screen.TVStatus();
                            Console.WriteLine("Disney+ Opened\n");
                            Console.WriteLine("Press any key to return to Smart Menu ...");
                            Console.ReadKey();
                            SmartMenu();
                            break;
                        case "5": // YouTube
                            Console.WriteLine("Opening YouTube ...");
                            Delay.delay();
                            screen.TVStatus();
                            Console.WriteLine("Youtube Opened\n");
                            Console.WriteLine("Press any key to return to Smart Menu ...");
                            Console.ReadKey();
                            SmartMenu();
                            break;
                        case "0": // Exit Apps
                            Console.WriteLine("Exiting Apps ...");
                            SmartMenu();
                            break;
                        default: // Invalid Command
                            Console.WriteLine("Invalid Command. Please try again.");
                            SmartMenu();
                            break;
                    }
                    break;
                case "2": // Gallery
                    Console.WriteLine("Opening Gallery ...");
                    Delay.delay();
                    Console.WriteLine("Press any key to return to Smart Menu ...");
                    Console.ReadKey();
                    SmartMenu();
                    break;
                case "3": // Web Browser
                    Console.WriteLine("Opening Web Browser ...");
                    Delay.delay();
                    Console.WriteLine("Press any key to return to Smart Menu ...");
                    Console.ReadKey();
                    SmartMenu();
                    break;
                case "4": // Screen Mirroring
                    Console.WriteLine("Opening Screen Mirroring ...");
                    Delay.delay();
                    Console.WriteLine("Press any key to return to Smart Menu ...");
                    Console.ReadKey();
                    SmartMenu();
                    break;
                case "5": // Voice Assistant
                    Console.WriteLine("Opening Voice Assistant ...");
                    Delay.delay();
                    Console.WriteLine("Press any key to return to Smart Menu ...");
                    Console.ReadKey();
                    SmartMenu();
                    break;
                case "0": // Exit Smart Menu
                    Console.WriteLine("Exiting Smart Menu ...");
                    screen.TVStatus();
                    break;
                default: // Invalid Command
                    Console.WriteLine("Invalid Command. Please try again.");
                    SmartMenu();
                    break;
            }

        }

        public void Settings() // Settings Menu
        {
            screen.SettingStatus();
            Console.WriteLine("Settings Menu"); // Settings Menu
            Console.WriteLine("   1 - Picture Mode");
            Console.WriteLine("   2 - Sound Mode");
            Console.WriteLine("   3 - Network");
            Console.WriteLine("   4 - Model Selection");
            Console.WriteLine("   5 - Input Mode");
            Console.WriteLine("   0 - Exit");
            Console.WriteLine("\nEnter a command:");

            string settingscmd = Console.ReadLine()!;
            Console.WriteLine();
            screen.SettingStatus();

            switch (settingscmd) // Settings Menu
            {
                case "1": // Picture Mode
                    screen.SettingStatus();
                    Console.WriteLine("Opening Picture Mode ..."); // Picture Mode Settings Menu
                    Delay.delay();
                    Console.WriteLine("");
                    Console.WriteLine("Picture Mode Settings");
                    Console.WriteLine("   1 - Standard");
                    Console.WriteLine("   2 - Dynamic");
                    Console.WriteLine("   3 - Natural");
                    Console.WriteLine("   4 - Movie");
                    Console.WriteLine("   5 - Game");
                    Console.WriteLine("   0 - Exit");
                    Console.WriteLine("\nEnter a command:");

                    string picmodecmd = Console.ReadLine()!;
                    Console.WriteLine();

                    switch (picmodecmd) // Picture Mode Settings
                    {
                        case "1": // Standard Picture Mode
                            Console.WriteLine("Setting Picture Mode to Standard ...\n");
                            if (this.screen.PictureMode == "Standard")
                            {
                                Console.WriteLine("Picture Mode is already set to Standard.");
                            }
                            else
                            {
                                this.screen.PictureMode = "Standard";
                                Console.WriteLine("Picture Mode set to Standard.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "2": // Dynamic Picture Mode
                            Console.WriteLine("Setting Picture Mode to Dynamic ...\n");
                            if (this.screen.PictureMode == "Dynamic")
                            {
                                Console.WriteLine("Picture Mode is already set to Dynamic.");
                            }
                            else
                            {
                                this.screen.PictureMode = "Dynamic";
                                Console.WriteLine("Picture Mode set to Dynamic.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "3": // Natural Picture Mode
                            Console.WriteLine("Setting Picture Mode to Natural ...\n");
                            if (this.screen.PictureMode == "Natural")
                            {
                                Console.WriteLine("Picture Mode is already set to Natural.");
                            }
                            else
                            {
                                this.screen.PictureMode = "Natural";
                                Console.WriteLine("Picture Mode set to Natural.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "4": // Movie Picture Mode
                            Console.WriteLine("Setting Picture Mode to Movie ...\n");
                            if (this.screen.PictureMode == "Movie")
                            {
                                Console.WriteLine("Picture Mode is already set to Movie.");
                            }
                            else
                            {
                                this.screen.PictureMode = "Movie";
                                Console.WriteLine("Picture Mode set to Movie.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "5": // Game Picture Mode
                            Console.WriteLine("Setting Picture Mode to Game ...\n");
                            if (this.screen.PictureMode == "Game") // Check if picture mode is already set
                            {
                                Console.WriteLine("Picture Mode is already set to Game.");
                            }
                            else
                            {
                                this.screen.PictureMode = "Game";
                                Console.WriteLine("Picture Mode set to Game.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "0": // Exit Picture Mode
                            Console.WriteLine("Exiting Settings ...");
                            Console.WriteLine();
                            screen.TVStatus();
                            break;
                        default: // Invalid Command
                            Console.WriteLine("Invalid Command. Please try again.");
                            Settings();
                            break;
                    }
                    break;
                case "2": // Sound Mode
                    screen.SettingStatus();
                    Console.WriteLine("Opening Sound Mode ...");
                    Delay.delay();
                    Console.WriteLine("");
                    Console.WriteLine("Sound Mode Settings"); // Sound Mode Settings Menu
                    Console.WriteLine("   1 - Standard");
                    Console.WriteLine("   2 - Music");
                    Console.WriteLine("   3 - Movie");
                    Console.WriteLine("   4 - Clear Voice");
                    Console.WriteLine("   5 - Amplify");
                    Console.WriteLine("   0 - Exit");
                    Console.WriteLine("\nEnter a command:");

                    string soundmodecmd = Console.ReadLine()!;
                    Console.WriteLine();

                    switch (soundmodecmd) // Sound Mode Settings
                    {
                        case "1": // Standard Sound Mode
                            Console.WriteLine("Setting Sound Mode to Standard ...\n");
                            if (this.screen.SoundMode == "Standard") // Check if sound mode is already set
                            {
                                Console.WriteLine("Sound Mode is already set to Standard.");
                            }
                            else
                            {
                                this.screen.SoundMode = "Standard";
                                Console.WriteLine("Sound Mode set to Standard.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "2": // Music Sound Mode
                            Console.WriteLine("Setting Sound Mode to Music ...\n");
                            if (this.screen.SoundMode == "Music") // Check if sound mode is already set
                            {
                                Console.WriteLine("Sound Mode is already set to Music.");
                            }
                            else
                            {
                                this.screen.SoundMode = "Music";
                                Console.WriteLine("Sound Mode set to Music.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "3": // Movie Sound Mode
                            Console.WriteLine("Setting Sound Mode to Movie ...\n");
                            if (this.screen.SoundMode == "Movie") // Check if sound mode is already set
                            {
                                Console.WriteLine("Sound Mode is already set to Movie.");
                            }
                            else
                            {
                                this.screen.SoundMode = "Movie";
                                Console.WriteLine("Sound Mode set to Movie.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "4": // Clear Voice Sound Mode
                            Console.WriteLine("Setting Sound Mode to Clear Voice ...\n");
                            if (this.screen.SoundMode == "Clear Voice") // Check if sound mode is already set
                            {
                                Console.WriteLine("Sound Mode is already set to Clear Voice.");
                            }
                            else
                            {
                                this.screen.SoundMode = "Clear Voice";
                                Console.WriteLine("Sound Mode set to Clear Voice.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "5": // Amplify Sound Mode
                            Console.WriteLine("Setting Sound Mode to Amplify ...\n");
                            if (this.screen.SoundMode == "Amplify") // Check if sound mode is already set
                            {
                                Console.WriteLine("Sound Mode is already set to Amplify.");
                            }
                            else
                            {
                                this.screen.SoundMode = "Amplify";
                                Console.WriteLine("Sound Mode set to Amplify.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "0": // Exit Sound Mode
                            Console.WriteLine("Exiting Sound Mode ...");
                            Settings();
                            break;
                        default: // Invalid Command
                            Console.WriteLine("Invalid Command. Please try again.");
                            Settings();
                            break;
                    }
                    break;
                case "3": // Network Settings
                    screen.SettingStatus();
                    Console.WriteLine("Opening Network ...");
                    Delay.delay();
                    Console.WriteLine("");
                    Console.WriteLine("Network Settings"); // Network Settings Menu
                    Console.WriteLine("   1 - Wi-Fi");
                    Console.WriteLine("   2 - Bluetooth");
                    Console.WriteLine("   0 - Exit");
                    Console.WriteLine("\nEnter a command:");

                    string networkcmd = Console.ReadLine()!;
                    Console.WriteLine();

                    switch (networkcmd) // Network Settings
                    {
                        case "1": // Wi-Fi Settings
                            screen.SettingStatus();
                            Console.WriteLine("Wi-Fi Settings"); // Wi-Fi Settings Menu
                            Console.WriteLine("   1 - Connect to Wi-Fi");
                            Console.WriteLine("   2 - Disconnect from Wi-Fi");
                            Console.WriteLine("   0 - Exit");
                            Console.WriteLine("\nEnter a command:");

                            string wificmd = Console.ReadLine()!;
                            Console.WriteLine();

                            switch (wificmd) // Wi-Fi Settings
                            {
                                case "1": // Connect Wi-Fi
                                    Console.WriteLine("Connecting to Wi-Fi ...\n");
                                    if (this.screen.Wifi == "Connected") // Check if Wi-Fi is already connected
                                    {
                                        Console.WriteLine("Wi-Fi is already connected.");
                                    }
                                    else
                                    {
                                        this.screen.Wifi = "Connected";
                                        Console.WriteLine("Wi-Fi connected.");
                                    }
                                    Delay.delay();
                                    Settings();
                                    break;
                                case "2": // Disconnect Wi-Fi
                                    Console.WriteLine("Disconnecting from Wi-Fi ...\n");
                                    if (this.screen.Wifi == "Disconnected") // Check if Wi-Fi is already disconnected
                                    {
                                        Console.WriteLine("Wi-Fi is already disconnected.");
                                    }
                                    else
                                    {
                                        this.screen.Wifi = "Disconnected";
                                        Console.WriteLine("Wi-Fi disconnected.");
                                    }
                                    Delay.delay();
                                    Settings();
                                    break;
                                case "0": // Exit Wi-Fi
                                    Console.WriteLine("Exiting Settings ...");
                                    Console.WriteLine();
                                    Settings();
                                    break;
                                default: // Invalid Command
                                    Console.WriteLine("Invalid Command. Please try again.");
                                    Settings();
                                    break;
                            }
                            break;
                        case "2": // Bluetooth Settings
                            screen.SettingStatus();
                            Console.WriteLine("Bluetooth Settings"); // Bluetooth Settings
                            Console.WriteLine("   1 - Connect to Bluetooth");
                            Console.WriteLine("   2 - Disconnect from Bluetooth");
                            Console.WriteLine("   0 - Exit");
                            Console.WriteLine("\nEnter a command:");

                            string btcmd = Console.ReadLine()!;
                            Console.WriteLine();

                            switch (btcmd) // Bluetooth Settings
                            {
                                case "1": // Connect Bluetooth
                                    Console.WriteLine("Connecting to Bluetooth ...");
                                    if (this.screen.Bluetooth == "Connected") // Check if Bluetooth is already connected
                                    {
                                        Console.WriteLine("Bluetooth is already connected.");
                                    }
                                    else
                                    {
                                        this.screen.Bluetooth = "Connected";
                                        Console.WriteLine("Bluetooth connected.");
                                    }
                                    Delay.delay();
                                    Settings();
                                    break;
                                case "2": // Disconnect Bluetooth
                                    Console.WriteLine("Disconnecting from Bluetooth ...");
                                    if (this.screen.Bluetooth == "Disconnected") // Check if Bluetooth is already disconnected
                                    {
                                        Console.WriteLine("Bluetooth is already disconnected.");
                                    }
                                    else
                                    {
                                        this.screen.Bluetooth = "Disconnected";
                                        Console.WriteLine("Bluetooth disconnected.");
                                    }
                                    Delay.delay();
                                    Settings();
                                    break;
                                case "0": // Exit Bluetooth
                                    Console.WriteLine("Exiting Settings ...");
                                    Settings();
                                    break;
                                default: // Invalid Command
                                    Console.WriteLine("Invalid Command. Please try again.");
                                    Settings();
                                    break;
                            }
                            break;
                        case "0": // Exit Network
                            Console.WriteLine("Exiting Network ...");
                            Settings();
                            break;
                        default: // Invalid Command
                            Console.WriteLine("Invalid Command. Please try again.");
                            Settings();
                            break;
                    }
                    break;
                case "4": // Model Selection
                    screen.SettingStatus();
                    Console.WriteLine($"Current Model: {this.screen.Model}\n");
                    Console.WriteLine("Model Selection"); // Model Selection
                    Console.WriteLine("   1 - 75TU7000");
                    Console.WriteLine("   2 - 70TU7000");
                    Console.WriteLine("   3 - 65TU7000");
                    Console.WriteLine("   4 - 58TU7000");
                    Console.WriteLine("   5 - 55TU7000");
                    Console.WriteLine("   6 - 50TU7000");
                    Console.WriteLine("   7 - 43TU7000");
                    Console.WriteLine("   0 - Exit");
                    Console.WriteLine("\nEnter a command:");

                    string modelcmd = Console.ReadLine()!;
                    Console.WriteLine();

                    switch (modelcmd) // Model Selection
                    {
                        case "1": // Model 75TU7000
                            Console.WriteLine("Setting Model to 75TU7000 ...");
                            if (this.screen.Model == "75TU7000") // Check if model is already set
                            {
                                Console.WriteLine("Model is already set to 75TU7000.");
                            }
                            else
                            {
                                this.screen.Model = "75TU7000";
                                Console.WriteLine("Model set to 75TU7000.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "2": // Model 70TU7000
                            Console.WriteLine("Setting Model to 70TU7000 ...");
                            if (this.screen.Model == "70TU7000") // Check if model is already set
                            {
                                Console.WriteLine("Model is already set to 70TU7000.");
                            }
                            else
                            {
                                this.screen.Model = "70TU7000";
                                Console.WriteLine("Model set to 70TU7000.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "3": // Model 65TU7000
                            Console.WriteLine("Setting Model to 65TU7000 ...");
                            if (this.screen.Model == "65TU7000") // Check if model is already set
                            {
                                Console.WriteLine("Model is already set to 65TU7000.");
                            }
                            else
                            {
                                this.screen.Model = "65TU7000";
                                Console.WriteLine("Model set to 65TU7000.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "4": // Model 58TU7000
                            Console.WriteLine("Setting Model to 58TU7000 ...");
                            if (this.screen.Model == "58TU7000") // Check if model is already set
                            {
                                Console.WriteLine("Model is already set to 58TU7000.");
                            }
                            else
                            {
                                this.screen.Model = "58TU7000";
                                Console.WriteLine("Model set to 58TU7000.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "5": // Model 55TU7000
                            Console.WriteLine("Setting Model to UN55TU7000 ...");
                            if (this.screen.Model == "55TU7000") // Check if model is already set
                            {
                                Console.WriteLine("Model is already set to 55TU7000.");
                            }
                            else
                            {
                                this.screen.Model = "55TU7000";
                                Console.WriteLine("Model set to 55TU7000.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "6": // Model 50TU7000
                            Console.WriteLine("Setting Model to UN50TU7000 ...");
                            if (this.screen.Model == "50TU7000")
                            {
                                Console.WriteLine("Model is already set to 50TU7000."); // Check if model is already set
                            }
                            else
                            {
                                this.screen.Model = "50TU7000";
                                Console.WriteLine("Model set to 50TU7000.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "7": // Model 43TU7000
                            Console.WriteLine("Setting Model to UN43TU7000 ...");
                            if (this.screen.Model == "43TU7000") // Check if model is already set
                            {
                                Console.WriteLine("Model is already set to 43TU7000.");
                            }
                            else
                            {
                                this.screen.Model = "43TU7000";
                                Console.WriteLine("Model set to 43TU7000.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "0": // Exit Settings
                            Console.WriteLine("Exiting Model Selection ...");
                            Settings();
                            break;
                        default: // Invalid Command
                            Console.WriteLine("Invalid Command. Please try again.");
                            Settings();
                            break;
                    }
                    break;
                case "5": // Input Mode
                    screen.SettingStatus();
                    Console.WriteLine("Opening Input Mode ...");
                    Delay.delay();
                    Console.WriteLine("");
                    Console.WriteLine("Input Mode Settings"); // Input Mode Settings Menu
                    Console.WriteLine("   1 - HDMI 1");
                    Console.WriteLine("   2 - HDMI 2");
                    Console.WriteLine("   3 - USB");
                    Console.WriteLine("   0 - Exit");
                    Console.WriteLine("\nEnter a command:");

                    string inputcmd = Console.ReadLine()!;
                    Console.WriteLine();

                    switch (inputcmd) // Input Mode Settings
                    {
                        case "1": // HDMI 1
                            Console.WriteLine("Setting Input Mode to HDMI 1 ...");
                            if (this.screen.InputMode == "HDMI 1") // Check if input mode is already set
                            {
                                Console.WriteLine("Input Mode is already set to HDMI 1.");
                            }
                            else
                            {
                                this.screen.InputMode = "HDMI 1";
                                Console.WriteLine("Input Mode set to HDMI 1.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "2": // HDMI 2
                            Console.WriteLine("Setting Input Mode to HDMI 2 ...");
                            if (this.screen.InputMode == "HDMI 2") // Check if input mode is already set
                            {
                                Console.WriteLine("Input Mode is already set to HDMI 2.");
                            }
                            else
                            {
                                this.screen.InputMode = "HDMI 2";
                                Console.WriteLine("Input Mode set to HDMI 2.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "3": // USB
                            Console.WriteLine("Setting Input Mode to USB ...");
                            if (this.screen.InputMode == "USB") // Check if input mode is already set
                            {
                                Console.WriteLine("Input Mode is already set to USB.");
                            }
                            else
                            {
                                this.screen.InputMode = "USB";
                                Console.WriteLine("Input Mode set to USB.");
                            }
                            Delay.delay();
                            Settings();
                            break;
                        case "0": // Exit Input Mode
                            Console.WriteLine("Exiting Input Mode ...");
                            Settings();
                            break;
                        default: // Invalid Command
                            Console.WriteLine("Invalid Command. Please try again.");
                            Settings();
                            break;
                    }
                    break;
                case "0": // Exit Settings
                    Console.WriteLine("Exiting Settings ...");
                    screen.TVStatus();
                    break;
                default: // Invalid Command
                    Console.WriteLine("Invalid Command. Please try again.");
                    screen.TVStatus();
                    break;
            }
        }
    }
    public class Screen // TV Screen
    {
        public PowerStatus Power { get; set; } = PowerStatus.Off; // Default is Off
        public string Model { get; set; } = "75TU7000"; // Default is 75TU7000
        public int Volume { get; set; } = 10; // Default is Volume 10
        public int Channel { get; set; } = 1; // Default is Channel 1
        public MuteStatus Mute { get; set; } = MuteStatus.Unmuted; // Default is Unmuted
        public string PictureMode { get; set; } = "Standard"; // Default is Standard
        public string SoundMode { get; set; } = "Standard"; // Default is Standard
        public string Wifi { get; set; } = "Connected"; // Default is connected
        public string Bluetooth { get; set; } = "Connected"; // Default is connected
        public string InputMode { get; set; } = "HDMI 1"; // Default is HDMI 1


        public void TVStatus() // TV Status
        {
            Delay.delay();
            Console.Clear();
            Console.WriteLine("TV Status:");
            Console.WriteLine($"   TV Power: {Power}");
            Console.WriteLine($"   Model: {Model}");
            Console.WriteLine($"   Volume: {Volume}");
            Console.WriteLine($"   Channel: {Remote.DisplayChannelList(Channel)}");
            Console.WriteLine($"   Mute: {Mute}");
            Console.WriteLine();
        }

        public void SettingStatus() // Settings Status
        {
            Delay.delay();
            Console.Clear();
            Console.WriteLine("Settings Status:");
            Console.WriteLine($"   Picture Mode: {PictureMode}");
            Console.WriteLine($"   Sound Mode: {SoundMode}");
            Console.WriteLine($"   Wifi: {Wifi}");
            Console.WriteLine($"   Bluetooth: {Bluetooth}");
            Console.WriteLine($"   Model Selection: {Model}");
            Console.WriteLine($"   Input Mode: {InputMode}");
            Console.WriteLine();
        }
    }

    public class Program // Main Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the TV Remote Control Program (Remote Model: TM-1240A)"); // Welcome Message
            Console.WriteLine("\nBooting up ...");

            Delay.delay(200);
            Console.WriteLine("\n           ...");
            Delay.delay(200);
            Console.WriteLine("\n           ...");
            Delay.delay(5);

            Screen screen = new Screen();
            Remote remote = new Remote(screen);
            bool exit = false;
            screen.TVStatus();


            while (!exit)
            {
                Delay.delay(5);
                Console.WriteLine("TV Remote Control Commands:"); // TV Remote Menu
                Console.WriteLine("   1 - Power On / Off");
                Console.WriteLine("   2 - Increase Volume");
                Console.WriteLine("   3 - Decrease Volume");
                Console.WriteLine("   4 - Increase Channel");
                Console.WriteLine("   5 - Decrease Channel");
                Console.WriteLine("   6 - Mute/Unmute");
                Console.WriteLine("   7 - Change Channel");
                Console.WriteLine("   8 - Open Smart Menu");
                Console.WriteLine("   9 - Open Settings");
                Console.WriteLine("   0 - Exit");
                Console.WriteLine("\nEnter a command:");

                string command = Console.ReadLine()!;
                Console.WriteLine();

                switch (command) // Remote Control Commands
                {
                    case "1": // Power On/Off
                        remote.Power();
                        Console.WriteLine();
                        screen.TVStatus();
                        break;
                    case "2": // Increase Volume
                        remote.Volume(1);
                        Console.WriteLine();
                        screen.TVStatus();
                        break;
                    case "3": // Decrease Volume
                        remote.Volume(0);
                        Console.WriteLine();
                        screen.TVStatus();
                        break;
                    case "4": // Increase Channel
                        remote.Channel(0);
                        Console.WriteLine();
                        screen.TVStatus();
                        break;
                    case "5": // Decrease Channel
                        remote.Channel(-1);
                        Console.WriteLine();
                        screen.TVStatus();
                        break;
                    case "6": // Mute/Unmute
                        remote.Mute();
                        Console.WriteLine();
                        screen.TVStatus();
                        break;
                    case "7": // Change Channel
                        if (screen.Power == PowerStatus.Off)
                        {
                            Console.WriteLine("TV is Off. Unable to change Channel.");
                            Delay.delay();
                            screen.TVStatus();
                            break;
                        }
                        // Show channel list
                        Console.Clear();
                        for (int i = 1; i <= 55; i++)
                        {
                            Console.Write(Remote.DisplayChannelList(i));
                            if (i % 4 == 0)
                            {
                                Console.WriteLine();
                                Delay.delay(15);
                            }
                            else
                            {
                                Console.Write("  ||  ");
                            }
                        }
                        Console.WriteLine("\n\nEnter the channel number:");
                        int newChannel;
                        if (int.TryParse(Console.ReadLine(), out newChannel))
                        {
                            if (newChannel < 1 || newChannel > 55)
                            {
                                Console.WriteLine("Invalid Channel Number. Please enter a number between 1 and 55.");
                                Delay.delay();
                                screen.TVStatus();
                                break;
                            }
                            remote.Channel(newChannel);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Channel Number");
                        }
                        Console.WriteLine();
                        screen.TVStatus();
                        break;
                    case "8": // Open Smart Menu
                        if (screen.Power == PowerStatus.Off)
                        {
                            Console.WriteLine("TV is Off. Unable to open Smart Menu.");
                            Delay.delay();
                            screen.TVStatus();
                            break;
                        }
                        remote.SmartMenu();
                        break;
                    case "9": // Open Settings
                        if (screen.Power == PowerStatus.Off)
                        {
                            Console.WriteLine("TV is Off. Unable to open Settings.");
                            Delay.delay();
                            screen.TVStatus();
                            break;
                        }
                        remote.Settings();
                        break;
                    case "0": // Exit Program
                        screen.TVStatus();
                        Console.WriteLine("Exiting Program ...\n");
                        exit = true;
                        Console.WriteLine("Goodbye!\n");
                        Delay.delay();
                        Console.Clear();
                        System.Environment.Exit(0);
                        break;
                    default: // Invalid Command
                        screen.TVStatus();
                        Console.WriteLine("Invalid Command. Please try again.");
                        screen.TVStatus();
                        break;
                }
            }
        }
    }
} // End of Project 1