namespace Ambev.DeveloperEvaluation.Common.Security;

public interface IBranch
{
    /// <summary>
    /// Gets the unique identifier of the branch.
    /// </summary>
    /// <returns>The branch's ID as a string.</returns>
    public string Id { get; }

    /// <summary>
    /// Gets the branch code.
    /// </summary>
    /// <returns>The branch code.</returns>
    public string Code { get; }
}
