using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class KeepersMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mask of the Fallen");
			base.Tooltip.SetDefault("8% increased magic damage\n6% increased magic critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 20;
			base.item.height = 22;
			base.item.value = Item.sellPrice(0, 0, 75, 0);
			base.item.rare = 3;
			base.item.defense = 2;
		}

		public override void UpdateEquip(Player player)
		{
			player.magicDamage *= 1.08f;
			player.magicCrit += 6;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("KeepersChestplate") && legs.type == base.mod.ItemType("KeepersLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+40 max mana and 8% reduced mana cost";
			player.statManaMax2 += 40;
			player.manaCost *= 0.92f;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = true);
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
