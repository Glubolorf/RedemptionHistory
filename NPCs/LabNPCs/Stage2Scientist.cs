﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.LabNPCs
{
	public class Stage2Scientist : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Scientist");
			Main.npcFrameCount[base.npc.type] = 15;
		}

		public override void SetDefaults()
		{
			base.npc.width = 46;
			base.npc.height = 50;
			base.npc.friendly = false;
			base.npc.damage = 90;
			base.npc.defense = 30;
			base.npc.lifeMax = 4250;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 220f;
			base.npc.knockBackResist = 0.01f;
			base.npc.aiStyle = 3;
			this.aiType = 271;
			this.animationType = 271;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 50; i++)
				{
					int num = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 273, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[num].velocity *= 1.9f;
				}
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 273, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			if (this.sludgeAttack)
			{
				this.throwCounter++;
				if (this.throwCounter > 5)
				{
					this.throwFrame++;
					this.throwCounter = 0;
				}
				if (this.throwFrame >= 6)
				{
					this.throwFrame = 0;
				}
			}
			float num = base.npc.Distance(Main.player[base.npc.target].Center);
			if (num <= 300f && Main.rand.Next(100) == 0 && !this.sludgeAttack)
			{
				this.sludgeAttack = true;
			}
			if (!this.sludgeAttack)
			{
				base.npc.aiStyle = 3;
			}
			if (this.sludgeAttack)
			{
				this.throwTimer++;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				if (this.throwTimer == 20)
				{
					if (base.npc.direction == -1)
					{
						Main.PlaySound(SoundID.Item7, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 42f, base.npc.position.Y + 24f), new Vector2((float)(-6 + Main.rand.Next(-6, 0)), (float)(-4 + Main.rand.Next(-4, 0))), base.mod.ProjectileType("GloopBallPro1"), 50, 3f, 255, 0f, 0f);
					}
					else
					{
						Main.PlaySound(SoundID.Item7, (int)base.npc.position.X, (int)base.npc.position.Y);
						Projectile.NewProjectile(new Vector2(base.npc.position.X + 12f, base.npc.position.Y + 24f), new Vector2((float)(6 + Main.rand.Next(0, 6)), (float)(-4 + Main.rand.Next(-4, 0))), base.mod.ProjectileType("GloopBallPro1"), 50, 3f, 255, 0f, 0f);
					}
				}
				if (this.throwTimer >= 30)
				{
					this.sludgeAttack = false;
					this.throwTimer = 0;
					this.throwCounter = 0;
					this.throwFrame = 0;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/LabNPCs/Stage2ScientistNYEH");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.sludgeAttack)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.sludgeAttack)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 6;
				int num2 = num * this.throwFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		private bool sludgeAttack;

		private int throwFrame;

		private int throwCounter;

		private int throwTimer;
	}
}