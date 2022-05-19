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
		private static string GetHUDHoverTextForGauge(
					PKEGaugeType gaugeType,
					float signalPercent ) {
			int signalIntPerc = (int)( signalPercent * 100f );
			signalIntPerc = ( signalIntPerc / 5 ) * 5;

			return $"{gaugeType.ToString()}: {signalIntPerc}%";
		}



		////////////////

		private void DrawHUDHoverTextAt_If(
					SpriteBatch sb,
					Vector2 widgetPos,
					Player plr,
					(Rectangle b, Rectangle g, Rectangle y, Rectangle r) gaugeRects,
					Rectangle marqueeRect,
					PKEGaugeValues values ) {
			IDictionary<PKEGaugeType, PKETextGetter> gaugeTexts = PKEMeterAPI.GetMeterTexts();

			PKEGaugeType gaugeType = 0;
			float signalPerc = 0f;
			string hoverText;
			Color color;

			if( gaugeRects.b.Contains(Main.mouseX, Main.mouseY) ) {
				gaugeType = PKEGaugeType.Blue;
				signalPerc = MathHelper.Lerp( values.BlueRealPercent, values.BlueSeenPercent, 0.75f );
				hoverText = PKEMeterHUD.GetHUDHoverTextForGauge( gaugeType, signalPerc );
			} else if( gaugeRects.g.Contains(Main.mouseX, Main.mouseY) ) {
				gaugeType = PKEGaugeType.Green;
				signalPerc = MathHelper.Lerp( values.GreenRealPercent, values.GreenSeenPercent, 0.75f );
				hoverText = PKEMeterHUD.GetHUDHoverTextForGauge( gaugeType, signalPerc );
			} else if( gaugeRects.y.Contains(Main.mouseX, Main.mouseY) ) {
				gaugeType = PKEGaugeType.Yellow;
				signalPerc = MathHelper.Lerp( values.YellowRealPercent, values.YellowSeenPercent, 0.75f );
				hoverText = PKEMeterHUD.GetHUDHoverTextForGauge( gaugeType, signalPerc );
			} else if( gaugeRects.r.Contains(Main.mouseX, Main.mouseY) ) {
				gaugeType = PKEGaugeType.Red;
				signalPerc = MathHelper.Lerp( values.RedRealPercent, values.RedSeenPercent, 0.75f );
				hoverText = PKEMeterHUD.GetHUDHoverTextForGauge( gaugeType, signalPerc );
			} else if( marqueeRect.Contains(Main.mouseX, Main.mouseY) ) {
				PKETextMessage currMsg = PKEMeterAPI.GetCurrentMeterText().message;

				color = currMsg?.Color ?? Color.White;
				hoverText = currMsg?.Message ?? "";
			} else {
				return;
			}

			//

			Vector2 textPos = Main.MouseScreen;
			textPos.Y += 16f;

			//

			PKETextMessage msg = gaugeTexts
				.GetOrDefault( gaugeType )?
				.Invoke( plr, plr.MountedCenter, values );

			color = msg?.Color ?? Color.White;

			//

			this.DrawHUDHoverText( sb, textPos, hoverText, color );
		}


		private void DrawHUDHoverText(
					SpriteBatch sb,
					Vector2 scrPos,
					string text,
					Color color ) {
			Vector2 textDim = Main.fontMouseText.MeasureString( text );

			if( (scrPos.X + textDim.X) > Main.screenWidth ) {
				scrPos.X = Main.screenWidth - textDim.X;
			} else if( scrPos.X < 0 ) {
				scrPos.X = 0f;
			}

			if( (scrPos.Y + textDim.Y) > Main.screenHeight ) {
				scrPos.Y = Main.screenHeight - textDim.Y;
			} else if( scrPos.Y < 0 ) {
				scrPos.Y = 0f;
			}

			//

			Utils.DrawBorderStringFourWay(
				sb: sb,
				font: Main.fontMouseText,
				text: text,
				x: scrPos.X,
				y: scrPos.Y,
				textColor: color,
				borderColor: Color.Black,
				origin: Vector2.Zero
			);

			//textPos.Y += 18;
		}
	}
}
