using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	public class JanitorMopPro1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mop");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 18;
			base.projectile.height = 18;
			base.projectile.aiStyle = 1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = 1;
			base.projectile.timeLeft = 300;
			base.projectile.ignoreWater = false;
			base.projectile.tileCollide = true;
			this.aiType = 81;
		}

		public override void AI()
		{
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] >= 30f)
			{
				base.projectile.friendly = true;
			}
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			if (target.type == base.mod.NPCType("JanitorBot"))
			{
				target.AddBuff(base.mod.BuffType("JanitorStun"), 5, false);
			}
		}

		public override void Kill(int timeleft)
		{
			Main.PlaySound(0, (int)base.projectile.position.X, (int)base.projectile.position.Y, 1, 1f, 0f);
			for (int num468 = 0; num468 < 4; num468++)
			{
				num468 = Dust.NewDust(new Vector2(base.projectile.Center.X, base.projectile.Center.Y), base.projectile.width, base.projectile.height, 78, -base.projectile.velocity.X * 0.2f, -base.projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
			}
		}
	}
}
