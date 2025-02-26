namespace Ambev.DeveloperEvaluation.Common.Security;

public interface ISale
{
    /// <summary>
    /// Gets the unique identifier of the sale.
    /// </summary>
    /// <returns>The sale's ID as a string.</returns>
    public string Id { get; }
}