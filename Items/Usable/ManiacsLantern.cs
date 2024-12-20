using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Usable
{
	public class ManiacsLantern : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Maniac's Lantern");
			base.Tooltip.SetDefault("When held, creates an invisible force that repels soulless enemies away from you");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 44;
			base.item.value = Item.sellPrice(0, 6, 0, 0);
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void HoldItem(Player player)
		{
			if (player.ownedProjectileCounts[ModContent.ProjectileType<ManiacsLantern_Pro>()] == 0)
			{
				Projectile.NewProjectile(new Vector2((player.direction == 1) ? (player.Center.X + 40f) : (player.Center.X - 40f), player.Center.Y - 40f), Vector2.Zero, ModContent.ProjectileType<ManiacsLantern_Pro>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}
