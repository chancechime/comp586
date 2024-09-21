using System;

namespace Project1
{
    public enum MuteStatus
    {
        Muted,
        Unmuted
    }

    public enum PowerStatus
    {
        On,
        Off
    }

    public enum ChannelDirectory
    {
        News,
        Sports,
        Movies,
        Music,
        Kids,
        Food,
        Shopping,
        Travel,
        Education,
        Lifestyle,
        Technology,
        Gaming,
        Fitness,
        Religion,
        Weather,
        Documentary,
        History,
        Science,
        Nature,
        Fashion,
        Comedy,
        Drama,
        Reality,
        TalkShow,
        GameShow,
        SoapOpera,
        Sitcom,
        Horror,
        Thriller,
        Action,
        Adventure,
        Mystery,
        Romance,
        Western,
        War,
        Crime,
        Legal,
        Medical,
        Police,
        Military,
        Espionage,
        Superhero,
        Fantasy,
        ScienceFiction,
        Animation,
        Family,
        Musical,
        Dance,
        Theatre,
        Variety,
        Awards,
        Cooking,
        Canada,
        Overseas,
        Northridge
    }

    public class Delay
    {
        public static void delay(int ms = 750)
        {
            System.Threading.Thread.Sleep(ms);
        }
    }

    public class Remote
    {
        private Screen screen;
        public Remote(Screen screen)
        {
            this.screen = screen;
        }
        public static string DisplayChannelList(int listedchannel)
        {
            var channelName = Enum.GetName(typeof(ChannelDirectory), listedchannel - 1);
            string theChannel = $"Channel {listedchannel} - {channelName}";
            return theChannel ?? "Unknown Channel";
        }
        public void Power()
        {
            this.screen.Power = this.screen.Power == PowerStatus.On ? PowerStatus.Off : PowerStatus.On;
            Console.WriteLine($"TV Power: {this.screen.Power}");
        }
        public void Volume(int vol)
        {
            if (this.screen.Power == PowerStatus.On)
            {
                switch (vol)
                {
                    case 1 when this.screen.Volume < 20:
                        this.screen.Volume++;
                        Console.WriteLine($"Volume: {this.screen.Volume}");
                        break;
                    case 1 when this.screen.Volume >= 20:
                        Console.WriteLine("Volume is at max level.");
                        break;

                    case 0 when this.screen.Volume > 0:
                        this.screen.Volume--;
                        Console.WriteLine($"Volume: {this.screen.Volume}");
                        break;
                    case 0 when this.screen.Volume <= 0:
                        Console.WriteLine("Volume is at min level.");
                        break;

                    default:
                        Console.WriteLine("Invalid volume adjustment.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("TV is Off. Unable to adjust volume.");
            }
        }
        public void Channel(int channel)
        {
            switch (channel)
            {
                case 0:
                    if (this.screen.Power == PowerStatus.On)
                    {
                        this.screen.Channel++;
                        if (this.screen.Channel == 56) { this.screen.Channel = 1; }
                        Console.WriteLine($"{DisplayChannelList(this.screen.Channel)}");
                    }
                    else
                    {
                        Console.WriteLine("TV is Off. Unable to change channel.");
                    }
                    break;
                case -1:
                    if (this.screen.Power == PowerStatus.On)
                    {
                        this.screen.Channel--;
                        if (this.screen.Channel == 0) { this.screen.Channel = 55; }
                        Console.WriteLine($"{DisplayChannelList(this.screen.Channel)}");
                    }
                    else
                    {
                        Console.WriteLine("TV is Off. Unable to change channel.");
                    }
                    break;
                default:
                    if (this.screen.Power == PowerStatus.On)
                    {
                        this.screen.Channel = channel;
                        if (this.screen.Channel < 1 || this.screen.Channel > 55)
                        {
                            Console.WriteLine("Invalid Channel Number. Please enter a number between 1 and 55.");
                            break;
                        }
                        Console.WriteLine($"{DisplayChannelList(this.screen.Channel)}");
                    }
                    else
                    {
                        Console.WriteLine("TV is Off. Unable to change channel.");
                    }
                    break;
            }
        }
        public void Mute()
        {
            if (this.screen.Power == PowerStatus.On)
            {
                this.screen.Mute = this.screen.Mute == MuteStatus.Muted ? MuteStatus.Unmuted : MuteStatus.Muted;
                Console.WriteLine($"Mute: {this.screen.Mute}");
            }
            else
            {
                Console.WriteLine("TV is Off. Unable to mute.");
            }
        }
        public void SmartMenu()
        {
            Console.WriteLine("Smart Menu is Open");
        }
        public void Settings()
        {
            Console.WriteLine("Settings Menu is Open");
        }
    }

    public class Screen
    {
        public PowerStatus Power { get; set; } = PowerStatus.Off;
        public string Model { get; set; } = "UN75TU7000";
        public int Volume { get; set; } = 10;
        public int Channel { get; set; } = 1;
        public MuteStatus Mute { get; set; } = MuteStatus.Unmuted;

        public void TVStatus()
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
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the TV Remote Control Program by Chance Chime");
            Console.WriteLine("Booting up...");
            Delay.delay(200);
            Console.WriteLine("\n   ...");
            Delay.delay(200);
            Console.WriteLine("\n   ...");
            Delay.delay(1000);
            Screen screen = new Screen();
            Remote remote = new Remote(screen);
            bool exit = false;
            screen.TVStatus();

            while (!exit)
            {
                Delay.delay(1);
                Console.WriteLine("TV Remote Control Commands:");
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

                switch (command)
                {
                    case "1":
                        remote.Power();
                        Console.WriteLine();
                        screen.TVStatus();
                        break;
                    case "2":
                        remote.Volume(1);
                        Console.WriteLine();
                        screen.TVStatus();
                        break;
                    case "3":
                        remote.Volume(0);
                        Console.WriteLine();
                        screen.TVStatus();
                        break;
                    case "4":
                        remote.Channel(0);
                        Console.WriteLine();
                        screen.TVStatus();
                        break;
                    case "5":
                        remote.Channel(-1);
                        Console.WriteLine();
                        screen.TVStatus();
                        break;
                    case "6":
                        remote.Mute();
                        Console.WriteLine();
                        screen.TVStatus();
                        break;
                    case "7":
                        if (screen.Power == PowerStatus.Off)
                        {
                            Console.WriteLine("TV is Off. Unable to change channel.");
                            Delay.delay();
                            screen.TVStatus();
                            break;
                        }
                        Console.WriteLine("Enter the channel number:");
                        int newChannel;
                        if (int.TryParse(Console.ReadLine(), out newChannel))
                        {
                            remote.Channel(newChannel);
                        }
                        else
                        {
                            Console.WriteLine("Invalid Channel Number");
                        }
                        Console.WriteLine();
                        screen.TVStatus();
                        break;
                    case "8":
                        if (screen.Power == PowerStatus.Off)
                        {
                            Console.WriteLine("TV is Off. Unable to open Smart Menu.");
                            Delay.delay();
                            screen.TVStatus();
                            break;
                        }
                        remote.SmartMenu();
                        break;
                    case "9":
                        if (screen.Power == PowerStatus.Off)
                        {
                            Console.WriteLine("TV is Off. Unable to open Settings.");
                            Delay.delay();
                            screen.TVStatus();
                            break;
                        }
                        remote.Settings();
                        break;
                    case "0":
                        Console.WriteLine("Exiting...\n");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Command. Please try again.");
                        Delay.delay();
                        Console.Clear();
                        screen.TVStatus();
                        break;
                }
            }
        }
    }
}