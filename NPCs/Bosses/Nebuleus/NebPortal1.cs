using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Nebuleus
{
	public class NebPortal1 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Nebulous Portal");
		}

		public override void SetDefaults()
		{
			base.projectile.width = 74;
			base.projectile.height = 80;
			base.projectile.penetrate = -1;
			base.projectile.hostile = false;
			base.projectile.friendly = false;
			base.projectile.ignoreWater = true;
			base.projectile.tileCollide = false;
			base.projectile.alpha = 255;
			base.projectile.timeLeft = 300;
		}

		public override void AI()
		{
			base.projectile.alpha -= 2;
			base.projectile.localAI[0] += 1f;
			if (base.projectile.localAI[0] == 60f)
			{
				Main.PlaySound(SoundID.Item8, base.projectile.position);
				int dustType = 62;
				int pieCut = 16;
				for (int i = 0; i < pieCut; i++)
				{
					int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 1.6f);
					Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(8f, 0f), (float)i / (float)pieCut * 6.28f);
					Main.dust[dustID].noLight = false;
					Main.dust[dustID].noGravity = true;
				}
				int num = Main.rand.Next(4);
				if (num == 0 && Main.netMode != 1)
				{
					int j = NPC.NewNPC((int)base.projectile.Center.X, (int)base.projectile.Center.Y, 420, 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[j].lifeMax = 4000;
					Main.npc[j].life = 4000;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, j, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (num == 1 && Main.netMode != 1)
				{
					int k = NPC.NewNPC((int)base.projectile.Center.X, (int)base.projectile.Center.Y, 421, 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[k].lifeMax = 4000;
					Main.npc[k].life = 4000;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, k, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (num == 2 && Main.netMode != 1)
				{
					int l = NPC.NewNPC((int)base.projectile.Center.X, (int)base.projectile.Center.Y, 423, 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[l].lifeMax = 4000;
					Main.npc[l].life = 4000;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, l, 0f, 0f, 0f, 0, 0, 0);
					}
				}
				if (num == 3 && Main.netMode != 1)
				{
					int m = NPC.NewNPC((int)base.projectile.Center.X, (int)base.projectile.Center.Y, 421, 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[m].lifeMax = 4000;
					Main.npc[m].life = 4000;
					if (Main.netMode == 2)
					{
						NetMessage.SendData(23, -1, -1, null, m, 0f, 0f, 0f, 0, 0, 0);
					}
				}
			}
			if (base.projectile.localAI[0] >= 120f)
			{
				int dustType2 = 62;
				int pieCut2 = 16;
				for (int n = 0; n < pieCut2; n++)
				{
					int dustID2 = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType2, 0f, 0f, 100, Color.White, 2f);
					Main.dust[dustID2].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)n / (float)pieCut2 * 6.28f);
					Main.dust[dustID2].noLight = false;
					Main.dust[dustID2].noGravity = true;
				}
				base.projectile.Kill();
			}
		}

		public override void Kill(int timeLeft)
		{
			int dustType = 62;
			int pieCut = 18;
			for (int i = 0; i < pieCut; i++)
			{
				int dustID = Dust.NewDust(new Vector2(base.projectile.Center.X - 1f, base.projectile.Center.Y - 1f), 2, 2, dustType, 0f, 0f, 100, Color.White, 2f);
				Main.dust[dustID].velocity = BaseUtility.RotateVector(default(Vector2), new Vector2(10f, 0f), (float)i / (float)pieCut * 6.28f);
				Main.dust[dustID].noLight = false;
				Main.dust[dustID].noGravity = true;
			}
		}
	}
}
