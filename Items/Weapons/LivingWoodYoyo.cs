using System;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class LivingWoodYoyo : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Wood Yoyo");
		}

		public override void SetDefaults()
		{
			base.item.damage = 10;
			base.item.melee = true;
			base.item.useTime = 22;
			base.item.useAnimation = 22;
			base.item.useStyle = 5;
			base.item.channel = true;
			base.item.knockBack = 2f;
			base.item.value = Item.buyPrice(0, 0, 5, 0);
			base.item.rare = 0;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<LivingWoodYoyoPro>();
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.UseSound = SoundID.Item1;
		}
	}
}
