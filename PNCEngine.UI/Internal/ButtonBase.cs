using PNCEngine.Assets;
using PNCEngine.Rendering;
using PNCEngine.Rendering.Extensions;
using PNCEngine.UI.Events;
using PNCEngine.UI.Interfaces;
using PNCEngine.UI.Internal.Events;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Xml;

namespace PNCEngine.UI.Internal
{
    public class ButtonBase : UIElement, IDrawableUIElement, IClickableUIElement
    {
        #region Private Fields

        private ButtonAnimation animation;
        private Color backgroundColor;
        private FloatRect boundingbox;
        private Color foregroundColor;
        private Color hoverBackgroundColor;
        private bool hovered;
        private Color hoverForegroundColor;
        private int hoverTexture;
        private AnimationState state;
        private string text;
        private int texture;
        private float transitionTime;
        private float transitionValue;

        private bool visible;

        #endregion Private Fields

        #region Public Constructors

        public ButtonBase(UIManager manager) : base(manager)
        {
            animation = ButtonAnimation.None;
            state = AnimationState.Stay;
            foregroundColor = Color.Black;
            backgroundColor = Color.White;
            hoverBackgroundColor = new Color(200, 200, 200);
            hoverForegroundColor = Color.Black;
            transitionTime = transitionValue = 0;
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<UIClickedEventArgs> Click;

        public event EventHandler<UIClickedEventArgs> DoubleClick;

        public event EventHandler<UIMouseEventArgs> Entered;

        public event EventHandler<UIMouseEventArgs> Exited;

        #endregion Public Events

        #region Public Properties

        public ButtonAnimation Animation
        {
            get { return animation; }
            set { animation = value; }
        }

        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        public FloatRect BoundingBox
        {
            get { return boundingbox; }
            set { boundingbox = value; }
        }

        public Color ForegroundColor
        {
            get { return foregroundColor; }
            set { foregroundColor = value; }
        }

        public Color HoverBackgroundColor
        {
            get { return hoverBackgroundColor; }
            set { hoverBackgroundColor = value; }
        }

        public bool Hovered
        {
            get { return hovered; }
            set { hovered = value; }
        }

        public Color HoverForegroundColor
        {
            get { return hoverForegroundColor; }
            set { hoverForegroundColor = value; }
        }

        public int HoverTexture
        {
            get { return hoverTexture; }
            set { hoverTexture = value; }
        }

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public int Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public float TransitionTime
        {
            get { return transitionTime; }
            set { transitionTime = value; }
        }

        public bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        #endregion Public Properties

        #region Public Methods

        public virtual void Draw(object sender, UIDrawEventArgs args)
        {
            switch (handle)
            {
                case GuiHandle.TopLeft:
                    DrawButton(args.CurrentBatch, new Vector2f(boundingbox.Left, boundingbox.Top));
                    break;

                case GuiHandle.TopCenter:
                    DrawButton(args.CurrentBatch, new Vector2f(boundingbox.Left, boundingbox.Top + boundingbox.Height / 2f));
                    break;

                case GuiHandle.TopRight:
                    DrawButton(args.CurrentBatch, new Vector2f(boundingbox.Left, boundingbox.Top + boundingbox.Height));
                    break;

                case GuiHandle.MiddleLeft:
                    DrawButton(args.CurrentBatch, new Vector2f(boundingbox.Left + boundingbox.Width / 2f, boundingbox.Top));
                    break;

                case GuiHandle.Center:
                    DrawButton(args.CurrentBatch, new Vector2f(boundingbox.Left + boundingbox.Width / 2f, boundingbox.Top + boundingbox.Height / 2f));
                    break;

                case GuiHandle.MiddleRight:
                    DrawButton(args.CurrentBatch, new Vector2f(boundingbox.Left + boundingbox.Width / 2f, boundingbox.Top + boundingbox.Height));
                    break;

                case GuiHandle.BottomLeft:
                    DrawButton(args.CurrentBatch, new Vector2f(boundingbox.Left + boundingbox.Width, boundingbox.Top));
                    break;

                case GuiHandle.BottomCenter:
                    DrawButton(args.CurrentBatch, new Vector2f(boundingbox.Left + boundingbox.Width, boundingbox.Top + boundingbox.Height / 2f));
                    break;

                case GuiHandle.BottomRight:
                    DrawButton(args.CurrentBatch, new Vector2f(boundingbox.Left + boundingbox.Width, boundingbox.Top + boundingbox.Height));
                    break;
            }
        }

        public void RecalculateBounds()
        {
            boundingbox.Height = AssetManager.GetTexture(texture).Height;
            boundingbox.Width = AssetManager.GetTexture(texture).Width;
        }

        public virtual void SetPosition(GuiHandle handle, Vector2f position)
        {
            switch (handle)
            {
                case GuiHandle.TopLeft:
                    boundingbox.Left = position.X;
                    boundingbox.Top = position.Y;
                    break;

                case GuiHandle.TopCenter:
                    boundingbox.Left = position.X - boundingbox.Width / 2f;
                    boundingbox.Top = position.Y;
                    break;

                case GuiHandle.TopRight:
                    boundingbox.Left = position.X - boundingbox.Width;
                    boundingbox.Top = position.Y;
                    break;

                case GuiHandle.MiddleLeft:
                    boundingbox.Left = position.X;
                    boundingbox.Top = position.Y - boundingbox.Height / 2f;
                    break;

                case GuiHandle.Center:
                    boundingbox.Left = position.X - boundingbox.Width / 2f;
                    boundingbox.Top = position.Y - boundingbox.Height / 2f;
                    break;

                case GuiHandle.MiddleRight:
                    boundingbox.Left = position.X - boundingbox.Width;
                    boundingbox.Top = position.Y - boundingbox.Height / 2f;
                    break;

                case GuiHandle.BottomLeft:
                    boundingbox.Left = position.X;
                    boundingbox.Top = position.Y - boundingbox.Height;
                    break;

                case GuiHandle.BottomCenter:
                    boundingbox.Left = position.X - boundingbox.Width / 2f;
                    boundingbox.Top = position.Y - boundingbox.Height;
                    break;

                case GuiHandle.BottomRight:
                    boundingbox.Left = position.X - boundingbox.Width;
                    boundingbox.Top = position.Y - boundingbox.Height;
                    break;
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected void DrawButton(SpriteBatch currentBatch, Vector2f position)
        {
            currentBatch.Draw(AssetManager.GetTexture(texture), position, backgroundColor.SetAlpha(enableColor()));
            if (enabled && (hovered || processAnimation() > 0 && transitionTime > 0))
                switch (animation)
                {
                    case ButtonAnimation.Color:
                        currentBatch.Draw(AssetManager.GetTexture(texture), position, hoverBackgroundColor.SetAlpha(processAnimation() * enableColor()));
                        break;

                    case ButtonAnimation.Texture:
                        currentBatch.Draw(AssetManager.GetTexture(hoverTexture), position, hoverBackgroundColor.SetAlpha(processAnimation() * enableColor()));
                        break;
                }
        }

        protected float enableColor()
        {
            return enabled ? 1 : 0.8f;
        }

        protected void OnClicked(Mouse.Button buttonClicked)
        {
            Click?.Invoke(this, new UIClickedEventArgs(this, buttonClicked));
        }

        protected void OnDoubleClicked()
        {
            DoubleClick?.Invoke(this, new UIClickedEventArgs(this, Mouse.Button.Left));
        }

        protected virtual void OnEntered()
        {
            state = AnimationState.FadeIn;
            Entered?.Invoke(this, new UIMouseEventArgs(Manager.MousePosition));
        }

        protected virtual void OnExited()
        {
            state = AnimationState.FadeOut;
            Exited?.Invoke(this, new UIMouseEventArgs(Manager.MousePosition));
        }

        protected float processAnimation()
        {
            /* transitionTime == 0 means there is no transition, it returns 1
             * transitionValue == 0 means no rendering so to prevent from dividing by zero we return 0
             * everything else simply returns the alpha-multiplicator for the hoveranimation.
             */
            if (transitionTime != 0)
            {
                return (transitionValue == 0) ? 0 : (transitionValue / transitionTime);
            }
            else
            {
                return 1;
            }
        }

        protected override void subscribe()
        {
            Manager.MouseMoved += Manager_MouseMoved;
            Manager.Clicked += Manager_Clicked;
            Manager.DoubleClicked += Manager_DoubleClicked;
            Manager.Drawed += Draw;
            Manager.Updated += Manager_Updated;
        }

        protected override void unsubscribe()
        {
            Manager.MouseMoved -= Manager_MouseMoved;
            Manager.Clicked -= Manager_Clicked;
            Manager.DoubleClicked -= Manager_DoubleClicked;
            Manager.Drawed -= Draw;
            Manager.Updated -= Manager_Updated;
        }

        #endregion Protected Methods

        #region Private Methods

        private void Manager_Clicked(object sender, UI.Events.ClickEventArgs e)
        {
            if (hovered)
                OnClicked(e.ButtonClicked);
        }

        private void Manager_DoubleClicked(object sender, ClickEventArgs e)
        {
            if (hovered)
                OnDoubleClicked();
        }

        private void Manager_MouseMoved(object sender, SFML.Window.MouseMoveEvent e)
        {
            if (hovered && !boundingbox.Contains(e.X, e.Y))
            {
                OnExited();
                hovered = false;
            }

            if (!hovered && boundingbox.Contains(e.X, e.Y))
            {
                OnEntered();
                hovered = true;
            }
        }

        private void Manager_Updated(object sender, UIUpdateEventArgs e)
        {
            switch (state)
            {
                case AnimationState.FadeIn:
                    transitionValue += e.ElapsedTime;

                    if (transitionValue >= transitionTime)
                    {
                        state = AnimationState.Stay;
                        transitionValue = transitionTime;
                    }
                    break;

                case AnimationState.FadeOut:
                    transitionValue -= e.ElapsedTime;

                    if (transitionValue <= 0)
                    {
                        state = AnimationState.Stay;
                        transitionValue = 0;
                    }
                    break;
            }
        }

        public override void Serialize(XmlWriter writer)
        {

        }

        public override void Deserialize(XmlReader reader)
        {

        }

        #endregion Private Methods
    }
}