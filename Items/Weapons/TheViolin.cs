using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class TheViolin : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Violin");
			base.Tooltip.SetDefault("'There is someone out there who uses a violin for smashing zombies faces in...'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 36;
			base.item.melee = true;
			base.item.width = 58;
			base.item.height = 58;
			base.item.useTime = 12;
			base.item.useAnimation = 12;
			base.item.useStyle = 1;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 1, 0, 0);
			base.item.rare = 4;
			base.item.UseSound = base.mod.GetLegacySoundSlot(2, "Sounds/Item/TheViolinSound");
			base.item.autoReuse = true;
		}
	}
}
