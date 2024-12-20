using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Melee
{
	public class DarkStar : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dark Star");
		}

		public override void SetDefaults()
		{
			base.item.damage = 500;
			base.item.width = 30;
			base.item.height = 32;
			base.item.useTime = 10;
			base.item.useAnimation = 10;
			base.item.useStyle = 1;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 0, 85, 0);
			base.item.maxStack = 6;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = true;
			base.item.melee = true;
			base.item.shoot = ModContent.ProjectileType<DarkStarPro>();
			base.item.shootSpeed = 10f;
			base.item.noMelee = true;
			base.item.noUseGraphic = true;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return new Color?(RedeColor.COLOR_WHITEFADE1);
		}

		public override bool CanUseItem(Player player)
		{
			return player.ownedProjectileCounts[base.item.shoot] < base.item.stack;
		}
	}
}
