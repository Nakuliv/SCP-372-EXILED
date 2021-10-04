using System.Collections.Generic;
using Exiled.API.Features;
using MEC;
using UnityEngine;

namespace SCP_372
{
    public class SCP372Component : MonoBehaviour
    {
        private void Start()
        {
            Timing.RunCoroutine(VoiceChatCheck().CancelWith(this).CancelWith(gameObject));
        }

        private IEnumerator<float> VoiceChatCheck()
        {
            while (true)
            {
                var player = Player.Get(gameObject);
                if (player.IsInvisible && player.IsVoiceChatting || player.IsInvisible && player.Radio.isTransmitting)
                {
                    player.IsInvisible = false;
                }
                else if (!player.IsInvisible && !player.IsVoiceChatting && !player.Radio.isTransmitting)
                {
                    player.IsInvisible = true;
                }
                yield return Timing.WaitForSeconds(0.5f);
            }
        }

        public void Destroy()
        {
            Destroy(this);
        }
    }
}