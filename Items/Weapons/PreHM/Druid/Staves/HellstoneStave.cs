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
	public class HellstoneStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Molten Stave");
			base.Tooltip.SetDefault("");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 33;
			base.item.width = 54;
			base.item.height = 48;
			base.item.useTime = 27;
			base.item.useAnimation = 27;
			base.item.crit = 4;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 0, 54, 30);
			base.item.rare = 3;
			base.item.UseSound = SoundID.Item43;
			base.item.shoot = ModContent.ProjectileType<MoltenStavePro>();
			base.item.shootSpeed = 11f;
			base.item.autoReuse = false;
			base.item.useTurn = true;
			this.defaultShoot = ModContent.ProjectileType<MoltenStavePro>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian12Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian12>();
			this.guardianTime = 1200;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 48.2f;
			this.guardianName = "Lava Guardian";
			this.guardianType = "Guardian";
			this.guardianAbility = "Swift-Cast/Molten Eruption";
			this.guardianEffects = "Staves swing a lot faster, Hitting an enemy has a chance to cast rising flames";
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 6, 0f, 0f, 0, default(Color), 1f);
			}
		}

		protected override bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 vector = Main.MouseWorld;
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, vector.X, vector.Y);
			return false;
		}

		public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			target.AddBuff(24, 160, false);
		}

		public override void AddRecipes()
		{
			ModRecipe modRecipe = new ModRecipe(base.mod);
			modRecipe.AddIngredient(175, 8);
			modRecipe.AddTile(null, "DruidicAltarTile");
			modRecipe.SetResult(this, 1);
			modRecipe.AddRecipe();
		}
	}
}
