using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class KeepersVisor : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Visor of the Fallen");
			base.Tooltip.SetDefault("8% increased ranged damage\n6% increased ranged critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 16;
			base.item.height = 14;
			base.item.value = Item.sellPrice(0, 0, 75, 0);
			base.item.rare = 3;
			base.item.defense = 3;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedDamage *= 1.08f;
			player.rangedCrit += 6;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("KeepersChestplate") && legs.type == base.mod.ItemType("KeepersLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "6% increased ranged damage";
			player.rangedDamage *= 1.06f;
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
