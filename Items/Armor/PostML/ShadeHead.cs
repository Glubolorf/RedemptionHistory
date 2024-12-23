﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.Armor.PostML
{
	[AutoloadEquip(new EquipType[]
	{
		0
	})]
	public class ShadeHead : DruidDamageSpirit
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadehead");
			base.Tooltip.SetDefault("10% increased druidic damage\n15% increased druidic critical strike chance\n4% increased damage reduction\nSpirits home in on enemies\nSpirits pierce through more targets\n[c/bdffff:Spirit Level +2]");
		}

		public override void SafeSetDefaults()
		{
			base.item.width = 28;
			base.item.height = 40;
			base.item.value = Item.sellPrice(0, 25, 0, 0);
			base.item.defense = 26;
			this.spiritWeapon = false;
			base.item.rare = 11;
			base.item.GetGlobalItem<RedeItem>().redeRarity = 2;
		}

		public override void UpdateEquip(Player player)
		{
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.1f;
			druidDamagePlayer.druidCrit += 15;
			RedePlayer redePlayer = (RedePlayer)player.GetModPlayer(base.mod, "RedePlayer");
			redePlayer.spiritHoming = true;
			redePlayer.spiritPierce = true;
			player.endurance *= 0.04f;
			redePlayer.spiritLevel += 2;
		}

		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<ShadeBody>() && legs.type == ModContent.ItemType<ShadeLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Getting hit has a chance to unleash a burst of Shadesouls that damage enemies and buff allies";
			((RedePlayer)player.GetModPlayer(base.mod, "RedePlayer")).shadeSet = true;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawHair = (drawAltHair = false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "Shadesoul", 3);
			modRecipe.AddTile(412);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
