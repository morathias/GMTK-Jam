using System;

public interface IWeapon
{
    void AddCooldown(float time);
    void Block();
    void Resume();
    bool HasAmmo { get; }
}
