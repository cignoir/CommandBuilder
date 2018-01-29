namespace CommandBuilder
{
    class CommandUtils
    {
        private static float FRAME_RATE = 59.94f;

        public static long FrameToMillis(float frameCount)
        {
            return (long)((1000 / FRAME_RATE) * frameCount);
        }

        public static float MillisToFrame(long millis)
        {
            return millis * FRAME_RATE / 1000;
        }
    }
}
