using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class CorruptedXenomiteHead : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Xenomite Headgear");
			base.Tooltip.SetDefault("6% increased magic and minion damage\n+5 max minions");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 24;
			base.item.value = Item.sellPrice(0, 35, 0, 0);
			base.item.rare = 10;
			base.item.defense = 14;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicDamage *= 1.06f;
			player.minionDamage *= 1.06f;
			player.maxMinions += 5;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("CorruptedXenomiteBody") && legs.type == base.mod.ItemType("CorruptedXenomiteLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+200 max mana, greatly increased movement speed, and increased mana regen.";
			player.AddBuff(11, 2, true);
			player.statManaMax2 += 200;
			player.moveSpeed += 8f;
			player.manaRegen += 10;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "CorruptedXenomite", 20);
			modRecipe.AddIngredient(null, "CorruptedStarliteBar", 10);
			modRecipe.AddTile(null, "XenoForgeTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "XenomiteHead", 1);
			modRecipe2.AddIngredient(null, "GirusChip", 1);
			modRecipe2.AddIngredient(1508, 10);
			modRecipe2.AddTile(null, "CorruptorTile");
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}
	}
}
