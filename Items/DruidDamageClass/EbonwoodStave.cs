using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.DruidProjectiles.Stave.Guardians;
using Redemption.Projectiles.v08;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class EbonwoodStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ebonwood Stave");
			base.Tooltip.SetDefault("Shoots a leaf");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 7;
			base.item.width = 48;
			base.item.height = 48;
			base.item.useTime = 28;
			base.item.useAnimation = 28;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.buyPrice(0, 0, 1, 50);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<KingsOakShot2>();
			base.item.shootSpeed = 7f;
			this.defaultShoot = ModContent.ProjectileType<KingsOakShot2>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian3Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian3>();
			this.guardianTime = 1200;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 48.2f;
			this.guardianName = "Corrupt Pixie";
			this.guardianType = "Pixie";
			this.guardianAbility = "Stream-Shot";
			this.guardianEffects = "Staves have a chance to shoot 2 extra projectiles";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(619, 8);
			modRecipe.AddIngredient(27, 1);
			modRecipe.AddTile(18);
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
