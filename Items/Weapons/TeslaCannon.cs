using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class TeslaCannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tesla Cannon");
			base.Tooltip.SetDefault("Rapidly fires Telsa Zaps\nGenerates an electrosphere upon hitting an enemy");
		}

		public override void SetDefaults()
		{
			base.item.damage = 200;
			base.item.useTime = 7;
			base.item.useAnimation = 7;
			base.item.autoReuse = true;
			base.item.shoot = base.mod.ProjectileType("TeslaLightning");
			base.item.shootSpeed = 18f;
			base.item.UseSound = SoundID.Item92;
			base.item.ranged = true;
			base.item.width = 80;
			base.item.height = 30;
			base.item.useStyle = 5;
			base.item.noMelee = true;
			base.item.knockBack = 0f;
			base.item.value = Item.buyPrice(8, 50, 0, 0);
			base.item.rare = 11;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-18f, 2f));
		}
	}
}
