using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		2
	})]
	public class SmallShadeLeggings : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Small Shade Greaves");
			base.Tooltip.SetDefault("40% increased movement speed\n10% increased druidic damage\n15% increased druidic critical strike chance\nStaves cast faster");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 20;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.defense = 26;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed *= 1.4f;
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.1f;
			druidDamagePlayer.druidCrit += 15;
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).staveSpeed += 0.05f;
		}

		public override bool DrawLegs()
		{
			return false;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SmallShadesoul", 8);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
