using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Datalogs
{
	public class StarcruiserRadar : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Starcruiser Radar");
			base.Tooltip.SetDefault("Points toward the Crashed Spaceship");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 4));
		}

		public override void SetDefaults()
		{
			base.item.value = Item.buyPrice(0, 1, 0, 0);
			base.item.rare = 9;
			base.item.width = 32;
			base.item.height = 28;
			base.item.accessory = true;
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<RadarEffect>().effect = true;
			if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[base.mod.ProjectileType("StarcruiserPointer")] < 1)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, base.mod.ProjectileType("StarcruiserPointer"), 0, 0f, Main.myPlayer, 0f, 0f);
			}
		}
	}
}
