namespace AdventOfCode.Problems
{
    internal class Problem202104A : IProblem
    {
        public string Solve(ProblemInput input)
        {
            var lines = input.GetStrings();

            var called = Array.ConvertAll(lines[0].Split(','), int.Parse);

            var boards = new List<int[][]>();

            int i = 2;
            while (i < lines.Length)
            {
                var board = new List<int[]>();
                for (int x = 0; x < 5; x++)
                {
                    board.Add(Array.ConvertAll(lines[i++].Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse));
                }
                i++;
                boards.Add(board.ToArray());
            }

            var winningIndex = int.MaxValue;
            var winningScore = -1;
            foreach (var board in boards)
            {
                var (index, score) = CalculateScore(board, called);
                if (index < winningIndex)
                {
                    winningIndex = index;
                    winningScore = score;
                }
            }

            return winningScore.ToString();
        }

        private (int index, int score) CalculateScore(int[][] board, int[] called)
        {
            for (int iteration = 0; iteration < called.Length; iteration++)
            {
                int num = called[iteration];
                for (int i = 0; i < board.Length; i++)
                {
                    for (int j = 0; j < board.Length; j++)
                    {
                        if (board[i][j] == num)
                        {
                            board[i][j] = -1;
                        }
                    }
                }

                var success =
                    Enumerable.Range(0, board.Length)
                        .Any(i => Enumerable.Range(0, board.Length).All(j => board[i][j] == -1)
                               || Enumerable.Range(0, board.Length).All(j => board[j][i] == -1));
                if (success)
                {
                    var sum = board.SelectMany(b => b).Where(n => n != -1).Sum();
                    return (iteration, sum * num);
                }
            }

            return (int.MaxValue, 0);
        }
    }
}
