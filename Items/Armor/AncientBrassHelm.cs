using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class AncientBrassHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Brass Helm");
			base.Tooltip.SetDefault("Increases melee damage by 2%");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 20;
			base.item.value = Item.sellPrice(0, 0, 12, 50);
			base.item.rare = 1;
			base.item.defense = 2;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.02f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("AncientBrassArmour") && legs.type == base.mod.ItemType("AncientBrassLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+2 Defence";
			player.statDefense += 2;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "AncientBrassIngot", 10);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
