using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandBuilder
{
    class CommandUtils
    {
        private static float FRAME_RATE = 59.94f;

        public static int FrameToMillis(float frameCount)
        {
            return (int)((1000 / FRAME_RATE) * frameCount);
        }

        public static int MillisToFrame(int millis)
        {
            return (int)(millis * FRAME_RATE / 1000);
        }
    }
}
