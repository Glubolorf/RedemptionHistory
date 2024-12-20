using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class NoblesSwordPlatinum : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Platinum Noble's Sword");
		}

		public override void SetDefaults()
		{
			base.item.damage = 28;
			base.item.melee = true;
			base.item.width = 36;
			base.item.height = 34;
			base.item.useTime = 20;
			base.item.useAnimation = 20;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 2, 60, 0);
			base.item.rare = 2;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}
	}
}
