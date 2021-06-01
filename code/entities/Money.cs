using Sandbox;
using System;

[Library( "ent_money", Title = "Money", Spawnable = true )]
public partial class MoneyEntity : Prop, IUse
{
	[Net]
	public int Money { get; set; }

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/citizen_props/newspaper01.vmdl_c" );
		SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
		Scale = 0.3f;
		RenderColor = new Color( 0f, 1f, 0f );

		Money = 1000;
	}

	public bool IsUsable( Entity user )
	{
		return true;
	}

	public override void StartTouch( Entity other )
	{
		base.StartTouch( other );

		if( other is MoneyEntity otherEnt )
		{
			Money += otherEnt.Money;

			var freezeEffect = Particles.Create( "particles/money_combine.vpcf" );
			freezeEffect.SetPos( 0, Position );

			otherEnt.Delete();
		}
	}

	public bool OnUse( Entity user )
	{
		if( user is SandboxPlayer ply )
		{
			ply.Money += Money;

			var freezeEffect = Particles.Create( "particles/money_pickup.vpcf" );
			freezeEffect.SetPos( 0, Position );

			Delete();
		}

		return false;
	}
}
