namespace Noo.Api.Works.Types;

public enum WorkTaskCheckStrategy
{
    /// <summary>
    /// Manual check
    /// </summary>
    Manual,

    /// <summary>
    /// Check if the answer is exactly the same as the correct answer, otherwise 0 points
    /// </summary>
    ExactMatchOrZero,

    /// <summary>
    /// Check if the answer is exactly the same as the correct answer, otherwise 0 points
    /// </summary>
    MinusPointForEveryWrongCharacter,

    /// <summary>
    /// Check if the answer is exactly the same as the correct answer, otherwise 0 points
    /// </summary>
    MinusPointForMissingOrWrongOrExtraCharacter,

    /// <summary>
    /// Check if the answer is exactly the same as the correct answer, otherwise 0 points
    /// </summary>
    Type4
}
