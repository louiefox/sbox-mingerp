using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Sandbox.UI
{
	public class BaseMoneyTag : Panel
	{
		public Label NameLabel;
		private MoneyEntity moneyEnt;

		public BaseMoneyTag( MoneyEntity moneyEnt )
		{
			this.moneyEnt = moneyEnt;
			NameLabel = Add.Label( "" );
		}

		public override void Tick()
		{
			base.Tick();

			NameLabel.Text = $"${String.Format( "{0:n0}", moneyEnt.Money )}";
		}
	}

	public class MoneyTags : Panel
	{
		Dictionary<MoneyEntity, BaseMoneyTag> ActiveTags = new Dictionary<MoneyEntity, BaseMoneyTag>();

		public float MaxDrawDistance = 400;
		public int MaxTagsToShow = 5;

		public MoneyTags()
		{
			StyleSheet.Load( "/ui/moneytags/MoneyTags.scss" );
		}

		public override void Tick()
		{
			base.Tick();

			var deleteList = new List<MoneyEntity>();
			deleteList.AddRange( ActiveTags.Keys );

			int count = 0;
			foreach ( var moneyEnt in Entity.All.OfType<MoneyEntity>().OrderBy( x => Vector3.DistanceBetween( x.Position, CurrentView.Position ) ) )
			{
				if ( UpdateNameTag( moneyEnt ) )
				{
					deleteList.Remove( moneyEnt );
					count++;
				}

				if ( count >= MaxTagsToShow )
					break;
			}

			foreach( var player in deleteList )
			{
				ActiveTags[player].Delete();
				ActiveTags.Remove( player );
			}
		}

		public virtual BaseMoneyTag CreateNameTag( MoneyEntity moneyEnt )
		{
			var tag = new BaseMoneyTag( moneyEnt );
			tag.Parent = this;
			return tag;
		}

		public bool UpdateNameTag( MoneyEntity moneyEnt )
		{
			//
			// Where we putting the label, in world coords
			//
			var labelPos = moneyEnt.Position;


			//
			// Are we too far away?
			//
			float dist = labelPos.Distance( CurrentView.Position );
			if ( dist > MaxDrawDistance )
				return false;

			//
			// Are we looking in this direction?
			//
			var lookDir = (labelPos - CurrentView.Position).Normal;
			if ( CurrentView.Rotation.Forward.Dot( lookDir ) < 0.5 )
				return false;

			// TODO - can we see them


			MaxDrawDistance = 400;

			// Max Draw Distance


			var alpha = dist.LerpInverse( MaxDrawDistance, MaxDrawDistance * 0.1f, true );

			// If I understood this I'd make it proper function
			var objectSize = 0.05f / dist / (2.0f * MathF.Tan( (CurrentView.FieldOfView / 2.0f).DegreeToRadian() )) * 1500.0f;

			objectSize = objectSize.Clamp( 0.05f, 1.0f );

			if ( !ActiveTags.TryGetValue( moneyEnt, out var tag ) )
			{
				tag = CreateNameTag( moneyEnt );
				if ( tag != null )
				{
					ActiveTags[moneyEnt] = tag;
				}
			}

			var screenPos = labelPos.ToScreen();

			tag.Style.Left = Length.Fraction( screenPos.x );
			tag.Style.Top = Length.Fraction( screenPos.y );
			tag.Style.Opacity = alpha;

			var transform = new PanelTransform();
			transform.AddTranslateY( Length.Fraction( -1.0f ) );
			transform.AddScale( objectSize );
			transform.AddTranslateX( Length.Fraction( -0.5f ) );

			tag.Style.Transform = transform;
			tag.Style.Dirty();

			return true;
		}
	}
}
