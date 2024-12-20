using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.Domain
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class IthonicCap : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sky Squire's Cap");
			base.Tooltip.SetDefault("6% increased damage");
		}

		public override void SetDefaults()
		{
			base.item.width = 22;
			base.item.height = 22;
			base.item.value = Item.sellPrice(0, 0, 24, 0);
			base.item.rare = 1;
			base.item.defense = 4;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			player.meleeDamage *= 1.04f;
			player.magicDamage *= 1.04f;
			player.minionDamage *= 1.04f;
			player.rangedDamage *= 1.04f;
			player.thrownDamage *= 1.04f;
			druidDamagePlayer.druidDamage *= 1.04f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<IthonicTabard>() && legs.type == ModContent.ItemType<IthonicGreaves>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Grants the user double jump";
			DruidDamagePlayer.ModPlayer(player);
			player.doubleJumpCloud = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "KaniteBar", 20);
			modRecipe.AddIngredient(225, 8);
			modRecipe.AddIngredient(751, 10);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
