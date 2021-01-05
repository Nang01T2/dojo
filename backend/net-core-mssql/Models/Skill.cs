using System.Collections.Generic;

namespace net_core_mssql.Models
{
  public class Skill
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Damage { get; set; }
    public List<CharacterSkill> CharacterSkills { get; set; }
  }
}