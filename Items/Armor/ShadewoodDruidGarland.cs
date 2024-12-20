using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class ShadewoodDruidGarland : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadedruid's Garland");
			base.Tooltip.SetDefault("5% increased druidic damage\n4% increased druidic critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 28;
			base.item.height = 26;
			base.item.value = 1050;
			base.item.rare = 1;
			base.item.defense = 4;
			base.item.GetGlobalItem<RedeItem>().druidTag = true;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.05f;
			druidDamagePlayer.druidCrit += 4;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("ShadewoodDruidBreastplate") && legs.type == base.mod.ItemType("ShadewoodDruidLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Throws seedbags faster & Thorns effect\nBeing in the crimson increases plant life time by 10%";
			RedePlayer modPlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			modPlayer.fasterSeedbags = true;
			player.thorns = 1f;
			if (player.ZoneCrimson)
			{
				modPlayer.seedLifeTime += 0.1f;
			}
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(911, 25);
			modRecipe.AddIngredient(1329, 8);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
