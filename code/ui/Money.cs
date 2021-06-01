using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;

public class Money : Panel
{
	public Label Label;

	public Money()
	{
		Label = Add.Label( "$0", "value" );
	}

	public override void Tick()
	{
		var player = Local.Pawn as SandboxPlayer;
		if ( player == null ) return;

		Label.Text = $"${String.Format( "{0:n0}", player.Money )}";
	}
}
