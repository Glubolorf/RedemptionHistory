using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.ChickenArmy;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.ChickenInvasion
{
	public class ChickenBallista : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Chicken Ballista");
			Main.npcFrameCount[base.npc.type] = 4;
		}

		public override void SetDefaults()
		{
			base.npc.lifeMax = 30000;
			base.npc.damage = 90;
			base.npc.defense = 27;
			base.npc.knockBackResist = 0f;
			base.npc.value = (float)Item.buyPrice(0, 2, 25, 0);
			base.npc.aiStyle = -1;
			base.npc.width = 122;
			base.npc.height = 70;
			base.npc.HitSound = base.mod.GetLegacySoundSlot(3, "Sounds/NPCHit/WoodHit");
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.lavaImmune = true;
		}

		public override void NPCLoot()
		{
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("HandheldBastilla"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(1200) == 0)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("ChickLauncher"), 1, false, 0, false, false);
			}
			if (ChickWorld.chickArmy)
			{
				ChickWorld.ChickPoints += 5;
			}
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			if (this.specialAttack)
			{
				this.attackCounter++;
				if (this.attackCounter > 5)
				{
					this.attackFrame++;
					this.attackCounter = 0;
				}
				if (this.attackFrame >= 10)
				{
					this.attackFrame = 0;
				}
			}
			base.npc.frameCounter += 1.0;
			if (base.npc.frameCounter >= 15.0)
			{
				base.npc.frameCounter = 0.0;
				NPC npc = base.npc;
				npc.frame.Y = npc.frame.Y + 72;
				if (base.npc.frame.Y > 216)
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
			this.shootTimer++;
			if (this.shootTimer >= 150 && !this.specialAttack)
			{
				this.specialAttack = true;
				this.shootTimer = -50;
			}
			if (!this.specialAttack)
			{
				base.npc.aiStyle = -1;
				BaseAI.AIZombie(base.npc, ref base.npc.ai, false, false, -1, 0.06f, 1.2f, 1, 1, 60, true, 10, 60, false, null, false);
			}
			if (this.specialAttack)
			{
				this.attackTimer++;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				if (this.attackTimer == 30)
				{
					float Speed = 14f;
					Vector2 vector8 = new Vector2(base.npc.Center.X, base.npc.Center.Y);
					int damage = 55;
					int type = base.mod.ProjectileType("BallistaArrow");
					float rotation = (float)Math.Atan2((double)(vector8.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector8.X - (player.position.X + (float)player.width * 0.5f)));
					int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)(Math.Cos((double)rotation) * (double)Speed * -1.0), (float)(Math.Sin((double)rotation) * (double)Speed * -1.0), type, damage, 0f, 0, 0f, 0f);
					Main.projectile[num54].netUpdate = true;
				}
				if (this.attackTimer >= 50)
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
			Texture2D attackAni = base.mod.GetTexture("NPCs/ChickenInvasion/ChickenBallistaFire");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.specialAttack)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.specialAttack)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = attackAni.Height / 10;
				int y6 = num214 * this.attackFrame;
				Main.spriteBatch.Draw(attackAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, attackAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)attackAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore4"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore5"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore6"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore7"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore10"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/TrojanChickenGore10"), 1f);
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
					goreIndex = Gore.NewGore(new Vector2(base.npc.position.X + (float)(base.npc.width / 2) - 24f, base.npc.position.Y + (float)(base.npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
					goreIndex = Gore.NewGore(new Vector2(base.npc.position.X + (float)(base.npc.width / 2) - 24f, base.npc.position.Y + (float)(base.npc.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
					Main.gore[goreIndex].scale = 1.5f;
					Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
					Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				}
			}
			if (Main.netMode != 1 && base.npc.life <= 0)
			{
				NPC.NewNPC((int)base.npc.Center.X, (int)base.npc.Center.Y, base.mod.NPCType("Chicken"), 0, 0f, 0f, 0f, 0f, 255);
			}
		}

		private bool specialAttack;

		private int attackFrame;

		private int attackTimer;

		private int attackCounter;

		private int shootTimer;
	}
}
