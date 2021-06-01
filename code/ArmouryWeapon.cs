using Sandbox;
using System;
using System.Collections.Generic;

public class ArmouryWeapon
{
	public static Dictionary<string, ArmouryWeapon> Weapons = new Dictionary<string, ArmouryWeapon>();

	public string Name;
	public string Class;
	public string Model;
	public Angles AngleOffset;

	public ArmouryWeapon( string name, string wepClass, string model )
	{
		Name = name;
		Class = wepClass;
		Model = model;

		foreach( var item in Weapons )
		{
			if( item.Key == wepClass )
			{
				Weapons.Remove( wepClass );
				break;
			}
		}

		Weapons.Add( wepClass, this );
	}
}
