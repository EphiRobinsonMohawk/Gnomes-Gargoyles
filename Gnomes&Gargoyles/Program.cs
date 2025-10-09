using System;
using System.Data;
using System.Text;
using System.Threading;
using System.Timers;
using Gnomes_Gargoyles;

class GridShell
{
    static float gargAttackMax = 5;
    static float[] gargAttackTimer = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    static int[] gargDamage = { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3};
    static int[] gargHealth = { 5, 5, 5, 5, 5, 10, 10, 10, 10, 10, 3, 3, 3, 3, 3}; //Array of gargoyles health stats
    static bool[] gargIsAlive = { true, true, true, true, true, true, true, true, true, true, false, false, false, false ,false}; //First 10 gargoyles start as true, Last 5 start as false so they can be reinforcements
    //gargoyle    0, 1, 2, 3, 4
    //setup       5, 6, 7, 8, 9     Remember arrays start at 0 (I forgot this twice while coding this)

    static bool iWantToHideThis = true;
    static bool isPlaying = false;
    static bool hasLost = false;
    static bool hasWon = false;
    static bool isPlacing = false;
    static bool isFlash = false;
    
    static int laneSelecting = 1;
    static int laneFlashing;
    static int gnomePlacing;
    static int gnomeDamage = 3;
    static int gnightDamage = 2;
    static int gizardDamage = 2;
    static int gnomeMoveTimer; 
    static int gizardMoveTimer;
    static int gnightMoveTimer;
    static int movePeriod = 4;
    static int gizardMovePeriod = 12;
    static int gnightMovePeriod = 2;

    static int tickTimer;
    static int enemyCount = 10;
    static int level = 1;
    static int socks = 30;
    static float timer = 300;
    static float timerMax = timer;
    static char socksSpace;
    static char socksSpace2;
    static char timerSpace;
    static readonly List<Gnome> gnomes = new();
    static readonly List<Gnight> gnights = new();
    static readonly List<Gizard> gizards = new();
    static readonly List<Projectile> projectiles = new();


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
    
    static void Start()
    {
        Audio.Load("gargoyle_pain", "Audio/gargoylepain.wav");
        Audio.Load("gargoyle_roar", "Audio/gargoyleroar.wav");
        Audio.Load("shatter", "Audio/shatter.wav"); 
        Audio.Load("misc_sound", "Audio/miscsound.wav");
        Audio.Load("foot_step", "Audio/footstep.wav"); // ✅
        Audio.Load("hit_sound", "Audio/hitsound.wav");
        Audio.Load("level_complete", "Audio/levelcomplete.wav"); // ✅
        Audio.Load("game_over", "Audio/gameover.wav"); // ✅
        Audio.Load("gnome_laugh", "Audio/gnomelaugh.wav"); // ✅
        Audio.Load("place", "Audio/gnomeplace.wav"); // ✅
        Audio.Load("gnome_hurt", "Audio/gnomepain.wav"); 
    }
    static void Main()
    {
        Start();
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
        //Code to keep screen current without flashing
        Console.SetCursorPosition(0, 0);
        

        //Code for making UI stay clean
        if (socks < 10)
        {
            socksSpace = ' ';
        }
        else if (socks >= 10)
        {
            socksSpace = '\0';
        }
        if (socks < 99)
        {
            socksSpace2 = ' ';
        }
        else if (socks >= 100)
        {
            socksSpace = '\0';
        }
        if(timer < 100)
        {
            timerSpace = ' ';
        }
        else if(timer >= 100)
        {
            timerSpace = '\0';
        }


        //Code to make gnomes die :O
        foreach (var g in gnomes)
        {
            if (g.Health <= 0)
            {
                g.IsAlive = false;
            }
        }

        foreach (var g in gnights)
        {
            if (g.Health <= 0)
            {
                g.IsAlive = false;
            }
        }

        foreach (var g in gizards)
        {
            if (g.Health <= 0)
            {
                g.IsAlive = false;
            }
        }

        projectiles.RemoveAll(p => p.hasHit);

        gnomes.RemoveAll(g => !g.IsAlive);

        gizards.RemoveAll(g => !g.IsAlive);

        gnights.RemoveAll(g => !g.IsAlive);

        if (!isPlaying && !hasLost && !hasWon)
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
            Console.WriteLine(@"▒░▒│ /\, Gnome                  │▒│/[/ Gaurdgoyle              │▒░▒");
            Console.WriteLine(@"▒░▒│ ouo (average melee unit)   │▒│ΘΘ£                         │▒░▒");
            Console.WriteLine(@"▒░▒│ rr)                        │▒│                            │▒░▒");
            Console.WriteLine(@"░▒░└────────────────────────────┘░└────────────────────────────┘░▒░");
            Console.WriteLine(@"░▒░┌────────────────────────────┐░┌────────────────────────────┐░▒░");
            Console.WriteLine(@"▒░▒│ /Σ, Gnight                 │▒│                            │▒░▒");
            Console.WriteLine(@"▒░▒│ ò∩ó (fast melee unit)      │▒│                            │▒░▒");
            Console.WriteLine(@"▒░▒│ (+≡                        │▒│                            │▒░▒");
            Console.WriteLine(@"░▒░└────────────────────────────┘░└────────────────────────────┘░▒░");
            Console.WriteLine(@"░▒░┌────────────────────────────┐░┌────────────────────────────┐░▒░");
            Console.WriteLine(@"▒░▒│ /^\ Gnomagician            │▒│                            │▒░▒");
            Console.WriteLine(@"▒░▒│ 0¬0 (slow ranged unit)     │▒│                            │▒░▒");
            Console.WriteLine(@"▒░▒│ ¿:¥                        │▒│                            │▒░▒");
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


        if (isPlaying && !hasLost && !hasWon)
        {
            DrawGrid();
            DrawGargoyles(); // Draws Gargoles first to set the stage
            DrawGnomes();    // Draws the default GNOMES
            DrawGizards(); //Draws the Gizards
            DrawGnights(); //Draws the Gnights
            DrawProjectiles(); //Draws the Projectiles


            tickTimer++;
            if (tickTimer >= 2 && timer > 0)
            {
                tickTimer = 0;
                timer--;
            }

            gnomeMoveTimer++;
            if (gnomeMoveTimer >= movePeriod)
            {
                foreach (var g in gnomes.Where(x => x.IsAlive))
                {
                    if (g.Row >= 4)
                    {
                        gnomeMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 3 && gargIsAlive[5] && g.Lane == 1)
                    {
                        gnomeMoveTimer = 0;
                        gargHealth[5] -= gnomeDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 3 && gargIsAlive[6] && g.Lane == 2)
                    {
                        gnomeMoveTimer = 0;
                        gargHealth[6] -= gnomeDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 3 && gargIsAlive[7] && g.Lane == 3)
                    {
                        gnomeMoveTimer = 0;
                        gargHealth[7] -= gnomeDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 3 && gargIsAlive[8] && g.Lane == 4)
                    {
                        gnomeMoveTimer = 0;
                        gargHealth[8] -= gnomeDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 3 && gargIsAlive[9] && g.Lane == 5)
                    {
                        gnomeMoveTimer = 0;
                        gargHealth[9] -= gnomeDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 2 && gargIsAlive[0] && g.Lane == 1)
                    {
                        gnomeMoveTimer = 0;
                        gargHealth[0] -= gnomeDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 2 && gargIsAlive[1] && g.Lane == 2)
                    {
                        gnomeMoveTimer = 0;
                        gargHealth[1] -= gnomeDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 2 && gargIsAlive[2] && g.Lane == 3)
                    {
                        gnomeMoveTimer = 0;
                        gargHealth[2] -= gnomeDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 2 && gargIsAlive[3] && g.Lane == 4)
                    {
                        gnomeMoveTimer = 0;
                        gargHealth[3] -= gnomeDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 2 && gargIsAlive[4] && g.Lane == 5)
                    {
                        gnomeMoveTimer = 0;
                        gargHealth[4] -= gnomeDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 3 && !gargIsAlive[5] && g.Lane == 1)
                    {
                        gnomeMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 3 && !gargIsAlive[6] && g.Lane == 2)
                    {
                        gnomeMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 3 && !gargIsAlive[7] && g.Lane == 3)
                    {
                        gnomeMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 3 && !gargIsAlive[8] && g.Lane == 4)
                    {
                        gnomeMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 3 && !gargIsAlive[9] && g.Lane == 5)
                    {
                        gnomeMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 2 && !gargIsAlive[0] && g.Lane == 1)
                    {
                        gnomeMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 2 && !gargIsAlive[1] && g.Lane == 2)
                    {
                        gnomeMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 2 && !gargIsAlive[2] && g.Lane == 3)
                    {
                        gnomeMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 2 && !gargIsAlive[3] && g.Lane == 4)
                    {
                        gnomeMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 2 && !gargIsAlive[4] && g.Lane == 5)
                    {
                        gnomeMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                }
            }

            gizardMoveTimer++;
            if (gizardMoveTimer >= gizardMovePeriod)
            {
                foreach (var g in gizards.Where(x => x.IsAlive))
                {
                    if (g.Row >= 5)
                    {
                        gizardMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row <= 4)
                    {
                        if (g.Lane == 1)
                        {
                            if (gargIsAlive[5] || gargIsAlive[0])
                            {
                                gizardMoveTimer = 0;
                                SpawnProjectile(1, 0);
                            }
                        }
                        else if (g.Lane == 2)
                        {
                            if (gargIsAlive[6] || gargIsAlive[1])
                            {
                                gizardMoveTimer = 0;
                                SpawnProjectile(2, 0);
                            }

                        }
                        else if (g.Lane == 3)
                        {
                            if (gargIsAlive[7] || gargIsAlive[2])
                            {
                                gizardMoveTimer = 0;
                                SpawnProjectile(3, 0);
                            }

                        }
                        else if (g.Lane == 4)
                        {
                            if (gargIsAlive[8] || gargIsAlive[3])
                            {
                                gizardMoveTimer = 0;
                                SpawnProjectile(4, 0);
                            }

                        }
                        else if (g.Lane == 5)
                        {
                            if (gargIsAlive[9] || gargIsAlive[4])
                            {
                                gizardMoveTimer = 0;
                                SpawnProjectile(5, 0);
                            }

                        }
                    }
                }
            }

            gnightMoveTimer++;
            if (gnightMoveTimer >= gnightMovePeriod)
            {
                foreach (var g in gnights.Where(x => x.IsAlive))
                {
                    if (g.Row >= 4)
                    {
                        gnightMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 3 && gargIsAlive[5] && g.Lane == 1)
                    {
                        gnightMoveTimer = 0;
                        gargHealth[5] -= gnightDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 3 && gargIsAlive[6] && g.Lane == 2)
                    {
                        gnightMoveTimer = 0;
                        gargHealth[6] -= gnightDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 3 && gargIsAlive[7] && g.Lane == 3)
                    {
                        gnightMoveTimer = 0;
                        gargHealth[7] -= gnightDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 3 && gargIsAlive[8] && g.Lane == 4)
                    {
                        gnightMoveTimer = 0;
                        gargHealth[8] -= gnightDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 3 && gargIsAlive[9] && g.Lane == 5)
                    {
                        gnightMoveTimer = 0;
                        gargHealth[9] -= gnightDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 2 && gargIsAlive[0] && g.Lane == 1)
                    {
                        gnightMoveTimer = 0;
                        gargHealth[0] -= gnightDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 2 && gargIsAlive[1] && g.Lane == 2)
                    {
                        gnightMoveTimer = 0;
                        gargHealth[1] -= gnightDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 2 && gargIsAlive[2] && g.Lane == 3)
                    {
                        gnightMoveTimer = 0;
                        gargHealth[2] -= gnightDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 2 && gargIsAlive[3] && g.Lane == 4)
                    {
                        gnightMoveTimer = 0;
                        gargHealth[3] -= gnightDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 2 && gargIsAlive[4] && g.Lane == 5)
                    {
                        gnightMoveTimer = 0;
                        gargHealth[4] -= gnightDamage;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (g.Row == 3 && !gargIsAlive[5] && g.Lane == 1)
                    {
                        gnightMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 3 && !gargIsAlive[6] && g.Lane == 2)
                    {
                        gnightMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 3 && !gargIsAlive[7] && g.Lane == 3)
                    {
                        gnightMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 3 && !gargIsAlive[8] && g.Lane == 4)
                    {
                        gnightMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 3 && !gargIsAlive[9] && g.Lane == 5)
                    {
                        gnightMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 2 && !gargIsAlive[0] && g.Lane == 1)
                    {
                        gnightMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 2 && !gargIsAlive[1] && g.Lane == 2)
                    {
                        gnightMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 2 && !gargIsAlive[2] && g.Lane == 3)
                    {
                        gnightMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 2 && !gargIsAlive[3] && g.Lane == 4)
                    {
                        gnightMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
                    else if (g.Row == 2 && !gargIsAlive[4] && g.Lane == 5)
                    {
                        gnightMoveTimer = 0;
                        g.Row--;
                        Audio.Play("foot_step");
                    }
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

            //Gnome spawning input
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true);
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    isPlacing = true;
                    gnomePlacing = 1;
                }
                if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {
                    isPlacing = true;
                    gnomePlacing = 2;
                }
                if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                {
                    isPlacing = true;
                    gnomePlacing = 3;
                }

                if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter)
                {
                    if (gnomePlacing == 1 && socks >= 3)
                    {
                        isPlacing = false;
                        socks -= 3;
                        SpawnGnome(laneSelecting, 9, 4);
                        Audio.Play("place");
                    }
                    if (gnomePlacing == 2 && socks >= 6)
                    {
                        isPlacing = false;
                        socks -= 6;
                        SpawnGnight(laneSelecting, 9, 3);
                        Audio.Play("place");
                    }
                    if (gnomePlacing == 3 && socks >= 3)
                    {
                        isPlacing = false;
                        socks -= 9;
                        SpawnGizard(laneSelecting, 9, 4);
                        Audio.Play("gnome_laugh");
                    }
                }

                if (key.Key == ConsoleKey.LeftArrow && laneSelecting > 1)
                {
                    laneSelecting--;
                    Row27[1] = ' ';
                    Row27[2] = ' ';
                    Row27[3] = ' ';
                    Row26[1] = ' ';
                    Row26[2] = ' ';
                    Row26[3] = ' ';
                    Row27[5] = ' ';
                    Row27[6] = ' ';
                    Row27[7] = ' ';
                    Row26[5] = ' ';
                    Row26[6] = ' ';
                    Row26[7] = ' ';
                    Row27[9] = ' ';
                    Row27[10] = ' ';
                    Row27[11] = ' ';
                    Row26[9] = ' ';
                    Row26[10] = ' ';
                    Row26[11] = ' ';
                    Row27[13] = ' ';
                    Row27[14] = ' ';
                    Row27[15] = ' ';
                    Row26[13] = ' ';
                    Row26[14] = ' ';
                    Row26[15] = ' ';
                    Row27[17] = ' ';
                    Row27[18] = ' ';
                    Row27[19] = ' ';
                    Row26[17] = ' ';
                    Row26[18] = ' ';
                    Row26[19] = ' ';
                }

                if (key.Key == ConsoleKey.RightArrow && laneSelecting < 5)
                {
                    laneSelecting++;
                    Row26[1] = ' ';
                    Row26[2] = ' ';
                    Row26[3] = ' ';
                    Row27[1] = ' ';
                    Row27[2] = ' ';
                    Row27[3] = ' ';
                    Row26[5] = ' ';
                    Row26[6] = ' ';
                    Row26[7] = ' ';
                    Row27[5] = ' ';
                    Row27[6] = ' ';
                    Row27[7] = ' ';
                    Row26[9] = ' ';
                    Row26[10] = ' ';
                    Row26[11] = ' ';
                    Row27[9] = ' ';
                    Row27[10] = ' ';
                    Row27[11] = ' ';
                    Row26[13] = ' ';
                    Row26[14] = ' ';
                    Row26[15] = ' ';
                    Row27[13] = ' ';
                    Row27[14] = ' ';
                    Row27[15] = ' ';
                    Row26[17] = ' ';
                    Row26[18] = ' ';
                    Row26[19] = ' ';
                    Row27[17] = ' ';
                    Row27[18] = ' ';
                    Row27[19] = ' ';
                }
            }

            if (isPlacing)
            {
                laneFlashing++;
                if (laneFlashing == 1)
                {
                    isFlash = !isFlash;
                    laneFlashing = 0;
                }


                if (laneSelecting == 1)
                {
                    if (isFlash)
                    {
                        Row27[1] = '▓';
                        Row27[2] = '▓';
                        Row27[3] = '▓';
                        Row26[1] = '▓';
                        Row26[2] = '▓';
                        Row26[3] = '▓';
                    }
                    if (!isFlash)
                    {
                        Row27[1] = ' ';
                        Row27[2] = ' ';
                        Row27[3] = ' ';
                        Row26[1] = ' ';
                        Row26[2] = ' ';
                        Row26[3] = ' ';
                    }
                }
                else if (laneSelecting == 2)
                {
                    if (isFlash)
                    {
                        Row27[5] = '▓';
                        Row27[6] = '▓';
                        Row27[7] = '▓';
                        Row26[5] = '▓';
                        Row26[6] = '▓';
                        Row26[7] = '▓';
                    }
                    if (!isFlash)
                    {
                        Row27[5] = ' ';
                        Row27[6] = ' ';
                        Row27[7] = ' ';
                        Row26[5] = ' ';
                        Row26[6] = ' ';
                        Row26[7] = ' ';
                    }
                }
                else if (laneSelecting == 3)
                {
                    if (isFlash)
                    {
                        Row27[9] = '▓';
                        Row27[10] = '▓';
                        Row27[11] = '▓';
                        Row26[9] = '▓';
                        Row26[10] = '▓';
                        Row26[11] = '▓';
                    }
                    if (!isFlash)
                    {
                        Row27[9] = ' ';
                        Row27[10] = ' ';
                        Row27[11] = ' ';
                        Row26[9] = ' ';
                        Row26[10] = ' ';
                        Row26[11] = ' ';
                    }
                }
                else if (laneSelecting == 4)
                {
                    if (isFlash)
                    {
                        Row27[13] = '▓';
                        Row27[14] = '▓';
                        Row27[15] = '▓';
                        Row26[13] = '▓';
                        Row26[14] = '▓';
                        Row26[15] = '▓';
                    }
                    if (!isFlash)
                    {
                        Row27[13] = ' ';
                        Row27[14] = ' ';
                        Row27[15] = ' ';
                        Row26[13] = ' ';
                        Row26[14] = ' ';
                        Row26[15] = ' ';
                    }
                }
                else if (laneSelecting == 5)
                {
                    if (isFlash)
                    {
                        Row27[17] = '▓';
                        Row27[18] = '▓';
                        Row27[19] = '▓';
                        Row26[17] = '▓';
                        Row26[18] = '▓';
                        Row26[19] = '▓';
                    }
                    if (!isFlash)
                    {
                        Row27[17] = ' ';
                        Row27[18] = ' ';
                        Row27[19] = ' ';
                        Row26[17] = ' ';
                        Row26[18] = ' ';
                        Row26[19] = ' ';
                    }
                }


            }



            //Printing each line (Brackets solely so you can collapse it) 
            Console.Write("║ ░Socks░ ██>  ░▒▒▓▌║"); Console.Write(new string(Row1)); Console.WriteLine("║▐▓▒▒░  <██ ░Timer░ ║");
            Console.Write("║   «ß»   ██> ░▒▒▓▓▌║"); Console.Write(new string(Row2)); Console.WriteLine("║▐▓▓▒▒░ <██   «ö»   ║");
            Console.Write($"║  ░{socks}░{socksSpace}{socksSpace2}  ██> ░▒▒▓▓▌║"); Console.Write(new string(Row3)); Console.WriteLine($"║▐▓▓▒▒░ <██░{timer}/{timerMax}{timerSpace} ║");
            Console.Write("∙========<██> ░▒▒▓▒▌║"); Console.Write(new string(Row4)); Console.WriteLine("║▐▓▒▒▒░ <██         ║");
            Console.Write("          ██> ░░▒▓▓▌║"); Console.Write(new string(Row5)); Console.WriteLine("║▐▓▒▒░░ <██░Enemies░║");
            Console.Write("          ██> ░░▒▓▓▌║"); Console.Write(new string(Row6)); Console.WriteLine("║▐▓▓▒░░ <██   «φ»   ║");
            Console.Write("          ██> ░░▒▓▓▌║"); Console.Write(new string(Row7)); Console.WriteLine($"║▐▓▓▒░░ <██  ░{enemyCount}/??░║");
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
            Console.Write("     O║█║║██> ░▒▒▓▒▌║"); Console.Write(new string(Row23)); Console.WriteLine("║▐▓▒▒▒░ <██║Press ↓ Recruit║");
            Console.Write("     L║█║║██> ░░▒▓▓▌║"); Console.Write(new string(Row24)); Console.WriteLine("║▐▓▒▒░░ <██║╔|1|╗╔|2|╗╔|3|╗║");
            Console.Write("     D║█║║██> ░░▒▓▓▌║"); Console.Write(new string(Row25)); Console.WriteLine("║▐▓▓▒░░ <██║ /\\,  /Σ,  /^\\ ║");
            Console.Write("     O║█║║██> ░░▒▓▓▌║"); Console.Write(new string(Row26)); Console.WriteLine("║▐▓▓▒░░ <██║ ouo  ò∩ó  0¬0 ║");
            Console.Write("     W║█║║██> ░▒▒▓▓▌║"); Console.Write(new string(Row27)); Console.WriteLine("║▐▓▓▒▒░ <██║╚   ╝╚   ╝╚   ╝║");
            Console.Write("     N║█║║██> ░░▒▓▓▌║"); Console.Write(new string(Row28)); Console.WriteLine("║▐▓▓▒░░ <██║«ß»3 «ß»6 «ß»9 ║");
            Console.WriteLine("      ╚═╝║███████████████████████████████████████████║░░░░░░░░░░░░░░░║");
        }

        if (hasLost)
        {

            Audio.Play("game_over");

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
            Console.WriteLine("                                                   ");
            Console.WriteLine("                                                   ");
            Console.WriteLine("                                                   ");
            Console.WriteLine("                                                   ");
            Console.WriteLine("                                                   ");

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
            Console.WriteLine("                             ▓▓▓    ███  ███           ");
            Console.WriteLine("                                  █▓███  ███▓█         ");
            Console.WriteLine("                                  █████  █████         ");
            Console.WriteLine("                                                       ");
            Console.WriteLine($"  Score - {(socks * 10) + (timer * 5)}                            ");
            Console.WriteLine("                                                       ");
            Console.WriteLine($"   Time - {timer}                                         ");
            Console.WriteLine("                                                       ");
            Console.WriteLine("                                                       ");
            Console.WriteLine("                                                       ");
            Console.WriteLine("                                                       ");
            Console.WriteLine("                                                       ");

            Audio.Play("level_complete");
        }

    }



    public static Gnome SpawnGnome(int lane, int row, int health)
    {
        var g = new Gnome(lane, row, health);
        gnomes.Add(g);
        return g;
    }
    public static Gnight SpawnGnight(int lane, int row, int health)
    {
        var g = new Gnight (lane, row, health);
        gnights.Add(g);
        return g;
    }
    public static Gizard SpawnGizard(int lane, int row, int health)
    {
        var g = new Gizard (lane, row, health);
        gizards.Add(g);
        return g;
    }

    public static Projectile SpawnProjectile(int lane, int stage)
    {
        var p = new Projectile(lane, stage);
        projectiles.Add(p);
        return p;
    }



    static void DrawGargoyles()
    {
        if (gargHealth[0] <= 0) gargIsAlive[0] = false;
        if (gargHealth[1] <= 0) gargIsAlive[1] = false;
        if (gargHealth[2] <= 0) gargIsAlive[2] = false;
        if (gargHealth[3] <= 0) gargIsAlive[3] = false;
        if (gargHealth[4] <= 0) gargIsAlive[4] = false;
        if (gargHealth[5] <= 0) gargIsAlive[5] = false;
        if (gargHealth[6] <= 0) gargIsAlive[6] = false;
        if (gargHealth[7] <= 0) gargIsAlive[7] = false;
        if (gargHealth[8] <= 0) gargIsAlive[8] = false;
        if (gargHealth[9] <= 0) gargIsAlive[9] = false;
        if (gargHealth[10] <= 0) gargIsAlive[10] = false;
        if (gargHealth[11] <= 0) gargIsAlive[11] = false;
        if (gargHealth[12] <= 0) gargIsAlive[12] = false;
        if (gargHealth[13] <= 0) gargIsAlive[13] = false;
        if (gargHealth[14] <= 0) gargIsAlive[14] = false;





    }
    static void DrawGrid()
    {
        Row1[0] = '+'; Row1[1] = '-'; Row1[2] = '-'; Row1[3] = '-'; Row1[4] = '+'; Row1[5] = '-'; Row1[6] = '-'; Row1[7] = '-'; Row1[8] = '+'; Row1[9] = '-'; Row1[10] = '-'; Row1[11] = '-'; Row1[12] = '+'; Row1[13] = '-'; Row1[14] = '-'; Row1[15] = '-'; Row1[16] = '+'; Row1[17] = '-'; Row1[18] = '-'; Row1[19] = '-'; Row1[20] = '+';

        Row2[0] = '|'; Row2[1] = ' '; Row2[2] = ' '; Row2[3] = ' '; Row2[4] = '|'; Row2[5] = ' '; Row2[6] = ' '; Row2[7] = ' '; Row2[8] = '|'; Row2[9] = ' '; Row2[10] = ' '; Row2[11] = ' '; Row2[12] = '|'; Row2[13] = ' '; Row2[14] = ' '; Row2[15] = ' '; Row2[16] = '|'; Row2[17] = ' '; Row2[18] = ' '; Row2[19] = ' '; Row2[20] = '|';

        Row3[0] = '|'; Row3[1] = ' '; Row3[2] = ' '; Row3[3] = ' '; Row3[4] = '|'; Row3[5] = ' '; Row3[6] = ' '; Row3[7] = ' '; Row3[8] = '|'; Row3[9] = ' '; Row3[10] = ' '; Row3[11] = ' '; Row3[12] = '|'; Row3[13] = ' '; Row3[14] = ' '; Row3[15] = ' '; Row3[16] = '|'; Row3[17] = ' '; Row3[18] = ' '; Row3[19] = ' '; Row3[20] = '|';

        Row4[0] = '+'; Row4[1] = '-'; Row4[2] = '-'; Row4[3] = '-'; Row4[4] = '+'; Row4[5] = '-'; Row4[6] = '-'; Row4[7] = '-'; Row4[8] = '+'; Row4[9] = '-'; Row4[10] = '-'; Row4[11] = '-'; Row4[12] = '+'; Row4[13] = '-'; Row4[14] = '-'; Row4[15] = '-'; Row4[16] = '+'; Row4[17] = '-'; Row4[18] = '-'; Row4[19] = '-'; Row4[20] = '+';

        Row5[0] = '|'; Row5[1] = ' '; Row5[2] = ' '; Row5[3] = ' '; Row5[4] = '|'; Row5[5] = ' '; Row5[6] = ' '; Row5[7] = ' '; Row5[8] = '|'; Row5[9] = ' '; Row5[10] = ' '; Row5[11] = ' '; Row5[12] = '|'; Row5[13] = ' '; Row5[14] = ' '; Row5[15] = ' '; Row5[16] = '|'; Row5[17] = ' '; Row5[18] = ' '; Row5[19] = ' '; Row5[20] = '|';

        Row6[0] = '|'; Row6[1] = ' '; Row6[2] = ' '; Row6[3] = ' '; Row6[4] = '|'; Row6[5] = ' '; Row6[6] = ' '; Row6[7] = ' '; Row6[8] = '|'; Row6[9] = ' '; Row6[10] = ' '; Row6[11] = ' '; Row6[12] = '|'; Row6[13] = ' '; Row6[14] = ' '; Row6[15] = ' '; Row6[16] = '|'; Row6[17] = ' '; Row6[18] = ' '; Row6[19] = ' '; Row6[20] = '|';

        Row7[0] = '+'; Row7[1] = '-'; Row7[2] = '-'; Row7[3] = '-'; Row7[4] = '+'; Row7[5] = '-'; Row7[6] = '-'; Row7[7] = '-'; Row7[8] = '+'; Row7[9] = '-'; Row7[10] = '-'; Row7[11] = '-'; Row7[12] = '+'; Row7[13] = '-'; Row7[14] = '-'; Row7[15] = '-'; Row7[16] = '+'; Row7[17] = '-'; Row7[18] = '-'; Row7[19] = '-'; Row7[20] = '+';

        Row8[0] = '|'; Row8[1] = ' '; Row8[2] = ' '; Row8[3] = ' '; Row8[4] = '|'; Row8[5] = ' '; Row8[6] = ' '; Row8[7] = ' '; Row8[8] = '|'; Row8[9] = ' '; Row8[10] = ' '; Row8[11] = ' '; Row8[12] = '|'; Row8[13] = ' '; Row8[14] = ' '; Row8[15] = ' '; Row8[16] = '|'; Row8[17] = ' '; Row8[18] = ' '; Row8[19] = ' '; Row8[20] = '|';

        Row9[0] = '|'; Row9[1] = ' '; Row9[2] = ' '; Row9[3] = ' '; Row9[4] = '|'; Row9[5] = ' '; Row9[6] = ' '; Row9[7] = ' '; Row9[8] = '|'; Row9[9] = ' '; Row9[10] = ' '; Row9[11] = ' '; Row9[12] = '|'; Row9[13] = ' '; Row9[14] = ' '; Row9[15] = ' '; Row9[16] = '|'; Row9[17] = ' '; Row9[18] = ' '; Row9[19] = ' '; Row9[20] = '|';

        Row10[0] = '+'; Row10[1] = '-'; Row10[2] = '-'; Row10[3] = '-'; Row10[4] = '+'; Row10[5] = '-'; Row10[6] = '-'; Row10[7] = '-'; Row10[8] = '+'; Row10[9] = '-'; Row10[10] = '-'; Row10[11] = '-'; Row10[12] = '+'; Row10[13] = '-'; Row10[14] = '-'; Row10[15] = '-'; Row10[16] = '+'; Row10[17] = '-'; Row10[18] = '-'; Row10[19] = '-'; Row10[20] = '+';

        Row11[0] = '|'; Row11[1] = ' '; Row11[2] = ' '; Row11[3] = ' '; Row11[4] = '|'; Row11[5] = ' '; Row11[6] = ' '; Row11[7] = ' '; Row11[8] = '|'; Row11[9] = ' '; Row11[10] = ' '; Row11[11] = ' '; Row11[12] = '|'; Row11[13] = ' '; Row11[14] = ' '; Row11[15] = ' '; Row11[16] = '|'; Row11[17] = ' '; Row11[18] = ' '; Row11[19] = ' '; Row11[20] = '|';

        Row12[0] = '|'; Row12[1] = ' '; Row12[2] = ' '; Row12[3] = ' '; Row12[4] = '|'; Row12[5] = ' '; Row12[6] = ' '; Row12[7] = ' '; Row12[8] = '|'; Row12[9] = ' '; Row12[10] = ' '; Row12[11] = ' '; Row12[12] = '|'; Row12[13] = ' '; Row12[14] = ' '; Row12[15] = ' '; Row12[16] = '|'; Row12[17] = ' '; Row12[18] = ' '; Row12[19] = ' '; Row12[20] = '|';

        Row13[0] = '+'; Row13[1] = '-'; Row13[2] = '-'; Row13[3] = '-'; Row13[4] = '+'; Row13[5] = '-'; Row13[6] = '-'; Row13[7] = '-'; Row13[8] = '+'; Row13[9] = '-'; Row13[10] = '-'; Row13[11] = '-'; Row13[12] = '+'; Row13[13] = '-'; Row13[14] = '-'; Row13[15] = '-'; Row13[16] = '+'; Row13[17] = '-'; Row13[18] = '-'; Row13[19] = '-'; Row13[20] = '+';

        Row14[0] = '|'; Row14[1] = ' '; Row14[2] = ' '; Row14[3] = ' '; Row14[4] = '|'; Row14[5] = ' '; Row14[6] = ' '; Row14[7] = ' '; Row14[8] = '|'; Row14[9] = ' '; Row14[10] = ' '; Row14[11] = ' '; Row14[12] = '|'; Row14[13] = ' '; Row14[14] = ' '; Row14[15] = ' '; Row14[16] = '|'; Row14[17] = ' '; Row14[18] = ' '; Row14[19] = ' '; Row14[20] = '|';

        Row15[0] = '|'; Row15[1] = ' '; Row15[2] = ' '; Row15[3] = ' '; Row15[4] = '|'; Row15[5] = ' '; Row15[6] = ' '; Row15[7] = ' '; Row15[8] = '|'; Row15[9] = ' '; Row15[10] = ' '; Row15[11] = ' '; Row15[12] = '|'; Row15[13] = ' '; Row15[14] = ' '; Row15[15] = ' '; Row15[16] = '|'; Row15[17] = ' '; Row15[18] = ' '; Row15[19] = ' '; Row15[20] = '|';

        Row16[0] = '+'; Row16[1] = '-'; Row16[2] = '-'; Row16[3] = '-'; Row16[4] = '+'; Row16[5] = '-'; Row16[6] = '-'; Row16[7] = '-'; Row16[8] = '+'; Row16[9] = '-'; Row16[10] = '-'; Row16[11] = '-'; Row16[12] = '+'; Row16[13] = '-'; Row16[14] = '-'; Row16[15] = '-'; Row16[16] = '+'; Row16[17] = '-'; Row16[18] = '-'; Row16[19] = '-'; Row16[20] = '+';

        Row17[0] = '|'; Row17[1] = ' '; Row17[2] = ' '; Row17[3] = ' '; Row17[4] = '|'; Row17[5] = ' '; Row17[6] = ' '; Row17[7] = ' '; Row17[8] = '|'; Row17[9] = ' '; Row17[10] = ' '; Row17[11] = ' '; Row17[12] = '|'; Row17[13] = ' '; Row17[14] = ' '; Row17[15] = ' '; Row17[16] = '|'; Row17[17] = ' '; Row17[18] = ' '; Row17[19] = ' '; Row17[20] = '|';

        Row18[0] = '|'; Row18[1] = ' '; Row18[2] = ' '; Row18[3] = ' '; Row18[4] = '|'; Row18[5] = ' '; Row18[6] = ' '; Row18[7] = ' '; Row18[8] = '|'; Row18[9] = ' '; Row18[10] = ' '; Row18[11] = ' '; Row18[12] = '|'; Row18[13] = ' '; Row18[14] = ' '; Row18[15] = ' '; Row18[16] = '|'; Row18[17] = ' '; Row18[18] = ' '; Row18[19] = ' '; Row18[20] = '|';

        Row19[0] = '+'; Row19[1] = '-'; Row19[2] = '-'; Row19[3] = '-'; Row19[4] = '+'; Row19[5] = '-'; Row19[6] = '-'; Row19[7] = '-'; Row19[8] = '+'; Row19[9] = '-'; Row19[10] = '-'; Row19[11] = '-'; Row19[12] = '+'; Row19[13] = '-'; Row19[14] = '-'; Row19[15] = '-'; Row19[16] = '+'; Row19[17] = '-'; Row19[18] = '-'; Row19[19] = '-'; Row19[20] = '+';

        Row20[0] = '|'; Row20[1] = ' '; Row20[2] = ' '; Row20[3] = ' '; Row20[4] = '|'; Row20[5] = ' '; Row20[6] = ' '; Row20[7] = ' '; Row20[8] = '|'; Row20[9] = ' '; Row20[10] = ' '; Row20[11] = ' '; Row20[12] = '|'; Row20[13] = ' '; Row20[14] = ' '; Row20[15] = ' '; Row20[16] = '|'; Row20[17] = ' '; Row20[18] = ' '; Row20[19] = ' '; Row20[20] = '|';

        Row21[0] = '|'; Row21[1] = ' '; Row21[2] = ' '; Row21[3] = ' '; Row21[4] = '|'; Row21[5] = ' '; Row21[6] = ' '; Row21[7] = ' '; Row21[8] = '|'; Row21[9] = ' '; Row21[10] = ' '; Row21[11] = ' '; Row21[12] = '|'; Row21[13] = ' '; Row21[14] = ' '; Row21[15] = ' '; Row21[16] = '|'; Row21[17] = ' '; Row21[18] = ' '; Row21[19] = ' '; Row21[20] = '|';

        Row22[0] = '+'; Row22[1] = '-'; Row22[2] = '-'; Row22[3] = '-'; Row22[4] = '+'; Row22[5] = '-'; Row22[6] = '-'; Row22[7] = '-'; Row22[8] = '+'; Row22[9] = '-'; Row22[10] = '-'; Row22[11] = '-'; Row22[12] = '+'; Row22[13] = '-'; Row22[14] = '-'; Row22[15] = '-'; Row22[16] = '+'; Row22[17] = '-'; Row22[18] = '-'; Row22[19] = '-'; Row22[20] = '+';

        Row23[0] = '|'; Row23[1] = ' '; Row23[2] = ' '; Row23[3] = ' '; Row23[4] = '|'; Row23[5] = ' '; Row23[6] = ' '; Row23[7] = ' '; Row23[8] = '|'; Row23[9] = ' '; Row23[10] = ' '; Row23[11] = ' '; Row23[12] = '|'; Row23[13] = ' '; Row23[14] = ' '; Row23[15] = ' '; Row23[16] = '|'; Row23[17] = ' '; Row23[18] = ' '; Row23[19] = ' '; Row23[20] = '|';

        Row24[0] = '|'; Row24[1] = ' '; Row24[2] = ' '; Row24[3] = ' '; Row24[4] = '|'; Row24[5] = ' '; Row24[6] = ' '; Row24[7] = ' '; Row24[8] = '|'; Row24[9] = ' '; Row24[10] = ' '; Row24[11] = ' '; Row24[12] = '|'; Row24[13] = ' '; Row24[14] = ' '; Row24[15] = ' '; Row24[16] = '|'; Row24[17] = ' '; Row24[18] = ' '; Row24[19] = ' '; Row24[20] = '|';

        Row25[0] = '+'; Row25[1] = '-'; Row25[2] = '-'; Row25[3] = '-'; Row25[4] = '+'; Row25[5] = '-'; Row25[6] = '-'; Row25[7] = '-'; Row25[8] = '+'; Row25[9] = '-'; Row25[10] = '-'; Row25[11] = '-'; Row25[12] = '+'; Row25[13] = '-'; Row25[14] = '-'; Row25[15] = '-'; Row25[16] = '+'; Row25[17] = '-'; Row25[18] = '-'; Row25[19] = '-'; Row25[20] = '+';

        Row26[0] = '|'; Row26[1] = ' '; Row26[2] = ' '; Row26[3] = ' '; Row26[4] = '|'; Row26[5] = ' '; Row26[6] = ' '; Row26[7] = ' '; Row26[8] = '|'; Row26[9] = ' '; Row26[10] = ' '; Row26[11] = ' '; Row26[12] = '|'; Row26[13] = ' '; Row26[14] = ' '; Row26[15] = ' '; Row26[16] = '|'; Row26[17] = ' '; Row26[18] = ' '; Row26[19] = ' '; Row26[20] = '|';

        Row27[0] = '|'; Row27[1] = ' '; Row27[2] = ' '; Row27[3] = ' '; Row27[4] = '|'; Row27[5] = ' '; Row27[6] = ' '; Row27[7] = ' '; Row27[8] = '|'; Row27[9] = ' '; Row27[10] = ' '; Row27[11] = ' '; Row27[12] = '|'; Row27[13] = ' '; Row27[14] = ' '; Row27[15] = ' '; Row27[16] = '|'; Row27[17] = ' '; Row27[18] = ' '; Row27[19] = ' '; Row27[20] = '|';

        Row28[0] = '+'; Row28[1] = '-'; Row28[2] = '-'; Row28[3] = '-'; Row28[4] = '+'; Row28[5] = '-'; Row28[6] = '-'; Row28[7] = '-'; Row28[8] = '+'; Row28[9] = '-'; Row28[10] = '-'; Row28[11] = '-'; Row28[12] = '+'; Row28[13] = '-'; Row28[14] = '-'; Row28[15] = '-'; Row28[16] = '+'; Row28[17] = '-'; Row28[18] = '-'; Row28[19] = '-'; Row28[20] = '+';

    }



    static void DrawProjectiles()
    {
        foreach (var p in projectiles)
        {
            
            p.Stage++;
            if (p.Lane == 1)
            {
                if (p.Stage == 1)
                {
                    Row10[2] = '¤';
                }
                else if (p.Stage == 2)
                {
                    Row10[2] = '-';
                    Row9[2] = '¤';
                }
                else if (p.Stage == 3)
                {
                    Row9[2] = ' ';
                    Row8[2] = '¤';
                }
                else if (p.Stage == 4)
                {
                    Row8[2] = ' ';
                    Row7[2] = '¤';
                }
                else if (p.Stage == 5)
                {
                    Row7[2] = '-';
                    if (gargIsAlive[5])
                    {
                        p.hasHit = true;
                        gargHealth[5] -= 1;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (!gargIsAlive[5])
                    {
                        Row6[2] = '¤';
                    }
                }
                else if (p.Stage == 6)
                {
                    Row6[2] = ' ';
                    Row5[2] = '¤';
                }
                else if (p.Stage == 7)
                {
                    Row6[2] = ' ';
                    Row5[2] = '¤';
                }
                else if (p.Stage == 8)
                {
                    Row5[2] = ' ';
                    Row4[2] = '¤';
                }
                else if (p.Stage == 9)
                {
                    Row4[2] = '-';
                    if (gargIsAlive[0])
                    {
                        p.hasHit = true;
                        gargHealth[0] -= 1;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (!gargIsAlive[0])
                    {
                        Row3[2] = '¤';
                    }
                }
                else if (p.Stage == 10)
                {
                    Row3[2] = ' ';
                    Row2[2] = '¤';
                }
                else if (p.Stage == 11)
                {
                    Row2[2] = ' ';
                    Row1[2] = '¤';
                }
                else if (p.Stage == 12)
                {
                    Row1[2] = '-';
                    p.hasHit = true;
                }


            }

            if (p.Lane == 2)
            {
                if (p.Stage == 1)
                {
                    Row10[6] = '¤';
                }
                else if (p.Stage == 2)
                {
                    Row10[6] = '-';
                    Row9[6] = '¤';
                }
                else if (p.Stage == 3)
                {
                    Row9[6] = ' ';
                    Row8[6] = '¤';
                }
                else if (p.Stage == 4)
                {
                    Row8[6] = ' ';
                    Row7[6] = '¤';
                }
                else if (p.Stage == 5)
                {
                    Row7[6] = '-';
                    if (gargIsAlive[6])
                    {
                        p.hasHit = true;
                        gargHealth[6] -= 1;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (!gargIsAlive[6])
                    {
                        Row6[6] = '¤';
                    }
                }
                else if (p.Stage == 6)
                {
                    Row6[6] = ' ';
                    Row5[6] = '¤';
                }
                else if (p.Stage == 7)
                {
                    Row6[6] = ' ';
                    Row5[6] = '¤';
                }
                else if (p.Stage == 8)
                {
                    Row5[6] = ' ';
                    Row4[6] = '¤';
                }
                else if (p.Stage == 9)
                {
                    Row4[6] = '-';
                    if (gargIsAlive[1])
                    {
                        p.hasHit = true;
                        gargHealth[1] -= 1;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (!gargIsAlive[1])
                    {
                        Row3[6] = '¤';
                    }
                }
                else if (p.Stage == 10)
                {
                    Row3[6] = ' ';
                    Row2[6] = '¤';
                }
                else if (p.Stage == 11)
                {
                    Row2[6] = ' ';
                    Row1[6] = '¤';
                }
                else if (p.Stage == 12)
                {
                    Row1[6] = '-';
                    p.hasHit = true;
                }
            }

            if (p.Lane == 3)
            {
                if (p.Stage == 1)
                {
                    Row10[10] = '¤';
                }
                else if (p.Stage == 2)
                {
                    Row10[10] = '-';
                    Row9[10] = '¤';
                }
                else if (p.Stage == 3)
                {
                    Row9[10] = ' ';
                    Row8[10] = '¤';
                }
                else if (p.Stage == 4)
                {
                    Row8[10] = ' ';
                    Row7[10] = '¤';
                }
                else if (p.Stage == 5)
                {
                    Row7[10] = '-';
                    if (gargIsAlive[7])
                    {
                        p.hasHit = true;
                        gargHealth[7] -= 1;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (!gargIsAlive[7])
                    {
                        Row6[10] = '¤';
                    }
                }
                else if (p.Stage == 6)
                {
                    Row6[10] = ' ';
                    Row5[10] = '¤';
                }
                else if (p.Stage == 7)
                {
                    Row6[10] = ' ';
                    Row5[10] = '¤';
                }
                else if (p.Stage == 8)
                {
                    Row5[10] = ' ';
                    Row4[10] = '¤';
                }
                else if (p.Stage == 9)
                {
                    Row4[10] = '-';
                    if (gargIsAlive[2])
                    {
                        p.hasHit = true;
                        gargHealth[2] -= 1;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (!gargIsAlive[2])
                    {
                        Row3[10] = '¤';
                    }
                }
                else if (p.Stage == 10)
                {
                    Row3[10] = ' ';
                    Row2[10] = '¤';
                }
                else if (p.Stage == 11)
                {
                    Row2[10] = ' ';
                    Row1[10] = '¤';
                }
                else if (p.Stage == 12)
                {
                    Row1[10] = '-';
                    p.hasHit = true;
                }
            }

            if (p.Lane == 4)
            {
                if (p.Stage == 1)
                {
                    Row10[14] = '¤';
                }
                else if (p.Stage == 2)
                {
                    Row10[14] = '-';
                    Row9[14] = '¤';
                }
                else if (p.Stage == 3)
                {
                    Row9[14] = ' ';
                    Row8[14] = '¤';
                }
                else if (p.Stage == 4)
                {
                    Row8[14] = ' ';
                    Row7[14] = '¤';
                }
                else if (p.Stage == 5)
                {
                    Row7[14] = '-';
                    if (gargIsAlive[8])
                    {
                        p.hasHit = true;
                        gargHealth[8] -= 1;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (!gargIsAlive[8])
                    {
                        Row6[14] = '¤';
                    }
                }
                else if (p.Stage == 6)
                {
                    Row6[14] = ' ';
                    Row5[14] = '¤';
                }
                else if (p.Stage == 7)
                {
                    Row6[14] = ' ';
                    Row5[14] = '¤';
                }
                else if (p.Stage == 8)
                {
                    Row5[14] = ' ';
                    Row4[14] = '¤';
                }
                else if (p.Stage == 9)
                {
                    Row4[14] = '-';
                    if (gargIsAlive[3])
                    {
                        p.hasHit = true;
                        gargHealth[3] -= 1;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (!gargIsAlive[3])
                    {
                        Row3[14] = '¤';
                    }
                }
                else if (p.Stage == 10)
                {
                    Row3[14] = ' ';
                    Row2[14] = '¤';
                }
                else if (p.Stage == 11)
                {
                    Row2[14] = ' ';
                    Row1[14] = '¤';
                }
                else if (p.Stage == 12)
                {
                    Row1[14] = '-';
                    p.hasHit = true;
                }
            }

            if (p.Lane == 5)
            {
                if (p.Stage == 1)
                {
                    Row10[18] = '¤';
                }
                else if (p.Stage == 2)
                {
                    Row10[18] = '-';
                    Row9[18] = '¤';
                }
                else if (p.Stage == 3)
                {
                    Row9[18] = ' ';
                    Row8[18] = '¤';
                }
                else if (p.Stage == 4)
                {
                    Row8[18] = ' ';
                    Row7[18] = '¤';
                }
                else if (p.Stage == 5)
                {
                    Row7[18] = '-';
                    if (gargIsAlive[9])
                    {
                        p.hasHit = true;
                        gargHealth[9] -= 1;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (!gargIsAlive[9])
                    {
                        Row6[18] = '¤';
                    }
                }
                else if (p.Stage == 6)
                {
                    Row6[18] = ' ';
                    Row5[18] = '¤';
                }
                else if (p.Stage == 7)
                {
                    Row6[18] = ' ';
                    Row5[18] = '¤';
                }
                else if (p.Stage == 8)
                {
                    Row5[18] = ' ';
                    Row4[18] = '¤';
                }
                else if (p.Stage == 9)
                {
                    Row4[18] = '-';
                    if (gargIsAlive[4])
                    {
                        p.hasHit = true;
                        gargHealth[4] -= 1;
                        Audio.Play("gargoyle_pain");
                    }
                    else if (!gargIsAlive[4])
                    {
                        Row3[18] = '¤';
                    }
                }
                else if (p.Stage == 10)
                {
                    Row3[18] = ' ';
                    Row2[18] = '¤';
                }
                else if (p.Stage == 11)
                {
                    Row2[18] = ' ';
                    Row1[18] = '¤';
                }
                else if (p.Stage == 12)
                {
                    Row1[18] = '-';
                    p.hasHit = true;
                }
            }

        }
    }



    static void DrawGnomes()
    {
        foreach (var g in gnomes.Where(x => x.IsAlive))
        {
            if (g.Lane == 1)
            {
                if (g.Row == 1)
                {
                    Row5[1] = ' '; Row5[2] = ' '; Row5[3] = ' ';
                    Row6[1] = ' '; Row6[2] = ' '; Row6[3] = ' ';
                    Row2[1] = '/';
                    Row2[2] = '\\';
                    Row2[3] = ',';
                    Row3[1] = 'o';
                    Row3[2] = 'u';
                    Row3[3] = '0';
                }
                else if (g.Row == 2)
                {
                    Row8[1] = ' '; Row8[2] = ' '; Row8[3] = ' ';
                    Row9[1] = ' '; Row9[2] = ' '; Row9[3] = ' ';
                    Row5[1] = '/';
                    Row5[2] = '\\';
                    Row5[3] = ',';
                    Row6[1] = 'o';
                    Row6[2] = 'u';
                    Row6[3] = '0';
                    if (gargIsAlive[0])
                    {
                        gargAttackTimer[0]++;
                        if (gargAttackTimer[0] >= gargAttackMax)
                        {
                            gargAttackTimer[0] = 0;
                            g.Health -= gargDamage[0];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 3)
                {
                    Row11[1] = ' '; Row11[2] = ' '; Row11[3] = ' ';
                    Row12[1] = ' '; Row12[2] = ' '; Row12[3] = ' ';
                    Row8[1] = '/';
                    Row8[2] = '\\';
                    Row8[3] = ',';
                    Row9[1] = 'o';
                    Row9[2] = 'u';
                    Row9[3] = '0';
                    if (gargIsAlive[5])
                    {
                        gargAttackTimer[5]++;
                        if (gargAttackTimer[5] >= gargAttackMax)
                        {
                            gargAttackTimer[5] = 0;
                            g.Health -= gargDamage[5];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 4)
                {
                    Row14[1] = ' '; Row14[2] = ' '; Row14[3] = ' ';
                    Row15[1] = ' '; Row15[2] = ' '; Row15[3] = ' ';
                    Row11[1] = '/';
                    Row11[2] = '\\';
                    Row11[3] = ',';
                    Row12[1] = 'o';
                    Row12[2] = 'u';
                    Row12[3] = '0';
                }
                else if (g.Row == 5)
                {
                    Row17[1] = ' '; Row17[2] = ' '; Row17[3] = ' ';
                    Row18[1] = ' '; Row18[2] = ' '; Row18[3] = ' ';
                    Row14[1] = '/';
                    Row14[2] = '\\';
                    Row14[3] = ',';
                    Row15[1] = 'o';
                    Row15[2] = 'u';
                    Row15[3] = '0';
                }
                else if (g.Row == 6)
                {
                    Row20[1] = ' '; Row20[2] = ' '; Row20[3] = ' ';
                    Row21[1] = ' '; Row21[2] = ' '; Row21[3] = ' ';
                    Row17[1] = '/';
                    Row17[2] = '\\';
                    Row17[3] = ',';
                    Row18[1] = 'o';
                    Row18[2] = 'u';
                    Row18[3] = '0';
                }
                else if (g.Row == 7)
                {
                    Row23[1] = ' '; Row23[2] = ' '; Row23[3] = ' ';
                    Row24[1] = ' '; Row24[2] = ' '; Row24[3] = ' ';
                    Row20[1] = '/';
                    Row20[2] = '\\';
                    Row20[3] = ',';
                    Row21[1] = 'o';
                    Row21[2] = 'u';
                    Row21[3] = '0';
                }
                else if (g.Row == 8)
                {
                    Row26[1] = ' '; Row26[2] = ' '; Row26[3] = ' ';
                    Row27[1] = ' '; Row27[2] = ' '; Row27[3] = ' ';
                    Row23[1] = '/';
                    Row23[2] = '\\';
                    Row23[3] = ',';
                    Row24[1] = 'o';
                    Row24[2] = 'u';
                    Row24[3] = '0';
                }
                else if (g.Row == 9)
                {
                    Row26[1] = '/';
                    Row26[2] = '\\';
                    Row26[3] = ',';
                    Row27[1] = 'o';
                    Row27[2] = 'u';
                    Row27[3] = '0';
                }
            }
            else if (g.Lane == 2)
            {
                if (g.Row == 1)
                {
                    Row2[5] = '/';
                    Row2[6] = '\\';
                    Row2[7] = ',';
                    Row3[5] = 'o';
                    Row3[6] = 'u';
                    Row3[7] = '0';

                    Row5[5] = ' '; Row5[6] = ' '; Row5[7] = ' ';
                    Row6[5] = ' '; Row6[6] = ' '; Row6[7] = ' ';
                }
                else if (g.Row == 2)
                {
                    Row8[5] = ' '; Row8[6] = ' '; Row8[7] = ' ';
                    Row9[5] = ' '; Row9[6] = ' '; Row9[7] = ' ';
                    Row5[5] = '/';
                    Row5[6] = '\\';
                    Row5[7] = ',';
                    Row6[5] = 'o';
                    Row6[6] = 'u';
                    Row6[7] = '0';
                    if (gargIsAlive[1])
                    {
                        gargAttackTimer[1]++;
                        if (gargAttackTimer[1] >= gargAttackMax)
                        {
                            gargAttackTimer[1] = 1;
                            g.Health -= gargDamage[1];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 3)
                {
                    Row11[5] = ' '; Row11[6] = ' '; Row11[7] = ' ';
                    Row12[5] = ' '; Row12[6] = ' '; Row12[7] = ' ';
                    Row8[5] = '/';
                    Row8[6] = '\\';
                    Row8[7] = ',';
                    Row9[5] = 'o';
                    Row9[6] = 'u';
                    Row9[7] = '0';
                    if (gargIsAlive[6])
                    {
                        gargAttackTimer[6]++;
                        if (gargAttackTimer[6] >= gargAttackMax)
                        {
                            gargAttackTimer[6] = 0;
                            g.Health -= gargDamage[6];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 4)
                {
                    Row14[5] = ' '; Row14[6] = ' '; Row14[7] = ' ';
                    Row15[5] = ' '; Row15[6] = ' '; Row15[7] = ' ';
                    Row11[5] = '/';
                    Row11[6] = '\\';
                    Row11[7] = ',';
                    Row12[5] = 'o';
                    Row12[6] = 'u';
                    Row12[7] = '0';
                }
                else if (g.Row == 5)
                {
                    Row17[5] = ' '; Row17[6] = ' '; Row17[7] = ' ';
                    Row18[5] = ' '; Row18[6] = ' '; Row18[7] = ' ';
                    Row14[5] = '/';
                    Row14[6] = '\\';
                    Row14[7] = ',';
                    Row15[5] = 'o';
                    Row15[6] = 'u';
                    Row15[7] = '0';
                }
                else if (g.Row == 6)
                {
                    Row20[5] = ' '; Row20[6] = ' '; Row20[7] = ' ';
                    Row21[5] = ' '; Row21[6] = ' '; Row21[7] = ' ';
                    Row17[5] = '/';
                    Row17[6] = '\\';
                    Row17[7] = ',';
                    Row18[5] = 'o';
                    Row18[6] = 'u';
                    Row18[7] = '0';
                }
                else if (g.Row == 7)
                {
                    Row23[5] = ' '; Row23[6] = ' '; Row23[7] = ' ';
                    Row24[5] = ' '; Row24[6] = ' '; Row24[7] = ' ';
                    Row20[5] = '/';
                    Row20[6] = '\\';
                    Row20[7] = ',';
                    Row21[5] = 'o';
                    Row21[6] = 'u';
                    Row21[7] = '0';
                }
                else if (g.Row == 8)
                {
                    Row26[5] = ' '; Row26[6] = ' '; Row26[7] = ' ';
                    Row27[5] = ' '; Row27[6] = ' '; Row27[7] = ' ';
                    Row23[5] = '/';
                    Row23[6] = '\\';
                    Row23[7] = ',';
                    Row24[5] = 'o';
                    Row24[6] = 'u';
                    Row24[7] = '0';
                }
                else if (g.Row == 9)
                {
                    Row26[5] = '/';
                    Row26[6] = '\\';
                    Row26[7] = ',';
                    Row27[5] = 'o';
                    Row27[6] = 'u';
                    Row27[7] = '0';
                }

            }
            else if (g.Lane == 3)
            {
                if (g.Row == 1)
                {
                    Row2[9] = '/';
                    Row2[10] = '\\';
                    Row2[11] = ',';
                    Row3[9] = 'o';
                    Row3[10] = 'u';
                    Row3[11] = '0';

                    Row5[9] = ' '; Row5[10] = ' '; Row5[11] = ' ';
                    Row6[9] = ' '; Row6[10] = ' '; Row6[11] = ' ';
                }
                else if (g.Row == 2)
                {
                    Row8[9] = ' '; Row8[10] = ' '; Row8[11] = ' ';
                    Row9[9] = ' '; Row9[10] = ' '; Row9[11] = ' ';
                    Row5[9] = '/';
                    Row5[10] = '\\';
                    Row5[11] = ',';
                    Row6[9] = 'o';
                    Row6[10] = 'u';
                    Row6[11] = '0';
                    if (gargIsAlive[2])
                    {
                        gargAttackTimer[2]++;
                        if (gargAttackTimer[2] >= gargAttackMax)
                        {
                            gargAttackTimer[2] = 0;
                            g.Health -= gargDamage[2];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 3)
                {
                    Row11[9] = ' '; Row11[10] = ' '; Row11[11] = ' ';
                    Row12[9] = ' '; Row12[10] = ' '; Row12[11] = ' ';
                    Row8[9] = '/';
                    Row8[10] = '\\';
                    Row8[11] = ',';
                    Row9[9] = 'o';
                    Row9[10] = 'u';
                    Row9[11] = '0';
                    if (gargIsAlive[7])
                    {
                        gargAttackTimer[7]++;
                        if (gargAttackTimer[7] >= gargAttackMax)
                        {
                            gargAttackTimer[7] = 0;
                            g.Health -= gargDamage[7];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 4)
                {
                    Row14[9] = ' '; Row14[10] = ' '; Row14[11] = ' ';
                    Row15[9] = ' '; Row15[10] = ' '; Row15[11] = ' ';
                    Row11[9] = '/';
                    Row11[10] = '\\';
                    Row11[11] = ',';
                    Row12[9] = 'o';
                    Row12[10] = 'u';
                    Row12[11] = '0';
                }
                else if (g.Row == 5)
                {
                    Row17[9] = ' '; Row17[10] = ' '; Row17[11] = ' ';
                    Row18[9] = ' '; Row18[10] = ' '; Row18[11] = ' ';
                    Row14[9] = '/';
                    Row14[10] = '\\';
                    Row14[11] = ',';
                    Row15[9] = 'o';
                    Row15[10] = 'u';
                    Row15[11] = '0';
                }
                else if (g.Row == 6)
                {
                    Row20[9] = ' '; Row20[10] = ' '; Row20[11] = ' ';
                    Row21[9] = ' '; Row21[10] = ' '; Row21[11] = ' ';
                    Row17[9] = '/';
                    Row17[10] = '\\';
                    Row17[11] = ',';
                    Row18[9] = 'o';
                    Row18[10] = 'u';
                    Row18[11] = '0';
                }
                else if (g.Row == 7)
                {
                    Row23[9] = ' '; Row23[10] = ' '; Row23[11] = ' ';
                    Row24[9] = ' '; Row24[10] = ' '; Row24[11] = ' ';
                    Row20[9] = '/';
                    Row20[10] = '\\';
                    Row20[11] = ',';
                    Row21[9] = 'o';
                    Row21[10] = 'u';
                    Row21[11] = '0';
                }
                else if (g.Row == 8)
                {
                    Row26[9] = ' '; Row26[10] = ' '; Row26[11] = ' ';
                    Row27[9] = ' '; Row27[10] = ' '; Row27[11] = ' ';
                    Row23[9] = '/';
                    Row23[10] = '\\';
                    Row23[11] = ',';
                    Row24[9] = 'o';
                    Row24[10] = 'u';
                    Row24[11] = '0';
                }
                else if (g.Row == 9)
                {
                    Row26[9] = '/';
                    Row26[10] = '\\';
                    Row26[11] = ',';
                    Row27[9] = 'o';
                    Row27[10] = 'u';
                    Row27[11] = '0';
                }

            }
            else if (g.Lane == 4)
            {
                if (g.Row == 1)
                {
                    Row2[13] = '/';
                    Row2[14] = '\\';
                    Row2[15] = ',';
                    Row3[13] = 'o';
                    Row3[14] = 'u';
                    Row3[15] = '0';

                    Row5[13] = ' '; Row5[14] = ' '; Row5[15] = ' ';
                    Row6[13] = ' '; Row6[14] = ' '; Row6[15] = ' ';
                }
                else if (g.Row == 2)
                {
                    Row8[13] = ' '; Row8[14] = ' '; Row8[15] = ' ';
                    Row9[13] = ' '; Row9[14] = ' '; Row9[15] = ' ';
                    Row5[13] = '/';
                    Row5[14] = '\\';
                    Row5[15] = ',';
                    Row6[13] = 'o';
                    Row6[14] = 'u';
                    Row6[15] = '0';
                    if (gargIsAlive[3])
                    {
                        gargAttackTimer[3]++;
                        if (gargAttackTimer[3] >= gargAttackMax)
                        {
                            gargAttackTimer[3] = 0;
                            g.Health -= gargDamage[3];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 3)
                {
                    Row11[13] = ' '; Row11[14] = ' '; Row11[15] = ' ';
                    Row12[13] = ' '; Row12[14] = ' '; Row12[15] = ' ';
                    Row8[13] = '/';
                    Row8[14] = '\\';
                    Row8[15] = ',';
                    Row9[13] = 'o';
                    Row9[14] = 'u';
                    Row9[15] = '0';
                    if (gargIsAlive[8])
                    {
                        gargAttackTimer[8]++;
                        if (gargAttackTimer[8] >= gargAttackMax)
                        {
                            gargAttackTimer[8] = 0;
                            g.Health -= gargDamage[8];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 4)
                {
                    Row14[13] = ' '; Row14[14] = ' '; Row14[15] = ' ';
                    Row15[13] = ' '; Row15[14] = ' '; Row15[15] = ' ';
                    Row11[13] = '/';
                    Row11[14] = '\\';
                    Row11[15] = ',';
                    Row12[13] = 'o';
                    Row12[14] = 'u';
                    Row12[15] = '0';
                }
                else if (g.Row == 5)
                {
                    Row17[13] = ' '; Row17[14] = ' '; Row17[15] = ' ';
                    Row18[13] = ' '; Row18[14] = ' '; Row18[15] = ' ';
                    Row14[13] = '/';
                    Row14[14] = '\\';
                    Row14[15] = ',';
                    Row15[13] = 'o';
                    Row15[14] = 'u';
                    Row15[15] = '0';
                }
                else if (g.Row == 6)
                {
                    Row20[13] = ' '; Row20[14] = ' '; Row20[15] = ' ';
                    Row21[13] = ' '; Row21[14] = ' '; Row21[15] = ' ';
                    Row17[13] = '/';
                    Row17[14] = '\\';
                    Row17[15] = ',';
                    Row18[13] = 'o';
                    Row18[14] = 'u';
                    Row18[15] = '0';
                }
                else if (g.Row == 7)
                {
                    Row23[13] = ' '; Row23[14] = ' '; Row23[15] = ' ';
                    Row24[13] = ' '; Row24[14] = ' '; Row24[15] = ' ';
                    Row20[13] = '/';
                    Row20[14] = '\\';
                    Row20[15] = ',';
                    Row21[13] = 'o';
                    Row21[14] = 'u';
                    Row21[15] = '0';
                }
                else if (g.Row == 8)
                {
                    Row26[13] = ' '; Row26[14] = ' '; Row26[15] = ' ';
                    Row27[13] = ' '; Row27[14] = ' '; Row27[15] = ' ';
                    Row23[13] = '/';
                    Row23[14] = '\\';
                    Row23[15] = ',';
                    Row24[13] = 'o';
                    Row24[14] = 'u';
                    Row24[15] = '0';
                }
                else if (g.Row == 9)
                {
                    Row26[13] = '/';
                    Row26[14] = '\\';
                    Row26[15] = ',';
                    Row27[13] = 'o';
                    Row27[14] = 'u';
                    Row27[15] = '0';
                }

            }
            else if (g.Lane == 5)
            {
                if (g.Row == 1)
                {
                    Row2[17] = '/';
                    Row2[18] = '\\';
                    Row2[19] = ',';
                    Row3[17] = 'o';
                    Row3[18] = 'u';
                    Row3[19] = '0';

                    Row5[17] = ' '; Row5[18] = ' '; Row5[19] = ' ';
                    Row6[17] = ' '; Row6[18] = ' '; Row6[19] = ' ';
                }
                else if (g.Row == 2)
                {
                    Row8[17] = ' '; Row8[18] = ' '; Row8[19] = ' ';
                    Row9[17] = ' '; Row9[18] = ' '; Row9[19] = ' ';
                    Row5[17] = '/';
                    Row5[18] = '\\';
                    Row5[19] = ',';
                    Row6[17] = 'o';
                    Row6[18] = 'u';
                    Row6[19] = '0';
                    if (gargIsAlive[4])
                    {
                        gargAttackTimer[4]++;
                        if (gargAttackTimer[4] >= gargAttackMax)
                        {
                            gargAttackTimer[4] = 0;
                            g.Health -= gargDamage[4];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 3)
                {
                    Row11[17] = ' '; Row11[18] = ' '; Row11[19] = ' ';
                    Row12[17] = ' '; Row12[18] = ' '; Row12[19] = ' ';
                    Row8[17] = '/';
                    Row8[18] = '\\';
                    Row8[19] = ',';
                    Row9[17] = 'o';
                    Row9[18] = 'u';
                    Row9[19] = '0';
                    if (gargIsAlive[9])
                    {
                        gargAttackTimer[9]++;
                        if (gargAttackTimer[9] >= gargAttackMax)
                        {
                            gargAttackTimer[9] = 0;
                            g.Health -= gargDamage[9];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 4)
                {
                    Row14[17] = ' '; Row14[18] = ' '; Row14[19] = ' ';
                    Row15[17] = ' '; Row15[18] = ' '; Row15[19] = ' ';
                    Row11[17] = '/';
                    Row11[18] = '\\';
                    Row11[19] = ',';
                    Row12[17] = 'o';
                    Row12[18] = 'u';
                    Row12[19] = '0';
                }
                else if (g.Row == 5)
                {
                    Row17[17] = ' '; Row17[18] = ' '; Row17[19] = ' ';
                    Row18[17] = ' '; Row18[18] = ' '; Row18[19] = ' ';
                    Row14[17] = '/';
                    Row14[18] = '\\';
                    Row14[19] = ',';
                    Row15[17] = 'o';
                    Row15[18] = 'u';
                    Row15[19] = '0';
                }
                else if (g.Row == 6)
                {
                    Row20[17] = ' '; Row20[18] = ' '; Row20[19] = ' ';
                    Row21[17] = ' '; Row21[18] = ' '; Row21[19] = ' ';
                    Row17[17] = '/';
                    Row17[18] = '\\';
                    Row17[19] = ',';
                    Row18[17] = 'o';
                    Row18[18] = 'u';
                    Row18[19] = '0';
                }
                else if (g.Row == 7)
                {
                    Row23[17] = ' '; Row23[18] = ' '; Row23[19] = ' ';
                    Row24[17] = ' '; Row24[18] = ' '; Row24[19] = ' ';
                    Row20[17] = '/';
                    Row20[18] = '\\';
                    Row20[19] = ',';
                    Row21[17] = 'o';
                    Row21[18] = 'u';
                    Row21[19] = '0';
                }
                else if (g.Row == 8)
                {
                    Row26[17] = ' '; Row26[18] = ' '; Row26[19] = ' ';
                    Row27[17] = ' '; Row27[18] = ' '; Row27[19] = ' ';
                    Row23[17] = '/';
                    Row23[18] = '\\';
                    Row23[19] = ',';
                    Row24[17] = 'o';
                    Row24[18] = 'u';
                    Row24[19] = '0';
                }
                else if (g.Row == 9)
                {
                    Row26[17] = '/';
                    Row26[18] = '\\';
                    Row26[19] = ',';
                    Row27[17] = 'o';
                    Row27[18] = 'u';
                    Row27[19] = '0';
                }

            }

        }
    }



    static void DrawGnights()
    {
        foreach (var g in gnights.Where(x => x.IsAlive))
        {
            if (g.Lane == 1)
            {
                if (g.Row == 1)
                {
                    Row2[1] = '/';
                    Row2[2] = 'Σ';
                    Row2[3] = ',';
                    Row3[1] = 'ò';
                    Row3[2] = '∩';
                    Row3[3] = 'ó';

                    Row5[1] = ' '; Row5[2] = ' '; Row5[3] = ' ';
                    Row6[1] = ' '; Row6[2] = ' '; Row6[3] = ' ';
                }
                else if (g.Row == 2)
                {
                    Row8[1] = ' '; Row8[2] = ' '; Row8[3] = ' ';
                    Row9[1] = ' '; Row9[2] = ' '; Row9[3] = ' ';
                    Row5[1] = '/';
                    Row5[2] = 'Σ';
                    Row5[3] = ',';
                    Row6[1] = 'ò';
                    Row6[2] = '∩';
                    Row6[3] = 'ó';
                    if (gargIsAlive[0])
                    {
                        gargAttackTimer[0]++;
                        if (gargAttackTimer[0] >= gargAttackMax)
                        {
                            gargAttackTimer[0] = 0;
                            g.Health -= gargDamage[0];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 3)
                {
                    Row11[1] = ' '; Row11[2] = ' '; Row11[3] = ' ';
                    Row12[1] = ' '; Row12[2] = ' '; Row12[3] = ' ';
                    Row8[1] = '/';
                    Row8[2] = 'Σ';
                    Row8[3] = ',';
                    Row9[1] = 'ò';
                    Row9[2] = '∩';
                    Row9[3] = 'ó';
                    if (gargIsAlive[5])
                    {
                        gargAttackTimer[5]++;
                        if (gargAttackTimer[5] >= gargAttackMax)
                        {
                            gargAttackTimer[5] = 0;
                            g.Health -= gargDamage[5];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 4)
                {
                    Row14[1] = ' '; Row14[2] = ' '; Row14[3] = ' ';
                    Row15[1] = ' '; Row15[2] = ' '; Row15[3] = ' ';
                    Row11[1] = '/';
                    Row11[2] = 'Σ';
                    Row11[3] = ',';
                    Row12[1] = 'ò';
                    Row12[2] = '∩';
                    Row12[3] = 'ó';
                }
                else if (g.Row == 5)
                {
                    Row17[1] = ' '; Row17[2] = ' '; Row17[3] = ' ';
                    Row18[1] = ' '; Row18[2] = ' '; Row18[3] = ' ';
                    Row14[1] = '/';
                    Row14[2] = 'Σ';
                    Row14[3] = ',';
                    Row15[1] = 'ò';
                    Row15[2] = '∩';
                    Row15[3] = 'ó';
                }
                else if (g.Row == 6)
                {
                    Row20[1] = ' '; Row20[2] = ' '; Row20[3] = ' ';
                    Row21[1] = ' '; Row21[2] = ' '; Row21[3] = ' ';
                    Row17[1] = '/';
                    Row17[2] = 'Σ';
                    Row17[3] = ',';
                    Row18[1] = 'ò';
                    Row18[2] = '∩';
                    Row18[3] = 'ó';
                }
                else if (g.Row == 7)
                {
                    Row23[1] = ' '; Row23[2] = ' '; Row23[3] = ' ';
                    Row24[1] = ' '; Row24[2] = ' '; Row24[3] = ' ';
                    Row20[1] = '/';
                    Row20[2] = 'Σ';
                    Row20[3] = ',';
                    Row21[1] = 'ò';
                    Row21[2] = '∩';
                    Row21[3] = 'ó';
                }
                else if (g.Row == 8)
                {
                    Row26[1] = ' '; Row26[2] = ' '; Row26[3] = ' ';
                    Row27[1] = ' '; Row27[2] = ' '; Row27[3] = ' ';
                    Row23[1] = '/';
                    Row23[2] = 'Σ';
                    Row23[3] = ',';
                    Row24[1] = 'ò';
                    Row24[2] = '∩';
                    Row24[3] = 'ó';
                }
                else if (g.Row == 9)
                {
                    Row26[1] = '/';
                    Row26[2] = 'Σ';
                    Row26[3] = ',';
                    Row27[1] = 'ò';
                    Row27[2] = '∩';
                    Row27[3] = 'ó';
                }
            }
            else if (g.Lane == 2)
            {
                if (g.Row == 1)
                {
                    Row2[5] = '/';
                    Row2[6] = 'Σ';
                    Row2[7] = ',';
                    Row3[5] = 'ò';
                    Row3[6] = '∩';
                    Row3[7] = 'ó';

                    Row5[5] = ' '; Row5[6] = ' '; Row5[7] = ' ';
                    Row6[5] = ' '; Row6[6] = ' '; Row6[7] = ' ';
                }
                else if (g.Row == 2)
                {
                    Row8[5] = ' '; Row8[6] = ' '; Row8[7] = ' ';
                    Row9[5] = ' '; Row9[6] = ' '; Row9[7] = ' ';
                    Row5[5] = '/';
                    Row5[6] = 'Σ';
                    Row5[7] = ',';
                    Row6[5] = 'ò';
                    Row6[6] = '∩';
                    Row6[7] = 'ó';
                    if (gargIsAlive[1])
                    {
                        gargAttackTimer[1]++;
                        if (gargAttackTimer[1] >= gargAttackMax)
                        {
                            gargAttackTimer[1] = 0;
                            g.Health -= gargDamage[1];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 3)
                {
                    Row11[5] = ' '; Row11[6] = ' '; Row11[7] = ' ';
                    Row12[5] = ' '; Row12[6] = ' '; Row12[7] = ' ';
                    Row8[5] = '/';
                    Row8[6] = 'Σ';
                    Row8[7] = ',';
                    Row9[5] = 'ò';
                    Row9[6] = '∩';
                    Row9[7] = 'ó';
                    if (gargIsAlive[6])
                    {
                        gargAttackTimer[6]++;
                        if (gargAttackTimer[6] >= gargAttackMax)
                        {
                            gargAttackTimer[6] = 0;
                            g.Health -= gargDamage[6];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 4)
                {
                    Row14[5] = ' '; Row14[6] = ' '; Row14[7] = ' ';
                    Row15[5] = ' '; Row15[6] = ' '; Row15[7] = ' ';
                    Row11[5] = '/';
                    Row11[6] = 'Σ';
                    Row11[7] = ',';
                    Row12[5] = 'ò';
                    Row12[6] = '∩';
                    Row12[7] = 'ó';
                }
                else if (g.Row == 5)
                {
                    Row17[5] = ' '; Row17[6] = ' '; Row17[7] = ' ';
                    Row18[5] = ' '; Row18[6] = ' '; Row18[7] = ' ';
                    Row14[5] = '/';
                    Row14[6] = 'Σ';
                    Row14[7] = ',';
                    Row15[5] = 'ò';
                    Row15[6] = '∩';
                    Row15[7] = 'ó';
                }
                else if (g.Row == 6)
                {
                    Row20[5] = ' '; Row20[6] = ' '; Row20[7] = ' ';
                    Row21[5] = ' '; Row21[6] = ' '; Row21[7] = ' ';
                    Row17[5] = '/';
                    Row17[6] = 'Σ';
                    Row17[7] = ',';
                    Row18[5] = 'ò';
                    Row18[6] = '∩';
                    Row18[7] = 'ó';
                }
                else if (g.Row == 7)
                {
                    Row23[5] = ' '; Row23[6] = ' '; Row23[7] = ' ';
                    Row24[5] = ' '; Row24[6] = ' '; Row24[7] = ' ';
                    Row20[5] = '/';
                    Row20[6] = 'Σ';
                    Row20[7] = ',';
                    Row21[5] = 'ò';
                    Row21[6] = '∩';
                    Row21[7] = 'ó';
                }
                else if (g.Row == 8)
                {
                    Row26[5] = ' '; Row26[6] = ' '; Row26[7] = ' ';
                    Row27[5] = ' '; Row27[6] = ' '; Row27[7] = ' ';
                    Row23[5] = '/';
                    Row23[6] = 'Σ';
                    Row23[7] = ',';
                    Row24[5] = 'ò';
                    Row24[6] = '∩';
                    Row24[7] = 'ó';
                }
                else if (g.Row == 9)
                {
                    Row26[5] = '/';
                    Row26[6] = 'Σ';
                    Row26[7] = ',';
                    Row27[5] = 'ò';
                    Row27[6] = '∩';
                    Row27[7] = 'ó';
                }

            }
            else if (g.Lane == 3)
            {
                if (g.Row == 1)
                {
                    Row2[9] = '/';
                    Row2[10] = 'Σ';
                    Row2[11] = ',';
                    Row3[9] = 'ò';
                    Row3[10] = '∩';
                    Row3[11] = 'ó';

                    Row5[9] = ' '; Row5[10] = ' '; Row5[11] = ' ';
                    Row6[9] = ' '; Row6[10] = ' '; Row6[11] = ' ';
                }
                else if (g.Row == 2)
                {
                    Row8[9] = ' '; Row8[10] = ' '; Row8[11] = ' ';
                    Row9[9] = ' '; Row9[10] = ' '; Row9[11] = ' ';
                    Row5[9] = '/';
                    Row5[10] = 'Σ';
                    Row5[11] = ',';
                    Row6[9] = 'ò';
                    Row6[10] = '∩';
                    Row6[11] = 'ó';
                    if (gargIsAlive[2])
                    {
                        gargAttackTimer[2]++;
                        if (gargAttackTimer[2] >= gargAttackMax)
                        {
                            gargAttackTimer[2] = 0;
                            g.Health -= gargDamage[2];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 3)
                {
                    Row11[9] = ' '; Row11[10] = ' '; Row11[11] = ' ';
                    Row12[9] = ' '; Row12[10] = ' '; Row12[11] = ' ';
                    Row8[9] = '/';
                    Row8[10] = 'Σ';
                    Row8[11] = ',';
                    Row9[9] = 'ò';
                    Row9[10] = '∩';
                    Row9[11] = 'ó';
                    if (gargIsAlive[7])
                    {
                        gargAttackTimer[7]++;
                        if (gargAttackTimer[7] >= gargAttackMax)
                        {
                            gargAttackTimer[7] = 0;
                            g.Health -= gargDamage[7];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 4)
                {
                    Row14[9] = ' '; Row14[10] = ' '; Row14[11] = ' ';
                    Row15[9] = ' '; Row15[10] = ' '; Row15[11] = ' ';
                    Row11[9] = '/';
                    Row11[10] = 'Σ';
                    Row11[11] = ',';
                    Row12[9] = 'ò';
                    Row12[10] = '∩';
                    Row12[11] = 'ó';
                }
                else if (g.Row == 5)
                {
                    Row17[9] = ' '; Row17[10] = ' '; Row17[11] = ' ';
                    Row18[9] = ' '; Row18[10] = ' '; Row18[11] = ' ';
                    Row14[9] = '/';
                    Row14[10] = 'Σ';
                    Row14[11] = ',';
                    Row15[9] = 'ò';
                    Row15[10] = '∩';
                    Row15[11] = 'ó';
                }
                else if (g.Row == 6)
                {
                    Row20[9] = ' '; Row20[10] = ' '; Row20[11] = ' ';
                    Row21[9] = ' '; Row21[10] = ' '; Row21[11] = ' ';
                    Row17[9] = '/';
                    Row17[10] = 'Σ';
                    Row17[11] = ',';
                    Row18[9] = 'ò';
                    Row18[10] = '∩';
                    Row18[11] = 'ó';
                }
                else if (g.Row == 7)
                {
                    Row23[9] = ' '; Row23[10] = ' '; Row23[11] = ' ';
                    Row24[9] = ' '; Row24[10] = ' '; Row24[11] = ' ';
                    Row20[9] = '/';
                    Row20[10] = 'Σ';
                    Row20[11] = ',';
                    Row21[9] = 'ò';
                    Row21[10] = '∩';
                    Row21[11] = 'ó';
                }
                else if (g.Row == 8)
                {
                    Row26[9] = ' '; Row26[10] = ' '; Row26[11] = ' ';
                    Row27[9] = ' '; Row27[10] = ' '; Row27[11] = ' ';
                    Row23[9] = '/';
                    Row23[10] = 'Σ';
                    Row23[11] = ',';
                    Row24[9] = 'ò';
                    Row24[10] = '∩';
                    Row24[11] = 'ó';
                }
                else if (g.Row == 9)
                {
                    Row26[9] = '/';
                    Row26[10] = 'Σ';
                    Row26[11] = ',';
                    Row27[9] = 'ò';
                    Row27[10] = '∩';
                    Row27[11] = 'ó';
                }

            }
            else if (g.Lane == 4)
            {
                if (g.Row == 1)
                {
                    Row2[13] = '/';
                    Row2[14] = 'Σ';
                    Row2[15] = ',';
                    Row3[13] = 'ò';
                    Row3[14] = '∩';
                    Row3[15] = 'ó';

                    Row5[13] = ' '; Row5[14] = ' '; Row5[15] = ' ';
                    Row6[13] = ' '; Row6[14] = ' '; Row6[15] = ' ';
                }
                else if (g.Row == 2)
                {
                    Row8[13] = ' '; Row8[14] = ' '; Row8[15] = ' ';
                    Row9[13] = ' '; Row9[14] = ' '; Row9[15] = ' ';
                    Row5[13] = '/';
                    Row5[14] = 'Σ';
                    Row5[15] = ',';
                    Row6[13] = 'ò';
                    Row6[14] = '∩';
                    Row6[15] = 'ó';
                    if (gargIsAlive[3])
                    {
                        gargAttackTimer[3]++;
                        if (gargAttackTimer[3] >= gargAttackMax)
                        {
                            gargAttackTimer[3] = 0;
                            g.Health -= gargDamage[3];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 3)
                {
                    Row11[13] = ' '; Row11[14] = ' '; Row11[15] = ' ';
                    Row12[13] = ' '; Row12[14] = ' '; Row12[15] = ' ';
                    Row8[13] = '/';
                    Row8[14] = 'Σ';
                    Row8[15] = ',';
                    Row9[13] = 'ò';
                    Row9[14] = '∩';
                    Row9[15] = 'ó';
                    if (gargIsAlive[8])
                    {
                        gargAttackTimer[8]++;
                        if (gargAttackTimer[8] >= gargAttackMax)
                        {
                            gargAttackTimer[8] = 0;
                            g.Health -= gargDamage[8];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 4)
                {
                    Row14[13] = ' '; Row14[14] = ' '; Row14[15] = ' ';
                    Row15[13] = ' '; Row15[14] = ' '; Row15[15] = ' ';
                    Row11[13] = '/';
                    Row11[14] = 'Σ';
                    Row11[15] = ',';
                    Row12[13] = 'ò';
                    Row12[14] = '∩';
                    Row12[15] = 'ó';
                }
                else if (g.Row == 5)
                {
                    Row17[13] = ' '; Row17[14] = ' '; Row17[15] = ' ';
                    Row18[13] = ' '; Row18[14] = ' '; Row18[15] = ' ';
                    Row14[13] = '/';
                    Row14[14] = 'Σ';
                    Row14[15] = ',';
                    Row15[13] = 'ò';
                    Row15[14] = '∩';
                    Row15[15] = 'ó';
                }
                else if (g.Row == 6)
                {
                    Row20[13] = ' '; Row20[14] = ' '; Row20[15] = ' ';
                    Row21[13] = ' '; Row21[14] = ' '; Row21[15] = ' ';
                    Row17[13] = '/';
                    Row17[14] = 'Σ';
                    Row17[15] = ',';
                    Row18[13] = 'ò';
                    Row18[14] = '∩';
                    Row18[15] = 'ó';
                }
                else if (g.Row == 7)
                {
                    Row23[13] = ' '; Row23[14] = ' '; Row23[15] = ' ';
                    Row24[13] = ' '; Row24[14] = ' '; Row24[15] = ' ';
                    Row20[13] = '/';
                    Row20[14] = 'Σ';
                    Row20[15] = ',';
                    Row21[13] = 'ò';
                    Row21[14] = '∩';
                    Row21[15] = 'ó';
                }
                else if (g.Row == 8)
                {
                    Row26[13] = ' '; Row26[14] = ' '; Row26[15] = ' ';
                    Row27[13] = ' '; Row27[14] = ' '; Row27[15] = ' ';
                    Row23[13] = '/';
                    Row23[14] = 'Σ';
                    Row23[15] = ',';
                    Row24[13] = 'ò';
                    Row24[14] = '∩';
                    Row24[15] = 'ó';
                }
                else if (g.Row == 9)
                {
                    Row26[13] = '/';
                    Row26[14] = 'Σ';
                    Row26[15] = ',';
                    Row27[13] = 'ò';
                    Row27[14] = '∩';
                    Row27[15] = 'ó';
                }

            }
            else if (g.Lane == 5)
            {
                if (g.Row == 1)
                {
                    Row2[17] = '/';
                    Row2[18] = 'Σ';
                    Row2[19] = ',';
                    Row3[17] = 'ò';
                    Row3[18] = '∩';
                    Row3[19] = 'ó';

                    Row5[17] = ' '; Row5[18] = ' '; Row5[19] = ' ';
                    Row6[17] = ' '; Row6[18] = ' '; Row6[19] = ' ';
                }
                else if (g.Row == 2)
                {
                    Row8[17] = ' '; Row8[18] = ' '; Row8[19] = ' ';
                    Row9[17] = ' '; Row9[18] = ' '; Row9[19] = ' ';
                    Row5[17] = '/';
                    Row5[18] = 'Σ';
                    Row5[19] = ',';
                    Row6[17] = 'ò';
                    Row6[18] = '∩';
                    Row6[19] = 'ó';
                    if (gargIsAlive[4])
                    {
                        gargAttackTimer[4]++;
                        if (gargAttackTimer[4] >= gargAttackMax)
                        {
                            gargAttackTimer[4] = 0;
                            g.Health -= gargDamage[4];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 3)
                {
                    Row11[17] = ' '; Row11[18] = ' '; Row11[19] = ' ';
                    Row12[17] = ' '; Row12[18] = ' '; Row12[19] = ' ';
                    Row8[17] = '/';
                    Row8[18] = 'Σ';
                    Row8[19] = ',';
                    Row9[17] = 'ò';
                    Row9[18] = '∩';
                    Row9[19] = 'ó';
                    if (gargIsAlive[9])
                    {
                        gargAttackTimer[9]++;
                        if (gargAttackTimer[9] >= gargAttackMax)
                        {
                            gargAttackTimer[9] = 0;
                            g.Health -= gargDamage[9];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 4)
                {
                    Row14[17] = ' '; Row14[18] = ' '; Row14[19] = ' ';
                    Row15[17] = ' '; Row15[18] = ' '; Row15[19] = ' ';
                    Row11[17] = '/';
                    Row11[18] = 'Σ';
                    Row11[19] = ',';
                    Row12[17] = 'ò';
                    Row12[18] = '∩';
                    Row12[19] = 'ó';
                }
                else if (g.Row == 5)
                {
                    Row17[17] = ' '; Row17[18] = ' '; Row17[19] = ' ';
                    Row18[17] = ' '; Row18[18] = ' '; Row18[19] = ' ';
                    Row14[17] = '/';
                    Row14[18] = 'Σ';
                    Row14[19] = ',';
                    Row15[17] = 'ò';
                    Row15[18] = '∩';
                    Row15[19] = 'ó';
                }
                else if (g.Row == 6)
                {
                    Row20[17] = ' '; Row20[18] = ' '; Row20[19] = ' ';
                    Row21[17] = ' '; Row21[18] = ' '; Row21[19] = ' ';
                    Row17[17] = '/';
                    Row17[18] = 'Σ';
                    Row17[19] = ',';
                    Row18[17] = 'ò';
                    Row18[18] = '∩';
                    Row18[19] = 'ó';
                }
                else if (g.Row == 7)
                {
                    Row23[17] = ' '; Row23[18] = ' '; Row23[19] = ' ';
                    Row24[17] = ' '; Row24[18] = ' '; Row24[19] = ' ';
                    Row20[17] = '/';
                    Row20[18] = 'Σ';
                    Row20[19] = ',';
                    Row21[17] = 'ò';
                    Row21[18] = '∩';
                    Row21[19] = 'ó';
                }
                else if (g.Row == 8)
                {
                    Row26[17] = ' '; Row26[18] = ' '; Row26[19] = ' ';
                    Row27[17] = ' '; Row27[18] = ' '; Row27[19] = ' ';
                    Row23[17] = '/';
                    Row23[18] = 'Σ';
                    Row23[19] = ',';
                    Row24[17] = 'ò';
                    Row24[18] = '∩';
                    Row24[19] = 'ó';
                }
                else if (g.Row == 9)
                {
                    Row26[17] = '/';
                    Row26[18] = 'Σ';
                    Row26[19] = ',';
                    Row27[17] = 'ò';
                    Row27[18] = '∩';
                    Row27[19] = 'ó';
                }
            }
        }
    }



    static void DrawGizards()
    {
        foreach (var g in gizards.Where(x => x.IsAlive))
        {
            if (g.Lane == 1)
            {
                if (g.Row == 1)
                {
                    Row2[1] = '/';
                    Row2[2] = '^';
                    Row2[3] = '\\';
                    Row3[1] = '0';
                    Row3[2] = '¬';
                    Row3[3] = '0';

                    Row5[1] = ' '; Row5[2] = ' '; Row5[3] = ' ';
                    Row6[1] = ' '; Row6[2] = ' '; Row6[3] = ' ';
                }
                else if (g.Row == 2)
                {
                    Row8[1] = ' '; Row8[2] = ' '; Row8[3] = ' ';
                    Row9[1] = ' '; Row9[2] = ' '; Row9[3] = ' ';
                    Row5[1] = '/';
                    Row5[2] = '^';
                    Row5[3] = '\\';
                    Row6[1] = '0';
                    Row6[2] = '¬';
                    Row6[3] = '0';
                    if (gargIsAlive[0])
                    {
                        gargAttackTimer[0]++;
                        if (gargAttackTimer[0] >= gargAttackMax)
                        {
                            gargAttackTimer[0] = 0;
                            g.Health -= gargDamage[0];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 3)
                {
                    Row11[1] = ' '; Row11[2] = ' '; Row11[3] = ' ';
                    Row12[1] = ' '; Row12[2] = ' '; Row12[3] = ' ';
                    Row8[1] = '/';
                    Row8[2] = '^';
                    Row8[3] = '\\';
                    Row9[1] = '0';
                    Row9[2] = '¬';
                    Row9[3] = '0';
                    if (gargIsAlive[5])
                    {
                        gargAttackTimer[5]++;
                        if (gargAttackTimer[5] >= gargAttackMax)
                        {
                            gargAttackTimer[5] = 0;
                            g.Health -= gargDamage[5];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 4)
                {
                    Row14[1] = ' '; Row14[2] = ' '; Row14[3] = ' ';
                    Row15[1] = ' '; Row15[2] = ' '; Row15[3] = ' ';
                    Row11[1] = '/';
                    Row11[2] = '^';
                    Row11[3] = '\\';
                    Row12[1] = '0';
                    Row12[2] = '¬';
                    Row12[3] = '0';
                }
                else if (g.Row == 5)
                {
                    Row17[1] = ' '; Row17[2] = ' '; Row17[3] = ' ';
                    Row18[1] = ' '; Row18[2] = ' '; Row18[3] = ' ';
                    Row14[1] = '/';
                    Row14[2] = '^';
                    Row14[3] = '\\';
                    Row15[1] = '0';
                    Row15[2] = '¬';
                    Row15[3] = '0';
                }
                else if (g.Row == 6)
                {
                    Row20[1] = ' '; Row20[2] = ' '; Row20[3] = ' ';
                    Row21[1] = ' '; Row21[2] = ' '; Row21[3] = ' ';
                    Row17[1] = '/';
                    Row17[2] = '^';
                    Row17[3] = '\\';
                    Row18[1] = '0';
                    Row18[2] = '¬';
                    Row18[3] = '0';
                }
                else if (g.Row == 7)
                {
                    Row23[1] = ' '; Row23[2] = ' '; Row23[3] = ' ';
                    Row24[1] = ' '; Row24[2] = ' '; Row24[3] = ' ';
                    Row20[1] = '/';
                    Row20[2] = '^';
                    Row20[3] = '\\';
                    Row21[1] = '0';
                    Row21[2] = '¬';
                    Row21[3] = '0';
                }
                else if (g.Row == 8)
                {
                    Row26[1] = ' '; Row26[2] = ' '; Row26[3] = ' ';
                    Row27[1] = ' '; Row27[2] = ' '; Row27[3] = ' ';
                    Row23[1] = '/';
                    Row23[2] = '^';
                    Row23[3] = '\\';
                    Row24[1] = '0';
                    Row24[2] = '¬';
                    Row24[3] = '0';
                }
                else if (g.Row == 9)
                {
                    Row26[1] = '/';
                    Row26[2] = '^';
                    Row26[3] = '\\';
                    Row27[1] = '0';
                    Row27[2] = '¬';
                    Row27[3] = '0';
                }
            }
            else if (g.Lane == 2)
            {
                if (g.Row == 1)
                {
                    Row2[5] = '/';
                    Row2[6] = '^';
                    Row2[7] = '\\';
                    Row3[5] = '0';
                    Row3[6] = '¬';
                    Row3[7] = '0';

                    Row5[5] = ' '; Row5[6] = ' '; Row5[7] = ' ';
                    Row6[5] = ' '; Row6[6] = ' '; Row6[7] = ' ';
                }
                else if (g.Row == 2)
                {
                    Row8[5] = ' '; Row8[6] = ' '; Row8[7] = ' ';
                    Row9[5] = ' '; Row9[6] = ' '; Row9[7] = ' ';
                    Row5[5] = '/';
                    Row5[6] = '^';
                    Row5[7] = '\\';
                    Row6[5] = '0';
                    Row6[6] = '¬';
                    Row6[7] = '0';
                    if (gargIsAlive[1])
                    {
                        gargAttackTimer[1]++;
                        if (gargAttackTimer[1] >= gargAttackMax)
                        {
                            gargAttackTimer[1] = 0;
                            g.Health -= gargDamage[1];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 3)
                {
                    Row11[5] = ' '; Row11[6] = ' '; Row11[7] = ' ';
                    Row12[5] = ' '; Row12[6] = ' '; Row12[7] = ' ';
                    Row8[5] = '/';
                    Row8[6] = '^';
                    Row8[7] = '\\';
                    Row9[5] = '0';
                    Row9[6] = '¬';
                    Row9[7] = '0';
                    if (gargIsAlive[6])
                    {
                        gargAttackTimer[6]++;
                        if (gargAttackTimer[6] >= gargAttackMax)
                        {
                            gargAttackTimer[6] = 0;
                            g.Health -= gargDamage[6];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 4)
                {
                    Row14[5] = ' '; Row14[6] = ' '; Row14[7] = ' ';
                    Row15[5] = ' '; Row15[6] = ' '; Row15[7] = ' ';
                    Row11[5] = '/';
                    Row11[6] = '^';
                    Row11[7] = '\\';
                    Row12[5] = '0';
                    Row12[6] = '¬';
                    Row12[7] = '0';
                }
                else if (g.Row == 5)
                {
                    Row17[5] = ' '; Row17[6] = ' '; Row17[7] = ' ';
                    Row18[5] = ' '; Row18[6] = ' '; Row18[7] = ' ';
                    Row14[5] = '/';
                    Row14[6] = '^';
                    Row14[7] = '\\';
                    Row15[5] = '0';
                    Row15[6] = '¬';
                    Row15[7] = '0';
                }
                else if (g.Row == 6)
                {
                    Row20[5] = ' '; Row20[6] = ' '; Row20[7] = ' ';
                    Row21[5] = ' '; Row21[6] = ' '; Row21[7] = ' ';
                    Row17[5] = '/';
                    Row17[6] = '^';
                    Row17[7] = '\\';
                    Row18[5] = '0';
                    Row18[6] = '¬';
                    Row18[7] = '0';
                }
                else if (g.Row == 7)
                {
                    Row23[5] = ' '; Row23[6] = ' '; Row23[7] = ' ';
                    Row24[5] = ' '; Row24[6] = ' '; Row24[7] = ' ';
                    Row20[5] = '/';
                    Row20[6] = '^';
                    Row20[7] = '\\';
                    Row21[5] = '0';
                    Row21[6] = '¬';
                    Row21[7] = '0';
                }
                else if (g.Row == 8)
                {
                    Row26[5] = ' '; Row26[6] = ' '; Row26[7] = ' ';
                    Row27[5] = ' '; Row27[6] = ' '; Row27[7] = ' ';
                    Row23[5] = '/';
                    Row23[6] = '^';
                    Row23[7] = '\\';
                    Row24[5] = '0';
                    Row24[6] = '¬';
                    Row24[7] = '0';
                }
                else if (g.Row == 9)
                {
                    Row26[5] = '/';
                    Row26[6] = '^';
                    Row26[7] = '\\';
                    Row27[5] = '0';
                    Row27[6] = '¬';
                    Row27[7] = '0';
                }
            }
            else if (g.Lane == 3)
            {
                if (g.Row == 1)
                {
                    Row2[9] = '/';
                    Row2[10] = '^';
                    Row2[11] = '\\';
                    Row3[9] = '0';
                    Row3[10] = '¬';
                    Row3[11] = '0';

                    Row5[9] = ' '; Row5[10] = ' '; Row5[11] = ' ';
                    Row6[9] = ' '; Row6[10] = ' '; Row6[11] = ' ';
                }
                else if (g.Row == 2)
                {
                    Row8[9] = ' '; Row8[10] = ' '; Row8[11] = ' ';
                    Row9[9] = ' '; Row9[10] = ' '; Row9[11] = ' ';
                    Row5[9] = '/';
                    Row5[10] = '^';
                    Row5[11] = '\\';
                    Row6[9] = '0';
                    Row6[10] = '¬';
                    Row6[11] = '0';
                    if (gargIsAlive[2])
                    {
                        gargAttackTimer[2]++;
                        if (gargAttackTimer[2] >= gargAttackMax)
                        {
                            gargAttackTimer[2] = 0;
                            g.Health -= gargDamage[2];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 3)
                {
                    Row11[9] = ' '; Row11[10] = ' '; Row11[11] = ' ';
                    Row12[9] = ' '; Row12[10] = ' '; Row12[11] = ' ';
                    Row8[9] = '/';
                    Row8[10] = '^';
                    Row8[11] = '\\';
                    Row9[9] = '0';
                    Row9[10] = '¬';
                    Row9[11] = '0';
                    if (gargIsAlive[7])
                    {
                        gargAttackTimer[7]++;
                        if (gargAttackTimer[7] >= gargAttackMax)
                        {
                            gargAttackTimer[7] = 0;
                            g.Health -= gargDamage[7];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 4)
                {
                    Row14[9] = ' '; Row14[10] = ' '; Row14[11] = ' ';
                    Row15[9] = ' '; Row15[10] = ' '; Row15[11] = ' ';
                    Row11[9] = '/';
                    Row11[10] = '^';
                    Row11[11] = '\\';
                    Row12[9] = '0';
                    Row12[10] = '¬';
                    Row12[11] = '0';
                }
                else if (g.Row == 5)
                {
                    Row17[9] = ' '; Row17[10] = ' '; Row17[11] = ' ';
                    Row18[9] = ' '; Row18[10] = ' '; Row18[11] = ' ';
                    Row14[9] = '/';
                    Row14[10] = '^';
                    Row14[11] = '\\';
                    Row15[9] = '0';
                    Row15[10] = '¬';
                    Row15[11] = '0';
                }
                else if (g.Row == 6)
                {
                    Row20[9] = ' '; Row20[10] = ' '; Row20[11] = ' ';
                    Row21[9] = ' '; Row21[10] = ' '; Row21[11] = ' ';
                    Row17[9] = '/';
                    Row17[10] = '^';
                    Row17[11] = '\\';
                    Row18[9] = '0';
                    Row18[10] = '¬';
                    Row18[11] = '0';
                }
                else if (g.Row == 7)
                {
                    Row23[9] = ' '; Row23[10] = ' '; Row23[11] = ' ';
                    Row24[9] = ' '; Row24[10] = ' '; Row24[11] = ' ';
                    Row20[9] = '/';
                    Row20[10] = '^';
                    Row20[11] = '\\';
                    Row21[9] = '0';
                    Row21[10] = '¬';
                    Row21[11] = '0';
                }
                else if (g.Row == 8)
                {
                    Row26[9] = ' '; Row26[10] = ' '; Row26[11] = ' ';
                    Row27[9] = ' '; Row27[10] = ' '; Row27[11] = ' ';
                    Row23[9] = '/';
                    Row23[10] = '^';
                    Row23[11] = '\\';
                    Row24[9] = '0';
                    Row24[10] = '¬';
                    Row24[11] = '0';
                }
                else if (g.Row == 9)
                {
                    Row26[9] = '/';
                    Row26[10] = '^';
                    Row26[11] = '\\';
                    Row27[9] = '0';
                    Row27[10] = '¬';
                    Row27[11] = '0';
                }
            }
            else if (g.Lane == 4)
            {
                if (g.Row == 1)
                {
                    Row2[13] = '/';
                    Row2[14] = '^';
                    Row2[15] = '\\';
                    Row3[13] = '0';
                    Row3[14] = '¬';
                    Row3[15] = '0';

                    Row5[13] = ' '; Row5[14] = ' '; Row5[15] = ' ';
                    Row6[13] = ' '; Row6[14] = ' '; Row6[15] = ' ';
                }
                else if (g.Row == 2)
                {
                    Row8[13] = ' '; Row8[14] = ' '; Row8[15] = ' ';
                    Row9[13] = ' '; Row9[14] = ' '; Row9[15] = ' ';
                    Row5[13] = '/';
                    Row5[14] = '^';
                    Row5[15] = '\\';
                    Row6[13] = '0';
                    Row6[14] = '¬';
                    Row6[15] = '0';
                    if (gargIsAlive[3])
                    {
                        gargAttackTimer[3]++;
                        if (gargAttackTimer[3] >= gargAttackMax)
                        {
                            gargAttackTimer[3] = 0;
                            g.Health -= gargDamage[3];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 3)
                {
                    Row11[13] = ' '; Row11[14] = ' '; Row11[15] = ' ';
                    Row12[13] = ' '; Row12[14] = ' '; Row12[15] = ' ';
                    Row8[13] = '/';
                    Row8[14] = '^';
                    Row8[15] = '\\';
                    Row9[13] = '0';
                    Row9[14] = '¬';
                    Row9[15] = '0';
                    if (gargIsAlive[8])
                    {
                        gargAttackTimer[8]++;
                        if (gargAttackTimer[8] >= gargAttackMax)
                        {
                            gargAttackTimer[8] = 0;
                            g.Health -= gargDamage[8];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 4)
                {
                    Row14[13] = ' '; Row14[14] = ' '; Row14[15] = ' ';
                    Row15[13] = ' '; Row15[14] = ' '; Row15[15] = ' ';
                    Row11[13] = '/';
                    Row11[14] = '^';
                    Row11[15] = '\\';
                    Row12[13] = '0';
                    Row12[14] = '¬';
                    Row12[15] = '0';
                }
                else if (g.Row == 5)
                {
                    Row17[13] = ' '; Row17[14] = ' '; Row17[15] = ' ';
                    Row18[13] = ' '; Row18[14] = ' '; Row18[15] = ' ';
                    Row14[13] = '/';
                    Row14[14] = '^';
                    Row14[15] = '\\';
                    Row15[13] = '0';
                    Row15[14] = '¬';
                    Row15[15] = '0';
                }
                else if (g.Row == 6)
                {
                    Row20[13] = ' '; Row20[14] = ' '; Row20[15] = ' ';
                    Row21[13] = ' '; Row21[14] = ' '; Row21[15] = ' ';
                    Row17[13] = '/';
                    Row17[14] = '^';
                    Row17[15] = '\\';
                    Row18[13] = '0';
                    Row18[14] = '¬';
                    Row18[15] = '0';
                }
                else if (g.Row == 7)
                {
                    Row23[13] = ' '; Row23[14] = ' '; Row23[15] = ' ';
                    Row24[13] = ' '; Row24[14] = ' '; Row24[15] = ' ';
                    Row20[13] = '/';
                    Row20[14] = '^';
                    Row20[15] = '\\';
                    Row21[13] = '0';
                    Row21[14] = '¬';
                    Row21[15] = '0';
                }
                else if (g.Row == 8)
                {
                    Row26[13] = ' '; Row26[14] = ' '; Row26[15] = ' ';
                    Row27[13] = ' '; Row27[14] = ' '; Row27[15] = ' ';
                    Row23[13] = '/';
                    Row23[14] = '^';
                    Row23[15] = '\\';
                    Row24[13] = '0';
                    Row24[14] = '¬';
                    Row24[15] = '0';
                }
                else if (g.Row == 9)
                {
                    Row26[13] = '/';
                    Row26[14] = '^';
                    Row26[15] = '\\';
                    Row27[13] = '0';
                    Row27[14] = '¬';
                    Row27[15] = '0';
                }
            }
            else if (g.Lane == 5)
            {
                if (g.Row == 1)
                {
                    Row2[17] = '/';
                    Row2[18] = '^';
                    Row2[19] = '\\';
                    Row3[17] = '0';
                    Row3[18] = '¬';
                    Row3[19] = '0';

                    Row5[17] = ' '; Row5[18] = ' '; Row5[19] = ' ';
                    Row6[17] = ' '; Row6[18] = ' '; Row6[19] = ' ';
                }
                else if (g.Row == 2)
                {
                    Row8[17] = ' '; Row8[18] = ' '; Row8[19] = ' ';
                    Row9[17] = ' '; Row9[18] = ' '; Row9[19] = ' ';
                    Row5[17] = '/';
                    Row5[18] = '^';
                    Row5[19] = '\\';
                    Row6[17] = '0';
                    Row6[18] = '¬';
                    Row6[19] = '0';
                    if (gargIsAlive[4])
                    {
                        gargAttackTimer[4]++;
                        if (gargAttackTimer[4] >= gargAttackMax)
                        {
                            gargAttackTimer[4] = 0;
                            g.Health -= gargDamage[4];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 3)
                {
                    Row11[17] = ' '; Row11[18] = ' '; Row11[19] = ' ';
                    Row12[17] = ' '; Row12[18] = ' '; Row12[19] = ' ';
                    Row8[17] = '/';
                    Row8[18] = '^';
                    Row8[19] = '\\';
                    Row9[17] = '0';
                    Row9[18] = '¬';
                    Row9[19] = '0';
                    if (gargIsAlive[9])
                    {
                        gargAttackTimer[9]++;
                        if (gargAttackTimer[9] >= gargAttackMax)
                        {
                            gargAttackTimer[9] = 0;
                            g.Health -= gargDamage[9];
                            Audio.Play("gnome_hurt");
                        }
                    }
                }
                else if (g.Row == 4)
                {
                    Row14[17] = ' '; Row14[18] = ' '; Row14[19] = ' ';
                    Row15[17] = ' '; Row15[18] = ' '; Row15[19] = ' ';
                    Row11[17] = '/';
                    Row11[18] = '^';
                    Row11[19] = '\\';
                    Row12[17] = '0';
                    Row12[18] = '¬';
                    Row12[19] = '0';
                }
                else if (g.Row == 5)
                {
                    Row17[17] = ' '; Row17[18] = ' '; Row17[19] = ' ';
                    Row18[17] = ' '; Row18[18] = ' '; Row18[19] = ' ';
                    Row14[17] = '/';
                    Row14[18] = '^';
                    Row14[19] = '\\';
                    Row15[17] = '0';
                    Row15[18] = '¬';
                    Row15[19] = '0';
                }
                else if (g.Row == 6)
                {
                    Row20[17] = ' '; Row20[18] = ' '; Row20[19] = ' ';
                    Row21[17] = ' '; Row21[18] = ' '; Row21[19] = ' ';
                    Row17[17] = '/';
                    Row17[18] = '^';
                    Row17[19] = '\\';
                    Row18[17] = '0';
                    Row18[18] = '¬';
                    Row18[19] = '0';
                }
                else if (g.Row == 7)
                {
                    Row23[17] = ' '; Row23[18] = ' '; Row23[19] = ' ';
                    Row24[17] = ' '; Row24[18] = ' '; Row24[19] = ' ';
                    Row20[17] = '/';
                    Row20[18] = '^';
                    Row20[19] = '\\';
                    Row21[17] = '0';
                    Row21[18] = '¬';
                    Row21[19] = '0';
                }
                else if (g.Row == 8)
                {
                    Row26[17] = ' '; Row26[18] = ' '; Row26[19] = ' ';
                    Row27[17] = ' '; Row27[18] = ' '; Row27[19] = ' ';
                    Row23[17] = '/';
                    Row23[18] = '^';
                    Row23[19] = '\\';
                    Row24[17] = '0';
                    Row24[18] = '¬';
                    Row24[19] = '0';
                }
                else if (g.Row == 9)
                {
                    Row26[17] = '/';
                    Row26[18] = '^';
                    Row26[19] = '\\';
                    Row27[17] = '0';
                    Row27[18] = '¬';
                    Row27[19] = '0';
                }
            }
        }
    }

}
