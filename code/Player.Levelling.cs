using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;

partial class DeathmatchPlayer
{
	[Net, Local]
	public int Experience { get; set; }

	[Net, Local]
	public int Level { get; set; }	
	
	[Net, Local]
	public int SkillPoints { get; set; }

	public void AddExperience(int experience)
	{
		int newExp = Experience + experience;
		int newLevel = Level;

		while( newExp >= GetRequiredExp(newLevel) )
		{
			newExp -= GetRequiredExp( newLevel );
			newLevel++;
		}

		Experience = newExp;

		if(newLevel != Level)
		{
			Level = newLevel;
			Health = MaxHealth;
			Armour = MaxArmour;
		}
	}	
	
	public int GetRequiredExp()
	{
		return (int)(Level * 500 * .75);
	}	
	
	public int GetRequiredExp(int level)
	{
		return (int)(level * 500 * .75);
	}
}
