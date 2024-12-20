using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class AJollyMadman : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("A Jolly Madman");
			Main.npcFrameCount[base.npc.type] = 3;
		}

		public override void SetDefaults()
		{
			base.npc.width = 40;
			base.npc.height = 58;
			base.npc.friendly = false;
			base.npc.damage = 40;
			base.npc.defense = 25;
			base.npc.lifeMax = 250;
			base.npc.HitSound = SoundID.NPCHit2;
			base.npc.DeathSound = SoundID.NPCDeath2;
			base.npc.value = 100f;
			base.npc.knockBackResist = 0.05f;
			base.npc.aiStyle = 3;
			this.aiType = 3;
			this.animationType = 3;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("AJollyMadmanBanner");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/AJollyMadmanGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/AJollyMadmanGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/AJollyMadmanGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/AJollyMadmanGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/AJollyMadmanGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/AJollyMadmanGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/AJollyMadmanGore5"), 1f);
			}
			if (RedeWorld.downedPatientZero)
			{
				if (Main.netMode != 1 && base.npc.life <= 0)
				{
					NPC.NewNPC((int)base.npc.position.X + 22, (int)base.npc.position.Y + 55, base.mod.NPCType("DarkSoul"), 0, 0f, 0f, 0f, 0f, 255);
					NPC.NewNPC((int)base.npc.Center.X + 12, (int)base.npc.Center.Y + 8, base.mod.NPCType("ShadesoulNPC"), 0, 0f, 0f, 0f, 0f, 255);
					NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y + 10, base.mod.NPCType("SmallShadesoulNPC"), 0, 0f, 0f, 0f, 0f, 255);
					NPC.NewNPC((int)base.npc.Center.X - 8, (int)base.npc.Center.Y - 4, base.mod.NPCType("SmallShadesoulNPC"), 0, 0f, 0f, 0f, 0f, 255);
					return;
				}
			}
			else if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 22, (int)base.npc.position.Y + 55, base.mod.NPCType("DarkSoul"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.Center.X + 12, (int)base.npc.Center.Y + 8, base.mod.NPCType("LostSoul2"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y + 10, base.mod.NPCType("LostSoul1"), 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)base.npc.Center.X - 8, (int)base.npc.Center.Y - 4, base.mod.NPCType("LostSoul1"), 0, 0f, 0f, 0f, 0f, 255);
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Cavern.Chance * 0.002f;
		}

		public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
		{
			if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
			{
				target.AddBuff(30, 1000, true);
			}
		}

		public override void AI()
		{
			if (this.specialAttack)
			{
				this.attackCounter++;
				if (this.attackCounter > 10)
				{
					this.attackFrame++;
					this.attackCounter = 0;
				}
				if (this.attackFrame >= 3)
				{
					this.attackFrame = 0;
				}
			}
			if (Main.rand.Next(100) == 0 && !this.specialAttack)
			{
				this.specialAttack = true;
			}
			if (this.specialAttack)
			{
				this.attackTimer++;
				if (this.attackTimer == 5 && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.DarkGray, "Hollow Slash!", true, true);
				}
				if (this.attackTimer == 5)
				{
					int minion = NPC.NewNPC((int)base.npc.position.X + 22, (int)base.npc.position.Y + 55, base.mod.NPCType("DarkSoul3"), 0, 0f, 0f, 0f, 0f, 255);
					Main.npc[minion].netUpdate = true;
					Main.PlaySound(SoundID.Item71, (int)base.npc.position.X, (int)base.npc.position.Y);
				}
				if (this.attackTimer >= 30)
				{
					this.specialAttack = false;
					this.attackTimer = 0;
					this.attackCounter = 0;
					this.attackFrame = 0;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D attackAni = base.mod.GetTexture("NPCs/AJollyMadmanAttack");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.specialAttack)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.specialAttack)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = attackAni.Height / 3;
				int y6 = num214 * this.attackFrame;
				Main.spriteBatch.Draw(attackAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, attackAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)attackAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		private bool specialAttack;

		private int attackFrame;

		private int attackCounter;

		private int attackTimer;
	}
}
