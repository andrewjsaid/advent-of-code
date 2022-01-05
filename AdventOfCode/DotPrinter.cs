namespace AdventOfCode
{
    public static class DotPrinter
    {
        public static void Print((int x, int y)[] dots)
        {
            Console.WriteLine();
            var maxX = dots.Max(d => d.x);
            var maxY = dots.Max(d => d.y);
            for (var i = 0; i <= maxY; i++)
            {
                var chars = new char[maxX + 1];
                for (int j = 0; j < chars.Length; j++)
                {
                    chars[j] = ' ';
                }

                foreach (var (x, y) in dots)
                {
                    if (y == i)
                    {
                        chars[x] = 'X';
                    }
                }

                var line = new string(chars);
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }
    }
}
