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
	public class SapphireStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Sapphire Stave");
			base.Tooltip.SetDefault("Shoots a piercing Sapphire Scythe");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 93;
			base.item.width = 58;
			base.item.height = 54;
			base.item.useTime = 23;
			base.item.useAnimation = 23;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 5, 60, 0);
			base.item.rare = 6;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			base.item.shoot = ModContent.ProjectileType<SapphireScythePro>();
			base.item.shootSpeed = 16f;
			this.defaultShoot = ModContent.ProjectileType<SapphireScythePro>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian20Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian20>();
			this.guardianTime = 600;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 58.2f;
			this.guardianName = "Sapphiron Spirit";
			this.guardianType = "Guardian";
			this.guardianAbility = "Quad-Shot/Sapphiron Soul Aura";
			this.guardianEffects = "Staves that shoot a single projectile will shoot 4 more in an arc, Defence Enhancement+/Mana Enhancement+/Thorns";
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(null, "SapphireBar", 12);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 41, 0f, 0f, 0, default(Color), 1f);
			}
		}
	}
}
