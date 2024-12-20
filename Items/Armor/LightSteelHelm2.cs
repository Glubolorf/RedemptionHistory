using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class LightSteelHelm2 : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shining Hikarite Headgear");
			base.Tooltip.SetDefault("4% increased magic and minion damage\n+2 max minions");
		}

		public override void SetDefaults()
		{
			base.item.width = 30;
			base.item.height = 26;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.rare = 8;
			base.item.defense = 20;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicDamage *= 1.04f;
			player.minionDamage *= 1.04f;
			player.maxMinions += 2;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<LightSteelBody2>() && legs.type == ModContent.ItemType<LightSteelLeggings2>();
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
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LightSteel", 15);
			modRecipe.AddIngredient(182, 10);
			modRecipe.AddTile(134);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
