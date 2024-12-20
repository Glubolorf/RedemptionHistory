using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class AncientPowerSurgeLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Power Surge Greaves");
			base.Tooltip.SetDefault("50% increased movement speed\n15% increased minion and magic damage\n20% increased magic critical strike chance\nIncreased minion capacity");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 16;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.defense = 28;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed *= 1.5f;
			player.maxMinions++;
			player.magicDamage *= 1.15f;
			player.minionDamage *= 1.15f;
			player.magicCrit += 20;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientPowerCore", 16);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
