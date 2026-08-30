using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Generator.Tests.Unit.DocSamples
{
    /// <summary>
    ///     Cuts a readable snippet out of a whole generated file.
    /// </summary>
    /// <remarks>
    ///     A generated file is several hundred lines and nobody reads one on a wiki page. These two strategies
    ///     produce something short that is still verbatim generator output:
    ///     <see cref="Section"/> when the interesting part is a named block, and <see cref="ChangedRegion"/>
    ///     when it is wherever the two runs happen to differ. Neither ever edits a line, so what appears on the
    ///     page is exactly what came out.
    /// </remarks>
    public static class DocSampleExtractor
    {
        /// <summary>
        ///     Returns the class, interface or method block whose declaration line contains <paramref name="marker"/>,
        ///     from its first attribute or comment down to its closing brace.
        /// </summary>
        public static string Section(string generated, string marker)
        {
            var lines = Lines(generated);
            var start = lines.FindIndex(l => l.Contains(marker));
            if (start < 0)
                throw new ArgumentException(string.Format("No line in the generated output contains '{0}'.", marker));

            // Walk back over attributes, comments and blank lines so the block keeps its header
            var from = start;
            while (from > 0)
            {
                var previous = lines[from - 1].Trim();
                if (previous.StartsWith("[") || previous.StartsWith("//") || previous.StartsWith("///"))
                    from--;
                else
                    break;
            }

            var depth = 0;
            var opened = false;
            var to = start;
            for (var i = start; i < lines.Count; i++)
            {
                depth += lines[i].Count(c => c == '{');
                if (depth > 0) opened = true;
                depth -= lines[i].Count(c => c == '}');

                to = i;
                if (opened && depth <= 0)
                    break;
            }

            return Join(lines.Skip(from).Take(to - from + 1));
        }

        /// <summary>
        ///     Returns the lines of <paramref name="side"/> that differ from the other side, plus
        ///     <paramref name="context"/> lines either side, so the reader can see where the change sits.
        /// </summary>
        /// <remarks>
        ///     Line-based longest common subsequence. Generated files are the friendly case for this - the two runs
        ///     are the same file with a handful of lines altered - so there is no need for anything cleverer.
        /// </remarks>
        public static string ChangedRegion(string side, string other, int context = 2)
        {
            var a = Lines(side);
            var b = Lines(other);
            var common = LongestCommonSubsequence(a, b);

            var keep = new HashSet<int>();
            for (var i = 0; i < a.Count; i++)
            {
                if (common.Contains(i))
                    continue;

                for (var j = Math.Max(0, i - context); j <= Math.Min(a.Count - 1, i + context); j++)
                    keep.Add(j);
            }

            if (keep.Count == 0)
                return string.Empty;

            var sb = new StringBuilder();
            var previous = -1;
            foreach (var index in keep.OrderBy(x => x))
            {
                if (previous >= 0 && index > previous + 1)
                    sb.AppendLine("    // ...");
                sb.AppendLine(a[index]);
                previous = index;
            }

            return sb.ToString().Replace("\r\n", "\n").TrimEnd('\n');
        }

        /// <summary>
        ///     Which line indexes of <paramref name="a"/> also appear, in order, in <paramref name="b"/>.
        /// </summary>
        private static HashSet<int> LongestCommonSubsequence(List<string> a, List<string> b)
        {
            var lengths = new int[a.Count + 1, b.Count + 1];
            for (var i = a.Count - 1; i >= 0; i--)
                for (var j = b.Count - 1; j >= 0; j--)
                    lengths[i, j] = a[i] == b[j]
                        ? lengths[i + 1, j + 1] + 1
                        : Math.Max(lengths[i + 1, j], lengths[i, j + 1]);

            var result = new HashSet<int>();
            int x = 0, y = 0;
            while (x < a.Count && y < b.Count)
            {
                if (a[x] == b[y])
                {
                    result.Add(x);
                    x++;
                    y++;
                }
                else if (lengths[x + 1, y] >= lengths[x, y + 1])
                    x++;
                else
                    y++;
            }

            return result;
        }

        private static List<string> Lines(string text)
        {
            return text.Replace("\r\n", "\n").Split('\n').ToList();
        }

        private static string Join(IEnumerable<string> lines)
        {
            return string.Join("\n", lines).TrimEnd('\n');
        }
    }
}
