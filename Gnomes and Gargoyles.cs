using System;

class GridShell
{
    const int Rows = 9;
    const int Columns = 5;

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
