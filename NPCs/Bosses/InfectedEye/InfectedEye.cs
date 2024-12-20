using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses.InfectedEye
{
	[AutoloadBossHead]
	public class InfectedEye : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Infected Eye");
			Main.npcFrameCount[base.npc.type] = 3;
		}

		public override void SetDefaults()
		{
			base.npc.aiStyle = 5;
			base.npc.lifeMax = 20000;
			base.npc.damage = 60;
			base.npc.defense = 20;
			base.npc.knockBackResist = 0f;
			base.npc.width = 110;
			base.npc.height = 166;
			base.npc.friendly = false;
			this.animationType = 2;
			base.npc.value = (float)Item.buyPrice(0, 6, 50, 0);
			base.npc.npcSlots = 1f;
			base.npc.boss = true;
			base.npc.lavaImmune = true;
			base.npc.noGravity = true;
			base.npc.noTileCollide = true;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.buffImmune[24] = true;
			base.npc.buffImmune[20] = true;
			base.npc.netAlways = true;
			this.bossBag = base.mod.ItemType("InfectedEyeBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/InfectedEyeGore3"), 1f);
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("InfectedEyeTrophy"), 1, false, 0, false, false);
			}
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("XenomiteStaff"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("TheInfectedEye"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("InfectousJavelin"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("Xenomite"), Main.rand.Next(4, 6), false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("AntiXenomiteApplier"), Main.rand.Next(2, 6), false, 0, false, false);
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = 499;
			if (!RedeWorld.downedInfectedEye)
			{
				for (int i = 0; i < 255; i++)
				{
					Player player2 = Main.player[i];
					if (player2.active)
					{
						for (int j = 0; j < player2.inventory.Length; j++)
						{
							if (player2.inventory[j].type == base.mod.ItemType("RedemptionTeller"))
							{
								Main.NewText("<Chalice of Alignment> Oh... That's only made the Infection worse... Well, it doesn't matter, at least Cthulhu's other eye is no more!", Color.DarkGoldenrod, false);
							}
						}
						CombatText.NewText(player2.getRect(), Color.Gray, "+0", true, false);
					}
				}
			}
			RedeWorld.downedInfectedEye = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			base.npc.lifeMax = (int)((float)base.npc.lifeMax * 0.579f * bossLifeScale);
			base.npc.damage = (int)((float)base.npc.damage * 0.6f);
		}

		public override void AI()
		{
			if (RedeConfigClient.Instance.classicRedeIE)
			{
				this.oldCounter++;
				if (this.oldCounter > 20)
				{
					this.oldFrame++;
					this.oldCounter = 0;
				}
				if (this.oldFrame >= 3)
				{
					this.oldFrame = 0;
				}
			}
			if (Main.dayTime)
			{
				NPC npc = base.npc;
				npc.position.Y = npc.position.Y - 300f;
			}
			this.Target();
			this.DespawnHandler();
			base.npc.ai[0] += 1f;
			Player player = Main.player[base.npc.target];
			if (base.npc.target < 0 || base.npc.target == 255 || Main.player[base.npc.target].dead || !Main.player[base.npc.target].active)
			{
				base.npc.TargetClosest(true);
			}
			base.npc.netUpdate = true;
			base.npc.ai[3] += 1f;
			if (base.npc.ai[3] == 100f)
			{
				float Speed = 12f;
				Vector2 vector8 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage = 36;
				int type = base.mod.ProjectileType("ToxicSludge2");
				float rotation = (float)Math.Atan2((double)(vector8.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector8.X - (this.player.position.X + (float)this.player.width * 0.5f)));
				int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), type, damage, 0f, 0, 0f, 0f);
				Main.projectile[num54].netUpdate = true;
				int num55 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), type, damage, 0f, 0, 0f, 0f);
				Main.projectile[num55].netUpdate = true;
				int num56 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), type, damage, 0f, 0, 0f, 0f);
				Main.projectile[num56].netUpdate = true;
				if (Main.expertMode)
				{
					int num57 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), type, damage, 0f, 0, 0f, 0f);
					Main.projectile[num57].netUpdate = true;
					int num58 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0) + (float)Main.rand.Next(-2, 2), type, damage, 0f, 0, 0f, 0f);
					Main.projectile[num58].netUpdate = true;
				}
			}
			if (base.npc.ai[3] == 160f || base.npc.ai[3] == 220f)
			{
				float Speed2 = 18f;
				Vector2 vector9 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage2 = 34;
				int type2 = base.mod.ProjectileType("XenomiteShot2");
				float rotation2 = (float)Math.Atan2((double)(vector9.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector9.X - (this.player.position.X + (float)this.player.width * 0.5f)));
				int num59 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-2, 2), type2, damage2, 0f, 0, 0f, 0f);
				int num60 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-2, 2), type2, damage2, 0f, 0, 0f, 0f);
				int num61 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-2, 2), type2, damage2, 0f, 0, 0f, 0f);
				int num62 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-2, 2), type2, damage2, 0f, 0, 0f, 0f);
				int num63 = Projectile.NewProjectile(vector9.X, vector9.Y, (float)(Math.Cos((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation2) * (double)Speed2 * -1.0) + (float)Main.rand.Next(-2, 2), type2, damage2, 0f, 0, 0f, 0f);
				Main.projectile[num59].netUpdate = true;
				Main.projectile[num60].netUpdate = true;
				Main.projectile[num61].netUpdate = true;
				Main.projectile[num62].netUpdate = true;
				Main.projectile[num63].netUpdate = true;
			}
			if (Main.expertMode)
			{
				if (base.npc.ai[3] == 300f || base.npc.ai[3] == 340f || base.npc.ai[3] == 380f || base.npc.ai[3] == 420f || base.npc.ai[3] == 460f)
				{
					Vector2 vector10 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
					float rotation3 = (float)Math.Atan2((double)(vector10.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector10.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
					base.npc.velocity.X = (float)(Math.Cos((double)rotation3) * 12.0) * -1f;
					base.npc.velocity.Y = (float)(Math.Sin((double)rotation3) * 12.0) * -1f;
					base.npc.ai[0] %= 6.2831855f;
					new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
					Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
					Color color = default(Color);
					Rectangle rectangle = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
					int count = 30;
					for (int i = 1; i <= count; i++)
					{
						int dust = Dust.NewDust(base.npc.position, rectangle.Width, rectangle.Height, 107, 0f, 0f, 100, color, 2.5f);
						Main.dust[dust].noGravity = false;
					}
					return;
				}
			}
			else if (base.npc.ai[3] == 300f || base.npc.ai[3] == 360f || base.npc.ai[3] == 440f)
			{
				Vector2 vector11 = new Vector2(base.npc.position.X + (float)base.npc.width * 0.5f, base.npc.position.Y + (float)base.npc.height * 0.5f);
				float rotation4 = (float)Math.Atan2((double)(vector11.Y - (Main.player[base.npc.target].position.Y + (float)Main.player[base.npc.target].height * 0.5f)), (double)(vector11.X - (Main.player[base.npc.target].position.X + (float)Main.player[base.npc.target].width * 0.5f)));
				base.npc.velocity.X = (float)(Math.Cos((double)rotation4) * 12.0) * -1f;
				base.npc.velocity.Y = (float)(Math.Sin((double)rotation4) * 12.0) * -1f;
				base.npc.ai[0] %= 6.2831855f;
				new Vector2((float)Math.Cos((double)base.npc.ai[0]), (float)Math.Sin((double)base.npc.ai[0]));
				Main.PlaySound(2, (int)base.npc.position.X, (int)base.npc.position.Y, 20, 1f, 0f);
				Color color2 = default(Color);
				Rectangle rectangle2 = new Rectangle((int)base.npc.position.X, (int)(base.npc.position.Y + (float)((base.npc.height - base.npc.width) / 2)), base.npc.width, base.npc.width);
				int count2 = 30;
				for (int j = 1; j <= count2; j++)
				{
					int dust2 = Dust.NewDust(base.npc.position, rectangle2.Width, rectangle2.Height, 107, 0f, 0f, 100, color2, 2.5f);
					Main.dust[dust2].noGravity = false;
				}
				return;
			}
			if (base.npc.ai[3] == 500f || base.npc.ai[3] == 550f || base.npc.ai[3] == 600f || base.npc.ai[3] == 650f || base.npc.ai[3] == 700f)
			{
				float Speed3 = 15f;
				Vector2 vector12 = new Vector2(base.npc.position.X + (float)(base.npc.width / 2), base.npc.position.Y + (float)(base.npc.height / 2));
				int damage3 = 36;
				int type3 = base.mod.ProjectileType("InfectedSpray");
				float rotation5 = (float)Math.Atan2((double)(vector12.Y - (this.player.position.Y + (float)this.player.height * 0.5f)), (double)(vector12.X - (this.player.position.X + (float)this.player.width * 0.5f)));
				int num64 = Projectile.NewProjectile(vector12.X, vector12.Y, (float)(Math.Cos((double)rotation5) * (double)Speed3 * -1.0) + (float)Main.rand.Next(-2, 2), (float)(Math.Sin((double)rotation5) * (double)Speed3 * -1.0) + (float)Main.rand.Next(-2, 2), type3, damage3, 0f, 0, 0f, 0f);
				Main.projectile[num64].netUpdate = true;
			}
			if (base.npc.life < (int)((float)base.npc.lifeMax * 0.5f))
			{
				if (Main.expertMode)
				{
					if (base.npc.ai[3] >= 760f && base.npc.ai[3] < 840f && Main.rand.Next(10) == 0)
					{
						int projID = Projectile.NewProjectile(base.npc.Center.X, base.npc.Center.Y, (float)Main.rand.Next(-12, 12), (float)Main.rand.Next(-12, 12), base.mod.ProjectileType("XenomiteShot2"), 35, 1f, 255, 0f, 0f);
						Main.projectile[projID].netUpdate = true;
					}
					if (base.npc.ai[3] >= 900f)
					{
						base.npc.ai[3] = 0f;
					}
					if (Main.rand.Next(30) == 0)
					{
						int projID2 = Projectile.NewProjectile(base.npc.Center.X + (float)Main.rand.Next(-80, 80), base.npc.Center.Y + (float)Main.rand.Next(-60, 60), 0f, 0f, base.mod.ProjectileType("DribblingOoze"), 30, 1f, 255, 0f, 0f);
						Main.projectile[projID2].netUpdate = true;
					}
				}
				else if (base.npc.ai[3] >= 700f)
				{
					base.npc.ai[3] = 0f;
				}
			}
			else if (base.npc.ai[3] >= 700f)
			{
				base.npc.ai[3] = 0f;
			}
			if (base.npc.ai[0] % 200f == 3f && NPC.CountNPCS(base.mod.NPCType("XenomiteEye")) <= 3)
			{
				int Minion = NPC.NewNPC((int)base.npc.position.X + 80, (int)base.npc.position.Y + 80, base.mod.NPCType("XenomiteEye"), 0, 0f, 0f, 0f, 0f, 255);
				Main.npc[Minion].netUpdate = true;
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
					return;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D oldAni = base.mod.GetTexture("NPCs/Bosses/InfectedEye/InfectedEye_OLD");
			Texture2D glowMask = base.mod.GetTexture("NPCs/Bosses/InfectedEye/InfectedEye_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			if (RedeConfigClient.Instance.classicRedeIE)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = oldAni.Height / 3;
				int y6 = num214 * this.oldFrame;
				Main.spriteBatch.Draw(oldAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, oldAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)oldAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			else
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
				spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			}
			return false;
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(base.mod.BuffType("XenomiteDebuff"), Main.rand.Next(500, 1000), true);
			}
			if (Main.rand.Next(9) == 0 || (Main.expertMode && Main.rand.Next(7) == 0))
			{
				target.AddBuff(base.mod.BuffType("XenomiteDebuff2"), Main.rand.Next(250, 500), true);
			}
		}

		private Player player;

		private int oldFrame;

		private int oldCounter;
	}
}
