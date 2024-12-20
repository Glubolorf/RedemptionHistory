using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class KeepersHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Helmet of the Fallen");
			base.Tooltip.SetDefault("8% increased melee damage\n6% increased melee critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 18;
			base.item.height = 20;
			base.item.value = Item.sellPrice(0, 0, 75, 0);
			base.item.rare = 3;
			base.item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.08f;
			player.meleeCrit += 6;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<KeepersChestplate>() && legs.type == ModContent.ItemType<KeepersLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "6% increased melee speed";
			player.meleeSpeed *= 1.06f;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DarkShard", 1);
			modRecipe.AddIngredient(null, "SmallLostSoul", 4);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
