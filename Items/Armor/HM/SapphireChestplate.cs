using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.HM
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	public class SapphireChestplate : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sapphire Chestplate");
			base.Tooltip.SetDefault("10% increased druidic damage\n10% increased druidic critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 22;
			base.item.value = Item.sellPrice(0, 4, 80, 0);
			base.item.rare = 6;
			base.item.defense = 24;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.1f;
			druidDamagePlayer.druidCrit += 10;
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SapphireBar", 24);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
