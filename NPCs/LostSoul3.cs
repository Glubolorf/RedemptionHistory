using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.NPCs
{
	public class LostSoul3 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Lost Soul");
		}

		public override void SetDefaults()
		{
			base.npc.width = 24;
			base.npc.height = 28;
			base.npc.damage = 0;
			base.npc.defense = 0;
			base.npc.lifeMax = 1;
			base.npc.HitSound = SoundID.NPCHit36;
			base.npc.DeathSound = SoundID.NPCDeath39;
			base.npc.value = 0f;
			base.npc.knockBackResist = 0f;
			base.npc.aiStyle = 2;
			base.npc.alpha = 100;
			base.npc.noGravity = true;
			this.aiType = 288;
			base.npc.catchItem = (short)base.mod.ItemType("LargeLostSoul");
		}

		public override void AI()
		{
			if (Main.rand.Next(1) == 0)
			{
				Dust.NewDust(new Vector2(base.npc.position.X, base.npc.position.Y), base.npc.width, base.npc.height, 20, base.npc.velocity.X * 0.2f, base.npc.velocity.Y * 0.2f, 20, default(Color), 2f);
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem((int)base.npc.position.X, (int)base.npc.position.Y, base.npc.width, base.npc.height, base.mod.ItemType("LargeLostSoul"), 1, false, 0, false, false);
		}

		public override bool CanHitPlayer(Player target, ref int cooldownSlot)
		{
			return false;
		}
	}
}
