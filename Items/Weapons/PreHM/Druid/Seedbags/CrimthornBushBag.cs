﻿using System;
using Redemption.Projectiles.Druid.Seedbag;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Druid.Seedbags
{
	public class CrimthornBushBag : DruidSeedBag
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crimthorn Seedbag");
			base.Tooltip.SetDefault("Throws a seed that grows into a spikey Crimthorn Bush");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 11;
			base.item.width = 22;
			base.item.height = 26;
			base.item.useTime = 35;
			base.item.useAnimation = 35;
			base.item.useStyle = 1;
			base.item.mana = 12;
			base.item.crit = 4;
			base.item.knockBack = 3f;
			base.item.value = Item.buyPrice(0, 0, 45, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item1;
			base.item.noMelee = true;
			base.item.autoReuse = false;
			base.item.shoot = ModContent.ProjectileType<Seed6>();
			base.item.shootSpeed = 19f;
			this.NativeTerrainIDs = TileLists.CrimsonTiles;
			this.nativeText = "Crimson";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LeatherPouch", 1);
			modRecipe.AddIngredient(2, 5);
			modRecipe.AddIngredient(2887, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
