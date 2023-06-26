namespace TowerDefense
{
    public enum TypeTower : byte
    {
        ArrowTower = 0,
        SiegeTower = 1,
        IceTower = 2,
        PoisonTower = 3,
        ChaosTower = 4
    }

    public enum TowerFunctionalType : byte
    {
        Template = 0,
        Original = 1
    }
    public enum BuildMod : byte
    {
        On,
        Off
    }

    public enum TowerDamageEffects : byte
    {
        None = 0,
        Freezing = 1,
        Poisoning = 2
    }
    public enum MobType : byte
    {
        Zombie = 0,
        WarZombie = 1,
        SkeletZombie = 2,
        ArmoredZombie = 3,
        Mutant = 4,
        LightGuardian = 5,
        MediumGuardian = 6,
        Paladin = 7,
        Vampier = 8,
        Vanguard = 9,
        Warrok = 10
    }
    public enum ArmorMobType : byte
    {
        Light = 0,
        Medium = 1,
        Heavy = 2
    }
    public enum StateType : byte
    {
        Move = 0,
        Death = 1
    }
    public enum DamageTypeArmorType : byte // % нанесенного урона снарядом в зависимости от типа брони моба
    {
        ArrowLight = 130,
        ArrowMedium = 100,
        ArrowHeavy = 75,
        SiegeLight = 100,
        SiegeMedium = 100,
        SiegeHeavy = 100,
        IceLight = 85,
        IceMedium = 85,
        IceHeavy = 85,
        PoisonLight = 90,
        PoisonMedium = 100,
        PoisonHeavy = 110,
        ChaosLight = 130,
        ChaosMedium = 130,
        ChaosnHeavy = 130

    }
}
