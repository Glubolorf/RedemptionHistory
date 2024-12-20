using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Accessories.HM
{
	public class LabGeigerCounter : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("IO-Locator");
			base.Tooltip.SetDefault("Points toward the Abandoned Lab");
		}

		public override void SetDefaults()
		{
			base.item.value = Item.buyPrice(0, 15, 50, 0);
			base.item.rare = 7;
			base.item.width = 34;
			base.item.height = 34;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<GeigerEffect>().effect = true;
			if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[ModContent.ProjectileType<LabPointer>()] < 1)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<LabPointer>(), 0, 0f, Main.myPlayer, 0f, 0f);
			}
		}
	}
}
