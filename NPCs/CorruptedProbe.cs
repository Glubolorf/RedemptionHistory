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
			Player P = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.netUpdate = true;
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower)
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
					float Speed = 5f;
					Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage = 30;
					int type = base.mod.ProjectileType("VlitchLaserPro");
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
					float rotation = (float)Math.Atan2((double)(vector8.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector8.X - (P.position.X + (float)P.width * 0.5f)));
					int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
					Main.projectile[num54].netUpdate = true;
					base.npc.ai[1] = 0f;
				}
				if (base.npc.life <= 500 && base.npc.ai[1] >= 60f)
				{
					float Speed2 = 6f;
					Vector2 vector9 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage2 = 30;
					int type2 = base.mod.ProjectileType("VlitchLaserPro");
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
					float rotation2 = (float)Math.Atan2((double)(vector9.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector9.X - (P.position.X + (float)P.width * 0.5f)));
					int num55 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0), type2, damage2, 0f, 0, 0f, 0f);
					Main.projectile[num55].netUpdate = true;
					base.npc.ai[1] = 0f;
				}
				if (base.npc.life <= 200 && base.npc.ai[1] >= 50f)
				{
					float Speed3 = 7f;
					Vector2 vector10 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage3 = 30;
					int type3 = base.mod.ProjectileType("VlitchLaserPro");
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
					float rotation3 = (float)Math.Atan2((double)(vector10.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector10.X - (P.position.X + (float)P.width * 0.5f)));
					int num56 = Projectile.NewProjectile(vector10.X, vector10.Y, (float)(Math.Cos((double)rotation3) * (double)Speed3 * -1.0), (float)(Math.Sin((double)rotation3) * (double)Speed3 * -1.0), type3, damage3, 0f, 0, 0f, 0f);
					Main.projectile[num56].netUpdate = true;
					base.npc.ai[1] = 0f;
				}
				if (base.npc.life <= 90 && base.npc.ai[1] >= 25f)
				{
					float Speed4 = 8f;
					Vector2 vector11 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
					int damage4 = 30;
					int type4 = base.mod.ProjectileType("VlitchLaserPro");
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 12, 1f, 0f);
					float rotation4 = (float)Math.Atan2((double)(vector11.Y - (P.position.Y + (float)P.height * 0.5f)), (double)(vector11.X - (P.position.X + (float)P.width * 0.5f)));
					int num57 = Projectile.NewProjectile(vector11.X, vector11.Y, (float)(Math.Cos((double)rotation4) * (double)Speed4 * -1.0), (float)(Math.Sin((double)rotation4) * (double)Speed4 * -1.0), type4, damage4, 0f, 0, 0f, 0f);
					Main.projectile[num57].netUpdate = true;
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
			return !Main.LocalPlayer.GetModPlayer<RedePlayer>().omegaPower;
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (base.npc.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			spriteBatch.Draw(base.mod.GetTexture("NPCs/CorruptedProbe_Glow"), new Vector2(base.npc.Center.X - Main.screenPosition.X, base.npc.Center.Y - Main.screenPosition.Y), new Rectangle?(base.npc.frame), Color.White, base.npc.rotation, new Vector2((float)base.npc.width * 0.5f, (float)base.npc.height * 0.5f), 1f, spriteEffects, 0f);
		}

		private int flyAwayTimer;
	}
}
