using System;
using System.Data;
using System.Text;
using System.Threading;
using Gnomes_Gargoyles;

class GridShell
{

    static int enemyCount = 0;
    static int level = 1;
    static int socks = 300;
    static int score = 0;
    static float timer = 300;
    static float timerMax = 300;


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


    static readonly List<Gnome> gnomes = new();
    static void Start()
    {

        //spawn Gargoyles here
    }

    static void Main()
    {
        Console.CursorVisible = false;
        while (true)
        {
            Update();

            timer -= 0.5f;
            // Press ESC to break out
            if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)
                break;

            Thread.Sleep(500); // 2 FPS
        }
    }
    static void Update() //Happens Every Frame
    { //Clearing the past frame's lines before printing new lines 
        Console.SetCursorPosition(0, 0);

        DrawGnomes();


        { //Printing each line (Brackets solely so you can collapse it) 
            Console.Write("║ ░Socks░ ██>  ░▒▒▓▌║"); Console.Write(new string(Row1)); Console.WriteLine("║▐▓▒▒░  <██ ░Timer░ ║");
            Console.Write("║   «ß»   ██> ░▒▒▓▓▌║"); Console.Write(new string(Row2)); Console.WriteLine("║▐▓▓▒▒░ <██   «ö»   ║");
            Console.Write($"║  ░{socks}░  ██> ░▒▒▓▓▌║"); Console.Write(new string(Row3)); Console.WriteLine($"║▐▓▓▒▒░ <██░{timer}/{timerMax}░║");
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
            Console.Write("     N║█║║██> ░░▒▓▓▌║"); Console.Write(new string(Row28)); Console.WriteLine("║▐▓▓▒░░ <██║«ß»5 «ß»5 «ß»5 ║");
            Console.WriteLine("      ╚═╝║███████████████████████████████████████████║░░░░░░░░░░░░░░░║");
        }

    }

    public static Gnome SpawnGnome(int lane, int row, int health)
    {
        var g = new Gnome(lane, row, health);
        gnomes.Add(g);
        return g;
    }

    static void DrawGnomes()
    {
        foreach (var g in gnomes.Where(x => x.IsAlive))
        {
            if (g.Lane == 1)
            {
                if (g.Row == 1)
                {

                }
                else if (g.Row == 2)
                {

                }
                else if (g.Row == 3)
                {

                }
                else if (g.Row == 4)
                {

                }
                else if (g.Row == 5)
                {

                }
                else if (g.Row == 6)
                {

                }
                else if (g.Row == 7)
                {

                }
                else if (g.Row == 8)
                {

                }
                else if (g.Row == 9)
                {

                }
            }
            else if (g.Lane == 2)
            {
                if (g.Row == 1)
                {

                }
                else if (g.Row == 2)
                {

                }
                else if (g.Row == 3)
                {

                }
                else if (g.Row == 4)
                {

                }
                else if (g.Row == 5)
                {

                }
                else if (g.Row == 6)
                {

                }
                else if (g.Row == 7)
                {

                }
                else if (g.Row == 8)
                {

                }
                else if (g.Row == 9)
                {

                }
            }
            else if (g.Lane == 3)
            {
                if (g.Row == 1)
                {

                }
                else if (g.Row == 2)
                {

                }
                else if (g.Row == 3)
                {

                }
                else if (g.Row == 4)
                {

                }
                else if (g.Row == 5)
                {

                }
                else if (g.Row == 6)
                {

                }
                else if (g.Row == 7)
                {

                }
                else if (g.Row == 8)
                {

                }
                else if (g.Row == 9)
                {

                }
            }
            else if (g.Lane == 4)
            {
                if (g.Row == 1)
                {

                }
                else if (g.Row == 2)
                {

                }
                else if (g.Row == 3)
                {

                }
                else if (g.Row == 4)
                {

                }
                else if (g.Row == 5)
                {

                }
                else if (g.Row == 6)
                {

                }
                else if (g.Row == 7)
                {

                }
                else if (g.Row == 8)
                {

                }
                else if (g.Row == 9)
                {

                }
            }
            else if (g.Lane == 5)
            {
                if (g.Row == 1)
                {

                }
                else if (g.Row == 2)
                {

                }
                else if (g.Row == 3)
                {

                }
                else if (g.Row == 4)
                {

                }
                else if (g.Row == 5)
                {

                }
                else if (g.Row == 6)
                {

                }
                else if (g.Row == 7)
                {

                }
                else if (g.Row == 8)
                {

                }
                else if (g.Row == 9)
                {

                }
            }
        }
    }

}
