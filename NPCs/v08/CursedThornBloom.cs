using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs.v08
{
	public class CursedThornBloom : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Cursed Bloom");
			Main.npcFrameCount[base.npc.type] = 2;
		}

		public override void SetDefaults()
		{
			base.npc.width = 38;
			base.npc.height = 60;
			base.npc.defense = 30;
			base.npc.damage = 75;
			base.npc.lifeMax = 6600;
			base.npc.HitSound = SoundID.NPCHit1;
			base.npc.DeathSound = SoundID.NPCDeath1;
			base.npc.knockBackResist = 0.1f;
			base.npc.aiStyle = 1;
			this.aiType = 1;
			this.animationType = 302;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (base.npc.life <= 0)
			{
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/CursedBloomGore1"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/CursedBloomGore2"), 1f);
				Gore.NewGore(base.npc.position, base.npc.velocity, base.mod.GetGoreSlot("Gores/v08/CursedBloomGore3"), 1f);
			}
		}

		public override void NPCLoot()
		{
			if (RedeWorld.downedThornPZ)
			{
				Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CursedThorns"), Main.rand.Next(1, 3), false, 0, false, false);
				return;
			}
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("CursedThornsF"), Main.rand.Next(1, 3), false, 0, false, false);
		}

		public override void AI()
		{
			base.npc.TargetClosest(true);
			Player player = Main.player[base.npc.target];
			if (Main.rand.Next(100) == 0)
			{
				float num = 20f;
				Vector2 vector;
				vector..ctor(base.npc.Center.X, base.npc.Center.Y);
				int num2 = 45;
				int num3 = base.mod.ProjectileType("CursedThornPro1");
				float num4 = (float)Math.Atan2((double)(vector.Y - (player.position.Y + (float)player.height * 0.5f)), (double)(vector.X - (player.position.X + (float)player.width * 0.5f)));
				int num5 = Projectile.NewProjectile(vector.X, vector.Y, (float)(Math.Cos((double)num4) * (double)num * -1.0), (float)(Math.Sin((double)num4) * (double)num * -1.0), num3, num2, 0f, 0, 0f, 0f);
				Main.projectile[num5].netUpdate = true;
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.OverworldDaySlime.Chance * ((Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type == 2 && RedeWorld.downedPatientZero) ? 0.1f : 0f);
		}
	}
}
