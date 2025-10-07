using System;

class GridShell
{
    const int Rows = 9;
    const int Columns = 5;

    public char[] Row1;
    public char[] Row2;
    public char[] Row3;
    public char[] Row4;
    public char[] Row5;
    public char[] Row6;
    public char[] Row7;
    public char[] Row8;
    public char[] Row9;
    public char[] Row10;
    public char[] Row11;
    public char[] Row12;
    public char[] Row13;
    public char[] Row14;
    public char[] Row15;
    public char[] Row16;
    public char[] Row17;
    public char[] Row18;
    public char[] Row19;
    public char[] Row20;
    public char[] Row21;
    public char[] Row22;
    public char[] Row23;
    public char[] Row24;
    public char[] Row25;
    public char[] Row26;
    public char[] Row27;
    public char[] Row28;

    void Start()
    {


    string[] Lines = [
        (Row1(1) )
        ]; 
    }


    static void Main()
    {
        DrawGridShell();
    }

    static void DrawGridShell()
    {
        Console.Clear();

        for (int row = 0; row < Rows; row++)
        {
            // Top border of every single row
            DrawHorizontalLine();

            // Empty line so that te cells can be filled with ASCII text
            DrawEmptyLine();
        }

        // Draw bottom border of the grid
        DrawHorizontalLine();
    }

    static void DrawHorizontalLine()
    {
        for (int col = 0; col < Columns; col++) // Loop that prints the lines to create the horizontal part in the grid
        {
            Console.Write("+---");
        }
        Console.WriteLine("+"); // Outside of this loop this just finishes off the border 
    }

    static void DrawEmptyLine()  // Another loop that 
    {
        for (int col = 0; col < Columns; col++)
        {
            Console.Write("|   ");
        }
        Console.WriteLine("|");
    }
}
