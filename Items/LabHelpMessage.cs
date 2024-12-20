using System;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class LabHelpMessage : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hint");
			base.Tooltip.SetDefault("The Abandoned Lab will always generate to the right of the spawn\nUsually somewhere between the spawn and Jungle/Dungeon, near Hell\nPlaying on a Small World makes it very easy to find the Lab by flying through Hell, as the bottom of the Lab can stick out\nUsing Calamity will alter the generation by a little, which could cause the Lab generating on top of the Jungle Temple, but its a small chance");
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
