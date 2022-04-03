using System;

namespace ToDo.Helpers
{
    /// <footer>
    /// This implementation is based on https://www.csharpstar.com/csharp-string-distance-algorithm/#:~:text=The%20Levenshtein%20distance%20between%20two,is%20named%20after%20Vladimir%20Levenshtein and
    /// https://en.wikipedia.org/wiki/Levenshtein_distance#:~:text=Informally%2C%20the%20Levenshtein%20distance%20between,considered%20this%20distance%20in%201965.
    /// </footer>
    public class LevenshteinDistance : IStringDistance
    {
        /// <inheritdoc/>
        public int Compute(string firstString, string secondString)
        {
            var firstStringLength = firstString.Length;
            var secondStringLength = secondString.Length;
            var matrix = new int[firstStringLength + 1, secondStringLength + 1];

            // If any of the strings length is 0, we return the length of other string
            if (firstStringLength == 0)
                return secondStringLength;

            if (secondStringLength == 0)
                return firstStringLength;

            // Matrix initialization
            for (var i = 0; i <= firstStringLength; matrix[i, 0] = i++) { }
            for (var j = 0; j <= secondStringLength; matrix[0, j] = j++) { }

            // Calculate distance
            for (var i = 1; i <= firstStringLength; i++)
            {
                for (var j = 1; j <= secondStringLength; j++)
                {
                    var cost = (secondString[j - 1] == firstString[i - 1]) ? 0 : 1;

                    matrix[i, j] = Math.Min(
                        Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), 
                            matrix[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return matrix[firstStringLength, secondStringLength];
        }
    }
}
