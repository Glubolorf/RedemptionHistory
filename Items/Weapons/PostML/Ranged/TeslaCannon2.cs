using System;
using Microsoft.Xna.Framework;
using Redemption.Projectiles.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PostML.Ranged
{
	public class TeslaCannon2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Tesla Cannon (Prototype)");
			base.Tooltip.SetDefault("Rapidly fires Telsa Zaps\nGenerates an electrosphere upon hitting an enemy");
		}

		public override void SetDefaults()
		{
			base.item.damage = 150;
			base.item.useTime = 7;
			base.item.useAnimation = 7;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<TeslaLightning>();
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

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "TeslaCannon", 1);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2?(new Vector2(-18f, 2f));
		}
	}
}
