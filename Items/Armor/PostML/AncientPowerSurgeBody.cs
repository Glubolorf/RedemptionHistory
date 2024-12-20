using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class AncientPowerSurgeBody : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Power Surge Breastplate");
			base.Tooltip.SetDefault("+50 max life and mana\n15% increased minion and magic damage\nIncreased mana and life regen");
		}

		public override void SetDefaults()
		{
			base.item.width = 40;
			base.item.height = 26;
			base.item.value = Item.sellPrice(0, 30, 0, 0);
			base.item.defense = 36;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.statLifeMax2 += 50;
			player.statManaMax2 += 50;
			player.lifeRegen += 3;
			player.manaRegen += 6;
			player.magicDamage *= 1.15f;
			player.minionDamage *= 1.15f;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientPowerCore", 18);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
