using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Materials.PreHM
{
	public class MoonflareFragment : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Moonflare Fragment");
			base.Tooltip.SetDefault("'Shines like the moon...'");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(8, 6));
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 20;
			base.item.maxStack = 999;
			base.item.value = 45;
			base.item.rare = 0;
		}
	}
}
