namespace DilemmaSvc.Domain.Model.Dilemma
{
    public class OptionContent
    {
        public string Description { get; }
        public byte[] Image { get; }

        public OptionContent(string description, byte[] image)
        {
            Description = description;
            Image = image;
        }
    }
}