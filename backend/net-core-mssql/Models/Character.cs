using System.Collections.Generic;

namespace net_core_mssql.Models
{
    public enum RpgClass
    {
        Knight = 1,
        Mage = 2,
        Cleric = 3
    }

    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;

        // The property UserId is necessary because we have to define this foreign key when seeding the data. 
        public int UserId { get; set; }
        
        public User User { get; set; }

        // Character - Weapon: 1-1 relationship, the dependency side is weapon.
        public Weapon Weapon { get; set; }
        public List<CharacterSkill> CharacterSkills { get; set; }
    }
}