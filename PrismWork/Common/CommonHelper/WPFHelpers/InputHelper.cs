using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Jisons
{
    public static class InputHelper
    {

        public static bool IsKeysDown(this System.Windows.Input.KeyboardDevice keyboardDevice, params Key[] keys)
        {
            bool isShift = false;

            if ((keyboardDevice.GetKeyStates(Key.B) & System.Windows.Input.KeyStates.Down) > 0 &&
                (keyboardDevice.GetKeyStates(Key.C) & System.Windows.Input.KeyStates.Down) > 0)
            {
                isShift = true;
            }
            if (isShift)
                isShift = false;

            return isShift;
        }

    }
}
