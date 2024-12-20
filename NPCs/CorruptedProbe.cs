using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class CorruptedProbe : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Corrupted Probe");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 32;
			base.npc.height = 32;
			base.npc.friendly = false;
			base.npc.damage = 30;
			base.npc.defense = 15;
			base.npc.lifeMax = 1000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.noTileCollide = true;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0.1f;
			base.npc.aiStyle = 2;
			this.aiType = 2;
			this.animationType = 2;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedProbeGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedProbeGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/CorruptedProbeGore3"), 1f);
			}
		}

		public override void AI()
		{
			base.npc.ai[0] += 1f;
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.netUpdate = true;
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).omegaPower)
			{
				base.npc.aiStyle = 10;
				this.aiType = 34;
				this.flyAwayTimer++;
				if (this.flyAwayTimer >= 600)
				{
					NPC npc = base.npc;
					npc.velocity.Y = npc.velocity.Y + -1f;
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
				}
			}
			else
			{
				base.npc.aiStyle = 2;
				this.aiType = 2;
				this.flyAwayTimer = 0;
				base.npc.ai[1] += 1f;
				if (base.npc.ai[1] >= 75f)
				{
					float num = 5f;
					Vector2 vector;
					vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num2 = 30;
					int num3 = base.mod.ProjectileType("VlitchLaserPro");
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
					float num4 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
					int num5 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0), (float)(Math.Sin((double)num4) * (double)num * -1.0), num3, num2, 0f, 0, 0f, 0f);
					Main.projectile[num5].netUpdate = true;
					base.npc.ai[1] = 0f;
				}
				if (base.npc.life <= 500 && base.npc.ai[1] >= 60f)
				{
					float num6 = 6f;
					Vector2 vector2;
					vector2..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num7 = 30;
					int num8 = base.mod.ProjectileType("VlitchLaserPro");
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
					float num9 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
					int num10 = Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num9) * (double)num6 * -1.0), (float)(Math.Sin((double)num9) * (double)num6 * -1.0), num8, num7, 0f, 0, 0f, 0f);
					Main.projectile[num10].netUpdate = true;
					base.npc.ai[1] = 0f;
				}
				if (base.npc.life <= 200 && base.npc.ai[1] >= 50f)
				{
					float num11 = 7f;
					Vector2 vector3;
					vector3..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num12 = 30;
					int num13 = base.mod.ProjectileType("VlitchLaserPro");
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
					float num14 = (float)Math.Atan2((double)(vector3.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector3.X - (player.position.X + (float)player.width * 0.5f)));
					int num15 = Projectile.NewProjectile(vector3.X, vector3.Y, (float)(Math.Cos((double)num14) * (double)num11 * -1.0), (float)(Math.Sin((double)num14) * (double)num11 * -1.0), num13, num12, 0f, 0, 0f, 0f);
					Main.projectile[num15].netUpdate = true;
					base.npc.ai[1] = 0f;
				}
				if (base.npc.life <= 90 && base.npc.ai[1] >= 25f)
				{
					float num16 = 8f;
					Vector2 vector4;
					vector4..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int num17 = 30;
					int num18 = base.mod.ProjectileType("VlitchLaserPro");
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
					float num19 = (float)Math.Atan2((double)(vector4.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector4.X - (player.position.X + (float)player.width * 0.5f)));
					int num20 = Projectile.NewProjectile(vector4.X, vector4.Y, (float)(Math.Cos((double)num19) * (double)num16 * -1.0), (float)(Math.Sin((double)num19) * (double)num16 * -1.0), num18, num17, 0f, 0, 0f, 0f);
					Main.projectile[num20].netUpdate = true;
					base.npc.ai[1] = 0f;
				}
			}
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("VlitchFlame"), 0f, 0f, 0, default(Color), 1f);
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return !Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).omegaPower;
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			SpriteEffects spriteEffects = 0;
			if (base.npc.spriteDirection == 1)
			{
				spriteEffects = 1;
			}
			spriteBatch.Draw(base.mod.GetTexture("NPCs/CorruptedProbe_Glow"), new Vector2(base.npc.Center.X - Main.screenPosition.X, base.npc.Center.Y - Main.screenPosition.Y), new Rectangle?(base.npc.frame), Color.White, base.npc.rotation, new Vector2((float)base.npc.width * 0.5f, (float)base.npc.height * 0.5f), 1f, spriteEffects, 0f);
		}

		private int flyAwayTimer;
	}
}
