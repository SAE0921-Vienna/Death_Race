namespace Weapons
{
    public interface IWeapon
    {
        public void Shoot();
        public int GetAmmo();
        public float GetFireRate();
    }
}