using System;
using Terraria;
using Terraria.ModLoader;

namespace Redemption.Items
{
	public class XenChomper : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Xen Chomper");
		}

		public override void SetDefaults()
		{
			base.item.questItem = true;
			base.item.maxStack = 1;
			base.item.width = 42;
			base.item.height = 30;
			base.item.uniqueStack = true;
			base.item.rare = -11;
		}

		public override bool IsQuestFish()
		{
			return true;
		}

		public override bool IsAnglerQuestAvailable()
		{
			return RedeWorld.downedInfectedEye && Main.hardMode;
		}

		public override void AnglerQuestChat(ref string description, ref string catchLocation)
		{
			description = "So, I went fishing in the Wasteland, and I wanted to get one of those weird green fishes. Then, I saw a weird, yellow one with yellow crystals! I want to do something with it. Get it to me!";
			catchLocation = "Caught in the Wasteland.";
		}
	}
}
