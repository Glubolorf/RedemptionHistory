﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class AncientCaster : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Ancient Timeshifter");
			Main.npcFrameCount[base.npc.type] = 6;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 22000;
			base.npc.damage = 60;
			base.npc.defense = 50;
			base.npc.knockBackResist = 0f;
			base.npc.value = (float)Item.buyPrice(0, 2, 0, 0);
			base.npc.aiStyle = -1;
			base.npc.width = 84;
			base.npc.height = 60;
			base.npc.noTileCollide = true;
			base.npc.noGravity = true;
			base.npc.aiStyle = 22;
			this.aiType = 316;
			base.npc.HitSound = SoundID.NPCHit54;
			base.npc.DeathSound = SoundID.NPCDeath52;
			base.npc.lavaImmune = true;
		}

		public override void NPCLoot()
		{
			if (RedeWorld.downedEaglecrestGolemPZ)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("AncientPowerCore"), Main.rand.Next(1, 4), false, 0, false, false);
				return;
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("AncientCoreF"), Main.rand.Next(1, 4), false, 0, false, false);
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			float num = base.npc.Distance(Main.player[base.npc.target].Center);
			if (player.Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 3.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 72;
				if (base.npc.frame.Y > 360)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (this.spell)
			{
				this.spellCounter++;
				if (this.spellCounter > 3)
				{
					this.spellFrame++;
					this.spellCounter = 0;
				}
				if (this.spellFrame >= 6)
				{
					this.spellFrame = 0;
				}
			}
			if (num <= 400f && base.npc.frame.Y == 0 && Main.rand.Next(20) == 0 && !this.spell)
			{
				this.spell = true;
			}
			if (this.spell)
			{
				this.spellTimer++;
				if (this.spellTimer == 1 && !Config.NoCombatText)
				{
					int num2 = Main.rand.Next(3);
					if (num2 == 0)
					{
						CombatText.NewText(base.npc.getRect(), Color.DarkGoldenrod, "Your TIME is up!", true, false);
					}
					if (num2 == 1)
					{
						CombatText.NewText(base.npc.getRect(), Color.DarkGoldenrod, "TIME to die!", true, false);
					}
					if (num2 == 2)
					{
						CombatText.NewText(base.npc.getRect(), Color.DarkGoldenrod, "Your TIME has come!", true, false);
					}
				}
				if (this.spellTimer == 9 || this.spellTimer == 18)
				{
					Main.PlaySound(SoundID.Item28, (int)base.npc.position.X, (int)base.npc.position.Y);
					float num3 = 4f;
					Vector2 vector;
					vector..ctor(base.npc.Center.X, base.npc.Center.Y);
					int num4 = 50;
					int num5 = base.mod.ProjectileType("AncientHourGlassPro");
					float num6 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
					int num7 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num6) * (double)num3 * -1.0), (float)(Math.Sin((double)num6) * (double)num3 * -1.0), num5, num4, 0f, 0, 0f, 0f);
					Main.projectile[num7].netUpdate = true;
				}
				if (this.spellTimer >= 18)
				{
					this.spell = false;
					this.spellTimer = 0;
					this.spellCounter = 0;
					this.spellFrame = 0;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/v08/AncientCasterAttack");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.spell)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.spell)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 6;
				int num2 = num * this.spellFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * (RedeWorld.downedPatientZero ? 0.04f : 0f);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				if (Main.netMode != 1 && base.npc.life <= 0)
				{
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("LostSoul3"), 0, 0f, 0f, 0f, 0f, 255);
				}
				for (int i = 0; i < 30; i++)
				{
					int num = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 269, 0f, 0f, 100, default(Color), 1f);
					Main.dust[num].velocity *= 4.6f;
				}
			}
			int num2 = Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 269, 0f, 0f, 100, default(Color), 1f);
			Main.dust[num2].velocity *= 1.6f;
		}

		private bool spell;

		private int spellFrame;

		private int spellCounter;

		private int spellTimer;
	}
}