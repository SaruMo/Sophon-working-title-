using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressing
{
    /// <summary>
    ///     true if the button was pressed on the last checked frame and the previous frame wasn't pressed
    /// </summary>
    bool m_isFirstFramePressed = false;
    public bool IsFirstFramePressed { get { return m_isFirstFramePressed; } }
    
    /// <summary>
    ///     true if the button was pressed on the last checked frame
    /// </summary>
    bool m_isCurrentlyPressed = false;
    public bool IsCurrentlyPressed { get { return m_isCurrentlyPressed; } }
    
    /// <summary>
    ///     true if the button was released in the last checked frame and the previous frame was pressed
    /// </summary>
    bool m_isFirstFrameReleased = false;
    public bool IsFirstFrameReleased { get { return m_isFirstFrameReleased; } }

    bool m_lastFrameWasButtonPressed = false;

    KeyCode m_keyCode;
    string m_buttonName;
    ButtonLogging m_buttonLogging;
    string m_name = "ButtonPressing";

    public ButtonPressing( KeyCode in_keyCode, ButtonLogging in_buttonLogging = null )
    {
        m_keyCode = in_keyCode;
        m_buttonLogging = in_buttonLogging;
    }

    public ButtonPressing(string in_buttonName, ButtonLogging in_buttonLogging = null)
    {
        m_buttonName = in_buttonName;
        m_buttonLogging = in_buttonLogging;
    }

    public void UpdateState()
    {
        if(m_keyCode != KeyCode.None)
        {
            CheckKeyCodeValue();
        }

        if(m_buttonName != null)
        {
            CheckButtonNameValue();
        }
    }

    void CheckKeyCodeValue()
    {
        var buttonPressed = Input.GetKeyDown( m_keyCode );
        if( buttonPressed )
        {
            if( !m_lastFrameWasButtonPressed )
            {
                //Logging.LogComment(m_name, "m_isFirstFramePressed = true");
                m_isFirstFramePressed = true;
            }
            else
            {
                //Logging.LogComment(m_name, "m_isFirstFramePressed = false");
                m_isFirstFramePressed = false;
            }
            m_isCurrentlyPressed = true;
        }
        else
        {
            if( m_lastFrameWasButtonPressed )
            {
                //Logging.LogComment(m_name, "m_isFirstFrameReleased = true");
                m_isFirstFrameReleased = true;
            }
            else
            {
                m_isFirstFrameReleased = false;
            }
        }
        Logging.LogComment(m_name, "buttonPressed = " + buttonPressed);
        m_lastFrameWasButtonPressed = buttonPressed;
    }

    void CheckButtonNameValue()
    { }
}