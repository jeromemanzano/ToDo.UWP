
namespace ToDo.Helpers
{
    public interface IStringMetric
    {
        /// <summary>
        /// Computes and returns the distance between two string
        /// </summary>
        /// <param name="firstString">first string to be compared</param>
        /// <param name="secondString">second string to be compared</param>
        /// <returns>calculated value based on <see cref="firstString"/> and <see cref="secondString"/></returns>
        int Compute(string firstString, string secondString);
    }
}
