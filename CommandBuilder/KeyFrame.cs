using System;
using System.Windows.Input;

namespace CommandBuilder
{
    class KeyFrame
    {
        public long millis;
        public float frameCount;
        public Key key;
        public KeyStatus status;

        public KeyFrame(Key key, KeyStatus status, int frameCount)
        {
            this.key = key;
            this.status = status;
            this.frameCount = frameCount;
            this.millis = CommandUtils.FrameToMillis(frameCount);
        }

        public KeyFrame(Key key, KeyStatus status, long millis)
        {
            this.key = key;
            this.status = status;
            this.millis = millis;
            this.frameCount = CommandUtils.MillisToFrame(millis); ;
        }

        public override string ToString()
        {
            return String.Format(@"{0} {1} {2}F {3}ms", key.ToString(), status.ToString(), frameCount, millis);
        }
    }

    enum KeyStatus
    {
        Down, Up
    }
}
