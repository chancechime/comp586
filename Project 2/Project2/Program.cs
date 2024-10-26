/*
    * Project 2: Remote Control
    * Student: Chance Chime
*/
using System;

namespace Project2
{
    public enum ChannelDirectory
    {
        News, Sports, Movies, Music, Kids,
        Food, Shopping, Travel, Education, Lifestyle,
        Technology, Gaming, Fitness, Religion, Weather,
        Documentary, History, Science, Nature, Fashion,
        Comedy, Drama, Reality, TalkShow, GameShow,
        SoapOpera, Sitcom, Horror, Thriller, Action,
        Adventure, Mystery, Romance, Western, War,
        Crime, Legal, Medical, Police, Military,
        Espionage, Superhero, Fantasy, ScienceFiction, Animation,
        Family, Musical, Dance, Theatre, Variety,
        Awards, Cooking, Canada, Overseas, Northridge
    }

    // Remote class - Part of the Facade Pattern
    public class Remote// Base - Remote
    {
        public void Power(string powered)
        {
            Console.WriteLine($"TV Power: {powered}");
        }

        public void Volume(string volchange, Screen _screen)
        {
            string _volumeChange = volchange;
            switch (_volumeChange)
            {
                case "up" when _screen.Volume < 20: // Increase Volume
                    _screen.Volume++;
                    Console.WriteLine($"Volume: {_screen.Volume}|20 {(_screen.Mute ? "(MUTED)" : "")}");
                    System.Threading.Thread.Sleep(500);
                    break;
                case "up" when _screen.Volume >= 20: // Volume is at max level
                    Console.WriteLine($"Volume is at max level. (20|20) {(_screen.Mute ? "(MUTED)" : "")}");
                    System.Threading.Thread.Sleep(500);
                    break;
                case "down" when _screen.Volume > 0: // Decrease Volume
                    _screen.Volume--;
                    Console.WriteLine($"Volume: {_screen.Volume}|20 {(_screen.Mute ? "(MUTED)" : "")}");
                    System.Threading.Thread.Sleep(500);
                    break;
                case "down" when _screen.Volume <= 0: // Volume is at min level
                    Console.WriteLine($"Volume is at min level. (0|20) {(_screen.Mute ? "(MUTED)" : "")}");
                    System.Threading.Thread.Sleep(500);
                    break;
                default: // Invalid Volume Adjustment
                    Console.WriteLine("Invalid volume adjustment.");
                    System.Threading.Thread.Sleep(500);
                    break;
            }
        }

        public void Channel(Remote _remote, Screen _screen, string chgchannel)
        {
            string _channelChange = chgchannel;
            switch (_channelChange)
            {
                case "up": // Increase Channel
                    _screen.Channel++;
                    if (_screen.Channel == 56) { _screen.Channel = 1; }
                    break;
                case "down": // Decrease Channel
                    _screen.Channel--;
                    if (_screen.Channel == 0) { _screen.Channel = 55; }
                    break;
                case "directory": // Channel Directory
                    for (int i = 1; i <= 55; i++)
                    {
                        int k = i - 1;
                        Console.Write($"{i} - {Enum.GetName(typeof(ChannelDirectory), k)}");
                        if (i % 5 == 0)
                        {
                            Console.WriteLine();
                            System.Threading.Thread.Sleep(50);
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
                            System.Threading.Thread.Sleep(100);
                            Console.Clear();
                            Console.WriteLine("Invalid Channel Number. Please enter a number between 1 and 55.");
                            System.Threading.Thread.Sleep(1000);
                            _screen.PrintStatus();
                            return;
                        }
                        _screen.Channel = newChannel;
                    }
                    Console.Clear();
                    break;
                default: // Change to specific channel
                    if (int.TryParse(_channelChange, out int channelNumber))
                    {
                        _screen.Channel = Math.Clamp(channelNumber, 1, 55);
                    }
                    else
                    {
                        Console.WriteLine("Invalid channel number.");
                    }
                    break;
            }

            var listedchannel = _screen.Channel;
            var channelName = Enum.GetName(typeof(ChannelDirectory), listedchannel - 1);
            Console.WriteLine($"You are now watching Channel: {_screen.Channel} | {channelName}");
            System.Threading.Thread.Sleep(800);
        }

        public void Mute(Screen _screen)
        {
            _screen.Mute = !_screen.Mute;
            string muted = _screen.Mute ? "MUTED" : "UNMUTED";
            Console.WriteLine($"TV has been {muted}.");
            System.Threading.Thread.Sleep(500);
        }

        public void SmartMenu(Remote remote, Screen screen)
        {
            screen.SmartMenu = !screen.SmartMenu;
            new TVFacade(remote, screen).SmartMenu();
            System.Threading.Thread.Sleep(1000);
        }

        public void Settings(Remote remote, Screen screen)
        {
            screen.Settings = !screen.Settings;
            new TVFacade(remote, screen).Settings();
            Console.WriteLine($"TV Settings: {screen.Settings}");
        }

        public void Exit(Screen screen)
        {
            screen.Exit = true;
            Console.Clear();
            System.Threading.Thread.Sleep(600);
            Console.WriteLine("Exiting...\n");
            Environment.Exit(0);
        }
    }

    // Screen class - Part of the Facade Pattern
    // Base class for Screen
    public class Screen // Base - Screen
    {
        //Status
        public string Model { get; set; } = "75TU7000";
        public bool Power { get; set; } = false;
        public int Volume { get; set; } = 10;
        public int Channel { get; set; } = 1;
        public bool Mute { get; set; } = false;
        public bool SmartMenu { get; set; } = false;
        public bool Settings { get; set; } = false;
        public bool Exit { get; set; } = false;

        public void PrintStatus()
        {
            Console.WriteLine("TV Status:");
            Console.WriteLine($"   Model: {Model}");
            Console.WriteLine($"   Power: {Power}");
            Console.WriteLine($"   Volume: {Volume}");
            Console.WriteLine($"   Channel: {Channel} | {Enum.GetName(typeof(ChannelDirectory), Channel - 1)}");
            Console.WriteLine($"   Mute: {Mute}");
            Console.WriteLine();
            Console.WriteLine();
        }

        // Settings
        public string PictureMode { get; set; } = "Standard";
        public string SoundMode { get; set; } = "Standard";
        public string Wifi { get; set; } = "Connected";
        public string Bluetooth { get; set; } = "Connected";
        public string InputMode { get; set; } = "HDMI 1";
    }

    // Factory class for Screen
    public interface IScreenFactory
    {
        string Model();
        bool Power();
        int Volume();
        int Channel();
        bool Mute();
        bool SmartMenu();
        bool Settings();
        bool Exit();

        string PictureMode();
        string SoundMode();
        string Wifi();
        string Bluetooth();
        string InputMode();
    }

    class Model75TU7000 : IScreenFactory
    {
        public string Model() => "75TU7000";
        public bool Power() => false;
        public int Volume() => 10;
        public int Channel() => 1;
        public bool Mute() => false;
        public bool SmartMenu() => false;
        public bool Settings() => false;
        public bool Exit() => false;
        public string PictureMode() => "Standard";
        public string SoundMode() => "Standard";
        public string Wifi() => "Connected";
        public string Bluetooth() => "Connected";
        public string InputMode() => "HDMI 1";
    }

    class Model70TU7000 : IScreenFactory
    {
        public string Model() => "70TU7000";
        public bool Power() => false;
        public int Volume() => 10;
        public int Channel() => 1;
        public bool Mute() => false;
        public bool SmartMenu() => false;
        public bool Settings() => false;
        public bool Exit() => false;
        public string PictureMode() => "Standard";
        public string SoundMode() => "Standard";
        public string Wifi() => "Connected";
        public string Bluetooth() => "Connected";
        public string InputMode() => "HDMI 1";
    }

    class Model65TU7000 : IScreenFactory
    {
        public string Model() => "65TU7000";
        public bool Power() => false;
        public int Volume() => 10;
        public int Channel() => 1;
        public bool Mute() => false;
        public bool SmartMenu() => false;
        public bool Settings() => false;
        public bool Exit() => false;
        public string PictureMode() => "Standard";
        public string SoundMode() => "Standard";
        public string Wifi() => "Connected";
        public string Bluetooth() => "Connected";
        public string InputMode() => "HDMI 1";
    }

    class Model58TU7000 : IScreenFactory
    {
        public string Model() => "58TU7000";
        public bool Power() => false;
        public int Volume() => 10;
        public int Channel() => 1;
        public bool Mute() => false;
        public bool SmartMenu() => false;
        public bool Settings() => false;
        public bool Exit() => false;
        public string PictureMode() => "Standard";
        public string SoundMode() => "Standard";
        public string Wifi() => "Connected";
        public string Bluetooth() => "Connected";
        public string InputMode() => "HDMI 1";
    }

    class Model55TU7000 : IScreenFactory
    {
        public string Model() => "55TU7000";
        public bool Power() => false;
        public int Volume() => 10;
        public int Channel() => 1;
        public bool Mute() => false;
        public bool SmartMenu() => false;
        public bool Settings() => false;
        public bool Exit() => false;
        public string PictureMode() => "Standard";
        public string SoundMode() => "Standard";
        public string Wifi() => "Connected";
        public string Bluetooth() => "Connected";
        public string InputMode() => "HDMI 1";
    }

    class Model50TU7000 : IScreenFactory
    {
        public string Model() => "50TU7000";
        public bool Power() => false;
        public int Volume() => 10;
        public int Channel() => 1;
        public bool Mute() => false;
        public bool SmartMenu() => false;
        public bool Settings() => false;
        public bool Exit() => false;
        public string PictureMode() => "Standard";
        public string SoundMode() => "Standard";
        public string Wifi() => "Connected";
        public string Bluetooth() => "Connected";
        public string InputMode() => "HDMI 1";
    }

    class Model43TU7000 : IScreenFactory
    {
        public string Model() => "43TU7000";
        public bool Power() => false;
        public int Volume() => 10;
        public int Channel() => 1;
        public bool Mute() => false;
        public bool SmartMenu() => false;
        public bool Settings() => false;
        public bool Exit() => false;
        public string PictureMode() => "Standard";
        public string SoundMode() => "Standard";
        public string Wifi() => "Connected";
        public string Bluetooth() => "Connected";
        public string InputMode() => "HDMI 1";
    }
    class ScreenFactory
    {
        public static IScreenFactory getScreen(string type)
        {
            IScreenFactory? screen = null;
            switch (type.ToLower())
            {
                case "75tu7000":
                    screen = new Model75TU7000();
                    break;
                case "70tu7000":
                    screen = new Model70TU7000();
                    break;
                case "65tu7000":
                    screen = new Model65TU7000();
                    break;
                case "58tu7000":
                    screen = new Model58TU7000();
                    break;
                case "55tu7000":
                    screen = new Model55TU7000();
                    break;
                case "50tu7000":
                    screen = new Model50TU7000();
                    break;
                case "43tu7000":
                    screen = new Model43TU7000();
                    break;
                default:
                    throw new ArgumentException("Invalid screen type");
            }
            return screen;
        }
    }

    // RemoteCommand class - Command Pattern
    public class RemoteCommand // Command - Remote (Actions that the Remote can perform)
    {
        public interface ICommand
        {
            void Execute();
        }

        // PowerCommand class - Command Pattern
        public class PowerCommand : ICommand
        {
            private Remote _remote;
            private Screen _screen;

            public PowerCommand(Remote remote, Screen screen)
            {
                _remote = remote;
                _screen = screen;
            }

            public void Execute()
            {
                _screen.Power = !_screen.Power;
                string powered = _screen.Power ? "POWERED ON" : "POWERED OFF";
                _remote.Power(powered);
                System.Threading.Thread.Sleep(500);
            }
        }

        // VolumeCommand class - Command Pattern
        public class VolumeCommand : ICommand
        {
            private Remote _remote;
            private Screen _screen;
            private string _volumeChange;

            public VolumeCommand(Remote remote, Screen screen, string volumeChange)
            {
                _remote = remote;
                _screen = screen;
                _volumeChange = volumeChange;
            }

            public void Execute()
            {
                if (_screen.Power)
                {
                    _remote.Volume(_volumeChange, _screen);
                }
                else
                {
                    Console.WriteLine("TV is Off. Unable to adjust Volume.");
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        // ChannelCommand class - Command Pattern
        public class ChannelCommand : ICommand
        {
            private Remote _remote;
            private Screen _screen;
            private string _channelChange;

            public ChannelCommand(Remote remote, Screen screen, string channelChange)
            {
                _remote = remote;
                _screen = screen;
                _channelChange = channelChange;
            }

            public void Execute()
            {
                Console.Clear();
                if (_screen.Power)
                {
                    _remote.Channel(_remote, _screen, _channelChange);
                }
                else
                {
                    Console.WriteLine("TV is Off. Unable to change Channel.");
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        public class MuteCommand : ICommand
        {
            private Remote _remote;
            private Screen _screen;

            public MuteCommand(Remote remote, Screen screen)
            {
                _remote = remote;
                _screen = screen;
            }

            public void Execute()
            {
                if (_screen.Power)
                {
                    _remote.Mute(_screen);
                }
                else
                {
                    Console.WriteLine("TV is Off. Unable to Mute.");
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        // SmartMenuCommand class - Command Pattern
        public class SmartMenuCommand : ICommand
        {
            private Remote _remote;
            private Screen _screen;

            public SmartMenuCommand(Remote remote, Screen screen)
            {
                _remote = remote;
                _screen = screen;
            }

            public void Execute()
            {
                if (_screen.Power)
                {
                    _remote.SmartMenu(_remote, _screen);
                }
                else
                {
                    Console.WriteLine("TV is Off. Unable to open Smart Menu.");
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        // SettingsCommand class - Command Pattern
        public class SettingsCommand : ICommand
        {
            private Remote _remote;
            private Screen _screen;

            public SettingsCommand(Remote remote, Screen screen)
            {
                _remote = remote;
                _screen = screen;
            }

            public void Execute()
            {
                if (_screen.Power)
                {
                    _remote.Settings(_remote, _screen);
                }
                else
                {
                    Console.WriteLine("TV is Off. Unable to open Settings.");
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }

        // ExitCommand class - Command Pattern
        public class ExitCommand : ICommand
        {
            private Remote _remote;
            private Screen _screen;

            public ExitCommand(Remote remote, Screen screen)
            {
                _remote = remote;
                _screen = screen;
            }

            public void Execute()
            {
                _remote.Exit(_screen);
            }
        }
    }

    // TVFacade class - Facade Pattern
    public class TVFacade // Facade Pattern
    {
        private Remote _remote;
        private Screen _screen;

        public TVFacade(Remote remote, Screen screen)
        {
            _remote = remote;
            _screen = screen;
        }

        public void SmartMenu()
        {
            Console.Clear();
            System.Threading.Thread.Sleep(200);
            _screen.SmartMenu = true;
            while (_screen.SmartMenu)
            {
                Console.WriteLine("Smart Menu");
                Console.WriteLine("   1 - Apps");
                Console.WriteLine("   2 - Gallery");
                Console.WriteLine("   3 - Web Browser");
                Console.WriteLine("   4 - Screen Mirroring");
                Console.WriteLine("   5 - Voice Assistant");
                Console.WriteLine("   0 - Exit");
                Console.WriteLine("\nEnter a command:");


                string? smartcmd = Console.ReadLine();
                Console.WriteLine();

                switch (smartcmd)
                {
                    case "1":
                        Console.WriteLine("Opening Apps ...");
                        System.Threading.Thread.Sleep(500);
                        Console.WriteLine("Press any key to return to Smart Menu ...");
                        Console.ReadKey();
                        System.Threading.Thread.Sleep(500);
                        Console.WriteLine("\nClosing Apps...");
                        System.Threading.Thread.Sleep(250);
                        Console.Clear();
                        break;
                    case "2":
                        Console.WriteLine("Opening Gallery ...");
                        System.Threading.Thread.Sleep(500);
                        Console.WriteLine("Press any key to return to Smart Menu ...");
                        Console.ReadKey();
                        System.Threading.Thread.Sleep(500);
                        Console.WriteLine("\nClosing Apps...");
                        System.Threading.Thread.Sleep(250);
                        Console.Clear();
                        break;
                    case "3":
                        Console.WriteLine("Opening Web Browser ...");
                        System.Threading.Thread.Sleep(500);
                        Console.WriteLine("Press any key to return to Smart Menu ...");
                        Console.ReadKey();
                        System.Threading.Thread.Sleep(500);
                        Console.WriteLine("\nClosing Apps...");
                        System.Threading.Thread.Sleep(250);
                        Console.Clear();
                        break;
                    case "4":
                        Console.WriteLine("Opening Screen Mirroring ...");
                        System.Threading.Thread.Sleep(500);
                        Console.WriteLine("Press any key to return to Smart Menu ...");
                        Console.ReadKey();
                        System.Threading.Thread.Sleep(500);
                        Console.WriteLine("\nClosing Apps...");
                        System.Threading.Thread.Sleep(250);
                        Console.Clear();
                        break;
                    case "5":
                        Console.WriteLine("Opening Voice Assistant ...");
                        System.Threading.Thread.Sleep(500);
                        Console.WriteLine("Press any key to return to Smart Menu ...");
                        Console.ReadKey();
                        System.Threading.Thread.Sleep(500);
                        Console.WriteLine("\nClosing Apps...");
                        System.Threading.Thread.Sleep(250);
                        Console.Clear();
                        break;
                    case "0":
                        Console.WriteLine("Exiting Smart Menu ...");
                        _screen.SmartMenu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Command. Please try again.");
                        break;
                }
            }
        }

        public void Settings()
        {
            Console.Clear();
            System.Threading.Thread.Sleep(200);
            _screen.Settings = true;
            while (_screen.Settings)
            {
                Console.WriteLine("Settings Menu:");

                Console.WriteLine("\nPicture Mode");
                Console.WriteLine("   11 - Standard");
                Console.WriteLine("   12 - Dynamic");
                Console.WriteLine("   13 - Natural");
                Console.WriteLine("   14 - Movie");
                Console.WriteLine("   15 - Game");

                Console.WriteLine("\nSound Mode");
                Console.WriteLine("   21 - Standard");
                Console.WriteLine("   22 - Music");
                Console.WriteLine("   23 - Movie");
                Console.WriteLine("   24 - Clear Voice");
                Console.WriteLine("   25 - Amplify");

                Console.WriteLine("\nWi-Fi Network");
                Console.WriteLine("   31 - Connect to Wifi Network");
                Console.WriteLine("   32 - Disconnect from Wifi Network");

                Console.WriteLine("\nBluetooth");
                Console.WriteLine("   41 - Connect to Bluetooth Device");
                Console.WriteLine("   42 - Disconnect from Bluetooth Device");

                Console.WriteLine("\nInput Mode");
                Console.WriteLine("   51 - HDMI 1");
                Console.WriteLine("   52 - HDMI 2");
                Console.WriteLine("   53 - DisplayPort");
                Console.WriteLine("   54 - DVI");

                Console.WriteLine("\nModel Selection");
                Console.WriteLine("   61 - 75TU7000");
                Console.WriteLine("   62 - 70TU7000");
                Console.WriteLine("   63 - 65TU7000");
                Console.WriteLine("   64 - 58TU7000");
                Console.WriteLine("   65 - 55TU7000");
                Console.WriteLine("   66 - 50TU7000");
                Console.WriteLine("   67 - 43TU7000");

                Console.WriteLine("\n   0 - Exit");
                Console.WriteLine("\nEnter a command:");

                string? settingscmd = Console.ReadLine();
                Console.WriteLine();
                Console.Clear();

                switch (settingscmd)
                {
                    case "11":
                        Console.WriteLine("Setting Picture Mode to Standard ...\n");
                        if (_screen.PictureMode == "Standard")
                        {
                            Console.WriteLine("Picture Mode is already set to Standard.");
                        }
                        else
                        {
                            _screen.PictureMode = "Standard";
                            Console.WriteLine("Picture Mode set to Standard.");
                        }
                        break;
                    case "12":
                        Console.WriteLine("Setting Picture Mode to Dynamic ...\n");
                        if (_screen.PictureMode == "Dynamic")
                        {
                            Console.WriteLine("Picture Mode is already set to Dynamic.");
                        }
                        else
                        {
                            _screen.PictureMode = "Dynamic";
                            Console.WriteLine("Picture Mode set to Dynamic.");
                        }
                        break;
                    case "13":
                        Console.WriteLine("Setting Picture Mode to Natural ...\n");
                        if (_screen.PictureMode == "Natural")
                        {
                            Console.WriteLine("Picture Mode is already set to Natural.");
                        }
                        else
                        {
                            _screen.PictureMode = "Natural";
                            Console.WriteLine("Picture Mode set to Natural.");
                        }
                        break;
                    case "14":
                        Console.WriteLine("Setting Picture Mode to Movie ...\n");
                        if (_screen.PictureMode == "Movie")
                        {
                            Console.WriteLine("Picture Mode is already set to Movie.");
                        }
                        else
                        {
                            _screen.PictureMode = "Movie";
                            Console.WriteLine("Picture Mode set to Movie.");
                        }
                        break;
                    case "15":
                        Console.WriteLine("Setting Picture Mode to Game ...\n");
                        if (_screen.PictureMode == "Game")
                        {
                            Console.WriteLine("Picture Mode is already set to Game.");
                        }
                        else
                        {
                            _screen.PictureMode = "Game";
                            Console.WriteLine("Picture Mode set to Game.");
                        }
                        break;
                    case "21":
                        Console.WriteLine("Setting Sound Mode to Standard ...\n");
                        if (_screen.SoundMode == "Standard")
                        {
                            Console.WriteLine("Sound Mode is already set to Standard.");
                        }
                        else
                        {
                            _screen.SoundMode = "Standard";
                            Console.WriteLine("Sound Mode set to Standard.");
                        }
                        break;
                    case "22":
                        Console.WriteLine("Setting Sound Mode to Music ...\n");
                        if (_screen.SoundMode == "Music")
                        {
                            Console.WriteLine("Sound Mode is already set to Music.");
                        }
                        else
                        {
                            _screen.SoundMode = "Music";
                            Console.WriteLine("Sound Mode set to Music.");
                        }
                        break;
                    case "23":
                        Console.WriteLine("Setting Sound Mode to Movie ...\n");
                        if (_screen.SoundMode == "Movie")
                        {
                            Console.WriteLine("Sound Mode is already set to Movie.");
                        }
                        else
                        {
                            _screen.SoundMode = "Movie";
                            Console.WriteLine("Sound Mode set to Movie.");
                        }
                        break;
                    case "24":
                        Console.WriteLine("Setting Sound Mode to Clear Voice ...\n");
                        if (_screen.SoundMode == "Clear Voice")
                        {
                            Console.WriteLine("Sound Mode is already set to Clear Voice.");
                        }
                        else
                        {
                            _screen.SoundMode = "Clear Voice";
                            Console.WriteLine("Sound Mode set to Clear Voice.");
                        }
                        break;
                    case "25":
                        Console.WriteLine("Setting Sound Mode to Amplify ...\n");
                        if (_screen.SoundMode == "Amplify")
                        {
                            Console.WriteLine("Sound Mode is already set to Amplify.");
                        }
                        else
                        {
                            _screen.SoundMode = "Amplify";
                            Console.WriteLine("Sound Mode set to Amplify.");
                        }
                        break;
                    case "31":
                        Console.WriteLine("Connecting to Wifi Network ...\n");
                        if (_screen.Wifi == "Connected")
                        {
                            Console.WriteLine("Already connected to Wifi Network.");
                        }
                        else
                        {
                            _screen.Wifi = "Connected";
                            Console.WriteLine("Connected to Wifi Network.");
                        }
                        break;
                    case "32":
                        Console.WriteLine("Disconnecting from Wifi Network ...\n");
                        if (_screen.Wifi == "Disconnected")
                        {
                            Console.WriteLine("Already disconnected from Wifi Network.");
                        }
                        else
                        {
                            _screen.Wifi = "Disconnected";
                            Console.WriteLine("Disconnected from Wifi Network.");
                        }
                        break;
                    case "41":
                        Console.WriteLine("Connecting to Bluetooth Device ...\n");
                        if (_screen.Bluetooth == "Connected")
                        {
                            Console.WriteLine("Already connected to Bluetooth Device.");
                        }
                        else
                        {
                            _screen.Bluetooth = "Connected";
                            Console.WriteLine("Connected to Bluetooth Device.");
                        }
                        break;
                    case "42":
                        Console.WriteLine("Disconnecting from Bluetooth Device ...\n");
                        if (_screen.Bluetooth == "Disconnected")
                        {
                            Console.WriteLine("Already disconnected from Bluetooth Device.");
                        }
                        else
                        {
                            _screen.Bluetooth = "Disconnected";
                            Console.WriteLine("Disconnected from Bluetooth Device.");
                        }
                        break;
                    case "51":
                        Console.WriteLine("Setting Input Mode to HDMI 1 ...\n");
                        if (_screen.InputMode == "HDMI 1")
                        {
                            Console.WriteLine("Input Mode is already set to HDMI 1.");
                        }
                        else
                        {
                            _screen.InputMode = "HDMI 1";
                            Console.WriteLine("Input Mode set to HDMI 1.");
                        }
                        break;
                    case "52":
                        Console.WriteLine("Setting Input Mode to HDMI 2 ...\n");
                        if (_screen.InputMode == "HDMI 2")
                        {
                            Console.WriteLine("Input Mode is already set to HDMI 2.");
                        }
                        else
                        {
                            _screen.InputMode = "HDMI 2";
                            Console.WriteLine("Input Mode set to HDMI 2.");
                        }
                        break;
                    case "53":
                        Console.WriteLine("Setting Input Mode to DisplayPort ...\n");
                        if (_screen.InputMode == "DisplayPort")
                        {
                            Console.WriteLine("Input Mode is already set to DisplayPort.");
                        }
                        else
                        {
                            _screen.InputMode = "DisplayPort";
                            Console.WriteLine("Input Mode set to DisplayPort.");
                        }
                        break;
                    case "54":
                        Console.WriteLine("Setting Input Mode to DVI ...\n");
                        if (_screen.InputMode == "DVI")
                        {
                            Console.WriteLine("Input Mode is already set to DVI.");
                        }
                        else
                        {
                            _screen.InputMode = "DVI";
                            Console.WriteLine("Input Mode set to DVI.");
                        }
                        break;
                    case "61":
                        Console.WriteLine("Setting Model to 75TU7000 ...\n");
                        if (_screen.Model == "75TU7000")
                        {
                            Console.WriteLine("Model is already set to 75TU7000.");
                        }
                        else
                        {
                            IScreenFactory screenFactory = ScreenFactory.getScreen("75TU7000");
                            _screen.Model = screenFactory.Model();
                            Console.WriteLine("Model set to 75TU7000.");
                        }
                        break;
                    case "62":
                        Console.WriteLine("Setting Model to 70TU7000 ...\n");
                        if (_screen.Model == "70TU7000")
                        {
                            Console.WriteLine("Model is already set to 70TU7000.");
                        }
                        else
                        {
                            IScreenFactory screenFactory = ScreenFactory.getScreen("70TU7000");
                            _screen.Model = screenFactory.Model();
                            Console.WriteLine("Model set to 70TU7000.");
                        }
                        break;
                    case "63":
                        Console.WriteLine("Setting Model to 65TU7000 ...\n");
                        if (_screen.Model == "65TU7000")
                        {
                            Console.WriteLine("Model is already set to 65TU7000.");
                        }
                        else
                        {
                            IScreenFactory screenFactory = ScreenFactory.getScreen("65TU7000");
                            _screen.Model = screenFactory.Model();
                            Console.WriteLine("Model set to 65TU7000.");
                        }
                        break;
                    case "64":
                        Console.WriteLine("Setting Model to 58TU7000 ...\n");
                        if (_screen.Model == "58TU7000")
                        {
                            Console.WriteLine("Model is already set to 58TU7000.");
                        }
                        else
                        {
                            IScreenFactory screenFactory = ScreenFactory.getScreen("58TU7000");
                            _screen.Model = screenFactory.Model();
                            Console.WriteLine("Model set to 58TU7000.");
                        }
                        break;
                    case "65":
                        Console.WriteLine("Setting Model to 55TU7000 ...\n");
                        if (_screen.Model == "55TU7000")
                        {
                            Console.WriteLine("Model is already set to 55TU7000.");
                        }
                        else
                        {
                            IScreenFactory screenFactory = ScreenFactory.getScreen("55TU7000");
                            _screen.Model = screenFactory.Model();
                            Console.WriteLine("Model set to 55TU7000.");
                        }
                        break;
                    case "66":
                        Console.WriteLine("Setting Model to 50TU7000 ...\n");
                        if (_screen.Model == "50TU7000")
                        {
                            Console.WriteLine("Model is already set to 50TU7000.");
                        }
                        else
                        {
                            IScreenFactory screenFactory = ScreenFactory.getScreen("50TU7000");
                            _screen.Model = screenFactory.Model();
                            Console.WriteLine("Model set to 50TU7000.");
                        }
                        break;
                    case "67":
                        Console.WriteLine("Setting Model to 43TU7000 ...\n");
                        if (_screen.Model == "43TU7000")
                        {
                            Console.WriteLine("Model is already set to 43TU7000.");
                        }
                        else
                        {
                            IScreenFactory screenFactory = ScreenFactory.getScreen("43TU7000");
                            _screen.Model = screenFactory.Model();
                            Console.WriteLine("Model set to 43TU7000.");
                        }
                        break;
                    case "0":
                        Console.WriteLine("Exiting Settings ...\n");
                        _screen.Settings = false;
                        break;
                    default:
                        Console.WriteLine("Invalid Command. Please try again.");
                        break;
                }
                System.Threading.Thread.Sleep(500);
                Console.Clear();
            }
        }
    }

    // Program class - Main Program
    public class Program // Main Program
    {
        public static void Main(string[] args)
        {
            Screen screen = new Screen();
            Remote remote = new Remote();

            Console.WriteLine($"Welcome to the TV Remote Control Program (TV Model: {screen.Model})"); // Welcome Message
            Console.WriteLine("\nBooting up ...");

            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("\n           ...");
            System.Threading.Thread.Sleep(200);
            Console.WriteLine("\n           ...\n");
            System.Threading.Thread.Sleep(200);
            Console.Clear();

            bool exit = false;
            bool s_menu = screen.SmartMenu;
            bool smenu = screen.Settings;

            while (!exit)
            {
                RemoteCommand.ICommand? command = null;
                Console.Clear();
                System.Threading.Thread.Sleep(200);
                screen.PrintStatus();
                System.Threading.Thread.Sleep(500);
                Console.WriteLine("TV Remote Control Commands:"); // TV Remote Menu
                Console.WriteLine("   1 - Power On / Off");
                Console.WriteLine("   2 - Increase Volume");
                Console.WriteLine("   3 - Decrease Volume");
                Console.WriteLine("   4 - Increase Channel");
                Console.WriteLine("   5 - Decrease Channel");
                Console.WriteLine("   6 - Change Channel");
                Console.WriteLine("   7 - Mute/Unmute");
                Console.WriteLine("   8 - Open Smart Menu");
                Console.WriteLine("   9 - Open Settings");
                Console.WriteLine("   0 - Exit");
                Console.WriteLine("\nEnter a command:");
                string? action = Console.ReadLine();
                if (action != null)
                {
                    command = GetCommand(action, remote, screen, ref exit, ref s_menu, ref smenu);
                    command?.Execute();
                }
                else
                {
                    Console.WriteLine("Invalid Command. Please try again.");
                }
            }
        }

        private static RemoteCommand.ICommand? GetCommand(string action, Remote remote, Screen screen, ref bool exit, ref bool s_menu, ref bool smenu)
        {
            switch (action)
            {
                case "1":
                    Console.WriteLine();
                    return new RemoteCommand.PowerCommand(remote, screen);
                case "2":
                    Console.WriteLine();
                    return new RemoteCommand.VolumeCommand(remote, screen, "up");
                case "3":
                    Console.WriteLine();
                    return new RemoteCommand.VolumeCommand(remote, screen, "down");
                case "4":
                    Console.WriteLine();
                    return new RemoteCommand.ChannelCommand(remote, screen, "up");
                case "5":
                    Console.WriteLine();
                    return new RemoteCommand.ChannelCommand(remote, screen, "down");
                case "6":
                    Console.WriteLine();
                    return new RemoteCommand.ChannelCommand(remote, screen, "directory");
                case "7":
                    Console.WriteLine();
                    return new RemoteCommand.MuteCommand(remote, screen);
                case "8":
                    Console.WriteLine();
                    return new RemoteCommand.SmartMenuCommand(remote, screen);
                case "9":
                    Console.WriteLine();
                    return new RemoteCommand.SettingsCommand(remote, screen);
                case "0":
                    return new RemoteCommand.ExitCommand(remote, screen);
                default:
                    Console.WriteLine("\nInvalid Command. Please try again.");
                    return null;
            }
        }
    }
}