using Sandbox;
using Sandbox.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

partial class OwnableProp : Prop
{
	[Net]
	public Player MRPOwner { get; set; }
}
