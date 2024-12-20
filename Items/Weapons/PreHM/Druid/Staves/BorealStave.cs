using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.Druid.Stave;
using Redemption.Projectiles.Druid.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.Weapons.PreHM.Druid.Staves
{
	public class BorealStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Boreal Stave");
			base.Tooltip.SetDefault("Shoots a Pine Needle");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 6;
			base.item.width = 42;
			base.item.height = 42;
			base.item.useTime = 27;
			base.item.useAnimation = 27;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 0, 1, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<KingsOakShot3>();
			base.item.shootSpeed = 8f;
			this.defaultShoot = ModContent.ProjectileType<KingsOakShot3>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian23Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian23>();
			this.guardianTime = 1500;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 42.2f;
			this.guardianName = "Icy Pixie";
			this.guardianType = "Pixie";
			this.guardianAbility = "Swift-Cast";
			this.guardianEffects = "Staves cast a lot faster";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(2503, 8);
			modRecipe.AddIngredient(27, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
