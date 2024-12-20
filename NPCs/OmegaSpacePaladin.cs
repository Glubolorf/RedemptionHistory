using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class OmegaSpacePaladin : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Omega Space Paladin");
			Main.npcFrameCount[base.npc.type] = 13;
		}

		public override void SetDefaults()
		{
			base.npc.width = 50;
			base.npc.height = 118;
			base.npc.friendly = false;
			base.npc.damage = 150;
			base.npc.defense = 75;
			base.npc.lifeMax = 20000;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.value = (float)Item.buyPrice(0, 15, 0, 0);
			base.npc.knockBackResist = 0.1f;
			base.npc.aiStyle = 3;
			this.aiType = 425;
			this.animationType = 425;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<OmegaSpacePaladinBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/OSpacePaladinGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/OSpacePaladinGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/OSpacePaladinGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/OSpacePaladinGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/OSpacePaladinGore3"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 235, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			Player player = Main.player[base.npc.target];
			if (base.npc.Distance(Main.player[base.npc.target].Center) <= 400f && Main.rand.Next(150) == 0 && !this.minigunAttack)
			{
				this.minigunAttack = true;
			}
			if (this.minigunAttack)
			{
				this.attackTimer++;
				if (this.attackTimer > 5 && this.attackTimer < 65)
				{
					this.spamTimer++;
					if (this.spamTimer == 2)
					{
						if (base.npc.direction == -1)
						{
							int p = Projectile.NewProjectile(new Vector2(base.npc.position.X + 26f, base.npc.position.Y + 54f), new Vector2(-6f, Utils.NextFloat(Main.rand, -1f, 1f)), 302, 40, 3f, 255, 0f, 0f);
							Main.projectile[p].netUpdate = true;
						}
						else
						{
							int p2 = Projectile.NewProjectile(new Vector2(base.npc.position.X + 62f, base.npc.position.Y + 54f), new Vector2(6f, Utils.NextFloat(Main.rand, -1f, 1f)), 302, 40, 3f, 255, 0f, 0f);
							Main.projectile[p2].netUpdate = true;
						}
					}
					if (this.spamTimer >= 3)
					{
						this.spamTimer = 0;
					}
				}
				if (this.attackTimer >= 65)
				{
					this.minigunAttack = false;
					this.attackTimer = 0;
				}
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Texture2D texture = Main.npcTexture[base.npc.type];
			Texture2D glowMask = base.mod.GetTexture("NPCs/OmegaSpacePaladin_Glow");
			SpriteEffects effects = (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(texture, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), drawColor, base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, (base.npc.spriteDirection == -1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
			spriteBatch.Draw(glowMask, base.npc.Center - Main.screenPosition, new Rectangle?(base.npc.frame), base.npc.GetAlpha(Color.White), base.npc.rotation, Utils.Size(base.npc.frame) / 2f, base.npc.scale, effects, 0f);
			return false;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * ((!RedeWorld.girusCloaked && Main.hardMode && NPC.downedPlantBoss && RedeWorld.downedVlitch3) ? 0.004f : 0f);
		}

		private bool minigunAttack;

		private int attackTimer;

		private int spamTimer;
	}
}
