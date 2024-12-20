using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.DruidProjectiles.Stave;
using Redemption.Projectiles.DruidProjectiles.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.DruidS
{
	public class ScarletStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Scarlet Stave");
			base.Tooltip.SetDefault("Shoots a sharp Scarlet Shard");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 88;
			base.item.width = 56;
			base.item.height = 56;
			base.item.useTime = 19;
			base.item.useAnimation = 19;
			base.item.crit = 4;
			base.item.knockBack = 5f;
			base.item.value = Item.sellPrice(0, 5, 60, 0);
			base.item.rare = 6;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<ScarletShardPro>();
			base.item.shootSpeed = 19f;
			this.defaultShoot = ModContent.ProjectileType<ScarletShardPro>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian21Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian21>();
			this.guardianTime = 600;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 56.2f;
			this.guardianName = "Scarlion Spirit";
			this.guardianType = "Guardian";
			this.guardianAbility = "Quad-Shot/Blood Pulse Aura";
			this.guardianEffects = "Staves that shoot a single projectile will shoot 4 more in an arc, Druidic Enhancement+/Life Enhancement+/Life-Steal";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "ScarletBar", 12);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 115, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
