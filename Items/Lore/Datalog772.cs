using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Redemption.Items.Lore
{
	public class Datalog772 : ModItem
	{
		public override string Texture
		{
			get
			{
				return "Redemption/Items/Lore/Datalog1";
			}
		}

		public override void SetStaticDefaults()
		{
			base.DisplayName.SetDefault("Data Log #722");
			base.Tooltip.SetDefault("It reads - [c/aee6f3:'Finally!]\n[c/aee6f3:I am close enough to Nabu III to get a scan of the planet.]\n[c/aee6f3:Seems to be a standard ocean planet with a radius of 5605.77km.]\n[c/aee6f3:35.3% iron, 32.0% oxygen, 15.1% silicon, 6.1% sodium, 4.2% aluminum, 2.7% other metals, 4.7% other elements.]\n[c/aee6f3:Gravitational pull is 7.84 m/s², less than my planet, but whatever...]\n[c/aee6f3:A cycle lasts 23.89 hours with an axis tilt of 28.37°.]\n[c/aee6f3:80% ice sheets, 19.8% ocean, 0.2% land. Atmospheric pressure of 91.83 kPa.]\n[c/aee6f3:The atmosphere is 78.4% sulphur dioxide, and the average temperature is -10°C.]\n[c/aee6f3:Basically, an uninhabitable frozen planet... Great.']");
			Main.RegisterItemAnimation(base.item.type, new DrawAnimationVertical(5, 2));
		}

		public override void SetDefaults()
		{
			base.item.width = 34;
			base.item.height = 30;
			base.item.maxStack = 1;
			base.item.value = 0;
			base.item.rare = 9;
		}
	}
}
