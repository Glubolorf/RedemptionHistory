using System;
using Redemption.Projectiles.Ranged;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Ranged
{
	public class FreedomStarN : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Freedom Star");
			base.Tooltip.SetDefault("'Wait, I don't remember adding this...'\nHold the use button to charge, and then release a powerful Charged Shot!");
		}

		public override void SetDefaults()
		{
			base.item.width = 74;
			base.item.height = 34;
			base.item.ranged = true;
			base.item.damage = 720;
			base.item.shoot = ModContent.ProjectileType<FreedomStarNPro>();
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 5;
			base.item.channel = true;
			Item.sellPrice(1, 0, 0, 0);
			base.item.noMelee = true;
			base.item.shootSpeed = 12f;
			base.item.noUseGraphic = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 3;
		}
	}
}
