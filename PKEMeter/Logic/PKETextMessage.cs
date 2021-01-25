using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using HamstarHelpers.Helpers.Debug;


namespace PKEMeter.Logic {
	public delegate PKETextMessage PKEText(
		Player player,
		Vector2 position,
		(float b, float g, float y, float r) gauges
	);




	////////////////

	public class PKETextMessage {
		public static KeyValuePair<string, PKETextMessage> EmptyMessage { get; }
				= new KeyValuePair<string, PKETextMessage>( "Default", new PKETextMessage( "", "", Color.White, 0f ) );



		////////////////

		public string Title { get; }
		public string Message { get; }
		public Color Color { get; }
		public float Priority { get; }



		////////////////

		public PKETextMessage( string title, string message, Color color, float priority ) {
			this.Title = title;
			this.Message = message;
			this.Color = color;
			this.Priority = priority;
		}

		public PKETextMessage( PKETextMessage msg ) {
			this.Title = msg.Title;
			this.Message = msg.Message;
			this.Color = msg.Color;
			this.Priority = msg.Priority;
		}

		////

		public override bool Equals( object obj ) {
			var myobj = obj as PKETextMessage;
			return myobj?.Title == this.Title
				&& myobj.Message == this.Message
				&& myobj.Color == this.Color
				&& myobj.Priority == this.Priority;
		}

		public override int GetHashCode() {
			return this.Title.GetHashCode()
				+ this.Message.GetHashCode()
				+ this.Color.GetHashCode()
				+ this.Priority.GetHashCode();
		}
	}
}
