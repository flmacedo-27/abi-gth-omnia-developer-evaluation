namespace Ambev.DeveloperEvaluation.Common.Security;

public interface ISaleItem
{
    /// <summary>
    /// Gets the unique identifier of the sale item.
    /// </summary>
    /// <returns>The sale's item ID as a string.</returns>
    public string Id { get; }
}