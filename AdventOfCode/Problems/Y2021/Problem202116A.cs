using System.Text;

namespace AdventOfCode.Problems.Y2021;

internal class Problem202116A : IProblem
{
    public string Solve(ProblemInput input)
    {
        var hex = input.GetString();
        var binary = HexToBinary(hex);
        var packets = Parse(binary.AsSpan(), int.MaxValue, out _, true);

        return Visit(packets.Single()).ToString();
    }

    private static int Visit(Packet packet)
    {
        if (packet is LiteralPacket lp)
        {
            return lp.Version;
        }
        
        if (packet is OperatorPacket op)
        {
            return op.Version + op.Packets.Sum(Visit);
        }

        throw new NotSupportedException("Packet type not supported");
    }

    private Packet[] Parse(ReadOnlySpan<char> chars, int limit, out ReadOnlySpan<char> final, bool isOuter)
    {
        var results = new List<Packet>();

        var intBuilder = new IntBuilder();
        while (!IsFinished(chars, isOuter) && results.Count < limit)
        {
            intBuilder.Append(chars[..3]);
            var version = intBuilder.Pop();

            intBuilder.Append(chars[3..6]);
            var type = intBuilder.Pop();

            chars = chars[6..];

            if (type == 4)
            {
                var hasNext = true;
                while (hasNext)
                {
                    hasNext = chars[0] == '1';
                    intBuilder.Append(chars[1..5]);
                    chars = chars[5..];
                }

                var value = intBuilder.Pop();
                results.Add(new LiteralPacket(version, value));
            }
            else
            {
                Packet[] inner;
                var isBitLength = chars[0] == '0';
                if (isBitLength)
                {
                    intBuilder.Append(chars[1..16]);
                    var bitLength = intBuilder.Pop();
                    inner = Parse(chars[16..(16 + bitLength)], int.MaxValue, out _, false);
                    chars = chars[(16 + bitLength)..];
                }
                else
                {
                    intBuilder.Append(chars[1..12]);
                    var packetCount = intBuilder.Pop();
                    chars = chars[12..];
                    inner = Parse(chars, packetCount, out chars, false);
                }

                results.Add(new OperatorPacket(version, type, inner));
            }
        }

        final = chars;
        return results.ToArray();
    }

    private static bool IsFinished(ReadOnlySpan<char> chars, bool isOuter)
    {
        if (!isOuter)
        {
            return chars.Length == 0;
        }
        
        foreach (var c in chars)
        {
            if (c != '0')
            {
                return false;
            }
        }

        return true;
    }

    private class IntBuilder
    {
        private int _value;

        public void Append(ReadOnlySpan<char> span)
        {
            foreach (var c in span)
            {
                _value = (_value << 1) | (c == '1' ? 1 : 0);
            }
        }

        public int Pop()
        {
            var result = _value;
            _value = 0;
            return result;
        }
    }

    private static string HexToBinary(string hex)
    {
        var result = new StringBuilder();
        foreach (var c in hex)
        {
            result.Append(
                c switch
                {
                    '0' => "0000",
                    '1' => "0001",
                    '2' => "0010",
                    '3' => "0011",
                    '4' => "0100",
                    '5' => "0101",
                    '6' => "0110",
                    '7' => "0111",
                    '8' => "1000",
                    '9' => "1001",
                    'A' => "1010",
                    'B' => "1011",
                    'C' => "1100",
                    'D' => "1101",
                    'E' => "1110",
                    'F' => "1111",
                    _ => throw new ArgumentException("Not a valid hex string")
                });
        }
        return result.ToString();
    }

    private abstract class Packet
    {
        protected Packet(int version)
        {
            Version = version;
        }

        public int Version { get; }

        public abstract int Type { get; }

    }
    
    private sealed class LiteralPacket : Packet
    {
        public LiteralPacket(int version, int value) : base(version)
        {
            Value = value;
        }

        public override int Type => 4;

        public int Value { get; }

        public override string ToString() => $"[{Version}, Value: {Value}]";
    }

    private sealed class OperatorPacket : Packet
    {
        public OperatorPacket(int version, int type, Packet[] packets) : base(version)
        {
            Packets = packets;
        }

        public override int Type { get; }

        public Packet[] Packets { get; }

        public override string ToString() => $"[{Version}, {Type}, Packets: {Packets.Length}]";
    }
}