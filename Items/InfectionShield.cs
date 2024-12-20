using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	[AutoloadEquip(new EquipType[]
	{
		10
	})]
	public class InfectionShield : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Thornshield");
			base.Tooltip.SetDefault("-2 defense\nDouble tap a direction to dash\n14% increased melee critical strike chance\nAttackers also take damage");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 32;
			base.item.rare = 7;
			base.item.value = 80000;
			base.item.damage = 40;
			base.item.melee = true;
			base.item.accessory = true;
			base.item.knockBack = 5f;
			base.item.expert = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(3097, 1);
			modRecipe.AddIngredient(null, "Xenomite", 10);
			modRecipe.AddIngredient(null, "StarliteBar", 6);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.dash = 2;
			player.statDefense -= 2;
			player.meleeCrit += 14;
			player.thorns = 0.5f;
		}
	}
}
