using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs.New
{
	public class PZ2Eyelid : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Patient Zero");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.width = 98;
			base.npc.height = 80;
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
			base.npc.ai[1] += 1f;
			if (base.npc.ai[1] > 30f)
			{
				this.bodyFrame++;
				base.npc.ai[1] = 0f;
			}
			if (this.bodyFrame >= 8)
			{
				this.bodyFrame = 0;
			}
			this.coverCounter++;
			if (this.coverCounter > 60)
			{
				this.coverFrame++;
				this.coverCounter = 0;
			}
			if (this.coverFrame >= 4)
			{
				this.coverFrame = 0;
			}
			base.npc.frame.Y = 164;
			if (RedeWorld.pzUS)
			{
				int p = Projectile.NewProjectile(new Vector2(base.npc.Center.X, base.npc.Center.Y + 2f), new Vector2(0f, 0f), base.mod.ProjectileType("PZ2HideTheAAA"), 0, 0f, 255, 0f, 0f);
				Main.projectile[p].netUpdate = true;
				base.npc.SetDefaults(ModContent.NPCType<PZ2Fight>(), -1f);
				return;
			}
			if ((NPC.CountNPCS(base.mod.NPCType("PZ2Eyelid")) >= 2 && Main.rand.Next(2) == 0) || RedeWorld.downedPatientZero)
			{
				base.npc.active = false;
			}
			base.npc.timeLeft = 10;
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
		}

		public override bool CheckActive()
		{
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

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D eyeAni = base.mod.GetTexture("NPCs/LabNPCs/New/PZ2Pupil");
			Texture2D eyeGlow = base.mod.GetTexture("NPCs/LabNPCs/New/PZ2Pupil_Glow");
			Texture2D bodyAni = base.mod.GetTexture("NPCs/LabNPCs/New/PZ2");
			Texture2D bodyGlow = base.mod.GetTexture("NPCs/LabNPCs/New/PZ2_Glow");
			Texture2D coverAni = base.mod.GetTexture("NPCs/LabNPCs/New/PZ2BodyCover2");
			Texture2D sludgeAni = base.mod.GetTexture("NPCs/LabNPCs/New/SlimeThings");
			int spriteDirection = base.npc.spriteDirection;
			Vector2 drawCenterC = new Vector2(base.npc.Center.X + 15f, base.npc.Center.Y + 7f);
			int num214C = sludgeAni.Height;
			int y6C = 0;
			Main.spriteBatch.Draw(sludgeAni, drawCenterC - Main.screenPosition, new Rectangle?(new Rectangle(0, y6C, sludgeAni.Width, num214C)), drawColor, base.npc.rotation, new Vector2((float)sludgeAni.Width / 2f, (float)num214C / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			Vector2 drawCenterB = new Vector2(base.npc.Center.X - 2f, base.npc.Center.Y + 14f);
			int num214B = bodyAni.Height / 8;
			int y6B = num214B * this.bodyFrame;
			Main.spriteBatch.Draw(bodyAni, drawCenterB - Main.screenPosition, new Rectangle?(new Rectangle(0, y6B, bodyAni.Width, num214B)), drawColor, base.npc.rotation, new Vector2((float)bodyAni.Width / 2f, (float)num214B / 2f), base.npc.scale * 2f, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			Main.spriteBatch.Draw(bodyGlow, drawCenterB - Main.screenPosition, new Rectangle?(new Rectangle(0, y6B, bodyAni.Width, num214B)), base.npc.GetAlpha(Color.White), base.npc.rotation, new Vector2((float)bodyAni.Width / 2f, (float)num214B / 2f), base.npc.scale * 2f, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			Vector2 drawCenterD = new Vector2(base.npc.Center.X - 2f, base.npc.Center.Y + 18f);
			int num214D = coverAni.Height / 4;
			int y6D = num214D * this.coverFrame;
			Main.spriteBatch.Draw(coverAni, drawCenterD - Main.screenPosition, new Rectangle?(new Rectangle(0, y6D, coverAni.Width, num214D)), drawColor, base.npc.rotation, new Vector2((float)coverAni.Width / 2f, (float)num214D / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			Vector2 drawCenterA = new Vector2(base.npc.Center.X, base.npc.Center.Y);
			int num214A = eyeAni.Height / 1;
			int y6A = num214A * this.eyeFrame;
			Main.spriteBatch.Draw(eyeAni, drawCenterA - Main.screenPosition, new Rectangle?(new Rectangle(0, y6A, eyeAni.Width, num214A)), drawColor, Utils.ToRotation(base.npc.DirectionTo(Main.player[base.npc.target].Center)), new Vector2((float)eyeAni.Width / 2f, (float)num214A / 2f), base.npc.scale, SpriteEffects.None, 0f);
			Main.spriteBatch.Draw(eyeGlow, drawCenterA - Main.screenPosition, new Rectangle?(new Rectangle(0, y6A, eyeAni.Width, num214A)), base.npc.GetAlpha(Color.White), Utils.ToRotation(base.npc.DirectionTo(Main.player[base.npc.target].Center)), new Vector2((float)eyeAni.Width / 2f, (float)num214A / 2f), base.npc.scale, SpriteEffects.None, 0f);
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			return false;
		}

		private int bodyFrame;

		private int eyeFrame;

		private int coverFrame;

		private int coverCounter;
	}
}
