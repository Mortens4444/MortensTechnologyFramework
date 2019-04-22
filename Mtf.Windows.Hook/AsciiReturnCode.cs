namespace Mtf.Windows.Hook
{
    public enum AsciiReturnCode
    {
        /// <summary>
        /// The specified virtual key has no translation for the current state of the keyboard.
        /// </summary>
        NoTranslation = 0,
        /// <summary>
        /// One character was copied to the buffer.
        /// </summary>
        OneCharacter = 1,
        /// <summary>
        /// Two characters were copied to the buffer. This usually happens when a dead-key character (accent or diacritic) stored in the keyboard layout cannot be composed with the specified virtual key to form a single character.
        /// </summary>
        TwoCharacter = 2
    }
}