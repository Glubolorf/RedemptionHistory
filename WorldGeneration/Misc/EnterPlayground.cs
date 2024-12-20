using System;
using SubworldLibrary;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Redemption.WorldGeneration.Misc
{
	public class EnterPlayground : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Placeholder";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("THINGY TO ENTER THE THING");
			base.Tooltip.SetDefault("Sends all players to the Playground");
		}

		public override void SetDefaults()
		{
			base.item.width = 24;
			base.item.height = 24;
			base.item.maxStack = 30;
			base.item.noUseGraphic = true;
			base.item.useAnimation = 45;
			base.item.useTime = 45;
			base.item.UseSound = SoundID.NPCDeath62;
			base.item.useStyle = 4;
			base.item.consumable = false;
		}

		public override bool UseItem(Player player)
		{
			if (!Subworld.AnyActive<Redemption>())
			{
				Subworld.Enter<PlaygroundSub>(false);
			}
			if (Subworld.IsActive<PlaygroundSub>())
			{
				Subworld.Exit(false);
			}
			return true;
		}
	}
}
