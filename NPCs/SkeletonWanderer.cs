using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class SkeletonWanderer : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Skeleton Wanderer");
			Main.npcFrameCount[base.npc.type] = Main.npcFrameCount[271];
		}

		public override void SetDefaults()
		{
			base.npc.width = 36;
			base.npc.height = 54;
			base.npc.damage = 19;
			base.npc.defense = 1;
			base.npc.lifeMax = 35;
			base.npc.HitSound = SoundID.NPCHit2;
			base.npc.DeathSound = SoundID.NPCDeath2;
			base.npc.value = (float)Item.buyPrice(0, 0, 0, 35);
			base.npc.knockBackResist = 0.5f;
			base.npc.aiStyle = 3;
			this.aiType = 271;
			this.animationType = 271;
			this.banner = base.npc.type;
			this.bannerItem = base.mod.ItemType("SkeletonWandererBanner");
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.hardMode)
			{
				return SpawnCondition.Cavern.Chance * 0.05f;
			}
			return SpawnCondition.Cavern.Chance * 0.1f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
			}
			Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/SkeleGoreBone"), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			if (Main.netMode != 1 && base.npc.life <= 0 && Main.rand.Next(2) == 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 30, (int)base.npc.position.Y + 36, base.mod.NPCType("LostSoul1"), 0, 0f, 0f, 0f, 0f, 255);
			}
		}

		public override void AI()
		{
			if (this.thrustAttack)
			{
				this.thrustCounter++;
				if (this.thrustCounter > 3)
				{
					this.thrustFrame++;
					this.thrustCounter = 0;
				}
				if (this.thrustFrame >= 6)
				{
					this.thrustFrame = 0;
				}
			}
			float num = base.npc.Distance(Main.player[base.npc.target].Center);
			if (num <= 80f && Main.rand.Next(20) == 0 && !this.thrustAttack)
			{
				this.thrustAttack = true;
			}
			if (!this.thrustAttack)
			{
				base.npc.aiStyle = 3;
			}
			if (this.thrustAttack)
			{
				this.thrustTimer++;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				if (this.thrustTimer == 9)
				{
					if (base.npc.direction == -1)
					{
						Projectile.NewProjectile(base.npc.position.X + -14f, base.npc.position.Y + 18f, 0f, 0f, base.mod.ProjectileType("DamagePro3"), 5, 3f, 255, 0f, 0f);
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					else
					{
						Projectile.NewProjectile(base.npc.position.X + 48f, base.npc.position.Y + 18f, 0f, 0f, base.mod.ProjectileType("DamagePro3"), 5, 3f, 255, 0f, 0f);
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
				}
				if (this.thrustTimer >= 18)
				{
					this.thrustAttack = false;
					this.thrustTimer = 0;
					this.thrustCounter = 0;
					this.thrustFrame = 0;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture2D = Main.npcTexture[base.npc.type];
			Texture2D texture = base.mod.GetTexture("NPCs/SkeletonWandererThrust");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.thrustAttack)
			{
				spriteBatch.Draw(texture2D, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			if (this.thrustAttack)
			{
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num = texture.Height / 6;
				int num2 = num * this.thrustFrame;
				Main.spriteBatch.Draw(texture, vector - Main.screenPosition, new Rectangle?(new Rectangle(0, num2, texture.Width, num)), drawColor, base.npc.rotation, new Vector2((float)texture.Width / 2f, (float)num / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? 0 : 1, 0f);
			}
			return false;
		}

		private bool thrustAttack;

		private int thrustFrame;

		private int thrustCounter;

		private int thrustTimer;
	}
}
