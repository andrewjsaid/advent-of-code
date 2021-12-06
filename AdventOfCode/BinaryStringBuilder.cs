using System.Text;

namespace AdventOfCode
{
    internal class BinaryStringBuilder
    {
        private StringBuilder _sb = new StringBuilder();

        public void AddZero() => _sb.Append("0");

        public void AddOne() => _sb.Append("1");

        public int Build() => Parse(_sb.ToString());

        public void Clear() => _sb.Clear();

        public override string ToString() => _sb.ToString();

        public static int Parse(string s)
        {
            var result = 0;
            for (int i = 0; i < s.Length; i++)
            {
                result *= 2;
                if (s[i] == '1')
                {
                    result += 1;
                }
            }
            return result;
        }
    }
}
