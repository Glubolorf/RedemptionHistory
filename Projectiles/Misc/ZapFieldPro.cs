using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Weapons.PostML.Ranged;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Misc
{
	public class ZapFieldPro : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Empty";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Electricity Field");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 100;
			base.projectile.height = 100;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 220;
		}

		public override void AI()
		{
			if (!Main.LocalPlayer.GetModPlayer<RedePlayer>().zapField)
			{
				base.projectile.Kill();
			}
			base.projectile.localAI[0] += 1f;
			Player player = Main.player[base.projectile.owner];
			base.projectile.Center = player.Center;
			if (RedeHelper.ClosestNPC(ref this.target, 300f, base.projectile.Center, false, player.MinionAttackTargetNPC, null) && base.projectile.localAI[0] % 50f == 0f && Main.myPlayer == base.projectile.owner)
			{
				Vector2 ai = RedeHelper.PolarVector(10f, Utils.ToRotation(this.target.Center - base.projectile.Center));
				float ai2 = (float)Main.rand.Next(100);
				Projectile.NewProjectile(base.projectile.Center, RedeHelper.PolarVector(18f, Utils.ToRotation(this.target.Center - base.projectile.Center)), ModContent.ProjectileType<TeslaLightningArc>(), base.projectile.damage, 0f, Main.myPlayer, Utils.ToRotation(ai), ai2);
			}
			if (player.dead || !player.active)
			{
				base.projectile.Kill();
			}
		}

		private NPC target;
	}
}
