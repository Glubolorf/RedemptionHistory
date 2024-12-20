using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.Projectiles.Minions
{
	public class MiniVlitchCore : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Mini-Vlitch Core");
			ProjectileID.Sets.MinionSacrificable[base.projectile.type] = true;
			ProjectileID.Sets.Homing[base.projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[base.projectile.type] = true;
			Main.projPet[base.projectile.type] = true;
		}

		public override void SetDefaults()
		{
			base.projectile.netImportant = true;
			base.projectile.width = 38;
			base.projectile.height = 34;
			base.projectile.friendly = true;
			base.projectile.minion = true;
			base.projectile.minionSlots = 0f;
			base.projectile.penetrate = -1;
			base.projectile.timeLeft = 18000;
			base.projectile.tileCollide = false;
			base.projectile.ignoreWater = true;
		}

		public override void AI()
		{
			Player player = Main.player[base.projectile.owner];
			RedePlayer modPlayer = player.GetModPlayer<RedePlayer>();
			if (player.dead)
			{
				modPlayer.vlitchCoreAcc = false;
			}
			if (modPlayer.vlitchCoreAcc)
			{
				base.projectile.timeLeft = 2;
			}
			this.dust--;
			if (this.dust >= 0)
			{
				int num501 = 50;
				for (int num502 = 0; num502 < num501; num502++)
				{
					int num503 = Dust.NewDust(new Vector2(base.projectile.position.X, base.projectile.position.Y + 16f), base.projectile.width, base.projectile.height - 16, 235, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num503].velocity *= 2f;
					Main.dust[num503].scale *= 1.15f;
				}
			}
			float num504 = (float)Main.rand.Next(90, 111) * 0.01f;
			num504 *= Main.essScale;
			Lighting.AddLight(base.projectile.Center, 1f * num504, 0f * num504, 0.15f * num504);
			base.projectile.rotation = base.projectile.velocity.X * 0.04f;
			if ((double)Math.Abs(base.projectile.velocity.X) > 0.2)
			{
				base.projectile.spriteDirection = -base.projectile.direction;
			}
			float num505 = 700f;
			float num506 = 800f;
			float num507 = 1200f;
			float num508 = 150f;
			float num509 = 0.05f;
			for (int num510 = 0; num510 < 1000; num510++)
			{
				bool flag23 = Main.projectile[num510].type == base.mod.ProjectileType("MiniVlitchCore");
				if (num510 != base.projectile.whoAmI && Main.projectile[num510].active && Main.projectile[num510].owner == base.projectile.owner && flag23 && Math.Abs(base.projectile.position.X - Main.projectile[num510].position.X) + Math.Abs(base.projectile.position.Y - Main.projectile[num510].position.Y) < (float)base.projectile.width)
				{
					if (base.projectile.position.X < Main.projectile[num510].position.X)
					{
						base.projectile.velocity.X = base.projectile.velocity.X - num509;
					}
					else
					{
						base.projectile.velocity.X = base.projectile.velocity.X + num509;
					}
					if (base.projectile.position.Y < Main.projectile[num510].position.Y)
					{
						base.projectile.velocity.Y = base.projectile.velocity.Y - num509;
					}
					else
					{
						base.projectile.velocity.Y = base.projectile.velocity.Y + num509;
					}
				}
			}
			bool flag24 = false;
			if (base.projectile.ai[0] == 2f)
			{
				base.projectile.ai[1] += 1f;
				base.projectile.extraUpdates = 1;
				if (base.projectile.ai[1] > 40f)
				{
					base.projectile.ai[1] = 1f;
					base.projectile.ai[0] = 0f;
					base.projectile.extraUpdates = 0;
					base.projectile.numUpdates = 0;
					base.projectile.netUpdate = true;
				}
				else
				{
					flag24 = true;
				}
			}
			if (flag24)
			{
				return;
			}
			Vector2 vector46 = base.projectile.position;
			bool flag25 = false;
			if (base.projectile.ai[0] != 1f)
			{
				base.projectile.tileCollide = false;
			}
			if (base.projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)base.projectile.Center.X / 16, (int)base.projectile.Center.Y / 16)))
			{
				base.projectile.tileCollide = false;
			}
			for (int num511 = 0; num511 < 200; num511++)
			{
				NPC nPC2 = Main.npc[num511];
				if (nPC2.CanBeChasedBy(base.projectile, false))
				{
					float num512 = Vector2.Distance(nPC2.Center, base.projectile.Center);
					if (((Vector2.Distance(base.projectile.Center, vector46) > num512 && num512 < num505) || !flag25) && Collision.CanHitLine(base.projectile.position, base.projectile.width, base.projectile.height, nPC2.position, nPC2.width, nPC2.height))
					{
						num505 = num512;
						vector46 = nPC2.Center;
						flag25 = true;
					}
				}
			}
			float num513 = num506;
			if (flag25)
			{
				num513 = num507;
			}
			if (Vector2.Distance(player.Center, base.projectile.Center) > num513)
			{
				base.projectile.ai[0] = 1f;
				base.projectile.tileCollide = false;
				base.projectile.netUpdate = true;
			}
			if (flag25 && base.projectile.ai[0] == 0f)
			{
				Vector2 vector47 = vector46 - base.projectile.Center;
				float num518 = vector47.Length();
				vector47.Normalize();
				if (num518 > 200f)
				{
					float scaleFactor2 = 8f;
					vector47 *= scaleFactor2;
					base.projectile.velocity = (base.projectile.velocity * 40f + vector47) / 41f;
				}
				else
				{
					float num514 = 4f;
					vector47 *= -num514;
					base.projectile.velocity = (base.projectile.velocity * 40f + vector47) / 41f;
				}
			}
			else
			{
				bool flag26 = false;
				if (!flag26)
				{
					flag26 = (base.projectile.ai[0] == 1f);
				}
				float num515 = 5f;
				if (flag26)
				{
					num515 = 12f;
				}
				Vector2 center2 = base.projectile.Center;
				Vector2 vector48 = player.Center - center2 + new Vector2(0f, -30f);
				float num519 = vector48.Length();
				if (num519 > 200f && num515 < 6.5f)
				{
					num515 = 6.5f;
				}
				if (num519 < num508 && flag26 && !Collision.SolidCollision(base.projectile.position, base.projectile.width, base.projectile.height))
				{
					base.projectile.ai[0] = 0f;
					base.projectile.netUpdate = true;
				}
				if (num519 > 2000f)
				{
					base.projectile.position.X = Main.player[base.projectile.owner].Center.X - (float)(base.projectile.width / 2);
					base.projectile.position.Y = Main.player[base.projectile.owner].Center.Y - (float)(base.projectile.height / 2);
					base.projectile.netUpdate = true;
				}
				if (num519 > 70f)
				{
					vector48.Normalize();
					vector48 *= num515;
					base.projectile.velocity = (base.projectile.velocity * 40f + vector48) / 41f;
				}
				else if (base.projectile.velocity.X == 0f && base.projectile.velocity.Y == 0f)
				{
					base.projectile.velocity.X = -0.2f;
					base.projectile.velocity.Y = -0.1f;
				}
			}
			if (base.projectile.ai[1] > 0f)
			{
				base.projectile.ai[1] += (float)Main.rand.Next(1, 4);
			}
			if (base.projectile.ai[1] > 80f)
			{
				base.projectile.ai[1] = 0f;
				base.projectile.netUpdate = true;
			}
			if (base.projectile.ai[0] == 0f)
			{
				float scaleFactor3 = 11f;
				int num516 = ModContent.ProjectileType<VlitchLaserPro>();
				if (flag25 && base.projectile.ai[1] == 0f)
				{
					base.projectile.ai[1] += 1f;
					if (Main.myPlayer == base.projectile.owner && Collision.CanHitLine(base.projectile.position, base.projectile.width, base.projectile.height, vector46, 0, 0))
					{
						Vector2 value19 = vector46 - base.projectile.Center;
						value19.Normalize();
						value19 *= scaleFactor3;
						int num517 = Projectile.NewProjectile(base.projectile.Center.X, base.projectile.Center.Y, value19.X, value19.Y, num516, base.projectile.damage, 0f, Main.myPlayer, 0f, 0f);
						Main.projectile[num517].friendly = true;
						Main.projectile[num517].hostile = false;
						Main.projectile[num517].timeLeft = 300;
						base.projectile.netUpdate = true;
					}
				}
			}
		}

		private int dust = 3;
	}
}
