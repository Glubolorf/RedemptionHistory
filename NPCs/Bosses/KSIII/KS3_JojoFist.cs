using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.KSIII
{
	public class KS3_JojoFist : KSHitbox
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/KSIII/KS3_JojoFist";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("King Slayer III");
			Main.projFrames[base.projectile.type] = 7;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.projectile.width = 14;
			base.projectile.height = 14;
			base.projectile.alpha = 0;
			base.projectile.timeLeft = 120;
		}

		public override void AI()
		{
			Projectile projectile = base.projectile;
			int num = projectile.frameCounter + 1;
			projectile.frameCounter = num;
			if (num >= 5)
			{
				base.projectile.frameCounter = 0;
				Projectile projectile2 = base.projectile;
				num = projectile2.frame + 1;
				projectile2.frame = num;
				if (num >= 7)
				{
					base.projectile.frame = 0;
					base.projectile.Kill();
				}
			}
			if (base.projectile.frame > 3)
			{
				base.projectile.hostile = false;
			}
			int slayer = (int)base.projectile.ai[0];
			if (slayer < 0 || slayer >= 200 || !Main.npc[slayer].active || Main.npc[slayer].type != ModContent.NPCType<KS3_Body>())
			{
				base.projectile.Kill();
			}
			NPC npc2 = Main.npc[(int)base.projectile.ai[0]];
			Vector2 HitPos = new Vector2((float)((npc2.spriteDirection == -1) ? (-28 + Main.rand.Next(-8, 8)) : (28 + Main.rand.Next(-8, 8))), (float)(-18 + Main.rand.Next(-18, 18)));
			float obj = base.projectile.localAI[1];
			if (!0f.Equals(obj))
			{
				if (1f.Equals(obj))
				{
					base.projectile.Center = new Vector2(npc2.Center.X + this.vector.X + this.offset, npc2.Center.Y + this.vector.Y);
					if (base.projectile.frame > 1)
					{
						if (npc2.Center.X > base.projectile.Center.X)
						{
							this.offset -= 15f;
						}
						else
						{
							this.offset += 15f;
						}
					}
				}
			}
			else
			{
				this.vector = HitPos;
				base.projectile.localAI[1] = 1f;
			}
			base.projectile.velocity = Vector2.Zero;
			base.projectile.rotation = 1.5707964f;
			if (npc2.Center.X > base.projectile.Center.X)
			{
				base.projectile.rotation = -1.5707964f;
			}
			else
			{
				base.projectile.rotation = 1.5707964f;
			}
			if (npc2.ai[1] != 5f || npc2.ai[0] != 5f)
			{
				base.projectile.Kill();
			}
		}

		public Vector2 vector;

		public float offset;
	}
}
