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
	public class ElegantStave : DruidStave
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Elegant Marble Stave");
			base.Tooltip.SetDefault("'Medusa won't like this...'\nWhile holding this, you are immune to Petrification");
		}

		public override void SafeSetDefaults()
		{
			base.item.damage = 21;
			base.item.width = 64;
			base.item.height = 64;
			base.item.useTime = 12;
			base.item.useAnimation = 12;
			base.item.crit = 16;
			base.item.knockBack = 7f;
			base.item.value = Item.sellPrice(0, 0, 40, 30);
			base.item.rare = 1;
			base.item.UseSound = SoundID.Item43;
			base.item.autoReuse = true;
			base.item.useTurn = true;
			this.defaultShoot = ModContent.ProjectileType<ExpandingMirror>();
			base.item.shootSpeed = 2f;
			this.guardianBuffID = ModContent.BuffType<NatureGuardian13Buff>();
			this.guardianProjectileID = ModContent.ProjectileType<NatureGuardian13>();
			this.guardianTime = 1800;
			this.singleShotStave = true;
			this.staveHoldOffset = new Vector2(4f, -15f);
			this.staveLength = 73.5f;
			this.guardianName = "Marble King Piece";
			this.guardianType = "Other";
			this.guardianAbility = "Marble Aura";
			this.guardianEffects = "Defence Enhancement+, Life & Mana Enhancement, Petrification Immunity";
		}

		protected override void ModifyVelocity(ref float speedX, ref float speedY)
		{
			float scalar = new Vector2(speedX, speedY).Length();
			Vector2 velocity = Utils.RotatedBy(Utils.SafeNormalize(new Vector2(speedX, speedY), -Vector2.UnitY), (double)(Utils.NextFloat(Main.rand) * 3.1415927f / 8f - 0.19634955f), default(Vector2)) * scalar;
			speedX = velocity.X;
			speedY = velocity.Y;
		}

		public override void HoldItem(Player player)
		{
			player.buffImmune[156] = true;
		}
	}
}
