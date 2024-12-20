using System;
using Microsoft.Xna.Framework;
using Redemption.NPCs.Bosses.EaglecrestGolem;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class GreatSkiesStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Great Skies Stave");
			base.Tooltip.SetDefault("Casts a blast of wind that can push both enemies or players");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 47;
			base.item.width = 60;
			base.item.height = 60;
			base.item.useTime = 38;
			base.item.useAnimation = 38;
			base.item.crit = 4;
			base.item.knockBack = 0f;
			base.item.value = Item.sellPrice(0, 2, 25, 0);
			base.item.rare = 5;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<GreatGust>();
			base.item.shootSpeed = 8f;
			this.defaultShoot = ModContent.ProjectileType<GreatGust>();
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 60.2f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(320, 10);
			modRecipe.AddIngredient(575, 20);
			modRecipe.AddIngredient(9, 30);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
