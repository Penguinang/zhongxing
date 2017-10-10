using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Text;


class DebugConnection : NetworkConnection
{
	public override void TransportRecieve(byte[] bytes, int numBytes, int channelId)
	{
		StringBuilder msg = new StringBuilder();
		for (int i = 0; i < numBytes; i++)
		{
			var s = String.Format("{0:X2}", bytes[i]);
			msg.Append(s);
			if (i > 50) break;
		}
		UnityEngine.Debug.Log("TransportRecieve h:" + hostId + " con:" + connectionId + " bytes:" + numBytes + " " + msg);

		HandleBytes(bytes, numBytes, channelId);
	}

	public override bool TransportSend(byte[] bytes, int numBytes, int channelId, out byte error)
	{
		StringBuilder msg = new StringBuilder();
		for (int i = 0; i < numBytes; i++)
		{
			var s = String.Format("{0:X2}", bytes[i]);
			msg.Append(s);
			if (i > 50) break;
		}
		UnityEngine.Debug.Log("TransportSend    h:" + hostId + " con:" + connectionId + " bytes:" + numBytes + " " + msg);

		return NetworkTransport.Send(hostId, connectionId, channelId, bytes, numBytes, out error);
	}
}
