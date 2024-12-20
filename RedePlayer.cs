using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption
{
	public class RedePlayer : ModPlayer
	{
		public override void UpdateBiomes()
		{
			this.ZoneXeno = (RedeWorld.xenoBiome > 75);
		}

		public override bool CustomBiomesMatch(Player other)
		{
			RedePlayer modPlayer = other.GetModPlayer<RedePlayer>(base.mod);
			return this.ZoneXeno == modPlayer.ZoneXeno;
		}

		public override void CopyCustomBiomesTo(Player other)
		{
			RedePlayer modPlayer = other.GetModPlayer<RedePlayer>(base.mod);
			modPlayer.ZoneXeno = this.ZoneXeno;
		}

		public override void SendCustomBiomes(BinaryWriter writer)
		{
			BitsByte bitsByte = default(BitsByte);
			bitsByte[0] = this.ZoneXeno;
			writer.Write(bitsByte);
		}

		public override Texture2D GetMapBackgroundImage()
		{
			if (this.ZoneXeno)
			{
				return base.mod.GetTexture("XenoBiomeMapBackground");
			}
			return null;
		}

		public override void ReceiveCustomBiomes(BinaryReader reader)
		{
			this.ZoneXeno = reader.ReadByte()[0];
		}

		public override void OnRespawn(Player player)
		{
			if (this.heartEmblem)
			{
				player.statLife = player.statLifeMax2 / 100 * 75;
				player.AddBuff(base.mod.BuffType("HeartEmblemBuff"), 1800, true);
			}
		}

		public override void SetupStartInventory(IList<Item> items)
		{
			Item item = new Item();
			item.SetDefaults(base.mod.ItemType("DruidNote"), false);
			item.stack = 1;
			items.Add(item);
		}

		public override void ResetEffects()
		{
			this.chickenMinion = false;
			this.xenoMinion = false;
			this.examplePet = false;
			this.corpseskullMinion = false;
			this.mk1MicrobotMinion = false;
			this.mk2MicrobotMinion = false;
			this.mk3MicrobotMinion = false;
			this.darkSoulMinion = false;
			this.xenoHatchlingMinion = false;
			this.heartEmblem = false;
			this.moreSeeds = false;
			this.fasterStaves = false;
			this.fasterSeedbags = false;
			this.fasterSpirits = false;
			this.moreSpirits = false;
		}

		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (base.player.FindBuffIndex(base.mod.BuffType("XenomiteDebuff")) != -1)
			{
				damageSource = PlayerDeathReason.ByCustomReason("You got infected");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("XenomiteDebuff2")) != -1)
			{
				damageSource = PlayerDeathReason.ByCustomReason("You got heavily infected");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("EmpoweredBuff")) != -1)
			{
				damageSource = PlayerDeathReason.ByCustomReason("You got too empowered");
			}
			if (base.player.FindBuffIndex(base.mod.BuffType("RadioactiveFalloutDebuff")) != -1)
			{
				damageSource = PlayerDeathReason.ByCustomReason("You forgot to wear a gas mask");
			}
			return true;
		}

		public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk)
		{
			if (junk)
			{
				return;
			}
			if (this.ZoneXeno && liquidType == 0 && questFish == base.mod.ItemType("XenChomper") && Main.rand.Next(1) == 0)
			{
				caughtType = base.mod.ItemType("XenChomper");
			}
		}

		private const int saveVersion = 0;

		public bool examplePet;

		public bool chickenMinion;

		public bool xenoMinion;

		public bool corpseskullMinion;

		public bool mk1MicrobotMinion;

		public bool mk2MicrobotMinion;

		public bool mk3MicrobotMinion;

		public bool darkSoulMinion;

		public bool xenoHatchlingMinion;

		public bool ZoneXeno;

		public bool heartEmblem;

		public bool moreSeeds;

		public bool fasterStaves;

		public bool fasterSeedbags;

		public bool fasterSpirits;

		public bool moreSpirits;
	}
}
