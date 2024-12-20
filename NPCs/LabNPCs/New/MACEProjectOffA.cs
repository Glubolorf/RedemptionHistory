﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	public class MACEProjectOffA : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("MACE Project");
		}

		public override void SetDefaults()
		{
			base.npc.width = 118;
			base.npc.height = 140;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = -1;
			base.npc.noGravity = true;
			base.npc.noTileCollide = false;
			base.npc.dontTakeDamage = true;
			base.npc.npcSlots = 0f;
		}

		public override void AI()
		{
			if (!NPC.AnyNPCs(base.mod.NPCType("MACEProjectJawOffA")))
			{
				int minion = NPC.NewNPC((int)base.npc.position.X + 58, (int)base.npc.position.Y + 170, base.mod.NPCType("MACEProjectJawOffA"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[minion].netUpdate = true;
			}
			if (Main.netMode != 1 && RedeWorld.maceUS)
			{
				base.npc.SetDefaults(ModContent.NPCType<MACEProjectHeadA>(), -1f);
				return;
			}
			base.npc.timeLeft = 10;
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D trolleyAni = base.mod.GetTexture("NPCs/LabNPCs/New/CraneTrolley2");
			Texture2D hookAni = base.mod.GetTexture("NPCs/LabNPCs/New/CraneHook");
			int spriteDirection = base.npc.spriteDirection;
			Vector2 drawCenterA = new Vector2(base.npc.Center.X, base.npc.Center.Y - 37f);
			int num214A = hookAni.Height / 1;
			int y6A = 0;
			Main.spriteBatch.Draw(hookAni, drawCenterA - Main.screenPosition, new Rectangle?(new Rectangle(0, y6A, hookAni.Width, num214A)), drawColor, base.npc.rotation, new Vector2((float)hookAni.Width / 2f, (float)num214A / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y - 64f);
			int num214 = trolleyAni.Height / 1;
			int y6 = 0;
			Main.spriteBatch.Draw(trolleyAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, trolleyAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)trolleyAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.6f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.5f);
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}
	}
}
