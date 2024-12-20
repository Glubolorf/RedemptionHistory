using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs;
using Redemption.Projectiles.DruidProjectiles.Stave;
using Redemption.Projectiles.DruidProjectiles.Stave.Guardians;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Items.DruidDamageClass.v08
{
	public class MoonlordStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Squidlyn Stave");
			base.Tooltip.SetDefault("Shoots phantasmal mines at the cursor's location that explode upon hitting an enemy");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 120;
			base.item.height = 58;
			base.item.width = 62;
			base.item.useTime = 14;
			base.item.useAnimation = 14;
			base.item.mana = 6;
			base.item.crit = 4;
			base.item.knockBack = 8f;
			base.item.value = Item.sellPrice(0, 20, 0, 0);
			base.item.rare = 10;
			base.item.UseSound = SoundID.Item117;
			base.item.autoReuse = true;
			base.item.shoot = ModContent.ProjectileType<MoonlordStavePro>();
			base.item.shootSpeed = 24f;
			this.defaultShoot = ModContent.ProjectileType<MoonlordStavePro>();
			this.guardianBuffID = ModContent.BuffType<NatureGuardian28Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian28>();
			this.guardianTime = 1800;
			this.singleShotStave = false;
			this.staveHoldOffset = new Vector2(4f, -10f);
			this.staveLength = 68.2f;
			this.guardianName = "Moon Baron";
			this.guardianType = "Mystic";
			this.guardianAbility = "Swift-Cast/Phantasmal Flames";
			this.guardianEffects = "Staves swing a lot faster, Stave casts will ignite targets with defense-reducing Phantasmal Flames";
		}

		protected override bool SpecialShootPattern(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 vector = Main.MouseWorld;
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, vector.X, vector.Y);
			return false;
		}
	}
}
