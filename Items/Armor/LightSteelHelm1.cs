using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class LightSteelHelm1 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Hikarite Headgear");
			base.Tooltip.SetDefault("12% increased melee and ranged damage\n4% increased ranged crit");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 28;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.rare = 8;
			base.item.defense = 22;
		}

		public override void UpdateEquip(Player player)
		{
			player.meleeDamage *= 1.12f;
			player.rangedDamage *= 1.12f;
			player.rangedCrit += 4;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("LightSteelBody1") && legs.type == base.mod.ItemType("LightSteelLeggings1");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Greatly increased speed and 4% damage reduction.";
			player.AddBuff(11, 2, true);
			player.moveSpeed += 2f;
			player.endurance += 0.04f;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = true);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LightSteel", 20);
			modRecipe.AddIngredient(182, 5);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
