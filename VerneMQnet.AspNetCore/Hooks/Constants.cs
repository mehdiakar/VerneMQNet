using System;
using System.Collections.Generic;
using System.Text;

namespace VerneMQNet.AspNetCore.Hooks
{
	public static class Constants
	{
		public const byte QOS_LEVEL_AT_MOST_ONCE = 0;
		public const byte QOS_LEVEL_AT_LEAST_ONCE = 1;
		public const byte QOS_LEVEL_EXACTLY_ONCE = 2;
		public const byte QOS_LEVEL_GRANTED_FAILURE = 128;
		public const byte M5_Success = 0;
		public const byte M5_NormalDisconnection = 0;
		public const byte M5_DisconnectWithWillMessage = 4;
		public const byte M5_NoMatchingSubscribers = 16;
		public const byte M5_NoSubscriptionExisted = 17;
		public const byte M5_ContinueAuthentication = 24;
		public const byte M5_ReAuthentication = 25;
		public const byte M5_UnspecifiedError = 128;
		public const byte M5_MalformedPacket = 129;
		public const byte M5_ProtocolError = 130;
		public const byte M5_ImpelemtationSpecificError = 131;
		public const byte M5_UnsupportedProtocolVersion = 132;
		public const byte M5_ClientIdentifierNotValid = 133;
		public const byte M5_BadUsernameOrPassword = 134;
		public const byte M5_NotAuthorized = 135;
		public const byte M5_ServerUnAvailable = 136;
		public const byte M5_ServerBusy = 137;
		public const byte M5_Banned = 138;
		public const byte M5_ServerShuttingDown = 139;
		public const byte M5_BadAuthenticationMethod = 140;
		public const byte M5_KeepAliveTimeout = 141;
		public const byte M5_SessionTakenOver = 142;
		public const byte M5_TopicFilterInvalid = 143;
		public const byte M5_TopicNameInvalid = 144;
		public const byte M5_PacketIdentifierInUse = 145;
		public const byte M5_PacketIdentifierNotFound= 146;
		public const byte M5_ReceiveMaximumExceeded = 147;
		public const byte M5_TopicAliasInvalid = 148;
		public const byte M5_PacketTooLarge = 149;
		public const byte M5_MessageRateTooHigh = 150;
		public const byte M5_QuotaExceeded = 151;
		public const byte M5_AdministrativeAction = 152;
		public const byte M5_PayloadFormatInvalid = 153;
		public const byte M5_RetainNotSupported = 154;
		public const byte M5_QosNotSupported = 155;
		public const byte M5_UseAnotherServer = 156;
		public const byte M5_ServerMoved = 157;
		public const byte M5_SharedsubscriptionsNotSupported = 158;
		public const byte M5_ConnectionRateExceeded = 159;
		public const byte M5_MaximumConnectTime = 160;
		public const byte M5_SubscriptionIdentifiersNotSupported = 161;
		public const byte M5_WildcardSubscriptionsNotSupported = 162;


	}
}
