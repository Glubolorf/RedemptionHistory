﻿using System;
using Redemption.Items.DruidDamageClass;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class DragonLeadHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Dragon-Lead Helm");
			base.Tooltip.SetDefault("[c/daf73a:---Druid Class---]\n6% increased ranged damage and crit\n6% increased druidic damage and crit");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 28;
			base.item.value = Item.sellPrice(0, 8, 50, 0);
			base.item.rare = 4;
			base.item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.rangedDamage *= 1.06f;
			player.rangedCrit += 6;
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.06f;
			druidDamagePlayer.druidCrit += 6;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == base.mod.ItemType("DragonLeadBody") && legs.type == base.mod.ItemType("DragonLeadLeggings");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Immune to lava\nA ring of flames circle around you, inflicting fire on contact";
			player.lavaImmune = true;
			player.AddBuff(116, 2, true);
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DragonLeadBar", 10);
			modRecipe.AddTile(16);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}