using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII.Clone
{
	public class KSShieldC : ModProjectile
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/OmegaSlayer/OSBubble";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Bubble Shield");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 175;
			base.projectile.height = 175;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
		}

		public override void AI()
		{
			int slayer = (int)base.projectile.ai[0];
			if (slayer < 0 || slayer >= 200 || !Main.npc[slayer].active || Main.npc[slayer].type != ModContent.NPCType<KS3_Body_Clone>())
			{
				base.projectile.Kill();
			}
			NPC npc2 = Main.npc[(int)base.projectile.ai[0]];
			base.projectile.Center = npc2.Center;
			base.projectile.velocity = Vector2.Zero;
			base.projectile.timeLeft = 10;
			if (base.projectile.alpha < 255)
			{
				base.projectile.alpha += 2;
			}
			if (base.projectile.alpha < 0)
			{
				base.projectile.alpha = 0;
			}
			if (base.projectile.scale <= 1f)
			{
				base.projectile.scale = 1f;
			}
			else
			{
				base.projectile.scale -= 0.02f;
			}
			if (base.projectile.scale > 2f)
			{
				base.projectile.scale = 2f;
			}
			foreach (Projectile proj in Enumerable.Where<Projectile>(Main.projectile, (Projectile x) => x.Hitbox.Intersects(base.projectile.Hitbox)))
			{
				if (base.projectile != proj && proj.active && proj.minionSlots == 0f && proj.damage > 0 && proj.friendly && !proj.hostile)
				{
					proj.Kill();
					base.projectile.localAI[0] += (float)proj.damage;
					CombatText.NewText(base.projectile.getRect(), Color.Orange, proj.damage, true, true);
					Main.PlaySound(SoundID.NPCHit34, base.projectile.position);
					base.projectile.alpha -= 40;
					base.projectile.scale += 0.04f;
					if (base.projectile.localAI[0] >= 9000f)
					{
						Main.PlaySound(SoundID.NPCDeath56, base.projectile.position);
						for (int i = 0; i < 20; i++)
						{
							double angle = Main.rand.NextDouble() * 2.0 * 3.141592653589793;
							this.vector.X = (float)(Math.Sin(angle) * 88.0);
							this.vector.Y = (float)(Math.Cos(angle) * 88.0);
							Dust dust2 = Main.dust[Dust.NewDust(base.projectile.Center + this.vector, 2, 2, 92, 0f, 0f, 100, default(Color), 3f)];
							dust2.noGravity = true;
							dust2.velocity = base.projectile.DirectionTo(dust2.position) * 4f;
						}
						base.projectile.Kill();
					}
				}
			}
		}

		private Vector2 vector;
	}
}
