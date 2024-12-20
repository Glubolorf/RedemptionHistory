using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class LivingWoodHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Living Wood Helmet");
			base.Tooltip.SetDefault("Increases your max number of minions\nIncreases minion damage by 3%");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 22;
			base.item.value = 0;
			base.item.rare = 0;
			base.item.defense = 1;
		}

		public override void UpdateEquip(Player player)
		{
			player.maxMinions++;
			player.minionDamage *= 1.03f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("LivingWoodBody") && legs.type == base.mod.ItemType("LivingWoodLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Immune to poison";
			player.buffImmune[20] = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LivingWood", 16);
			modRecipe.AddIngredient(null, "LivingLeaf", 8);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
