﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	[AutoloadBossHead]
	public class SkullDigger : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Skull Digger");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 94;
			base.npc.height = 124;
			base.npc.friendly = false;
			base.npc.damage = 35;
			base.npc.defense = 0;
			base.npc.lifeMax = 3000;
			base.npc.HitSound = SoundID.NPCHit3;
			base.npc.DeathSound = SoundID.NPCDeath51;
			base.npc.value = (float)Item.buyPrice(0, 2, 0, 0);
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 22;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			this.aiType = 316;
			this.animationType = 316;
			base.npc.alpha = 255;
			base.npc.boss = true;
			base.npc.netAlways = true;
			this.music = base.mod.GetSoundSlot(51, "Sounds/Music/SilentCaverns");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkullDiggerGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkullDiggerGore2"), 1f);
				for (int i = 0; i < 50; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 5, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[num].velocity *= 1.9f;
				}
			}
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 48, (int)base.npc.position.Y + 38, base.mod.NPCType("LostSoul2"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.position.X + 30, (int)base.npc.position.Y + 62, base.mod.NPCType("LostSoul2"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.position.X + 48, (int)base.npc.position.Y + 98, base.mod.NPCType("LostSoul1"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.position.X + 58, (int)base.npc.position.Y + 60, base.mod.NPCType("LostSoul1"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.position.X + 36, (int)base.npc.position.Y + 94, base.mod.NPCType("LostSoul1"), 0, 0f, 0f, 0f, 0f, 255);
				CombatText.NewText(base.npc.getRect(), Colors.RarityPurple, "You're... Soulless...", true, false);
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			potionType = base.mod.ItemType("DarkShard");
			if (!RedeWorld.downedSkullDigger)
			{
				RedeWorld.redemptionPoints -= 3;
				CombatText.NewText(player.getRect(), Color.Red, "-3", true, false);
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> Oh... Oh damn...", Color.DarkGoldenrod, false);
							}
						}
					}
				}
			}
			RedeWorld.downedSkullDigger = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void AI()
		{
			if (this.specialStart)
			{
				this.specialCounter++;
				if (this.specialCounter > 10)
				{
					this.specialFrame++;
					this.specialCounter = 0;
				}
				if (this.specialFrame >= 6)
				{
					this.specialFrame = 0;
				}
			}
			this.skullDiggerTimer++;
			if (this.skullDiggerTimer == 1 && !Main.dedServ)
			{
				Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/SpookyNoise").WithVolume(0.9f).WithPitchVariance(0f), -1, -1);
			}
			if (this.skullDiggerTimer <= 120)
			{
				base.npc.alpha -= 4;
				base.npc.dontTakeDamage = true;
			}
			if (this.skullDiggerTimer > 120)
			{
				base.npc.dontTakeDamage = false;
			}
			if (this.skullDiggerTimer == 1)
			{
				string text = "The caverns go silent...";
				Color rarityBlue = Colors.RarityBlue;
				byte r = rarityBlue.R;
				Color rarityBlue2 = Colors.RarityBlue;
				byte g = rarityBlue2.G;
				Color rarityBlue3 = Colors.RarityBlue;
				Main.NewText(text, r, g, rarityBlue3.B, false);
			}
			if (this.skullDiggerTimer == 120)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityPurple, "I have... been looking... for you...", true, false);
			}
			if (this.skullDiggerTimer == 220)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityPurple, "You... you slayed... her...", true, false);
			}
			if (this.skullDiggerTimer == 360)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityPurple, "You've... killed the Keeper...", true, false);
			}
			if (this.skullDiggerTimer == 560)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityPurple, "Do you understand... what shes been through...", true, false);
			}
			if (this.skullDiggerTimer == 960)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityPurple, "Hundreds of years... trapped with no other soul...", true, false);
			}
			if (this.skullDiggerTimer == 1500)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityPurple, "I... have to... avenge her...", true, false);
			}
			if (Main.rand.Next(400) == 0 && !this.specialStart && this.skullDiggerTimer >= 220)
			{
				this.specialStart = true;
			}
			if (this.specialStart)
			{
				int num = Main.rand.Next(3);
				if (num == 0)
				{
					this.specialTimer++;
					if (this.specialTimer == 30)
					{
						int num2 = NPC.NewNPC((int)base.npc.position.X + 48, (int)base.npc.position.Y + 38, base.mod.NPCType("DarkSoul3"), 0, 0f, 0f, 0f, 0f, 255);
						num2 = NPC.NewNPC((int)base.npc.position.X + 30, (int)base.npc.position.Y + 48, base.mod.NPCType("DarkSoul3"), 0, 0f, 0f, 0f, 0f, 255);
						num2 = NPC.NewNPC((int)base.npc.position.X + 54, (int)base.npc.position.Y + 50, base.mod.NPCType("DarkSoul3"), 0, 0f, 0f, 0f, 0f, 255);
						num2 = NPC.NewNPC((int)base.npc.position.X + 36, (int)base.npc.position.Y + 80, base.mod.NPCType("DarkSoul3"), 0, 0f, 0f, 0f, 0f, 255);
						num2 = NPC.NewNPC((int)base.npc.position.X + 45, (int)base.npc.position.Y + 90, base.mod.NPCType("DarkSoul3"), 0, 0f, 0f, 0f, 0f, 255);
						num2 = NPC.NewNPC((int)base.npc.position.X + 42, (int)base.npc.position.Y + 78, base.mod.NPCType("DarkSoul3"), 0, 0f, 0f, 0f, 0f, 255);
						num2 = NPC.NewNPC((int)base.npc.position.X + 37, (int)base.npc.position.Y + 28, base.mod.NPCType("DarkSoul3"), 0, 0f, 0f, 0f, 0f, 255);
						num2 = NPC.NewNPC((int)base.npc.position.X + 51, (int)base.npc.position.Y + 60, base.mod.NPCType("DarkSoul3"), 0, 0f, 0f, 0f, 0f, 255);
						num2 = NPC.NewNPC((int)base.npc.position.X + 37, (int)base.npc.position.Y + 40, base.mod.NPCType("DarkSoul3"), 0, 0f, 0f, 0f, 0f, 255);
						num2 = NPC.NewNPC((int)base.npc.position.X + 44, (int)base.npc.position.Y + 54, base.mod.NPCType("DarkSoul3"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[num2].netUpdate = true;
						Main.PlaySound(SoundID.NPCDeath6, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					if (this.specialTimer >= 60)
					{
						this.specialTimer = 0;
						this.specialStart = false;
						this.specialFrame = 0;
						this.specialCounter = 0;
					}
				}
				if (num == 1)
				{
					this.specialTimer++;
					if (this.specialTimer == 30)
					{
						int num3 = NPC.NewNPC((int)base.npc.position.X + 48, (int)base.npc.position.Y + 38, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
						num3 = NPC.NewNPC((int)base.npc.position.X + 30, (int)base.npc.position.Y + 48, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
						num3 = NPC.NewNPC((int)base.npc.position.X + 54, (int)base.npc.position.Y + 50, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
						num3 = NPC.NewNPC((int)base.npc.position.X + 36, (int)base.npc.position.Y + 80, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
						num3 = NPC.NewNPC((int)base.npc.position.X + 45, (int)base.npc.position.Y + 90, base.mod.NPCType("DarkSoul2"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[num3].netUpdate = true;
						Main.PlaySound(SoundID.NPCDeath6, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					if (this.specialTimer >= 60)
					{
						this.specialTimer = 0;
						this.specialStart = false;
						this.specialFrame = 0;
						this.specialCounter = 0;
					}
				}
				if (num == 2)
				{
					this.specialTimer++;
					if (this.specialTimer == 30)
					{
						int num4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 74f), new Vector2(0f, -6f), base.mod.ProjectileType("DarkSoulPro3"), 15, 3f, 255, 0f, 0f);
						num4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 74f), new Vector2(0f, 6f), base.mod.ProjectileType("DarkSoulPro3"), 15, 3f, 255, 0f, 0f);
						num4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 74f), new Vector2(-6f, 0f), base.mod.ProjectileType("DarkSoulPro3"), 15, 3f, 255, 0f, 0f);
						num4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 74f), new Vector2(6f, 0f), base.mod.ProjectileType("DarkSoulPro3"), 15, 3f, 255, 0f, 0f);
						num4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 74f), new Vector2(4f, 4f), base.mod.ProjectileType("DarkSoulPro3"), 15, 3f, 255, 0f, 0f);
						num4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 74f), new Vector2(4f, -4f), base.mod.ProjectileType("DarkSoulPro3"), 15, 3f, 255, 0f, 0f);
						num4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 74f), new Vector2(-4f, 4f), base.mod.ProjectileType("DarkSoulPro3"), 15, 3f, 255, 0f, 0f);
						num4 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 44f, base.npc.position.Y + 74f), new Vector2(-4f, -4f), base.mod.ProjectileType("DarkSoulPro3"), 15, 3f, 255, 0f, 0f);
						Main.projectile[num4].netUpdate = true;
						Main.PlaySound(SoundID.NPCDeath6, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					if (this.specialTimer >= 60)
					{
						this.specialTimer = 0;
						this.specialStart = false;
						this.specialFrame = 0;
						this.specialCounter = 0;
					}
				}
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.5f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.5f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/SkullDiggerSpecial");
			Texture2D texture2 = base.mod.GetTexture("NPCs/SkullDiggerSpecial_Glow");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.specialStart)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.specialStart)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 6;
				int num2 = num * this.specialFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
				Main.spriteBatch.Draw(texture2, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == 1) ? 0 : 1, 0f);
			}
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * ((RedeWorld.downedTheKeeper && !RedeWorld.downedSkullDigger && !NPC.AnyNPCs(base.mod.NPCType("SkullDigger"))) ? 0.002f : 0f);
		}

		private int skullDiggerTimer;

		private bool specialStart;

		private int specialFrame;

		private int specialCounter;

		private int specialTimer;

		private bool beginFight;
	}
}
