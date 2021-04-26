using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressing : MonoBehaviour
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
    bool m_isFirstFramneReleased = false;
    public bool IsFirstFrameReleased { get { return m_isFirstFramneReleased; } }

    bool m_lastFrameWasButtonPressed = false;

    KeyCode m_keyCode;
    string m_buttonName;
    ButtonLogging m_buttonLogging

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

    void Start()
    { }

    void Update()
    {
        if(m_keyCode != null)
        {
            CheckKeyCodeValue();
        }

        if(m_keyName != null)
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
                m_isFirstFramePressed = true;
            }
            else
            {
                m_isFirstFramePressed = false;
            }
            m_isCurrentlyPressed = true;

            m_isFirstFramneReleased = true;
        }
        else
        {
            if( m_lastFrameWasButtonPressed )
            {
                m_isFirstFramneReleased = true;
            }
            else
            {
                m_isFirstFramneReleased = false;
            }
        }

        m_lastFrameWasButtonPressed = buttonPressed;
    }

    void CheckButtonNameValue()
    { }
}