
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using Sandbox.UI.Tests;
using System.Linq;
using System.Collections.Generic;
using Ascension.UI;

namespace Ascension.UI.MainMenu
{
	public class ArmouryPage : Panel
	{
		private Label PrimaryName;
		private ModelDisplay PrimaryModel;		
		
		private Label SecondaryName;
		private ModelDisplay SecondaryModel;

		public ArmouryPage()
		{
			StyleSheet.Load( "/ui/mainmenu/ArmouryPage.scss" );

			Panel slotRow = Add.Panel( "slotrow" );

			// Primary Slot
			Panel primarySlot = slotRow.AddChild<Panel>( "slot primary" );
			primarySlot.Add.Label( "PRIMARY", "slotlabel" );

			PrimaryName = primarySlot.Add.Label( "Empty", "slotweaponname" );

			PrimaryModel = primarySlot.AddChild<ModelDisplay>();
			PrimaryModel.Style.Set( "width: 100%; height: 100%;" );

			SetPrimaryWeapon( "dm_shotgun" );

			// Secondary Slot
			Panel secondarySlot = slotRow.AddChild<Panel>( "slot" );
			secondarySlot.Add.Label( "SECONDARY", "slotlabel" );

			SecondaryName = secondarySlot.Add.Label( "Empty", "slotweaponname" );

			SecondaryModel = secondarySlot.AddChild<ModelDisplay>();
			SecondaryModel.Style.Set( "width: 100%; height: 100%;" );

			SetSecondaryWeapon( "dm_smg" );

			// Weapon List
			Add.Label( "WEAPONS", "weapons" );

			var data = new List<string>();

			foreach ( var entry in ArmouryWeapon.Weapons )
			{
				ArmouryWeapon weapon = entry.Value;
				data.Add( weapon.Name );
			}			
			foreach ( var entry in ArmouryWeapon.Weapons )
			{
				ArmouryWeapon weapon = entry.Value;
				data.Add( weapon.Name );
			}			
			foreach ( var entry in ArmouryWeapon.Weapons )
			{
				ArmouryWeapon weapon = entry.Value;
				data.Add( weapon.Name );
			}			foreach ( var entry in ArmouryWeapon.Weapons )
			{
				ArmouryWeapon weapon = entry.Value;
				data.Add( weapon.Name );
			}			foreach ( var entry in ArmouryWeapon.Weapons )
			{
				ArmouryWeapon weapon = entry.Value;
				data.Add( weapon.Name );
			}			foreach ( var entry in ArmouryWeapon.Weapons )
			{
				ArmouryWeapon weapon = entry.Value;
				data.Add( weapon.Name );
			}

			var vs = AddChild<VirtualScrollPanel<Label>>( "weaponscontainer" );

			vs.Data.AddRange( data.Select( x => (object)x ) );
			vs.Layout.ItemSize = new Vector2( 200, 100 );
			vs.Layout.AutoColumns = true;

			//Panel weaponsContainer = AddChild<Panel>( "weaponscontainer" );	

			//foreach( var entry in ArmouryWeapon.Weapons )
			//{
			//	ArmouryWeapon weapon = entry.Value;

			//	Panel weaponPanel = weaponsContainer.Add.Panel( "weaponspanel" );
			//	weaponPanel.Add.Label( weapon.Name, "weaponsname" );
			//}
		}

		public void SetPrimaryWeapon( string wepClass )
		{
			ArmouryWeapon weapon = ArmouryWeapon.Weapons[wepClass];
			PrimaryName.Text = weapon.Name;
			PrimaryModel.UpdateModel( weapon.Model, weapon.AngleOffset );
		}		
		
		public void SetSecondaryWeapon( string wepClass )
		{
			ArmouryWeapon weapon = ArmouryWeapon.Weapons[wepClass];
			SecondaryName.Text = weapon.Name;
			SecondaryModel.UpdateModel( weapon.Model, weapon.AngleOffset );
		}

		public override void Tick()
		{
			base.Tick();

			DeathmatchPlayer ply = Local.Pawn as DeathmatchPlayer;
			if ( ply == null ) { return; }
		}
	}
}
