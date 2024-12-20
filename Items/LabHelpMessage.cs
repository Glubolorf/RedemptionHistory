using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class LabHelpMessage : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hint");
			base.Tooltip.SetDefault("The Abandoned Lab will always generate to the right of the spawn\nUsually somewhere between the spawn and Jungle/Dungeon, near Hell\nPlaying on a Small World makes it very easy to find the Lab by flying through Hell, as the bottom of the Lab can stick out");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 24;
			base.item.maxStack = 1;
			base.item.value = 0;
			base.item.rare = 7;
		}
	}
}
