using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass
{
	public class TrueHallowedStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("True Hallowed Stave");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 69;
			base.item.width = 56;
			base.item.height = 56;
			base.item.useTime = 22;
			base.item.useAnimation = 22;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 10, 0, 30);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			base.item.shoot = base.mod.ProjectileType("TrueStavePro");
			base.item.shootSpeed = 18f;
			this.defaultShoot = base.mod.ProjectileType("TrueStavePro");
			this.guardianBuffID = base.mod.BuffType("NatureGuardian17Buff");
			this.guardianProjectileID = base.mod.ProjectileType("NatureGuardian17");
			this.guardianTime = 600;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 56.2f;
			this.guardianName = "True Holy Statuette";
			this.guardianType = "Mystic";
			this.guardianAbility = "Stream-Shot/Triple-Shot/True Holy Aura";
			this.guardianEffects = "Staves have a chance to shoot 2 extra projectiles, Staves that shoot a single projectile will shoot 2 more in an arc,\nDefence Enhancement++/Improved Sight+/Mobility Enhancement+ at day/Life Enhancement+";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "HallowedStave", 1);
			modRecipe.AddIngredient(null, "BrokenHeroStave", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 57, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
