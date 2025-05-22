namespace Noo.Api.Works.Types;

public enum WorkTaskCheckStrategy
{
    /// <summary>
    /// Manual check
    /// </summary>
    Manual,

    /// <summary>
    /// Identical to the answer key: max points, otherwise 0
    /// </summary>
    ExactMatchOrZero,

    /// <summary>
    /// Аully correct order: maximum points, for every wrong one - minus 1 point
    /// </summary>
    ExactMatchWithWrongCharacter,

    /// <summary>
    /// fully correct, order does not matter: maximum points;
    /// for every mistake (extra and missing choice counts as well) - minus one
    /// </summary>
    MultipleChoice,

    /// <summary>
    /// fully correct order: maximum points;
    /// 2 characters swapped with one another - minus 1 point
    /// </summary>
    Sequence
}
