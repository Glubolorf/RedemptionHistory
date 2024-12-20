using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class XenomiteGlaive : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Glaive");
		}

		public override void SetDefaults()
		{
			base.item.damage = 14;
			base.item.melee = true;
			base.item.width = 122;
			base.item.height = 122;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.channel = true;
			base.item.useStyle = 100;
			base.item.knockBack = 6f;
			base.item.value = Item.buyPrice(0, 1, 0, 0);
			base.item.rare = 3;
			base.item.shoot = base.mod.ProjectileType("XenomiteGlaivePro");
			base.item.noUseGraphic = true;
			base.item.noMelee = true;
		}

		public override bool UseItemFrame(Player player)
		{
			player.bodyFrame.Y = 3 * player.bodyFrame.Height;
			return true;
		}
	}
}
