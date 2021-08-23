namespace dndCompanion.Models.Character.Class
{
    public interface ICasterClass: ICharacterClass
    {
    }

    public abstract class CasterClass : CharacterClass, ICasterClass
    {
    }
}
