using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Bosses
{
	[AutoloadBossHead]
	public class KingChicken : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("The Mighty King Chicken");
			Main.npcFrameCount[base.npc.type] = 7;
		}

		public override void SetDefaults()
		{
			base.npc.width = 28;
			base.npc.height = 24;
			base.npc.defense = 0;
			base.npc.lifeMax = 5;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0.5f;
			base.npc.aiStyle = 7;
			this.aiType = 46;
			this.animationType = 46;
			base.npc.boss = true;
			this.bossBag = base.mod.ItemType("KingChickenBag");
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ChickenGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/KingChickenGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/KingChickenGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ChickenGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/ChickenGore3"), 1f);
			}
		}

		public override void AI()
		{
			if (this.peckPeck)
			{
				this.peckCounter++;
				if (this.peckCounter > 5)
				{
					this.peckFrame++;
					this.peckCounter = 0;
				}
				if (this.peckFrame >= 6)
				{
					this.peckFrame = 0;
				}
			}
			if (Main.rand.Next(500) == 0 && !this.peckPeck)
			{
				this.peckPeck = true;
			}
			if (this.peckPeck)
			{
				base.npc.velocity.X = 0f;
				this.peckTimer++;
				if (this.peckTimer >= 30)
				{
					this.peckPeck = false;
					this.peckCounter = 0;
					this.peckFrame = 0;
					this.peckTimer = 0;
				}
			}
			if (Main.rand.Next(500) == 0 && !this.cluckCluck)
			{
				this.cluckCluck = true;
			}
			if (this.cluckCluck)
			{
				this.cluckTimer++;
				if (this.cluckTimer == 1)
				{
					switch (Main.rand.Next(3))
					{
					case 0:
						if (!Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/ChickenCluck1").WithVolume(0.5f).WithPitchVariance(0.1f), base.npc.position);
						}
						break;
					case 1:
						if (!Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/ChickenCluck2").WithVolume(0.5f).WithPitchVariance(0.1f), base.npc.position);
						}
						break;
					case 2:
						if (!Main.dedServ)
						{
							Main.PlaySound(base.mod.GetLegacySoundSlot(50, "Sounds/Custom/ChickenCluck3").WithVolume(0.5f).WithPitchVariance(0.1f), base.npc.position);
						}
						break;
					}
				}
				if (this.cluckTimer >= 2)
				{
					this.cluckCluck = false;
					this.cluckTimer = 0;
				}
			}
			this.timer++;
			if (this.timer == 1)
			{
				for (int g = 0; g < 2; g++)
				{
					int goreIndex = Gore.NewGore(new Vector2(base.npc.position.X + (float)(base.npc.width / 2) - 24f, base.npc.position.Y + (float)(base.npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
					goreIndex = Gore.NewGore(new Vector2(base.npc.position.X + (float)(base.npc.width / 2) - 24f, base.npc.position.Y + (float)(base.npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				}
			}
			if (this.timer == 40)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "You fool... You dare think you can kill me?", true, false);
			}
			if (this.timer == 200)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "I am the Mighty King Chicken, and I have come to annihilate you!", true, false);
			}
			if (this.timer == 600)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "So face me, mortal! And accept your fate!", true, false);
			}
			if (this.timer == 900)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "Stubborn fool... Are you letting me make the first move?", true, false);
			}
			if (this.timer == 1400)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "So be it! A GOD DOES NOT FEAR DEATH!", true, false);
				base.npc.damage = 1;
			}
			if (this.timer == 1500)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "*pecks gracefully*", true, true);
				this.peckPeck = true;
			}
			if (this.timer == 2800)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "You still stand before me?", true, false);
			}
			if (this.timer == 4000)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "*clucks impatiently*", true, true);
			}
			if (this.timer == 8000)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "You bore me, mortal! I shall be gone, but I will return!   ...   Maybe.", true, false);
			}
			if (this.timer == 8100)
			{
				CombatText.NewText(base.npc.getRect(), Colors.RarityOrange, "*clucks a goodbye*", true, true);
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 264, 1, false, 0, false, false);
				base.npc.active = false;
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			Player player3 = Main.player[base.npc.target];
			string text = "It's a freakin' chicken, what did you expect...";
			Color rarityGreen = Colors.RarityGreen;
			byte r = rarityGreen.R;
			rarityGreen = Colors.RarityGreen;
			byte g = rarityGreen.G;
			rarityGreen = Colors.RarityGreen;
			Main.NewText(text, r, g, rarityGreen.B, false);
			potionType = 0;
			if (!RedeWorld.downedKingChicken)
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
								Main.NewText("<Chalice of Alignment> ... Hehe.", Color.DarkGoldenrod, false);
							}
						}
						CombatText.NewText(player2.getRect(), Color.Gray, "+0", true, false);
					}
				}
			}
			RedeWorld.downedKingChicken = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KingChickenTrophy"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ChickenEgg"), Main.rand.Next(4, 6), false, 0, false, false);
			if (Main.expertMode)
			{
				base.npc.DropBossBags();
				return;
			}
			if (Main.rand.Next(7) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("KingChickenMask"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CrownOfTheKing"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("EggStaff"), 1, false, 0, false, false);
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("Grain"), 1, false, 0, false, false);
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, 264, 1, false, 0, false, false);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D peckAni = base.mod.GetTexture("NPCs/Bosses/KingChickenPeck");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.peckPeck)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.peckPeck)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = peckAni.Height / 6;
				int y6 = num214 * this.peckFrame;
				Main.spriteBatch.Draw(peckAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, peckAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)peckAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return this.timer >= 1450;
		}

		private bool peckPeck;

		private int peckFrame;

		private int peckCounter;

		private int peckTimer;

		private bool cluckCluck;

		private int cluckTimer;

		private int timer;
	}
}
