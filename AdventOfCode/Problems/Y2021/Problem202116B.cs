using System.Text;

namespace AdventOfCode.Problems.Y2021;

internal class Problem202116B : IProblem
{
    public string Solve(ProblemInput input)
    {
        var hex = input.GetString();
        var binary = HexToBinary(hex);
        var packets = Parse(binary.AsSpan(), int.MaxValue, out _, true);

        return Visit(packets.Single()).ToString();
    }

    private static long Visit(Packet packet)
    {
        if (packet is LiteralPacket lp)
        {
            return lp.Value;
        }
        
        if (packet is OperatorPacket op)
        {
            return op.Type switch
            {
                0 => // sum
                    op.Packets.Sum(Visit),

                1 => // product
                    op.Packets.Aggregate(1L, (acc, p) => acc * Visit(p)),

                2 => // min
                    op.Packets.Min(Visit),

                3 => // max
                    op.Packets.Max(Visit),

                5 => // gt
                    Visit(op.Packets[0]) > Visit(op.Packets[1]) ? 1 : 0,

                6 => // lt
                    Visit(op.Packets[0]) < Visit(op.Packets[1]) ? 1 : 0,

                7 => // eq
                    Visit(op.Packets[0]) == Visit(op.Packets[1]) ? 1 : 0,

                _ => throw new ArgumentOutOfRangeException(nameof(op.Type))
            };
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
            var version = intBuilder.Pop32();

            intBuilder.Append(chars[3..6]);
            var type = intBuilder.Pop32();

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

                var value = intBuilder.Pop64();
                results.Add(new LiteralPacket(version, value));
            }
            else
            {
                Packet[] inner;
                var isBitLength = chars[0] == '0';
                if (isBitLength)
                {
                    intBuilder.Append(chars[1..16]);
                    var bitLength = intBuilder.Pop32();
                    inner = Parse(chars[16..(16 + bitLength)], int.MaxValue, out _, false);
                    chars = chars[(16 + bitLength)..];
                }
                else
                {
                    intBuilder.Append(chars[1..12]);
                    var packetCount = intBuilder.Pop32();
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
        private long _value;

        public void Append(ReadOnlySpan<char> span)
        {
            foreach (var c in span)
            {
                _value = (_value << 1) | (c == '1' ? 1L : 0L);
            }
        }

        public int Pop32()
        {
            var result = (int)_value;
            _value = 0;
            return result;
        }

        public long Pop64()
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
        public LiteralPacket(int version, long value) : base(version)
        {
            Value = value;
        }

        public override int Type => 4;

        public long Value { get; }

        public override string ToString() => $"{Value}";
    }

    private sealed class OperatorPacket : Packet
    {
        public OperatorPacket(int version, int type, Packet[] packets) : base(version)
        {
            Packets = packets;
            Type = type;
        }

        public override int Type { get; }

        public Packet[] Packets { get; }

        public override string ToString() => Type switch
        {
            0 => "sum",
            1 => "product",
            2 => "min",
            3 => "max",
            5 => "gt",
            6 => "lt",
            7 => "eq",
            _ => throw new ArgumentOutOfRangeException(nameof(Type))
        };
    }
}
