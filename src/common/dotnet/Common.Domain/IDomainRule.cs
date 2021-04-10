namespace DilemmaApp.Services.Common.Domain
{
    /// <summary>
    /// Contract for objects responsible for checking a domain business rule.
    /// </summary>
    public interface IDomainRule
    {
        /// <summary>
        /// Error code associated this business rule.
        /// </summary>
        string ErrorCode { get; }
        /// <summary>
        /// Ret
        /// </summary>
        /// <returns>True if the business rule has not been broken, otherwise false.</returns>
        bool Valid();
    }
}