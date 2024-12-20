using System;
using Microsoft.Xna.Framework;
using Redemption.Buffs.Debuffs;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.VCleaver
{
	public class CleaverHitbox : ModProjectile
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
			base.DisplayName.SetDefault("Vlitch Cleaver");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 58;
			base.projectile.height = 58;
			base.projectile.aiStyle = -1;
			base.projectile.friendly = false;
			base.projectile.hostile = true;
			base.projectile.penetrate = -1;
			base.projectile.alpha = 255;
			base.projectile.tileCollide = false;
		}

		public override bool ShouldUpdatePosition()
		{
			return false;
		}

		public override void AI()
		{
			int boss = (int)base.projectile.ai[0];
			if (boss < 0 || boss >= 200 || !Main.npc[boss].active || Main.npc[boss].type != ModContent.NPCType<VlitchCleaver>())
			{
				base.projectile.Kill();
			}
			NPC host = Main.npc[(int)base.projectile.ai[0]];
			base.projectile.Center = host.Center;
			base.projectile.rotation = host.rotation;
			base.projectile.timeLeft = 10;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(ModContent.BuffType<SnippedDebuff>(), Main.expertMode ? 1200 : 600, true);
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			float collisionPoint = 0f;
			return new bool?(Collision.CheckAABBvLineCollision(Utils.TopLeft(targetHitbox), Utils.Size(targetHitbox), base.projectile.Center, base.projectile.Center + Utils.ToRotationVector2(base.projectile.rotation + -1.5707964f) * 140f, (float)projHitbox.Width, ref collisionPoint));
		}
	}
}
