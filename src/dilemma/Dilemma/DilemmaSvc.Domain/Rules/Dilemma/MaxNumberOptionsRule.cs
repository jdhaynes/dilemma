using Common.Domain;

namespace Dilemma.Rules
{
    public class MaxNumberOptionsRule : IDomainRule
    {
        public string ErrorCode { get; }
        public bool Valid()
        {
            throw new System.NotImplementedException();
        }
    }
}