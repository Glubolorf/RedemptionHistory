﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Banners;
using Redemption.NPCs.Friendly;
using Redemption.Projectiles.Misc;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.PreHM
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
			this.bannerItem = ModContent.ItemType<SkeletonWandererBanner>();
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.hardMode)
			{
				return SpawnCondition.Cavern.Chance * 0.04f;
			}
			return SpawnCondition.Cavern.Chance * 0.1f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 5; i++)
				{
					Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/SkeleGoreBone"), 1f);
					Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/SkeleGoreBone"), 1f);
					Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/SkeleGoreBone"), 1f);
					Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/SkeleGoreBone"), 1f);
					Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/SkeleGoreBone"), 1f);
				}
			}
			Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/SkeleGoreBone"), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			if (Main.netMode != 1 && base.npc.life <= 0 && Utils.NextBool(Main.rand, 2))
			{
				NPC.NewNPC((int)base.npc.position.X + 30, (int)base.npc.position.Y + 36, ModContent.NPCType<LostSoul1>(), 0, 0f, 0f, 0f, 0f, 255);
			}
		}

		public override void AI()
		{
			if (!this.change)
			{
				int num = Main.rand.Next(3);
				if (num == 0)
				{
					base.npc.SetDefaults(ModContent.NPCType<SkeletonAssassin>(), -1f);
					this.change = true;
				}
				if (num == 1)
				{
					base.npc.SetDefaults(ModContent.NPCType<SkeletonDueller>(), -1f);
					this.change = true;
				}
				if (num >= 2)
				{
					if (Main.rand.Next(4) == 0)
					{
						if (NPC.downedBoss1)
						{
							base.npc.SetDefaults(ModContent.NPCType<SkeletonWanderer2>(), -1f);
							this.change = true;
						}
					}
					else
					{
						this.change = true;
					}
				}
			}
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
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 80f && !Main.LocalPlayer.GetModPlayer<RedePlayer>().skeletonFriendly && Main.rand.Next(20) == 0 && !this.thrustAttack)
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
				if (this.thrustTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.Gray, "Thrust!", true, true);
				}
				if (this.thrustTimer == 9)
				{
					if (base.npc.direction == -1)
					{
						int p = Projectile.NewProjectile(base.npc.position.X + -14f, base.npc.position.Y + 18f, 0f, 0f, ModContent.ProjectileType<DamagePro3>(), 5, 3f, 255, 0f, 0f);
						Main.projectile[p].netUpdate = true;
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					else
					{
						int p2 = Projectile.NewProjectile(base.npc.position.X + 48f, base.npc.position.Y + 18f, 0f, 0f, ModContent.ProjectileType<DamagePro3>(), 5, 3f, 255, 0f, 0f);
						Main.projectile[p2].netUpdate = true;
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

		public override void NPCLoot()
		{
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D thrustAni = base.mod.GetTexture("NPCs/PreHM/SkeletonWandererThrust");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.thrustAttack)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.thrustAttack)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = thrustAni.Height / 6;
				int y6 = num214 * this.thrustFrame;
				Main.spriteBatch.Draw(thrustAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, thrustAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)thrustAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return !Main.LocalPlayer.GetModPlayer<RedePlayer>().skeletonFriendly;
		}

		private bool thrustAttack;

		private int thrustFrame;

		private int thrustCounter;

		private int thrustTimer;

		private bool change;
	}
}
