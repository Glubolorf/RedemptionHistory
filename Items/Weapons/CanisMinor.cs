using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class CanisMinor : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Canis Minor");
			base.Tooltip.SetDefault("Inflicts poison");
		}

		public override void SetDefaults()
		{
			base.item.damage = 15;
			base.item.melee = true;
			base.item.width = 32;
			base.item.height = 32;
			base.item.useTime = 9;
			base.item.useAnimation = 9;
			base.item.useStyle = 3;
			base.item.knockBack = 7f;
			base.item.value = 3100;
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.autoReuse = false;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(20, 600, false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3519, 1);
			modRecipe.AddIngredient(null, "Nightshade", 3);
			modRecipe.AddRecipeGroup("Redemption:EvilWood", 5);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(3483, 1);
			modRecipe2.AddIngredient(null, "Nightshade", 3);
			modRecipe2.AddRecipeGroup("Redemption:EvilWood", 5);
			modRecipe2.AddTile(16);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
