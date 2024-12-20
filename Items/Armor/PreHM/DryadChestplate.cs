using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PreHM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class DryadChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dryad's Chestplate");
			base.Tooltip.SetDefault("2% increased druidic damage\n2% increased druidic critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 32;
			base.item.height = 26;
			base.item.value = 75;
			base.item.rare = 1;
			base.item.defense = 4;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.02f;
			druidDamagePlayer.druidCrit += 2;
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.anyWood = true;
			modRecipe.AddIngredient(9, 24);
			modRecipe.AddIngredient(27, 6);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
