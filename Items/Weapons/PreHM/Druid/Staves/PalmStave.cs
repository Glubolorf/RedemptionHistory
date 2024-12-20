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
	public class PalmStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Palm Stave");
			base.Tooltip.SetDefault("Shoots a leaf");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 8;
			base.item.width = 40;
			base.item.height = 40;
			base.item.useTime = 27;
			base.item.useAnimation = 27;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 0, 1, 0);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<KingsOakShot2>();
			base.item.shootSpeed = 7f;
			this.defaultShoot = ModContent.ProjectileType<KingsOakShot2>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian22Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian22>();
			this.guardianTime = 1200;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 40.2f;
			this.guardianName = "Sunny Pixie";
			this.guardianType = "Pixie";
			this.guardianAbility = "Swift-Cast/Warmth";
			this.guardianEffects = "Staves cast a lot faster, Reduces damage from cold sources";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(2504, 8);
			modRecipe.AddIngredient(27, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
