﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.Neb
{
	public class StarWyvernBody : StarWyvernHead
	{
		public override string Texture
		{
			get
			{
				return "Redemption/NPCs/Bosses/Neb/StarWyvernBody";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Giant Star Serpent");
			Main.npcFrameCount[base.npc.type] = 7;
			NPCID.Sets.TechnicallyABoss[base.npc.type] = true;
		}

		public override void SetDefaults()
		{
			base.SetDefaults();
			base.npc.boss = false;
			base.npc.width = 54;
			base.npc.height = 54;
			base.npc.dontCountMe = true;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return new bool?(false);
		}

		public override bool PreNPCLoot()
		{
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 2; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 58, 0f, 0f, 100, default(Color), 3f);
					Main.dust[dustIndex].velocity *= 1.8f;
				}
			}
		}

		public override bool PreAI()
		{
			Vector2 directionVector = Main.npc[(int)base.npc.ai[1]].Center - base.npc.Center;
			base.npc.spriteDirection = ((directionVector.X > 0f) ? 1 : -1);
			if (base.npc.ai[3] > 0f)
			{
				base.npc.realLife = (int)base.npc.ai[3];
			}
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead)
			{
				base.npc.TargetClosest(true);
			}
			if (Main.player[base.npc.target].dead && base.npc.timeLeft > 300)
			{
				base.npc.timeLeft = 300;
			}
			if (base.npc.alpha > 0)
			{
				base.npc.dontTakeDamage = true;
				base.npc.alpha -= 12;
				for (int spawnDust = 0; spawnDust < 2; spawnDust++)
				{
					int num935 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 242, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num935].noGravity = true;
					Main.dust[num935].noLight = true;
				}
			}
			else
			{
				base.npc.dontTakeDamage = false;
				base.npc.alpha = 0;
			}
			if (Main.netMode != 1 && (!Main.npc[(int)base.npc.ai[1]].active || Main.npc[(int)base.npc.ai[3]].type != ModContent.NPCType<StarWyvernHead>()))
			{
				base.npc.life = 0;
				base.npc.HitEffect(0, 10.0);
				base.npc.active = false;
				NetMessage.SendData(28, -1, -1, null, base.npc.whoAmI, -1f, 0f, 0f, 0, 0, 0);
			}
			if ((double)base.npc.ai[1] < (double)Main.npc.Length)
			{
				Vector2 npcCenter = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
				float dirX = Main.npc[(int)base.npc.ai[1]].position.X + (float)(Main.npc[(int)base.npc.ai[1]].width / 2) - npcCenter.X;
				float dirY = Main.npc[(int)base.npc.ai[1]].position.Y + (float)(Main.npc[(int)base.npc.ai[1]].height / 2) - npcCenter.Y;
				base.npc.rotation = (float)Math.Atan2((double)dirY, (double)dirX) + 1.57f;
				float length = (float)Math.Sqrt((double)(dirX * dirX + dirY * dirY));
				float dist = (length - (float)base.npc.width) / length;
				float posX = dirX * dist;
				float posY = dirY * dist;
				if (dirX < 0f)
				{
					base.npc.spriteDirection = 1;
				}
				else
				{
					base.npc.spriteDirection = -1;
				}
				base.npc.velocity = Vector2.Zero;
				base.npc.position.X = base.npc.position.X + posX;
				base.npc.position.Y = base.npc.position.Y + posY;
			}
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.netUpdate = true;
			return false;
		}

		public override void FindFrame(int frameHeight)
		{
			base.npc.frame.Y = frameHeight * (int)base.npc.ai[2];
		}

		public override bool CheckActive()
		{
			if (NPC.AnyNPCs(ModContent.NPCType<StarWyvernHead>()))
			{
				return false;
			}
			base.npc.active = false;
			return true;
		}
	}
}
