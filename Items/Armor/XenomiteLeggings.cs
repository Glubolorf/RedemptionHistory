using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class XenomiteLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xenomite Leggings");
			base.Tooltip.SetDefault("+15 max mana\n5% increased magic damage\n35% increased movement speed");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 18;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 7;
			base.item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.35f;
			player.magicDamage *= 1.05f;
			player.statManaMax2 += 15;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Xenomite", 15);
			modRecipe.AddIngredient(null, "StarliteBar", 5);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
