using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

public class PropInfo : Panel
{
	public Label Label;

	public PropInfo()
	{
		Label = Add.Label( "", "value" );
	}

	public override void Tick()
	{
		var player = Local.Pawn as SandboxPlayer;
		if ( player == null ) return;

		var tr = Trace.Ray( player.EyePos, player.EyePos + player.EyeRot.Forward * 200.0f )
			.UseHitboxes()
			.Ignore( player )
			.HitLayer( CollisionLayer.Debris )
			.Run();

		if ( tr.Hit && !tr.Entity.IsWorld && tr.Entity.IsValid() )
		{
			if( tr.Entity is Prop trProp && trProp.MRPOwner.IsValid() )
			{
				var entCl = trProp.MRPOwner.GetClientOwner();
				Label.Text = $"Owner: {entCl.Name}";
			} else
			{
				Label.Text = "Owner: World";
			}

			SetClass( "active", true );
		} else
		{
			SetClass( "active", false );
		}
	}
}
