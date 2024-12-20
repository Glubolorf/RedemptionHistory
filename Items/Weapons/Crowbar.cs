using System;
using Redemption.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class Crowbar : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crowbar");
			base.Tooltip.SetDefault("Deals triple damage to Infected enemies");
		}

		public override void SetDefaults()
		{
			base.item.damage = 85;
			base.item.melee = true;
			base.item.width = 48;
			base.item.height = 38;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.channel = true;
			base.item.useStyle = 100;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 4, 25, 0);
			base.item.rare = 6;
			base.item.shoot = ModContent.ProjectileType<CrowbarPro>();
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
			base.item.UseSound = SoundID.Item1;
		}

		public override bool UseItemFrame(Player player)
		{
			player.bodyFrame.Y = 3 * player.bodyFrame.Height;
			return true;
		}
	}
}
