using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.DruidProjectiles.Stave;
using Redemption.Projectiles.DruidProjectiles.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class CrimtaneStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Crimtane Stave");
			base.Tooltip.SetDefault("Shoots a Crimson Blast");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 20;
			base.item.width = 40;
			base.item.height = 44;
			base.item.useTime = 26;
			base.item.useAnimation = 26;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 0, 27, 30);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item43;
			base.item.shoot = ModContent.ProjectileType<CrimsonBlast2>();
			base.item.shootSpeed = 16f;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			this.defaultShoot = ModContent.ProjectileType<CrimsonBlast2>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian9Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian9>();
			this.guardianTime = 1500;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 44.2f;
			this.guardianName = "Crimson Fairy";
			this.guardianType = "Fairy";
			this.guardianAbility = "Swift-Cast/Crimson Aura";
			this.guardianEffects = "Staves cast a lot faster, Mobility Enhancement while in Crimson";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(1257, 8);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
