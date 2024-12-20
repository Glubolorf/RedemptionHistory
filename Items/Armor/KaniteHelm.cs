using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class KaniteHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Kanite Helmet");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 24;
			base.item.value = Item.sellPrice(0, 0, 9, 50);
			base.item.rare = 0;
			base.item.defense = 2;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<KaniteBody>() && legs.type == ModContent.ItemType<KaniteLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+1 Defence";
			player.statDefense++;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "KaniteBar", 20);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
