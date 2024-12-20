using System;
using Microsoft.Xna.Framework;
using Redemption.Items.Placeable.Banners;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.Varients
{
	public class ShadeForestGolem : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Shadewood Forest Golem");
			Main.npcFrameCount[base.npc.type] = 21;
		}

		public override void SetDefaults()
		{
			base.npc.width = 40;
			base.npc.height = 62;
			base.npc.damage = 16;
			base.npc.friendly = false;
			base.npc.defense = 6;
			base.npc.lifeMax = 60;
			base.npc.HitSound = base.mod.GetLegacySoundSlot(3, "Sounds/NPCHit/WoodHit");
			base.npc.DeathSound = SoundID.NPCDeath3;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0.4f;
			base.npc.aiStyle = 3;
			this.aiType = 482;
			this.animationType = 482;
			this.banner = base.npc.type;
			this.bannerItem = ModContent.ItemType<ForestGolemBanner>();
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				for (int i = 0; i < 10; i++)
				{
					int dustIndex2 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 77, 0f, 0f, 100, default(Color), 1.5f);
					Main.dust[dustIndex2].velocity *= 1.4f;
				}
			}
			int dustIndex3 = Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 77, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[dustIndex3].velocity *= 1.4f;
		}

		public override void AI()
		{
			if (Main.raining)
			{
				this.regenTimer++;
				if (this.regenTimer >= 40 && base.npc.life < base.npc.lifeMax)
				{
					base.npc.life++;
					base.npc.HealEffect(1, true);
					this.regenTimer = 0;
				}
			}
			if (base.npc.wet && !base.npc.lavaWet)
			{
				this.regenTimer++;
				if (this.regenTimer >= 30 && base.npc.life < base.npc.lifeMax)
				{
					base.npc.life++;
					base.npc.HealEffect(1, true);
					this.regenTimer = 0;
				}
			}
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return !Main.LocalPlayer.GetModPlayer<RedePlayer>().forestFriendly;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (Main.raining)
			{
				return SpawnCondition.Crimson.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == 199 && !RedeWorld.downedPatientZero) ? 0.06f : 0f);
			}
			return SpawnCondition.Crimson.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == 199 && !RedeWorld.downedPatientZero) ? 0.02f : 0f);
		}

		private int regenTimer;
	}
}
