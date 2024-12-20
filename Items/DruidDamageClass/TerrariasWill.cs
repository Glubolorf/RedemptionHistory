﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	[AutoloadEquip(new EquipType[]
	{
		7
	})]
	public class TerrariasWill : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Terraria's Will");
			base.Tooltip.SetDefault("[c/91dc16:---Druid Class---]\n'With every leaf born, the planet blooms'\nWalking about creates ancient spores that grows into powerful plants that fight for you\n10% increased druid damage\n10% increased druid critical strike chance");
		}

		public override void SetDefaults()
		{
			base.item.width = 26;
			base.item.height = 28;
			base.item.value = Item.sellPrice(3, 50, 0, 0);
			base.item.rare = 8;
			base.item.accessory = true;
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(base.mod.ItemType("DBLiquidFlames"), 1);
			modRecipe.AddIngredient(base.mod.ItemType("DBSunAndMoon"), 1);
			modRecipe.AddIngredient(base.mod.ItemType("SeedNade"), 1);
			modRecipe.AddIngredient(base.mod.ItemType("PowerCellWristband"), 1);
			modRecipe.AddIngredient(2766, 100);
			modRecipe.AddIngredient(base.mod.ItemType("SoulOfBloom"), 100);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y) > 1f && !player.rocketFrame)
			{
				for (int i = 0; i < 1; i++)
				{
					if (Main.rand.Next(40) == 0)
					{
						Projectile.NewProjectile(new Vector2(player.position.X + Utils.NextFloat(Main.rand, (float)player.width), player.position.Y + Utils.NextFloat(Main.rand, (float)player.height)), new Vector2(0f, 0f), base.mod.ProjectileType("AncientSporePro"), 80, 0f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			DruidDamagePlayer druidDamagePlayer = DruidDamagePlayer.ModPlayer(player);
			druidDamagePlayer.druidDamage += 0.25f;
			druidDamagePlayer.druidCrit += 25;
		}
	}
}
