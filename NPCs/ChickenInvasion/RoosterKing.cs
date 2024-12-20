using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.ChickenArmy;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.ChickenInvasion
{
	[AutoloadBossHead]
	public class RoosterKing : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Rooster King");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 900;
			base.npc.damage = 100;
			base.npc.defense = 0;
			base.npc.knockBackResist = 0f;
			base.npc.value = (float)Item.buyPrice(0, 3, 0, 0);
			base.npc.aiStyle = -1;
			base.npc.width = 44;
			base.npc.height = 34;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.lavaImmune = true;
			base.npc.boss = true;
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("RoosterWings"), 1, false, 0, false, false);
			if (Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("RoyalBattleHorn"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KingRoosterMask"), 1, false, 0, false, false);
			}
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			base.npc.frameCounter += (double)(base.npc.velocity.X * 0.5f);
			if (base.npc.frameCounter >= 5.0 || base.npc.frameCounter <= -5.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 38;
				if (base.npc.frame.Y > 114)
				{
					base.npc.frameCounter = 0.0;
					base.npc.frame.Y = 0;
				}
			}
			if (player.Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			if (!NPC.AnyNPCs(base.mod.NPCType("ChickenSwarmer")))
			{
				this.roarTimer++;
			}
			if (this.roarTimer >= 500 && !this.roar)
			{
				this.roar = true;
			}
			if (!this.roar)
			{
				base.npc.aiStyle = -1;
				BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.06f, 2.5f, 40, 20, 60, true, 10, 60, false, null, false);
			}
			if (this.roar)
			{
				this.roarTimer2++;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				if (this.roarTimer2 == 1)
				{
					if (!Main.dedServ)
					{
						Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/RoosterRoar").WithVolume(0.8f).WithPitchVariance(0.1f), base.npc.position);
					}
					for (int i = 0; i < 8; i++)
					{
						int Minion = NPC.NewNPC((int)player.position.X + Main.rand.Next(-200, 200), (int)player.position.Y + Main.rand.Next(-200, 200) - 1000, base.mod.NPCType("ChickenSwarmer"), 0, 0f, 0f, 0f, 0f, 255);
						Main.npc[Minion].netUpdate = true;
					}
				}
				if (this.roarTimer2 >= 180)
				{
					this.roar = false;
					this.roarTimer2 = 0;
					this.roarTimer = 0;
					this.roarFrame = 0;
				}
			}
			this.talkTimer++;
			if (this.talkTimer == 120)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "You fool! You idiot!", true, false);
			}
			if (this.talkTimer == 280)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "Did you really think that dumb chicken was king?", true, false);
			}
			if (this.talkTimer == 450)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "IT'S A CHICKEN, CHICKENS ARE FEMALE, FOOOOOOOOOOOOL!", true, false);
			}
			if (this.talkTimer == 600)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "I am the true 'King Chicken'! Or 'King Rooster' to be more accurate.", true, false);
			}
			if (this.talkTimer == 880)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "So fight me, and feel the 'Wrath of the Rooster'!", true, false);
			}
			if (this.talkTimer == 2000)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "You sure are taking your time to kill me.", true, false);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D attackAni = base.mod.GetTexture("NPCs/ChickenInvasion/RoosterKingRoar");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.roar)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.roar)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = attackAni.Height / 1;
				int y6 = num214 * this.roarFrame;
				Main.spriteBatch.Draw(attackAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, attackAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)attackAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			damage *= 0.01;
			return true;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 15; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 5, 0f, 0f, 100, default(Color), 1.2f);
					Main.dust[dustIndex].velocity *= 1.8f;
				}
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/RoosterKingGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/RoosterKingGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/RoosterKingGore3"), 1f);
				if (base.npc.FindBuffIndex(24) != -1)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("FriedChicken"), Main.rand.Next(1, 3), false, 0, false, false);
				}
				if (ChickWorld.chickArmy)
				{
					ChickWorld.ChickPoints2 += 200;
				}
			}
			Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 5, 0f, 0f, 100, default(Color), 1.2f);
		}

		private bool roar;

		private int roarFrame;

		private int roarTimer;

		private int talkTimer;

		private int roarTimer2;
	}
}
