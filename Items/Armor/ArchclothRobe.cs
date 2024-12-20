using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		1
	})]
	internal class ArchclothRobe : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Archcloth Robe");
			base.Tooltip.SetDefault("'Only the Nobles of Anglon wear these robes'\nIncreases coin pickup range\nShops have lower prices");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 28;
			base.item.rare = 4;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.vanity = true;
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
		{
			robes = true;
			equipSlot = base.mod.GetEquipSlot("ArchclothRobe_Legs", 2);
		}

		public override void UpdateEquip(Player player)
		{
			player.goldRing = true;
			player.discount = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Archcloth", 15);
			modRecipe.AddTile(86);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
		}
	}
}
