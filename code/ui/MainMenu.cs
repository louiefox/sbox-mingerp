using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using Ascension.UI;

public class MainMenu : Panel
{
	private bool IsOpen = false;
	private float LastToggleTime = 0;

	private Panel NavigationBar;
	private Panel PageContainer;

	private string ActivePage = "";
	private Dictionary<string, Sandbox.UI.Label> Buttons = new Dictionary<string, Sandbox.UI.Label>();

	public MainMenu()
	{
		StyleSheet.Load( "/ui/MainMenu.scss" );

		NavigationBar = Add.Panel( "navBar" );
		PageContainer = Add.Panel( "pageArea" );

		AddPage( "ARMOURY", () => PageContainer.AddChild<Ascension.UI.MainMenu.ArmouryPage>() );
		AddPage( "STATISTICS", () => PageContainer.AddChild<Ascension.UI.MainMenu.StatisticsPage>() );
		AddPage( "SOCIAL", () => PageContainer.AddChild<Ascension.UI.MainMenu.SocialPage>() );
	}

	private void AddPage( string name, Func<Panel> act = null )
	{
		var button = NavigationBar.Add.Label( name, "navButton" );
		button.AddEvent( "onclick", () => { 
			SwitchPage( name );
			act?.Invoke().AddClass( "page" ); 
		} );

		Buttons[name] = button;

		if( ActivePage == "" )
		{
			SwitchPage( name );
			act?.Invoke().AddClass( "page" );
			ActivePage = name;
		}
	}

	private void SwitchPage( string name )
	{
		PageContainer.DeleteChildren();

		foreach ( var child in NavigationBar.Children )
		{
			if( child is Sandbox.UI.Label button )
			{
				child.SetClass( "active", false );
			}
		}

		Buttons[name].SetClass( "active", true );
	}

	public override void Tick()
	{
		base.Tick();

		if( Local.Client.Input.Pressed( InputButton.Menu ) && Time.Now >= LastToggleTime+.1f )
		{
			LastToggleTime = Time.Now;
			IsOpen = !IsOpen;

			SetClass( "open", IsOpen );
		}
	}
}
