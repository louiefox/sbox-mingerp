
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using Ascension.UI;

namespace Ascension.UI.MainMenu
{
	public class StatisticsPage : Panel
	{
		private Label SkillPoints;

		public StatisticsPage()
		{
			StyleSheet.Load( "/ui/mainmenu/StatisticsPage.scss" );

			// Skills
			Panel skillsPanel = AddChild<Panel>( "skillspanel" );
			SkillPoints = skillsPanel.Add.Label( "Skill Points: 0", "skillpoints" );
		}

		public override void Tick()
		{
			base.Tick();

			DeathmatchPlayer ply = Local.Pawn as DeathmatchPlayer;
			if ( ply == null ) { return; }

			SkillPoints.Text = $"Skill Points: {ply.SkillPoints}";
		}
	}
}
