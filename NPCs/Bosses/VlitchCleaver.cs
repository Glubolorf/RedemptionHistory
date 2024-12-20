﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	[AutoloadBossHead]
	public class VlitchCleaver : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Vlitch Cleaver");
			Main.npcFrameCount[base.npc.type] = 6;
		}

		public override void SetDefaults()
		{
			base.npc.width = 98;
			base.npc.height = 280;
			base.npc.friendly = false;
			base.npc.damage = 250;
			base.npc.defense = 60;
			base.npc.lifeMax = 35000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.value = 600f;
			base.npc.boss = true;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			this.animationType = 83;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/BossVlitch1");
			this.bossBag = base.mod.ItemType("VlitchCleaverBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 80; i++)
				{
					int num = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 235, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num].velocity *= 1.9f;
				}
				for (int j = 0; j < 45; j++)
				{
					int num2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 6, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[num2].velocity *= 1.8f;
				}
				for (int k = 0; k < 25; k++)
				{
					int num3 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 31, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[num3].velocity *= 1.8f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("VlitchTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(14) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("GirusMask"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("GirusDagger"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("GirusLance"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CorruptedXenomite"), Main.rand.Next(12, 24), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("VlitchBattery"), Main.rand.Next(1, 3), false, 0, false, false);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 499;
			RedeWorld.downedVlitch1 = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
			if (!RedeWorld.girusTalk1 && !NPC.AnyNPCs(base.mod.NPCType("VlitchWormHead")) && !NPC.AnyNPCs(base.mod.NPCType("OmegaOblitDamaged")))
			{
				Projectile.NewProjectile(new Vector2(base.npc.position.X, base.npc.position.Y), new Vector2(0f, 0f), base.mod.ProjectileType("GirusTalking1"), 0, 0f, 255, 0f, 0f);
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.5f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void AI()
		{
			if (Main.dayTime)
			{
				base.npc.timeLeft = 0;
				NPC npc = base.npc;
				npc.position.Y = npc.position.Y - 300f;
			}
			this.Target();
			this.DespawnHandler();
			base.npc.ai[1] += 1f;
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.netUpdate = true;
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, base.mod.DustType("VlitchFlame"), 0f, 0f, 0, default(Color), 1f);
			}
			if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).omegaPower)
			{
				this.omegaTimer++;
				if (this.omegaTimer == 600)
				{
					Main.NewText("Wait... Why aren't my minions targetting you?", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				if (this.omegaTimer == 800)
				{
					Main.NewText("Oh, because you got that damn exoskeleton on...", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
				}
				if (this.omegaTimer == 1300)
				{
					this.takeAction = true;
				}
			}
			if (base.npc.life >= 20000)
			{
				base.npc.ai[1] += 1f;
			}
			if (base.npc.ai[1] % 200f == 80f && NPC.CountNPCS(base.mod.NPCType("CorruptedProbe")) <= 6)
			{
				NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 120, base.mod.NPCType("CorruptedProbe"), 0, 0f, 0f, 0f, 0f, 255);
			}
			if (base.npc.life <= 19500)
			{
				base.npc.ai[2] += 1f;
			}
			if (base.npc.ai[2] >= 250f)
			{
				float num = 10f;
				Vector2 vector;
				vector..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num2 = 300;
				int num3 = base.mod.ProjectileType("VlitchCleaverPro");
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 33, 1f, 0f);
				float num4 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0), (float)(Math.Sin((double)num4) * (double)num * -1.0), num3, num2, 0f, 0, 0f, 0f);
				base.npc.ai[2] = 0f;
			}
			if (base.npc.ai[2] % 200f == 80f && NPC.CountNPCS(base.mod.NPCType("CorruptedBlade")) <= 6)
			{
				NPC.NewNPC((int)base.npc.position.X + 70, (int)base.npc.position.Y + 120, base.mod.NPCType("CorruptedBlade"), 0, 0f, 0f, 0f, 0f, 255);
			}
			if (base.npc.life <= 2000)
			{
				base.npc.ai[3] += 1f;
			}
			if (base.npc.ai[3] >= 100f)
			{
				float num5 = 10f;
				Vector2 vector2;
				vector2..ctor(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int num6 = 300;
				int num7 = base.mod.ProjectileType("VlitchCleaverPro");
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 33, 1f, 0f);
				float num8 = (float)Math.Atan2((double)(vector2.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector2.X - (player.position.X + (float)player.width * 0.5f)));
				Projectile.NewProjectile(vector2.X, vector2.Y, (float)(Math.Cos((double)num8) * (double)num5 * -1.0), (float)(Math.Sin((double)num8) * (double)num5 * -1.0), num7, num6, 0f, 0, 0f, 0f);
				base.npc.ai[3] = 0f;
			}
			if (base.npc.life <= 19500)
			{
				this.takeAction = true;
			}
			if (this.takeAction)
			{
				this.timer++;
				if (this.timer == 1)
				{
					if (Main.LocalPlayer.GetModPlayer<RedePlayer>(base.mod).omegaPower)
					{
						Main.NewText("No matter, I guess I'll take action...", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
					}
					else
					{
						Main.NewText("Guess its time to take action...", Color.IndianRed.R, Color.IndianRed.G, Color.IndianRed.B, false);
					}
				}
				this.timer2++;
				if (this.timer2 <= 120 && this.player.active)
				{
					NPC npc2 = base.npc;
					npc2.velocity.Y = npc2.velocity.Y * 0.2f;
				}
				if (this.timer2 >= 120)
				{
					if (this.player.active)
					{
						NPC npc3 = base.npc;
						npc3.velocity.Y = npc3.velocity.Y * -0.2f;
					}
					if (this.timer2 == 240)
					{
						this.timer2 = 0;
					}
				}
				base.npc.aiStyle = 5;
				this.aiType = 205;
				base.npc.noGravity = true;
				base.npc.noTileCollide = true;
			}
		}

		private void Target()
		{
			this.player = Main.player[base.npc.target];
		}

		private void DespawnHandler()
		{
			if (!this.player.active || this.player.dead)
			{
				base.npc.TargetClosest(false);
				this.player = Main.player[base.npc.target];
				if (!this.player.active || this.player.dead)
				{
					base.npc.velocity = new Vector2(0f, -10f);
					if (base.npc.timeLeft > 10)
					{
						base.npc.timeLeft = 10;
					}
				}
			}
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			SpriteEffects spriteEffects = 0;
			if (base.npc.spriteDirection == 1)
			{
				spriteEffects = 1;
			}
			spriteBatch.Draw(base.mod.GetTexture("NPCs/Bosses/VlitchCleaver_Glow"), new Vector2(base.npc.Center.X - Main.screenPosition.X, base.npc.Center.Y - Main.screenPosition.Y), new Rectangle?(base.npc.frame), Color.White, base.npc.rotation, new Vector2((float)base.npc.width * 0.5f, (float)base.npc.height * 0.5f), 1f, spriteEffects, 0f);
		}

		private Player player;

		public int timer;

		public int timer2;

		private int omegaTimer;

		private bool takeAction;
	}
}
