using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class LifeFruitAxe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shining Heart Waraxe");
			base.Tooltip.SetDefault("Makes the target lovestruck and inflicts Ichor\nWhile holding this, life regen is greatly increased");
		}

		public override void SetDefaults()
		{
			base.item.damage = 75;
			base.item.melee = true;
			base.item.width = 94;
			base.item.height = 94;
			base.item.axe = 40;
			base.item.useTime = 29;
			base.item.useAnimation = 31;
			base.item.useStyle = 1;
			base.item.knockBack = 9f;
			base.item.value = Item.sellPrice(0, 5, 0, 0);
			base.item.rare = 7;
			base.item.UseSound = SoundID.Item7;
			base.item.autoReuse = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(119, 300, false);
			target.AddBuff(69, 300, false);
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(58, 4, true);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LifeCrystalWaraxe", 1);
			modRecipe.AddIngredient(1291, 1);
			modRecipe.AddIngredient(1006, 18);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "LifeCrystalWaraxe2", 1);
			modRecipe2.AddIngredient(1291, 1);
			modRecipe2.AddIngredient(1006, 18);
			modRecipe2.AddTile(134);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
