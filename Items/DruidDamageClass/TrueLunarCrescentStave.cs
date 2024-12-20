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
	public class TrueLunarCrescentStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("True Lunar Crescent Stave");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 78;
			base.item.width = 58;
			base.item.height = 64;
			base.item.useTime = 31;
			base.item.useAnimation = 31;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 10, 0, 0);
			base.item.rare = 8;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<TrueCrescentPro>();
			base.item.shootSpeed = 18f;
			this.defaultShoot = ModContent.ProjectileType<TrueCrescentPro>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian15Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian15>();
			this.guardianTime = 600;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 76.2f;
			this.guardianName = "True Lunar Statuette";
			this.guardianType = "Mystic";
			this.guardianAbility = "Quad-Shot/Nightshade's Embrace+";
			this.guardianEffects = "Staves that shoot a single projectile will shoot 4 more in an arc, Staves cast a lot faster,\nMana Enhancement+/Improved Sight+/Mobility Enhancement+ at night";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "LunarCrescentStave", 1);
			modRecipe.AddIngredient(null, "BrokenHeroStave", 1);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
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
