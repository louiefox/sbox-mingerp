using Sandbox;
using System;

[Library( "ent_moneyprinter", Title = "Money Printer", Spawnable = true )]
public partial class MoneyPrinterEntity : Prop
{
	[Net]
	public TimeSince LastPrint { get; set; }

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "models/citizen_props/trashcan02.vmdl_c" );
		SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );

		LastPrint = 0;
	}

	[Event( "server.tick" )]
	private void SpawnMoney()
	{
		if( LastPrint < 5 ) { return; }
		LastPrint = 0;

		new MoneyEntity
		{
			Position = Position + Rotation.Up * 20,
			Rotation = Rotation,
			Velocity = Rotation.Forward * 150,
			Money = 1000
		};
	}
}
