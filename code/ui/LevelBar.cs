
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

public class LevelBar : Panel
{
	public Label CurrentLevel;
	public Label Experience;
	public Label NextLevel;
	public Panel BarExp;

	public LevelBar()
	{
		Panel BarPanel = Add.Panel( "bar" );
		BarExp = BarPanel.Add.Panel( "barExp" );

		Panel TextPanel = Add.Panel( "textRow" );
		CurrentLevel = TextPanel.Add.Label( "Lvl 1", "currentLevel" );
		Experience = TextPanel.Add.Label( "0/500", "experience" );
		NextLevel = TextPanel.Add.Label( "Lvl 2", "nextLevel" );
	}

	public override void Tick()
	{
		var player = Local.Pawn as DeathmatchPlayer;
		if ( player == null ) return;

		CurrentLevel.Text = $"Lvl {player.Level}";
		Experience.Text = $"{player.Experience}/{player.GetRequiredExp()}";
		NextLevel.Text = $"Lvl {player.Level+1}";

		BarExp.Style.Dirty();
		BarExp.Style.Width = Length.Percent( ((float)player.Experience / (float)player.GetRequiredExp()) * 100f );
	}
}
