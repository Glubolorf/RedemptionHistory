using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class ViolinString : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Violin Bow");
			base.Tooltip.SetDefault("'The bow appears to be indestructible...'");
		}

		public override void SetDefaults()
		{
			base.item.damage = 18;
			base.item.melee = true;
			base.item.width = 60;
			base.item.height = 62;
			base.item.useTime = 6;
			base.item.useAnimation = 6;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 0, 5, 0);
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}
	}
}
