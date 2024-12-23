﻿using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.Druid.Stave;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Druid.Staves
{
	public class LunarCrescentStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lunar Crescent Stave");
			base.Tooltip.SetDefault("Shoots a vortex that pulls in enemies and explodes");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 35;
			base.item.width = 56;
			base.item.height = 60;
			base.item.useTime = 40;
			base.item.useAnimation = 40;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 1, 8, 30);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item43;
			base.item.shoot = ModContent.ProjectileType<DarksideVortex>();
			base.item.shootSpeed = 16f;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			this.defaultShoot = ModContent.ProjectileType<DarksideVortex>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian14Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian14>();
			this.guardianTime = 900;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 60f;
			this.guardianName = "Lunar Statuette";
			this.guardianType = "Mystic";
			this.guardianAbility = "Triple-Shot/Nightshade's Embrace";
			this.guardianEffects = "Staves that shoot a single projectile will shoot 2 more in an arc,\nMana Enhancement/Improved Sight/Mobility Enhancement at night";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "DemoniteStave", 1);
			modRecipe.AddIngredient(null, "GrassStave", 1);
			modRecipe.AddIngredient(null, "DonjonStave", 1);
			modRecipe.AddIngredient(null, "HellstoneStave", 1);
			modRecipe.AddTile(26);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
			ModRecipe modRecipe2 = new ModRecipe(base.mod);
			modRecipe2.AddIngredient(null, "CrimtaneStave", 1);
			modRecipe2.AddIngredient(null, "GrassStave", 1);
			modRecipe2.AddIngredient(null, "DonjonStave", 1);
			modRecipe2.AddIngredient(null, "HellstoneStave", 1);
			modRecipe2.AddTile(26);
			modRecipe2.SetResult(this, 1);
			modRecipe2.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 21, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
