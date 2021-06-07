using Sandbox;
using System;

[Library( "ent_money", Title = "Money", Spawnable = true )]
public partial class MoneyEntity : Prop, IUse
{
	[Net]
	public int Money { get; set; } = 1000;

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/mingerp/money_prop.vmdl" );
		SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );
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

			var combineEffect = Particles.Create( "particles/money_combine.vpcf" );
			combineEffect.SetPos( 0, Position );

			if ( Host.IsServer ) { otherEnt.Delete(); }
		}
	}

	public bool OnUse( Entity user )
	{
		if( user is SandboxPlayer ply )
		{
			ply.Money += Money;

			var pickupEffect = Particles.Create( "particles/money_pickup.vpcf" );
			pickupEffect.SetPos( 0, Position );

			Delete();
		}

		return false;
	}
}
