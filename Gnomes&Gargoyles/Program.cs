using System;
using System.Data;
using System.Text;
using System.Threading;
using Gnomes_Gargoyles;

class GridShell
{
    static bool isPlaying = false;
    static bool hasLost = false;
    static bool hasWon = false;

    static int moveTimer;
    static int movePeriod = 4;
    static int tickTimer;
    static int enemyCount = 10;
    static int level = 1;
    static int socks = 30;
    static int score = 0;
    static float timer = 300;
    static float timerMax = timer;


    //Character arrays for each line
    static char[] Row1 = { '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+' };
    static char[] Row2 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row3 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row4 = { '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+' };
    static char[] Row5 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row6 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row7 = { '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+' };
    static char[] Row8 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row9 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row10 = { '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+' };
    static char[] Row11 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row12 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row13 = { '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+' };
    static char[] Row14 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row15 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row16 = { '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+' };
    static char[] Row17 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row18 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row19 = { '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+' };
    static char[] Row20 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row21 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row22 = { '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+' };
    static char[] Row23 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row24 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row25 = { '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+' };
    static char[] Row26 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row27 = { '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|', ' ', ' ', ' ', '|' };
    static char[] Row28 = { '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+', '-', '-', '-', '+' };
    //===========$$$$$$$=============
    // Gargoyle "shells" as placeholder blocks (3x2 cells for our shortened art)
    //===========$$$$$$$=============

    // Each block is a 3x2 cell made for our Gargole ASCII art
    static char[,] GargoyleShell1 = new char[2, 3]
    {
        { ' ', ' ', ' ' },
        { ' ', ' ', ' ' }
    };

    static char[,] GargoyleShell2 = new char[2, 3]
    {
        { ' ', ' ', ' ' },
        { ' ', ' ', ' ' }
    };

    static char[,] GargoyleShell3 = new char[2, 3]
    {
        { ' ', ' ', ' ' },
        { ' ', ' ', ' ' }
    };


    static readonly List<Gnome> gnomes = new();

    //============$$$$$$$===========================
    // Gargoyle shells being displayed(3 columns × 2 rows each)
    //============$$$$$$$======================
    static void Start()
    {
        

    }

    static void Main()
    {
        Console.CursorVisible = false;
        Console.OutputEncoding = new UTF8Encoding(false); //Emoji Enabler
        Console.CursorVisible = false; //Curser bug Disabler
        while (true)
        {
            Update();

            // Press ESC to break out
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                break;

            Thread.Sleep(500); // 2 FPS
        }
    }

    static void Update()
    {
        Console.SetCursorPosition(0, 0);
        if (!isPlaying & !hasLost & !hasWon)
        {
            Console.WriteLine(@"██▓▓▓▓▓▓▓▒▒▒▒▒░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░▒▒▒▒▒▒▒▓▓▓▓▓▓▓██");
            Console.WriteLine(@"█▓▓▓▓▓▓▒▒▒░░░░░░░░░░░░█▀▀░█▀█░█▀█░█▄█░█▀▀░█▀▀░░░░░░░░░░░░▒▒▒▓▓▓▓▓▓█");
            Console.WriteLine(@"▓▓▓▓▓▒▒▒░░░░░░░░░░░░░░█░█▒█░█▒█░█▒█▒█▒█▀▀▒▀▀█░░░░░░░░░░░░░░▒▒▒▓▓▓▓▓");
            Console.WriteLine(@"▓▓▓▓▓▒▒░░░░░░░░░░░░░░░▀▀▀▒▀▒▀▒▀▀▀▒▀▒▀▒▀▀▀▒▀▀▀░░░░░░░░░░░░░░░▒▒▓▓▓▓▓");
            Console.WriteLine(@"▓▓▓▒▒▒░░░░░                  ░░▒▄▀▒░░░                  ░░░░░▒▒▒▓▓▓");
            Console.WriteLine(@"▒▒▒▒▒░░░░░                   ░░▒▄█▀▒░░                   ░░░░░▒▒▒▒▒");
            Console.WriteLine(@"▒░░░░░░░                     ░░░▒▀▀▒░░                     ░░░░░░░▒");
            Console.WriteLine(@"░░░░░          ░█▀▀░█▀█░█▀▄░█▀▀░█▀█░█░█░█░▒░█▀▀░█▀▀░          ░░░░░");
            Console.WriteLine(@"░░░            ░█░█▒█▀█▒█▀▄▒█░█▒█░█▒░█░▒█▒▒▒█▀▀▒▀▀█░            ░░░");
            Console.WriteLine(@"░░░            ░▀▀▀▒▀░▀▒▀░▀▒▀▀▀▒▀▀▀▒▒▀▒▒▀▀▀▒▀▀▀▒▀▀▀░            ░░░");
            Console.WriteLine(@"░▒░┌────────────────────────────┐░┌────────────────────────────┐░▒░");
            Console.WriteLine(@"▒░▒│ /\, Gnimble Gnome          │▒│/[/ Gaurdgoyle              │▒░▒");
            Console.WriteLine(@"▒░▒│ σ σ                        │▒│ΘΘ£                         │▒░▒");
            Console.WriteLine(@"▒░▒│ ∙O∙                        │▒│                            │▒░▒");
            Console.WriteLine(@"░▒░└────────────────────────────┘░└────────────────────────────┘░▒░");
            Console.WriteLine(@"░▒░┌────────────────────────────┐░┌────────────────────────────┐░▒░");
            Console.WriteLine(@"▒░▒│ /E, Gnight Gnome           │▒│                            │▒░▒");
            Console.WriteLine(@"▒░▒│ ò ó                        │▒│                            │▒░▒");
            Console.WriteLine(@"▒░▒│ °O°                        │▒│                            │▒░▒");
            Console.WriteLine(@"░▒░└────────────────────────────┘░└────────────────────────────┘░▒░");
            Console.WriteLine(@"░▒░┌────────────────────────────┐░┌────────────────────────────┐░▒░");
            Console.WriteLine(@"▒░▒│ /^\ Gnomage/Gnomagician    │▒│                            │▒░▒");
            Console.WriteLine(@"▒░▒│ u u                        │▒│                            │▒░▒");
            Console.WriteLine(@"▒░▒│ /:\                        │▒│                            │▒░▒");
            Console.WriteLine(@"░▒░└────────────────────────────┘░└────────────────────────────┘░▒░");
            Console.WriteLine(@"░░░                                                             ░░░");
            Console.WriteLine(@"▒░░░                   !Press Space to Play!                   ░░░▒");
            Console.WriteLine("");
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Spacebar)
                {
                    isPlaying = true;
                }
            }
        }


        if (isPlaying & !hasLost & !hasWon)
        {
            DrawGargoyles(); // Draws Gargoles first to set the stage
            DrawGnomes();    // Then th
                             // e gnomes


            tickTimer++;
            if (tickTimer >= 2 & timer > 0)
            {
                tickTimer = 0;
                timer--;
            }

            moveTimer++;
            if (moveTimer >= movePeriod)
            {
                foreach (var g in gnomes.Where(x => x.IsAlive))
                {
                    moveTimer = 0;
                    g.Row++;
                }
            }
            
            if (timer <= 0)
            {
                hasLost = true;
                
            }

            if (enemyCount <= 0)
            {
                hasWon = true;

            }

            //Printing each line (Brackets solely so you can collapse it) 
            Console.Write("║ ░Socks░ ██>  ░▒▒▓▌║"); Console.Write(new string(Row1)); Console.WriteLine("║▐▓▒▒░  <██ ░Timer░ ║");
            Console.Write("║   «ß»   ██> ░▒▒▓▓▌║"); Console.Write(new string(Row2)); Console.WriteLine("║▐▓▓▒▒░ <██   «ö»   ║");
            Console.Write($"║  ░{socks}░   ██> ░▒▒▓▓▌║"); Console.Write(new string(Row3)); Console.WriteLine($"║▐▓▓▒▒░ <██░{timer}/{timerMax} ║");
            Console.Write("∙========<██> ░▒▒▓▒▌║"); Console.Write(new string(Row4)); Console.WriteLine("║▐▓▒▒▒░ <██         ║");
            Console.Write("          ██> ░░▒▓▓▌║"); Console.Write(new string(Row5)); Console.WriteLine("║▐▓▒▒░░ <██░Enemies░║");
            Console.Write("          ██> ░░▒▓▓▌║"); Console.Write(new string(Row6)); Console.WriteLine("║▐▓▓▒░░ <██   «φ»   ║");
            Console.Write("          ██> ░░▒▓▓▌║"); Console.Write(new string(Row7)); Console.WriteLine($"║▐▓▓▒░░ <██  ░{enemyCount}/??░ ║");
            Console.Write("          ██> ░▒▒▓▓▌║"); Console.Write(new string(Row8)); Console.WriteLine("║▐▓▓▒▒░ <██>========∙");
            Console.Write("          ██> ░▒▒▓▓▌║"); Console.Write(new string(Row9)); Console.WriteLine("║▐▒▓▒▒░ <██          ");
            Console.Write("          ██> ░░▒▒▓▌║"); Console.Write(new string(Row10)); Console.WriteLine("║▐▓▒▒░░ <██          ");
            Console.Write("          ██> ░░▒▒▓▌║"); Console.Write(new string(Row11)); Console.WriteLine("║▐▓▒▒░░ <██          ");
            Console.Write("          ██>  ░▒▓▓▌║"); Console.Write(new string(Row12)); Console.WriteLine("║▐▓▓▒░  <██          ");
            Console.Write("          ██>  ░▒▒▓▌║"); Console.Write(new string(Row13)); Console.WriteLine("║▐▓▒▒░  <██          ");
            Console.Write("          ██> ░▒▒▓▓▌║"); Console.Write(new string(Row14)); Console.WriteLine("║▐▓▓▒▒░ <██          ");
            Console.Write("          ██> ░▒▒▓▓▌║"); Console.Write(new string(Row15)); Console.WriteLine("║▐▓▓▒▒░ <██          ");
            Console.Write("          ██> ░▒▒▓▒▌║"); Console.Write(new string(Row16)); Console.WriteLine("║▐▓▒▒▒░ <██          ");
            Console.Write("          ██> ░░▒▓▓▌║"); Console.Write(new string(Row17)); Console.WriteLine("║▐▓▒▒░░ <██          ");
            Console.Write("          ██> ░░▒▓▓▌║"); Console.Write(new string(Row18)); Console.WriteLine("║▐▓▓▒░░ <██          ");
            Console.Write("          ██> ░░▒▓▓▌║"); Console.Write(new string(Row19)); Console.WriteLine("║▐▓▓▒░░ <██          ");
            Console.Write("      ╔═╗║██> ░▒▒▓▓▌║"); Console.Write(new string(Row20)); Console.WriteLine("║▐▓▓▒▒░ <██║░░░░░░░░░░░░║");
            Console.Write("     C║█║║██> ░▒▒▓▓▌║"); Console.Write(new string(Row21)); Console.WriteLine("║▐▓▓▒▒░ <██║{  GNOMES  }║");
            Console.Write("     O║█║║██> ░▒▒▓▓▌║"); Console.Write(new string(Row22)); Console.WriteLine("║▐▓▓▒▒░ <██║░░░░░░░░░░░░░░░║");
            Console.Write("     O║█║║██> ░▒▒▓▒▌║"); Console.Write(new string(Row23)); Console.WriteLine("║▐▓▒▒▒░ <██║╔|1|╗╔|2|╗╔|3|╗║");
            Console.Write("     L║█║║██> ░░▒▓▓▌║"); Console.Write(new string(Row24)); Console.WriteLine("║▐▓▒▒░░ <██║ /\\,  /Σ,  /^\\ ║");
            Console.Write("     D║█║║██> ░░▒▓▓▌║"); Console.Write(new string(Row25)); Console.WriteLine("║▐▓▓▒░░ <██║ σ σ  ò ó  u u ║");
            Console.Write("     O║█║║██> ░░▒▓▓▌║"); Console.Write(new string(Row26)); Console.WriteLine("║▐▓▓▒░░ <██║ ∙O∙  °O°  /:\\ ║");
            Console.Write("     W║█║║██> ░▒▒▓▓▌║"); Console.Write(new string(Row27)); Console.WriteLine("║▐▓▓▒▒░ <██║╚   ╝╚   ╝╚   ╝║");
            Console.Write("     N║█║║██> ░░▒▓▓▌║"); Console.Write(new string(Row28)); Console.WriteLine("║▐▓▓▒░░ <██║«ß»3 «ß»6 «ß»9 ║");
            Console.WriteLine("      ╚═╝║███████████████████████████████████████████║░░░░░░░░░░░░░░░║");
        }

        if (hasLost)
        {
            Console.WriteLine("           ▓▓▓██                  ██▓▓▓            ");
            Console.WriteLine("          █▓▒▒▒▓███            ███▓▒▒▒▓█           ");
            Console.WriteLine("          ▓▒▒▒▒▓██▒██        ██▒██▓▒▒▒▒▓           ");
            Console.WriteLine("          ▓▒▒▒▒▓██░▒▓█      █▓▒▒██▓▒▒▒▒▓           ");
            Console.WriteLine("           ▓▒▒▒▓█▒░░▒▒▓█  █▓▒▒░░▒█▓▒▒▒▓            ");
            Console.WriteLine("            █▓▒▒▓▒▒▒▒░▒    ▒░▒▒▒▒▓▒▒▓█             ");
            Console.WriteLine("                                                   ");
            Console.WriteLine("            ░▒░                     ░█░            ");
            Console.WriteLine("            ░█▒█▒▒░░▒░░░░░▒▒▒░▒▒░▓▓░▓█░            ");
            Console.WriteLine("             ░░█▓██▓█▓█▒▓█▓██░█████▓█░             ");
            Console.WriteLine("               ▒▒██▒███▓▓████▒███▓█░░              ");
            Console.WriteLine("                ░▓█▒███▓▓████▓██▓▓▓                ");
            Console.WriteLine("                 ░▒▒███▓▓████▓▓█▒░                 ");
            Console.WriteLine("                  ░▒█▓█▓▓████▒▓█░                  ");
            Console.WriteLine("                   ░▒▒▓▒▒█▒░▒░░░                   ");
            Console.WriteLine("                                                   ");
            Console.WriteLine(" ▄▄ •  ▄▄▄· • ▌ ▄ ·. ▄▄▄ .     ▄█▀▄  ▌ ▐·▄▄▄ .▄▄▄  ");
            Console.WriteLine("▐█·▀ ▪▐█ ▀█ ·██ ▐███▪▀▄.▀·    ▐█▌.▐▌▪█·█▌▀▄.▀·▀▄ █·");
            Console.WriteLine("▄█ ▀█▄▄█▀▀█ ▐█ ▌▐▌▐█·▐▀▀▪▄    ▐█· ▐▌▐█▐█•▐▀▀▪▄▐▀▀▄ ");
            Console.WriteLine("▐█▄▪▐█▐█· ▐▌██ ██▌▐█▌▐█▄▄▌    ▐█▄ █  ███ ▐█▄▄▌▐█ █▌");
            Console.WriteLine("·▀▀▀▀  ▀  ▀ ▀▀  █▪▀▀▀ ▀▀▀      ▀█▄▀▪. ▀   ▀▀▀ .▀  ▀");
            Console.WriteLine("                                                   ");
            Console.WriteLine("                                                   ");
            Console.WriteLine("                    Space to Exit                  ");

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.Spacebar)
                {
                    Environment.Exit(0);
                }
            }
        }
        if (hasWon)
        {
            Console.WriteLine("                                     █▓           ┌───┐");
            Console.WriteLine("                                    ███▓          ├───┤");
            Console.WriteLine("██╗   ██╗ ██████╗ ██╗   ██╗        ▓████▓         │   │");
            Console.WriteLine("╚██╗ ██╔╝██╔═══██╗██║   ██║        ██████▓       _⌡   │");
            Console.WriteLine(" ╚████╔╝ ██║   ██║██║   ██║       ▓██████▓▓     /     ⌡");
            Console.WriteLine("  ╚██╔╝  ██║   ██║██║   ██║       ████████▓     \\____/ ");
            Console.WriteLine("   ██║   ╚██████╔╝╚██████╔╝      ▓▓████████▓           ");
            Console.WriteLine("   ╚═╝    ╚═════╝  ╚═════╝      ▓    ▄    ▄ ▓    ▓▓▓   ");
            Console.WriteLine("                                ▓   █▄▌  █▄▌ ░▓ ▓   ▓  ");
            Console.WriteLine(" ██╗    ██╗██╗███╗   ██╗██╗     ▓            ▓ █▓   ▓  ");
            Console.WriteLine(" ██║    ██║██║████╗  ██║██║     ▓▒   ▒▒▒▒▒▒ ▒▓█████▓   ");
            Console.WriteLine(" ██║ █╗ ██║██║██╔██╗ ██║██║    ███▓▒▒▒▒▒▒▒▒▒▓████▓     ");
            Console.WriteLine(" ██║███╗██║██║██║╚██╗██║╚═╝   ▓████▓▒▒▒▒▒▒▒▓███▓       ");
            Console.WriteLine(" ╚███╔███╔╝██║██║ ╚████║██╗  ▓████▓█████▓▓███▓         ");
            Console.WriteLine("  ╚══╝╚══╝ ╚═╝╚═╝  ╚═══╝╚═╝  ████ ██████▓▓████         ");
            Console.WriteLine("                            ▓   ▓ ▓▓▓▓▓▓█▓▓▓▓▓         ");
            Console.WriteLine("                            ▓   ▓ ████████████         ");
            Console.WriteLine("   Time - _____              ▓▓▓    ███  ███           ");
            Console.WriteLine("                                  █▓███  ███▓█         ");
            Console.WriteLine("  Score - _____                   █████  █████         ");

        }







    }

    public static Gnome SpawnGnome(int lane, int row, int health)
    {
        var g = new Gnome(lane, row, health);
        gnomes.Add(g);
        return g;
    }
    static void DrawGargoyles()
    {

    }
    static void DrawGnomes()
    {
        foreach (var g in gnomes.Where(x => x.IsAlive))
        {
            if (g.Lane == 1)
            {
                if (g.Row == 1)
                {
                    Row2[2] = '/';
                    Row2[3] = '\\';
                    Row2[4] = ',';
                    Row3[2] = 'o';
                    Row3[3] = 'u';
                    Row3[4] = '0';
                }
                else if (g.Row == 2)
                {
                    Row2[2] = ' ';
                    Row2[3] = ' ';
                    Row2[4] = ' ';
                    Row3[2] = ' ';
                    Row3[3] = ' ';
                    Row3[4] = ' ';
                    Row5[2] = '/';
                    Row5[3] = '\\';
                    Row5[4] = ',';
                    Row6[2] = 'o';
                    Row6[3] = 'u';
                    Row6[4] = '0';
                }
                else if (g.Row == 3)
                {
                    Row5[2] = ' ';
                    Row5[3] = ' ';
                    Row5[4] = ' ';
                    Row6[2] = ' ';
                    Row6[3] = ' ';
                    Row6[4] = ' ';
                    Row8[2] = '/';
                    Row8[3] = '\\';
                    Row8[4] = ',';
                    Row9[2] = 'o';
                    Row9[3] = 'u';
                    Row9[4] = '0';
                }
                else if (g.Row == 4)
                {
                    Row8[2] = ' ';
                    Row8[3] = ' ';
                    Row8[4] = ' ';
                    Row9[2] = ' ';
                    Row9[3] = ' ';
                    Row9[4] = ' ';
                    Row11[2] = '/';
                    Row11[3] = '\\';
                    Row11[4] = ',';
                    Row12[2] = 'o';
                    Row12[3] = 'u';
                    Row12[4] = '0';
                }
                else if (g.Row == 5)
                {
                    Row11[2] = ' ';
                    Row11[3] = ' ';
                    Row11[4] = ' ';
                    Row12[2] = ' ';
                    Row12[3] = ' ';
                    Row12[4] = ' ';
                    Row14[2] = '/';
                    Row14[3] = '\\';
                    Row14[4] = ',';
                    Row15[2] = 'o';
                    Row15[3] = 'u';
                    Row15[4] = '0';
                }
                else if (g.Row == 6)
                {
                    Row14[2] = ' ';
                    Row14[3] = ' ';
                    Row14[4] = ' ';
                    Row15[2] = ' ';
                    Row15[3] = ' ';
                    Row15[4] = ' ';
                    Row17[2] = '/';
                    Row17[3] = '\\';
                    Row17[4] = ',';
                    Row18[2] = 'o';
                    Row18[3] = 'u';
                    Row18[4] = '0';
                }
                else if (g.Row == 7)
                {
                    Row17[2] = ' ';
                    Row17[3] = ' ';
                    Row17[4] = ' ';
                    Row18[2] = ' ';
                    Row18[3] = ' ';
                    Row18[4] = ' ';
                    Row20[2] = '/';
                    Row20[3] = '\\';
                    Row20[4] = ',';
                    Row21[2] = 'o';
                    Row21[3] = 'u';
                    Row21[4] = '0';
                }
                else if (g.Row == 8)
                {
                    Row20[2] = ' ';
                    Row20[3] = ' ';
                    Row20[4] = ' ';
                    Row21[2] = ' ';
                    Row21[3] = ' ';
                    Row21[4] = ' ';
                    Row23[2] = '/';
                    Row23[3] = '\\';
                    Row23[4] = ',';
                    Row24[2] = 'o';
                    Row24[3] = 'u';
                    Row24[4] = '0';
                }
                else if (g.Row == 9)
                {
                    Row23[2] = ' ';
                    Row23[3] = ' ';
                    Row23[4] = ' ';
                    Row24[2] = ' ';
                    Row24[3] = ' ';
                    Row24[4] = ' ';
                    Row26[2] = '/';
                    Row26[3] = '\\';
                    Row26[4] = ',';
                    Row27[2] = 'o';
                    Row27[3] = 'u';
                    Row27[4] = '0';
                }
            }
            else if (g.Lane == 2)
            {
                if (g.Row == 1)
                {
                    Row2[6] = '/';
                    Row2[7] = '\\';
                    Row2[8] = ',';
                    Row3[6] = 'o';
                    Row3[7] = 'u';
                    Row3[8] = '0';
                }
                else if (g.Row == 2)
                {
                    Row2[6] = ' ';
                    Row2[7] = ' ';
                    Row2[8] = ' ';
                    Row3[6] = ' ';
                    Row3[7] = ' ';
                    Row3[8] = ' ';
                    Row5[6] = '/';
                    Row5[7] = '\\';
                    Row5[8] = ',';
                    Row6[6] = 'o';
                    Row6[7] = 'u';
                    Row6[8] = '0';
                }
                else if (g.Row == 3)
                {
                    Row5[6] = ' ';
                    Row5[7] = ' ';
                    Row5[8] = ' ';
                    Row6[6] = ' ';
                    Row6[7] = ' ';
                    Row6[8] = ' ';
                    Row8[6] = '/';
                    Row8[7] = '\\';
                    Row8[8] = ',';
                    Row9[6] = 'o';
                    Row9[7] = 'u';
                    Row9[8] = '0';
                }
                else if (g.Row == 4)
                {
                    Row8[6] = ' ';
                    Row8[7] = ' ';
                    Row8[8] = ' ';
                    Row9[6] = ' ';
                    Row9[7] = ' ';
                    Row9[8] = ' ';
                    Row11[6] = '/';
                    Row11[7] = '\\';
                    Row11[8] = ',';
                    Row12[6] = 'o';
                    Row12[7] = 'u';
                    Row12[8] = '0';
                }
                else if (g.Row == 5)
                {
                    Row11[6] = ' ';
                    Row11[7] = ' ';
                    Row11[8] = ' ';
                    Row12[6] = ' ';
                    Row12[7] = ' ';
                    Row12[8] = ' ';
                    Row14[6] = '/';
                    Row14[7] = '\\';
                    Row14[8] = ',';
                    Row15[6] = 'o';
                    Row15[7] = 'u';
                    Row15[8] = '0';
                }
                else if (g.Row == 6)
                {
                    Row14[6] = ' ';
                    Row14[7] = ' ';
                    Row14[8] = ' ';
                    Row15[6] = ' ';
                    Row15[7] = ' ';
                    Row15[8] = ' ';
                    Row17[6] = '/';
                    Row17[7] = '\\';
                    Row17[8] = ',';
                    Row18[6] = 'o';
                    Row18[7] = 'u';
                    Row18[8] = '0';
                }
                else if (g.Row == 7)
                {
                    Row17[6] = ' ';
                    Row17[7] = ' ';
                    Row17[8] = ' ';
                    Row18[6] = ' ';
                    Row18[7] = ' ';
                    Row18[8] = ' ';
                    Row20[6] = '/';
                    Row20[7] = '\\';
                    Row20[8] = ',';
                    Row21[6] = 'o';
                    Row21[7] = 'u';
                    Row21[8] = '0';
                }
                else if (g.Row == 8)
                {
                    Row20[6] = ' ';
                    Row20[7] = ' ';
                    Row20[8] = ' ';
                    Row21[6] = ' ';
                    Row21[7] = ' ';
                    Row21[8] = ' ';
                    Row23[6] = '/';
                    Row23[7] = '\\';
                    Row23[8] = ',';
                    Row24[6] = 'o';
                    Row24[7] = 'u';
                    Row24[8] = '0';
                }
                else if (g.Row == 9)
                {
                    Row23[6] = ' ';
                    Row23[7] = ' ';
                    Row23[8] = ' ';
                    Row24[6] = ' ';
                    Row24[7] = ' ';
                    Row24[8] = ' ';
                    Row26[6] = '/';
                    Row26[7] = '\\';
                    Row26[8] = ',';
                    Row27[6] = 'o';
                    Row27[7] = 'u';
                    Row27[8] = '0';
                }

            }
            else if (g.Lane == 3)
            {
                if (g.Row == 1)
                {
                    Row2[10] = '/';
                    Row2[11] = '\\';
                    Row2[12] = ',';
                    Row3[10] = 'o';
                    Row3[11] = 'u';
                    Row3[12] = '0';
                }
                else if (g.Row == 2)
                {
                    Row2[10] = ' ';
                    Row2[11] = ' ';
                    Row2[12] = ' ';
                    Row3[10] = ' ';
                    Row3[11] = ' ';
                    Row3[12] = ' ';
                    Row5[10] = '/';
                    Row5[11] = '\\';
                    Row5[12] = ',';
                    Row6[10] = 'o';
                    Row6[11] = 'u';
                    Row6[12] = '0';
                }
                else if (g.Row == 3)
                {
                    Row5[10] = ' ';
                    Row5[11] = ' ';
                    Row5[12] = ' ';
                    Row6[10] = ' ';
                    Row6[11] = ' ';
                    Row6[12] = ' ';
                    Row8[10] = '/';
                    Row8[11] = '\\';
                    Row8[12] = ',';
                    Row9[10] = 'o';
                    Row9[11] = 'u';
                    Row9[12] = '0';
                }
                else if (g.Row == 4)
                {
                    Row8[10] = ' ';
                    Row8[11] = ' ';
                    Row8[12] = ' ';
                    Row9[10] = ' ';
                    Row9[11] = ' ';
                    Row9[12] = ' ';
                    Row11[10] = '/';
                    Row11[11] = '\\';
                    Row11[12] = ',';
                    Row12[10] = 'o';
                    Row12[11] = 'u';
                    Row12[12] = '0';
                }
                else if (g.Row == 5)
                {
                    Row11[10] = ' ';
                    Row11[11] = ' ';
                    Row11[12] = ' ';
                    Row12[10] = ' ';
                    Row12[11] = ' ';
                    Row12[12] = ' ';
                    Row14[10] = '/';
                    Row14[11] = '\\';
                    Row14[12] = ',';
                    Row15[10] = 'o';
                    Row15[11] = 'u';
                    Row15[12] = '0';
                }
                else if (g.Row == 6)
                {
                    Row14[10] = ' ';
                    Row14[11] = ' ';
                    Row14[12] = ' ';
                    Row15[10] = ' ';
                    Row15[11] = ' ';
                    Row15[12] = ' ';
                    Row17[10] = '/';
                    Row17[11] = '\\';
                    Row17[12] = ',';
                    Row18[10] = 'o';
                    Row18[11] = 'u';
                    Row18[12] = '0';
                }
                else if (g.Row == 7)
                {
                    Row17[10] = ' ';
                    Row17[11] = ' ';
                    Row17[12] = ' ';
                    Row18[10] = ' ';
                    Row18[11] = ' ';
                    Row18[12] = ' ';
                    Row20[10] = '/';
                    Row20[11] = '\\';
                    Row20[12] = ',';
                    Row21[10] = 'o';
                    Row21[11] = 'u';
                    Row21[12] = '0';
                }
                else if (g.Row == 8)
                {
                    Row20[10] = ' ';
                    Row20[11] = ' ';
                    Row20[12] = ' ';
                    Row21[10] = ' ';
                    Row21[11] = ' ';
                    Row21[12] = ' ';
                    Row23[10] = '/';
                    Row23[11] = '\\';
                    Row23[12] = ',';
                    Row24[10] = 'o';
                    Row24[11] = 'u';
                    Row24[12] = '0';
                }
                else if (g.Row == 9)
                {
                    Row23[10] = ' ';
                    Row23[11] = ' ';
                    Row23[12] = ' ';
                    Row24[10] = ' ';
                    Row24[11] = ' ';
                    Row24[12] = ' ';
                    Row26[10] = '/';
                    Row26[11] = '\\';
                    Row26[12] = ',';
                    Row27[10] = 'o';
                    Row27[11] = 'u';
                    Row27[12] = '0';
                }

            }
            else if (g.Lane == 4)
            {
                if (g.Row == 1)
                {
                    Row2[14] = '/';
                    Row2[15] = '\\';
                    Row2[16] = ',';
                    Row3[14] = 'o';
                    Row3[15] = 'u';
                    Row3[16] = '0';
                }
                else if (g.Row == 2)
                {
                    Row2[14] = ' ';
                    Row2[15] = ' ';
                    Row2[16] = ' ';
                    Row3[14] = ' ';
                    Row3[15] = ' ';
                    Row3[16] = ' ';
                    Row5[14] = '/';
                    Row5[15] = '\\';
                    Row5[16] = ',';
                    Row6[14] = 'o';
                    Row6[15] = 'u';
                    Row6[16] = '0';
                }
                else if (g.Row == 3)
                {
                    Row5[14] = ' ';
                    Row5[15] = ' ';
                    Row5[16] = ' ';
                    Row6[14] = ' ';
                    Row6[15] = ' ';
                    Row6[16] = ' ';
                    Row8[14] = '/';
                    Row8[15] = '\\';
                    Row8[16] = ',';
                    Row9[14] = 'o';
                    Row9[15] = 'u';
                    Row9[16] = '0';
                }
                else if (g.Row == 4)
                {
                    Row8[14] = ' ';
                    Row8[15] = ' ';
                    Row8[16] = ' ';
                    Row9[14] = ' ';
                    Row9[15] = ' ';
                    Row9[16] = ' ';
                    Row11[14] = '/';
                    Row11[15] = '\\';
                    Row11[16] = ',';
                    Row12[14] = 'o';
                    Row12[15] = 'u';
                    Row12[16] = '0';
                }
                else if (g.Row == 5)
                {
                    Row11[14] = ' ';
                    Row11[15] = ' ';
                    Row11[16] = ' ';
                    Row12[14] = ' ';
                    Row12[15] = ' ';
                    Row12[16] = ' ';
                    Row14[14] = '/';
                    Row14[15] = '\\';
                    Row14[16] = ',';
                    Row15[14] = 'o';
                    Row15[15] = 'u';
                    Row15[16] = '0';
                }
                else if (g.Row == 6)
                {
                    Row14[14] = ' ';
                    Row14[15] = ' ';
                    Row14[16] = ' ';
                    Row15[14] = ' ';
                    Row15[15] = ' ';
                    Row15[16] = ' ';
                    Row17[14] = '/';
                    Row17[15] = '\\';
                    Row17[16] = ',';
                    Row18[14] = 'o';
                    Row18[15] = 'u';
                    Row18[16] = '0';
                }
                else if (g.Row == 7)
                {
                    Row17[14] = ' ';
                    Row17[15] = ' ';
                    Row17[16] = ' ';
                    Row18[14] = ' ';
                    Row18[15] = ' ';
                    Row18[16] = ' ';
                    Row20[14] = '/';
                    Row20[15] = '\\';
                    Row20[16] = ',';
                    Row21[14] = 'o';
                    Row21[15] = 'u';
                    Row21[16] = '0';
                }
                else if (g.Row == 8)
                {
                    Row20[14] = ' ';
                    Row20[15] = ' ';
                    Row20[16] = ' ';
                    Row21[14] = ' ';
                    Row21[15] = ' ';
                    Row21[16] = ' ';
                    Row23[14] = '/';
                    Row23[15] = '\\';
                    Row23[16] = ',';
                    Row24[14] = 'o';
                    Row24[15] = 'u';
                    Row24[16] = '0';
                }
                else if (g.Row == 9)
                {
                    Row23[14] = ' ';
                    Row23[15] = ' ';
                    Row23[16] = ' ';
                    Row24[14] = ' ';
                    Row24[15] = ' ';
                    Row24[16] = ' ';
                    Row26[14] = '/';
                    Row26[15] = '\\';
                    Row26[16] = ',';
                    Row27[14] = 'o';
                    Row27[15] = 'u';
                    Row27[16] = '0';
                }
            
            }
            else if (g.Lane == 5)
            {
                if (g.Row == 1)
                {
                    Row2[18] = '/';
                    Row2[19] = '\\';
                    Row2[20] = ',';
                    Row3[18] = 'o';
                    Row3[19] = 'u';
                    Row3[20] = '0';
                }
                else if (g.Row == 2)
                {
                    Row2[18] = ' ';
                    Row2[19] = ' ';
                    Row2[20] = ' ';
                    Row3[18] = ' ';
                    Row3[19] = ' ';
                    Row3[20] = ' ';
                    Row5[18] = '/';
                    Row5[19] = '\\';
                    Row5[20] = ',';
                    Row6[18] = 'o';
                    Row6[19] = 'u';
                    Row6[20] = '0';
                }
                else if (g.Row == 3)
                {
                    Row5[18] = ' ';
                    Row5[19] = ' ';
                    Row5[20] = ' ';
                    Row6[18] = ' ';
                    Row6[19] = ' ';
                    Row6[20] = ' ';
                    Row8[18] = '/';
                    Row8[19] = '\\';
                    Row8[20] = ',';
                    Row9[18] = 'o';
                    Row9[19] = 'u';
                    Row9[20] = '0';
                }
                else if (g.Row == 4)
                {
                    Row8[18] = ' ';
                    Row8[19] = ' ';
                    Row8[20] = ' ';
                    Row9[18] = ' ';
                    Row9[19] = ' ';
                    Row9[20] = ' ';
                    Row11[18] = '/';
                    Row11[19] = '\\';
                    Row11[20] = ',';
                    Row12[18] = 'o';
                    Row12[19] = 'u';
                    Row12[20] = '0';
                }
                else if (g.Row == 5)
                {
                    Row11[18] = ' ';
                    Row11[19] = ' ';
                    Row11[20] = ' ';
                    Row12[18] = ' ';
                    Row12[19] = ' ';
                    Row12[20] = ' ';
                    Row14[18] = '/';
                    Row14[19] = '\\';
                    Row14[20] = ',';
                    Row15[18] = 'o';
                    Row15[19] = 'u';
                    Row15[20] = '0';
                }
                else if (g.Row == 6)
                {
                    Row14[18] = ' ';
                    Row14[19] = ' ';
                    Row14[20] = ' ';
                    Row15[18] = ' ';
                    Row15[19] = ' ';
                    Row15[20] = ' ';
                    Row17[18] = '/';
                    Row17[19] = '\\';
                    Row17[20] = ',';
                    Row18[18] = 'o';
                    Row18[19] = 'u';
                    Row18[20] = '0';
                }
                else if (g.Row == 7)
                {
                    Row17[18] = ' ';
                    Row17[19] = ' ';
                    Row17[20] = ' ';
                    Row18[18] = ' ';
                    Row18[19] = ' ';
                    Row18[20] = ' ';
                    Row20[18] = '/';
                    Row20[19] = '\\';
                    Row20[20] = ',';
                    Row21[18] = 'o';
                    Row21[19] = 'u';
                    Row21[20] = '0';
                }
                else if (g.Row == 8)
                {
                    Row20[18] = ' ';
                    Row20[19] = ' ';
                    Row20[20] = ' ';
                    Row21[18] = ' ';
                    Row21[19] = ' ';
                    Row21[20] = ' ';
                    Row23[18] = '/';
                    Row23[19] = '\\';
                    Row23[20] = ',';
                    Row24[18] = 'o';
                    Row24[19] = 'u';
                    Row24[20] = '0';
                }
                else if (g.Row == 9)
                {
                    Row23[18] = ' ';
                    Row23[19] = ' ';
                    Row23[20] = ' ';
                    Row24[18] = ' ';
                    Row24[19] = ' ';
                    Row24[20] = ' ';
                    Row26[18] = '/';
                    Row26[19] = '\\';
                    Row26[20] = ',';
                    Row27[18] = 'o';
                    Row27[19] = 'u';
                    Row27[20] = '0';
                }

            }
        }
    }

}
