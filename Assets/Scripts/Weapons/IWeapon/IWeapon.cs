public interface IWeapon
{
    void AddCooldown(float time);
    void Block();
    void Resume();
}
