using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.ChickenArmy;
using Redemption.Items.Usable.Potions;
using Redemption.Items.Weapons.PostML.Melee;
using Redemption.Items.Weapons.PostML.Ranged;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.ChickenInv
{
	public class GreatChickenWarrior : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Greater Chickman");
			Main.npcFrameCount[base.npc.type] = 10;
		}

		public override void SetDefaults()
		{
			base.npc.width = 54;
			base.npc.height = 70;
			base.npc.friendly = false;
			if (RedeWorld.downedPatientZero)
			{
				base.npc.damage = 90;
				base.npc.defense = 33;
				base.npc.lifeMax = 22000;
				base.npc.knockBackResist = 0f;
			}
			else
			{
				base.npc.damage = 30;
				base.npc.defense = 9;
				base.npc.lifeMax = 100;
				base.npc.knockBackResist = 0.1f;
			}
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.value = 1000f;
			base.npc.aiStyle = 3;
			this.aiType = 73;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 20; i++)
				{
					int dustIndex = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 5, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex].velocity *= 1.4f;
				}
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Friendly/ChickenGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/GreaterChickmanGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/GreaterChickmanGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/GreaterChickmanGore3"), 1f);
				if (base.npc.FindBuffIndex(24) != -1)
				{
					Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<FriedChicken>(), Main.rand.Next(2, 5), false, 0, false, false);
				}
			}
		}

		public override void NPCLoot()
		{
			if (RedeWorld.downedPatientZero && Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<GreatChickenShield>(), 1, false, 0, false, false);
			}
			if (RedeWorld.downedPatientZero && Main.rand.Next(1200) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, ModContent.ItemType<ChickLauncher>(), 1, false, 0, false, false);
			}
			if (ChickWorld.chickArmy)
			{
				ChickWorld.ChickPoints += 2;
				if (Main.netMode != 0)
				{
					NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
				}
			}
		}

		public override void AI()
		{
			base.npc.TargetClosest(true);
			if (Main.player[base.npc.target].Center.X > base.npc.Center.X)
			{
				base.npc.spriteDirection = 1;
			}
			else
			{
				base.npc.spriteDirection = -1;
			}
			if (base.npc.velocity.Y == 0f)
			{
				this.hop = false;
				base.npc.frameCounter += 1.0;
				if (base.npc.frameCounter >= 10.0)
				{
					base.npc.frameCounter = 0.0;
					NPC npc = base.npc;
					npc.frame.Y = npc.frame.Y + 74;
					if (base.npc.frame.Y > 666)
					{
						base.npc.frameCounter = 0.0;
						base.npc.frame.Y = 0;
						return;
					}
				}
			}
			else
			{
				this.hop = true;
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D hopAni = base.mod.GetTexture("NPCs/ChickenInv/GreatChickenWarriorHop");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.hop)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.hop)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = hopAni.Height / 1;
				int y6 = 0;
				Main.spriteBatch.Draw(hopAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, hopAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)hopAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		private bool hop;
	}
}
