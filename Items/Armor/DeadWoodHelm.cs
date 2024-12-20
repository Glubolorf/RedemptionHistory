using System;
using Redemption.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class DeadWoodHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Petrified Wood Helmet");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 20;
			base.item.value = 0;
			base.item.rare = 7;
			base.item.defense = 1;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DeadWoodArmour>() && legs.type == ModContent.ItemType<DeadWoodLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Immune to Radioactive Fallout";
			player.buffImmune[ModContent.BuffType<RadioactiveFalloutDebuff>()] = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DeadWood", 25);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
