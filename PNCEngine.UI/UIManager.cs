using PNCEngine.Rendering;
using PNCEngine.UI.Events;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;

namespace PNCEngine.UI
{
    public class UIManager
    {
        #region Private Fields

        private Clock clickTimer;
        private float doubleClickTimer;
        private float elapsedTime;
        private float fixedTimeStep;
        private Dictionary<Keyboard.Key, bool> keystates;
        private bool leftClicked;
        private bool middleClicked;
        private Vector2i mousePosition;
        private bool rightClicked;
        private SpriteBatch spriteBatch;
        private Clock timer;

        #endregion Private Fields

        #region Public Constructors

        public UIManager()
        {
            timer = new Clock();
            clickTimer = new Clock();
            fixedTimeStep = 0.2f;
            doubleClickTimer = 0.1f;
            spriteBatch = new SpriteBatch();
            keystates = new Dictionary<Keyboard.Key, bool>();
            for (int keyID = 0; keyID < (int)Keyboard.Key.KeyCount; keyID++)
                keystates.Add((Keyboard.Key)keyID, false);
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<ClickEventArgs> Clicked;

        public event EventHandler<ClickEventArgs> DoubleClicked;

        public event EventHandler<UIDrawEventArgs> Drawed;

        public event EventHandler<UIUpdateEventArgs> FixedUpdated;

        public event EventHandler<KeyPressedEventArgs> KeyPressed;

        public event EventHandler<ClickEventArgs> MiddleClicked;

        public event EventHandler<MouseMoveEvent> MouseMoved;

        public event EventHandler<ClickEventArgs> RightClicked;

        public event EventHandler<UIUpdateEventArgs> Updated;

        #endregion Public Events

        #region Public Properties

        public float FixedTimeStep
        {
            get { return fixedTimeStep; }
            set { fixedTimeStep = value; }
        }

        public Vector2i MousePosition
        {
            get { return mousePosition; }
        }

        #endregion Public Properties

        #region Public Methods

        public void Draw()
        {
            spriteBatch.Begin();
            OnDrawed();
            spriteBatch.End();
        }

        public void Update()
        {
            float currentTimer = timer.ElapsedTime.AsSeconds();
            UpdateInput();
            OnUpdated(Math.Abs(elapsedTime - currentTimer));
            if (timer.ElapsedTime.AsSeconds() >= fixedTimeStep)
            {
                OnFixedUpdate(currentTimer);
                timer.Restart();
            }
            elapsedTime = currentTimer;
        }

        #endregion Public Methods

        #region Protected Methods

        protected void OnMouseMoved()
        {
            MouseMoveEvent args = new MouseMoveEvent();
            args.X = mousePosition.X;
            args.Y = mousePosition.Y;
            MouseMoved?.Invoke(this, args);
        }

        #endregion Protected Methods

        #region Private Methods

        private void KeyboardInput()
        {
            bool shift = Keyboard.IsKeyPressed(Keyboard.Key.LShift) || Keyboard.IsKeyPressed(Keyboard.Key.RShift);
            bool ctrl = Keyboard.IsKeyPressed(Keyboard.Key.LControl) || Keyboard.IsKeyPressed(Keyboard.Key.RControl);

            foreach (Keyboard.Key key in keystates.Keys)
            {
                bool pressed = Keyboard.IsKeyPressed(key);
                if (pressed && pressed != keystates[key])
                    OnKeyPressed(key, shift, ctrl);
            }
        }

        private void MouseInput()
        {
            mousePosition = Mouse.GetPosition();

            bool left = Mouse.IsButtonPressed(Mouse.Button.Left);
            bool middle = Mouse.IsButtonPressed(Mouse.Button.Middle);
            bool right = Mouse.IsButtonPressed(Mouse.Button.Right);

            if (left && leftClicked != left)
            {
                if (this.clickTimer.ElapsedTime.AsSeconds() - doubleClickTimer > 0)
                    OnDoubleLeftClicked();
                else
                {
                    OnLeftClicked();
                    this.clickTimer.Restart();
                }
            }
            if (middle && middleClicked != middle)
                OnMiddleClicked();
            if (right && rightClicked != right)
                OnRightClicked();

            leftClicked = left;
            middleClicked = middle;
            rightClicked = right;
        }

        private void OnDoubleLeftClicked()
        {
            DoubleClicked?.Invoke(this, new ClickEventArgs(Mouse.Button.Left));
        }

        private void OnDrawed()
        {
            Drawed?.Invoke(this, new UIDrawEventArgs(spriteBatch));
        }

        private void OnFixedUpdate(float elapsedTime)
        {
            FixedUpdated?.Invoke(this, new UIUpdateEventArgs(elapsedTime));
        }

        private void OnKeyPressed(Keyboard.Key key, bool shift, bool ctrl)
        {
            KeyPressed?.Invoke(this, new KeyPressedEventArgs(key, shift, ctrl));
        }

        private void OnLeftClicked()
        {
            Clicked?.Invoke(this, new ClickEventArgs(Mouse.Button.Left));
        }

        private void OnMiddleClicked()
        {
            MiddleClicked?.Invoke(this, new ClickEventArgs(Mouse.Button.Middle));
        }

        private void OnRightClicked()
        {
            RightClicked?.Invoke(this, new ClickEventArgs(Mouse.Button.Right));
        }

        private void OnUpdated(float elapsedTime)
        {
            Updated?.Invoke(this, new UIUpdateEventArgs(elapsedTime));
        }

        private void UpdateInput()
        {
            MouseInput();
            KeyboardInput();
        }

        #endregion Private Methods
    }
}