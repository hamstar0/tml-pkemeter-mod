using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using ModLibsCore.Libraries.Debug;
using ModLibsCore.Libraries.DotNET.Extensions;
using HUDElementsLib;
using PKEMeter.Logic;


namespace PKEMeter.HUD {
	public partial class PKEMeterHUD : HUDElement {
		private void DrawHUDHoverText(
					SpriteBatch sb,
					Vector2 widgetPos,
					Player plr,
					PKEGaugeValues values ) {
			IDictionary<PKEGaugeType, PKETextGetter> texts = PKEMeterAPI.GetMeterTexts();

			Vector2 textPos = Main.MouseScreen;
			textPos.Y += 16f;

			foreach( (PKEGaugeType gaugeType, PKETextGetter text) in texts ) {
				PKETextMessage msg = text.Invoke( plr, textPos, values );
				if( string.IsNullOrEmpty(msg.Message) ) {
					continue;
				}

				//

				Utils.DrawBorderStringFourWay(
					sb: sb,
					font: Main.fontMouseText,
					text: msg.Message+": "+values.GetGaugeValue( gaugeType, true ),
					x: textPos.X,
					y: textPos.Y,
					textColor: msg.Color,
					borderColor: Color.Black,
					origin: Vector2.Zero
				);

				textPos.Y += 18;
			}
		}
	}
}
