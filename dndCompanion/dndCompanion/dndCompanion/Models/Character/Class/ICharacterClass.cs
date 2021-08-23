namespace dndCompanion.Models.Character.Class
{
    public interface ICharacterClass
    {
        string Name { get; }
        //string Description { get; }
    }
    public abstract class CharacterClass : ICharacterClass
    {
        public abstract string Name { get; }
        //public abstract string Description { get; }
    }
}
