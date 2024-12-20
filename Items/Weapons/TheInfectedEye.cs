using System;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class TheInfectedEye : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Infected Eye");
		}

		public override void SetDefaults()
		{
			base.item.damage = 41;
			base.item.melee = true;
			base.item.useTime = 24;
			base.item.useAnimation = 24;
			base.item.useStyle = 5;
			base.item.channel = true;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 1, 50, 0);
			base.item.rare = 7;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<TheInfectedEyePro>();
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.UseSound = SoundID.Item1;
		}
	}
}
