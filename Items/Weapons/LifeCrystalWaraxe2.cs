using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons
{
	public class LifeCrystalWaraxe2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crystal Heart Waraxe");
			base.Tooltip.SetDefault("'They are gonna love this!'\nMakes the target lovestruck\nWhile holding this, life regen is increased");
		}

		public override void SetDefaults()
		{
			base.item.damage = 23;
			base.item.melee = true;
			base.item.width = 84;
			base.item.height = 84;
			base.item.axe = 7;
			base.item.useTime = 19;
			base.item.useAnimation = 23;
			base.item.useStyle = 1;
			base.item.knockBack = 8f;
			base.item.value = Item.sellPrice(0, 0, 75, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item7;
			base.item.autoReuse = false;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(119, 300, false);
		}

		public override void HoldItem(Player player)
		{
			player.AddBuff(2, 4, true);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(29, 1);
			modRecipe.AddIngredient(706, 18);
			modRecipe.AddIngredient(751, 5);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
