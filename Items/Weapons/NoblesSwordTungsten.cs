using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class NoblesSwordTungsten : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tungsten Noble's Sword");
		}

		public override void SetDefaults()
		{
			base.item.damage = 24;
			base.item.melee = true;
			base.item.width = 34;
			base.item.height = 32;
			base.item.useTime = 26;
			base.item.useAnimation = 26;
			base.item.useStyle = 1;
			base.item.knockBack = 4f;
			base.item.value = Item.buyPrice(0, 1, 60, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
		}
	}
}
