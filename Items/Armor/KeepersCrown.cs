using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class KeepersCrown : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crown of the Fallen");
			base.Tooltip.SetDefault("Increases minion damage by 8%\nIncreases your max number of minions");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 14;
			base.item.value = Item.sellPrice(0, 0, 75, 0);
			base.item.rare = 3;
			base.item.defense = 2;
		}

		public override void UpdateEquip(Player player)
		{
			player.minionDamage *= 1.08f;
			player.maxMinions++;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<KeepersChestplate>() && legs.type == ModContent.ItemType<KeepersLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "+1 minion capacity";
			player.maxMinions++;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = true;
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
