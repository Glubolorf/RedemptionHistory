using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.HM
{
	public class Android : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Android");
			Main.npcFrameCount[base.npc.type] = 15;
		}

		public override void SetDefaults()
		{
			base.npc.width = 40;
			base.npc.height = 56;
			base.npc.damage = 40;
			base.npc.defense = 40;
			base.npc.lifeMax = 200;
			base.npc.HitSound = SoundID.NPCHit4;
			base.npc.DeathSound = SoundID.NPCDeath14;
			base.npc.value = (float)Item.buyPrice(0, 0, 5, 0);
			base.npc.knockBackResist = 0.1f;
			base.npc.aiStyle = 3;
			this.aiType = 140;
			this.animationType = 140;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<AndroidBanner>();
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldNightMonster.Chance * ((Main.hardMode && !RedeWorld.downedVlitch3) ? 0.04f : 0f);
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/AndroidGore3"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/AndroidGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/Hostile/AndroidGore1"), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
				Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
			}
			Dust.NewDust(base.npc.position + base.npc.velocity, base.npc.width, base.npc.height, 226, base.npc.velocity.X * 0.5f, base.npc.velocity.Y * 0.5f, 0, default(Color), 1f);
		}

		public override void AI()
		{
			if (!this.change)
			{
				int num = Main.rand.Next(100);
				if (num == 0)
				{
					base.npc.SetDefaults(ModContent.NPCType<Apidroid>(), -1f);
					this.change = true;
				}
				if (num >= 1)
				{
					this.change = true;
				}
			}
		}

		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (base.npc.spriteDirection == 1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			spriteBatch.Draw(base.mod.GetTexture("NPCs/HM/Android_Glow"), new Vector2(base.npc.Center.X - Main.screenPosition.X, base.npc.Center.Y - Main.screenPosition.Y), new Rectangle?(base.npc.frame), Color.White, base.npc.rotation, new Vector2((float)base.npc.width * 0.5f, (float)base.npc.height * 0.5f), 1f, spriteEffects, 0f);
		}

		private bool change;
	}
}
