using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class Holokey : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Holokey");
			base.Tooltip.SetDefault("'Unlocks Holochests found in a crashed spaceship'\nOnly one is needed");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 2));
		}

		public override void SetDefaults()
		{
			base.item.width = 14;
			base.item.height = 24;
			base.item.rare = 9;
			base.item.maxStack = 1;
			base.item.value = 0;
		}
	}
}
