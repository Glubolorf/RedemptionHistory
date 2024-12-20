using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class CursedShinkiteBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shinkite Curseplate");
			base.Tooltip.SetDefault("+50 max mana\n10% increased damage\nIncreased mana regen");
		}

		public override void SetDefaults()
		{
			base.item.width = 38;
			base.item.height = 26;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.defense = 30;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.statManaMax2 += 50;
			player.manaRegen += 3;
			player.meleeDamage *= 1.1f;
			player.rangedDamage *= 1.1f;
			player.magicDamage *= 1.1f;
			player.minionDamage *= 1.1f;
			player.thrownDamage *= 1.1f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ShinkiteCursed", 18);
			modRecipe.AddIngredient(172, 50);
			modRecipe.AddIngredient(101, 1);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
