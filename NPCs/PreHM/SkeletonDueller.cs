using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Banners;
using Redemption.Items.Quest;
using Redemption.Items.Quest.Zephos;
using Redemption.NPCs.Friendly;
using Redemption.Projectiles.Misc;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.PreHM
{
	public class SkeletonDueller : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Skeleton Duelist");
			Main.npcFrameCount[base.npc.type] = Main.npcFrameCount[271];
		}

		public override void SetDefaults()
		{
			base.npc.width = 44;
			base.npc.height = 60;
			base.npc.damage = 22;
			base.npc.defense = 2;
			base.npc.lifeMax = 75;
			base.npc.HitSound = SoundID.NPCHit2;
			base.npc.DeathSound = SoundID.NPCDeath2;
			base.npc.value = (float)Item.buyPrice(0, 0, 1, 80);
			base.npc.knockBackResist = 0.5f;
			base.npc.aiStyle = 3;
			this.aiType = 271;
			this.animationType = 271;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<SkeletonDuellerBanner>();
		}

		public override void AI()
		{
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			if (!this.change)
			{
				int VepdorGear = player.FindItem(ModContent.ItemType<VepdorHat>());
				int changeChoice = Main.rand.Next(500);
				if ((RedeQuests.zephosQuests == 3) ? (changeChoice >= 300 && VepdorGear < 0) : (changeChoice == 0))
				{
					if (RedeWorld.downedKeeper && !NPC.AnyNPCs(ModContent.NPCType<Vepdor>()))
					{
						base.npc.SetDefaults(ModContent.NPCType<Vepdor>(), -1f);
						this.change = true;
					}
					else
					{
						this.change = true;
					}
				}
				if (changeChoice >= 1)
				{
					this.change = true;
				}
			}
			if (this.specialAttack)
			{
				this.attackCounter++;
				if (this.attackCounter > 3)
				{
					this.attackFrame++;
					this.attackCounter = 0;
				}
				if (this.attackFrame >= 11)
				{
					this.attackFrame = 0;
				}
			}
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 80f && !Main.LocalPlayer.GetModPlayer<RedePlayer>().skeletonFriendly && Main.rand.Next(20) == 0 && !this.specialAttack)
			{
				this.specialAttack = true;
			}
			if (!this.specialAttack)
			{
				base.npc.aiStyle = 3;
			}
			if (this.specialAttack)
			{
				this.attackTimer++;
				base.npc.aiStyle = 0;
				base.npc.velocity.X = 0f;
				if (this.attackTimer == 1 && !RedeConfigClient.Instance.NoCombatText)
				{
					CombatText.NewText(base.npc.getRect(), Color.Gray, "Slice! Stab!", true, true);
				}
				if (this.attackTimer == 6)
				{
					if (base.npc.direction == -1)
					{
						int p = Projectile.NewProjectile(base.npc.position.X + -14f, base.npc.position.Y + 4f, 0f, 0f, ModContent.ProjectileType<DamagePro2>(), 5, 3f, 255, 0f, 0f);
						Main.projectile[p].netUpdate = true;
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					else
					{
						int p2 = Projectile.NewProjectile(base.npc.position.X + 48f, base.npc.position.Y + 4f, 0f, 0f, ModContent.ProjectileType<DamagePro2>(), 5, 3f, 255, 0f, 0f);
						Main.projectile[p2].netUpdate = true;
						Main.PlaySound(SoundID.Item1, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
				}
				if (this.attackTimer == 24)
				{
					if (base.npc.direction == -1)
					{
						int p3 = Projectile.NewProjectile(base.npc.position.X + -14f, base.npc.position.Y + 28f, 0f, 0f, ModContent.ProjectileType<DamagePro>(), 5, 3f, 255, 0f, 0f);
						Main.projectile[p3].netUpdate = true;
						Main.PlaySound(SoundID.Item19, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
					else
					{
						int p4 = Projectile.NewProjectile(base.npc.position.X + 48f, base.npc.position.Y + 28f, 0f, 0f, ModContent.ProjectileType<DamagePro>(), 5, 3f, 255, 0f, 0f);
						Main.projectile[p4].netUpdate = true;
						Main.PlaySound(SoundID.Item19, (int)base.npc.position.X, (int)base.npc.position.Y);
					}
				}
				if (this.attackTimer >= 42)
				{
					this.specialAttack = false;
					this.attackTimer = 0;
					this.attackCounter = 0;
					this.attackFrame = 0;
				}
			}
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/SkeleGoreBone"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/SkeleGoreBone"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/SkeleGoreBone"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/SkeleGoreBone"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/SkeleGoreBone"), 1f);
			}
			Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/SkeleGoreBone"), 1f);
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 1, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			if (Main.netMode != 1 && base.npc.life <= 0 && Main.rand.Next(2) == 0)
			{
				NPC.NewNPC((int)base.npc.position.X + 30, (int)base.npc.position.Y + 34, ModContent.NPCType<LostSoul1>(), 0, 0f, 0f, 0f, 0f, 255);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D attackAni = base.mod.GetTexture("NPCs/PreHM/SkeletonDuellerSlashStab");
			int spriteDirection = base.npc.spriteDirection;
			if (!this.specialAttack)
			{
				spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			if (this.specialAttack)
			{
				Vector2 drawCenter = new Vector2(base.npc.Center.X, base.npc.Center.Y);
				int num214 = attackAni.Height / 11;
				int y6 = num214 * this.attackFrame;
				Main.spriteBatch.Draw(attackAni, drawCenter - Main.screenPosition, new Rectangle?(new Rectangle(0, y6, attackAni.Width, num214)), drawColor, base.npc.rotation, new Vector2((float)attackAni.Width / 2f, (float)num214 / 2f), base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			}
			return false;
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return !Main.LocalPlayer.GetModPlayer<RedePlayer>().skeletonFriendly;
		}

		private bool specialAttack;

		private int attackFrame;

		private int attackCounter;

		private int attackTimer;

		private bool change;
	}
}
