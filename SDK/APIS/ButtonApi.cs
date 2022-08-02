using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements;
using VRC.UI.Elements.Controls;
using VRC.UI.Elements.Menus;

namespace Discreet.SDK.APIS
{
    //Blazes Button Api Heavily Modified THANKS BLAZE :)
    public class ButtonAPI
    {
        // Replace this with whatever you want. This is to prevent your buttons from overlapping with other mods buttons
        public const string Identifier = "_Discreet_API";

        public static List<QMSingleButton> allQMSingleButtons = new List<QMSingleButton>();
        public static List<QMNestedButton> allQMNestedButtons = new List<QMNestedButton>();
        public static List<QMToggleButton> allQMToggleButtons = new List<QMToggleButton>();
        public static List<GameObject> AllNButtons = new List<GameObject>();
        public static List<GameObject> Allhbuttons = new List<GameObject>();
        public static List<QMTabMenu> allQMTabMenus = new List<QMTabMenu>();
        public static List<QMSlider> allQMSliders = new List<QMSlider>();
        public static Sprite halfbuttonimg = null;
        public static Sprite buttonimg = null;
    }
    public class QMButtonBase
    {
        protected GameObject button;
        protected string btnQMLoc;
        protected string btnType;
        protected string btnTag;
        protected int[] initShift = { 0, 0 };
        protected Color OrigBackground;
        protected Color OrigText;
        protected int RandomNumb;

        public GameObject GetGameObject()
        {
            return button;
        }

        public void SetActive(bool state)
        {
            button.gameObject.SetActive(state);
        }

        public void SetLocation(float buttonXLoc, float buttonYLoc)
        {
            button.GetComponent<RectTransform>().anchoredPosition += Vector2.right * (232 * (buttonXLoc + initShift[0]));
            button.GetComponent<RectTransform>().anchoredPosition += Vector2.down * (210 * (buttonYLoc + initShift[1]));

            btnTag = "(" + buttonXLoc + "," + buttonYLoc + ")";
            button.name = btnQMLoc + "/" + btnType + btnTag;
            button.GetComponent<Button>().name = $"{ButtonAPI.Identifier}-{btnType}{btnTag}";
        }

        public void SetToolTip(string buttonToolTip)
        {
            button.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = buttonToolTip;
            button.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_1 = buttonToolTip;
        }

        public void DestroyMe()
        {
            try
            {
                UnityEngine.Object.Destroy(button);
            }
            catch { }
        }
    }

    public class QMScrollMenu : QMButtonBase
    {
        public QMNestedButton BaseMenu;
        public QMSingleButton NextButton;
        public QMSingleButton BackButton;
        public QMSingleButton IndexButton;
        public QMSingleButton RefreshButton;
        public List<ScrollObject> QMButtons = new List<ScrollObject>();
        private int Posx = 0;
        private int Posy = 0;
        private int Index = 0;
        private Action<QMScrollMenu> OpenAction;
        public int currentMenuIndex = 0;
        public bool ShouldChangePos = true;
        public bool AllowOverStepping = false;
        public bool IgnoreEverything = false;

        public class ScrollObject
        {
            public QMButtonBase ButtonBase;

            public int Index;
        }

        public QMScrollMenu(QMNestedButton basemenu)
        {
            BaseMenu = basemenu;
            IndexButton = new QMSingleButton(BaseMenu, 4f, 1.65f, "Page:\n" + (currentMenuIndex + 1).ToString() + " of " + (Index + 1).ToString(), delegate ()
            {

            }, "");
            IndexButton.GetGameObject().GetComponent<Button>().enabled = false;
            BackButton = new QMSingleButton(BaseMenu, 4f, 0.75f, "Back", delegate ()
            {
                ShowMenu(currentMenuIndex - 1);
            }, "Go Back");
            NextButton = new QMSingleButton(BaseMenu, 4f, 2.55f, "Next", delegate ()
            {
                ShowMenu(currentMenuIndex + 1);
            }, "Go Next");
        }

        public void ShowMenu(int MenuIndex)
        {
            if (!AllowOverStepping && (MenuIndex < 0 || MenuIndex > Index))
            {
                foreach (ScrollObject scrollObject in QMButtons)
                {
                    if (scrollObject.Index == MenuIndex)
                    {
                        QMButtonBase buttonBase = scrollObject.ButtonBase;
                        if (buttonBase != null)
                        {
                            buttonBase.SetActive(true);
                        }
                    }
                    else
                    {
                        QMButtonBase buttonBase2 = scrollObject.ButtonBase;
                        if (buttonBase2 != null)
                        {
                            buttonBase2.SetActive(false);
                        }
                    }
                }
                currentMenuIndex = MenuIndex;
                IndexButton.SetButtonText("Page:\n" + (currentMenuIndex + 1).ToString() + " of " + (Index + 1).ToString());
            }
        }

        public void SetAction(Action<QMScrollMenu> Open, bool shouldClear = true)
        {
            try
            {
                OpenAction = Open;
                BaseMenu.GetMainButton().SetAction(delegate
                {
                    if (shouldClear)
                    {
                        Clear();
                    }
                    OpenAction(this);
                    BaseMenu.OpenMe();
                    ShowMenu(0);
                });
            }
            catch (Exception value) { Console.WriteLine(value); }
        }

        public void Refresh()
        {
            Clear();
            Action<QMScrollMenu> openAction = OpenAction;
            if (openAction != null)
            {
                openAction(this);
            }
            BaseMenu.OpenMe();
            ShowMenu(0);
        }

        public void DestroyMe()
        {
            foreach (ScrollObject scrollObject in QMButtons)
            {
                UnityEngine.Object.Destroy(scrollObject.ButtonBase.GetGameObject());
            }
            QMButtons.Clear();
            if (BaseMenu.GetBackButton() != null)
            {
                UnityEngine.Object.Destroy(BaseMenu.GetBackButton());
            }
            if (IndexButton != null)
            {
                IndexButton.DestroyMe();
            }
            if (BackButton != null)
            {
                BackButton.DestroyMe();
            }
            if (NextButton != null)
            {
                NextButton.DestroyMe();
            }
        }

        public void Clear()
        {
            try
            {
                foreach (ScrollObject scrollObject in QMButtons)
                {
                    UnityEngine.Object.Destroy(scrollObject.ButtonBase.GetGameObject());
                }
                QMButtons.Clear();
                Posx = 0;
                Posy = 0;
                Index = 0;
                currentMenuIndex = 0;
            }
            catch (Exception ex) { }
        }
        public void Add(QMSingleButton Button)
        {
            if (Posx < 4)
            {
                Posx++;
            }
            if (Posx == 4)
            {
                Posx = 1;
                Posy++;
            }
            if (Posy == 4)
            {
                Posy = 0;
                Index++;
            }
            bool shouldChangePos = ShouldChangePos;
            if (shouldChangePos)
            {
                Button.SetLocation(Posx, Posy);
            }
            Button.SetActive(false);
            QMButtons.Add(new ScrollObject
            {
                ButtonBase = Button,
                Index = Index
            });
        }

        public void Add(QMButtonBase Button, int Page, float POSX = 0f, float POSY = 0f)
        {
            if (ShouldChangePos)
            {
                Button.SetLocation(Posx, Posy);
            }
            Button.SetActive(false);
            QMButtons.Add(new ScrollObject
            {
                ButtonBase = Button,
                Index = Page
            });
            if (!IgnoreEverything)
            {
                if (Page > Index)
                {
                    Index = Page;
                }
            }
        }
    }

    public class QMSingleButton : QMButtonBase
    {

        public QMSingleButton(QMNestedButton btnMenu, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string btnToolTip, bool halfBtn = false, bool halfBtnA = false)
        {
            btnQMLoc = btnMenu.GetMenuName();
            if (halfBtnA)
            {
                btnYLocation += 4.6f;
                ButtonAPI.Allhbuttons.Add(this.GetGameObject());
            }
            if (halfBtn)
            {
                btnYLocation -= 0.21f;
                ButtonAPI.Allhbuttons.Add(this.GetGameObject());
            }

            InitButton(btnXLocation, btnYLocation, btnText, btnAction, btnToolTip);
            if (halfBtn || halfBtnA)
            {
                // 2.0175f

                button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
                button.GetComponentInChildren<TMPro.TextMeshProUGUI>().rectTransform.anchoredPosition = new Vector2(0, 22);
            }

        }
        public QMSingleButton(GameObject btnMenugo, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string btnToolTip, bool halfBtn = false, bool halfBtnA = false)
        {
            button.transform.parent = btnMenugo.transform;

            if (halfBtnA)
            {
                btnYLocation += 4.6f;
                ButtonAPI.Allhbuttons.Add(this.GetGameObject());
            }
            if (halfBtn)
            {
                btnYLocation -= 0.21f;
                ButtonAPI.Allhbuttons.Add(this.GetGameObject());
            }

            InitButton(btnXLocation, btnYLocation, btnText, btnAction, btnToolTip);
            if (halfBtn || halfBtnA)
            {
                // 2.0175f

                button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
                button.GetComponentInChildren<TMPro.TextMeshProUGUI>().rectTransform.anchoredPosition = new Vector2(0, 22);
            }

        }


        public QMSingleButton(string btnMenu, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string btnToolTip, bool halfBtn = false, bool halfBtnA = false)
        {
            btnQMLoc = btnMenu;
            if (halfBtnA)
            {

                btnYLocation += 4.6f;
                ButtonAPI.Allhbuttons.Add(this.GetGameObject());
            }
            if (halfBtn)
            {
                btnYLocation -= 0.21f;
                ButtonAPI.Allhbuttons.Add(this.GetGameObject());
            }
            InitButton(btnXLocation, btnYLocation, btnText, btnAction, btnToolTip);

            if (halfBtn || halfBtnA)
            {


                button.GetComponentInChildren<RectTransform>().sizeDelta /= new Vector2(1f, 2f);
                button.GetComponentInChildren<TMPro.TextMeshProUGUI>().rectTransform.anchoredPosition = new Vector2(0, 22);
            }


        }

        public QMSingleButton(QMTabMenu btnMenu, float btnXLocation, float btnYLocation, string btnText, Action btnAction, string btnToolTip, bool halfBtn = false, bool halfBtnA = false)
        {
            if (halfBtnA)
            {
                btnYLocation += 4.6f;
                ButtonAPI.Allhbuttons.Add(this.GetGameObject());
            }

            if (halfBtn)
            {
                btnYLocation -= 0.21f;
                ButtonAPI.Allhbuttons.Add(this.GetGameObject());
            }

            InitButton(btnXLocation, btnYLocation, btnText, btnAction, btnToolTip);

        }


        private protected void InitButton(float btnXLocation, float btnYLocation, string btnText, Action btnAction, string btnToolTip,  bool halfBtn = false, bool halfBtnA = false)
        {

            btnType = "SingleButton";
            button = UnityEngine.Object.Instantiate(APIUtils.SingleButtonTemplate(), GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/" + btnQMLoc).transform, true);
            RandomNumb = APIUtils.RandomNumbers();
            button.GetComponentInChildren<TextMeshProUGUI>().fontSize = 30;
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 176);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(-68, 796);
            button.transform.Find("Icon").GetComponentInChildren<Image>().gameObject.SetActive(false);
            button.GetComponentInChildren<TextMeshProUGUI>().rectTransform.anchoredPosition += new Vector2(0, 50);
            initShift[0] = 0;
            initShift[1] = 0;
            SetLocation(btnXLocation, btnYLocation);
            SetButtonText(btnText);
            SetToolTip(btnToolTip);
            SetAction(btnAction);
            OrigText = button.GetComponentInChildren<TextMeshProUGUI>().color;
            SetActive(true);
            ButtonAPI.allQMSingleButtons.Add(this);
            if (halfBtn || halfBtnA)
            {

                button.transform.Find("Background").GetComponent<Image>().sprite = ButtonAPI.halfbuttonimg;
                button.transform.Find("Background").GetComponent<Image>().overrideSprite = ButtonAPI.halfbuttonimg;

            }
            else if (halfBtn == false || !halfBtnA == false)
            {

                button.transform.Find("Background").GetComponent<Image>().sprite = ButtonAPI.buttonimg;
                button.transform.Find("Background").GetComponent<Image>().overrideSprite = ButtonAPI.buttonimg;
            }

        }

        public void SetBackgroundImage(Sprite fullImg = null, Sprite halfimg = null)
        {
            if (button.GetComponentInChildren<TextMeshProUGUI>().rectTransform.anchoredPosition.y <= 22)
            {
                button.transform.Find("Background").GetComponent<Image>().sprite = halfimg;
                button.transform.Find("Background").GetComponent<Image>().overrideSprite = halfimg;
                RefreshButton();
            }
            else
            {
                button.transform.Find("Background").GetComponent<Image>().sprite = fullImg;
                button.transform.Find("Background").GetComponent<Image>().overrideSprite = fullImg;
                RefreshButton();

            }

        }



        public void SetButtonText(string buttonText)
        {
            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = buttonText;
        }

        public void SetAction(Action buttonAction)
        {
            button.GetComponent<Button>().onClick = new Button.ButtonClickedEvent();
            if (buttonAction != null)
                button.GetComponent<Button>().onClick.AddListener(UnhollowerRuntimeLib.DelegateSupport.ConvertDelegate<UnityAction>(buttonAction));
        }

        public void SetInteractable(bool newState)
        {
            button.GetComponent<Button>().interactable = newState;
            RefreshButton();
        }

        public void ClickMe()
        {
            button.GetComponent<Button>().onClick.Invoke();
        }

        public Image GetBackgroundImage()
        {
            return button.transform.Find("Background").GetComponent<Image>();
        }

        private void RefreshButton()
        {
            button.SetActive(false);
            button.SetActive(true);
        }
    }


    public class QMToggleButton : QMButtonBase
    {
        protected TextMeshProUGUI btnTextComp;
        protected Button btnComp;
        protected Image btnImageComp;
        protected bool currentState;
        protected Action OnAction;
        protected Action OffAction;

        public QMToggleButton(QMNestedButton location, float btnXPos, float btnYPos, string btnText, bool state, Action onAction, Action offAction, string btnToolTip)
        {
            btnQMLoc = location.GetMenuName();
            Initialize(btnXPos, btnYPos, btnText, onAction, offAction, btnToolTip, state);
        }
        public QMToggleButton(GameObject location, float btnXPos, float btnYPos, string btnText, bool state, Action onAction, Action offAction, string btnToolTip)
        {
            button.transform.parent = location.transform;
            Initialize(btnXPos, btnYPos, btnText, onAction, offAction, btnToolTip, state);
        }

        public QMToggleButton(string location, float btnXPos, float btnYPos, string btnText, bool state, Action onAction, Action offAction, string btnToolTip)
        {
            btnQMLoc = location;
            Initialize(btnXPos, btnYPos, btnText, onAction, offAction, btnToolTip, state);
        }

        private void Initialize(float btnXLocation, float btnYLocation, string btnText, Action onAction, Action offAction, string btnToolTip, bool state)
        {
            btnType = "ToggleButton";
            button = UnityEngine.Object.Instantiate(APIUtils.SingleButtonTemplate(), GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/" + btnQMLoc).transform, true);
            RandomNumb = APIUtils.RandomNumbers();
            button.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 176);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(-68, 796);
            btnTextComp = button.GetComponentInChildren<TextMeshProUGUI>(true);
            btnComp = button.GetComponentInChildren<Button>(true);
            btnComp.onClick = new Button.ButtonClickedEvent();
            btnComp.onClick.AddListener(new Action(HandleClick));
            btnImageComp = button.transform.Find("Icon").GetComponentInChildren<Image>(true);
            button.transform.Find("Background").GetComponent<Image>().sprite = ButtonAPI.buttonimg;
            button.transform.Find("Background").GetComponent<Image>().overrideSprite = ButtonAPI.buttonimg;
            button.transform.Find("Text_H4").GetComponent<TMP_Text>().faceColor = Color.red;
            button.transform.Find("Background").GetComponent<Image>().color = Color.red;
            button.transform.Find("Icon").GetComponent<Image>().color = Color.red;
            initShift[0] = 0;
            initShift[1] = 0;
            currentState = state;
            var tmpIcon = currentState ? APIUtils.GetOnIconSprite() : APIUtils.GetOffIconSprite();
            btnImageComp.sprite = tmpIcon;
            btnImageComp.overrideSprite = tmpIcon;
            ButtonAPI.allQMToggleButtons.Add(this);
            SetLocation(btnXLocation, btnYLocation);
            SetButtonText(btnText);
            SetButtonActions(onAction, offAction);
            SetToolTip(btnToolTip);
            SetActive(true);
        }

        private void HandleClick()
        {
            currentState = !currentState;
            var stateIcon = currentState ? APIUtils.GetOnIconSprite() : APIUtils.GetOffIconSprite();
            btnImageComp.sprite = stateIcon;
            btnImageComp.overrideSprite = stateIcon;
            if (currentState)
            {
                OnAction.Invoke();
            }
            else
            {
                OffAction.Invoke();
            }
        }

        public void SetButtonText(string buttonText)
        {
            button.GetComponentInChildren<TextMeshProUGUI>().text = buttonText;
        }

        public void SetButtonActions(Action onAction, Action offAction)
        {
            OnAction = onAction;
            OffAction = offAction;
        }

        public void SetToggleState(bool newState, bool shouldInvoke = false)
        {
            try
            {
                var newIcon = newState ? APIUtils.GetOnIconSprite() : APIUtils.GetOffIconSprite();
                btnImageComp.sprite = newIcon;
                btnImageComp.overrideSprite = newIcon;
                currentState = newState;

                if (shouldInvoke)
                {
                    if (newState)
                    {
                        OnAction.Invoke();
                    }
                    else
                    {
                        OffAction.Invoke();
                    }
                }
            }
            catch (Exception ex) { }
        }

        public void ClickMe()
        {
            HandleClick();
        }

        public bool GetCurrentState()
        {
            return currentState;
        }
    }

    public class QMNestedButton
    {
        protected string btnQMLoc;
        protected GameObject MenuObject;
        protected TextMeshProUGUI MenuTitleText;
        protected UIPage MenuPage;
        protected bool IsMenuRoot;
        protected GameObject BackButton;
        protected QMSingleButton MainButton;
        protected string MenuName;
        protected string QmColor;
        protected string TextColor;
        public QMNestedButton(QMNestedButton location, float posX, float posY, string btnText, string toolTipText, string menuTitle, bool halfButton = false)
        {
            btnQMLoc = location.GetMenuName();
            Initialize(false, btnText, posX, posY, toolTipText, menuTitle, halfButton);
        }

        public QMNestedButton(string location, float posX, float posY, string btnText, string toolTipText, string menuTitle, bool halfButton = false)
        {
            btnQMLoc = location;
            Initialize(location.StartsWith("Menu_"), btnText, posX, posY, toolTipText, menuTitle, halfButton);
        }

        public QMNestedButton(QMTabMenu location, float posX, float posY, string btnText, string toolTipText, string menuTitle, bool halfButton = false)
        {
            btnQMLoc = location.GetMenuName();
            Initialize(false, btnText, posX, posY, toolTipText, menuTitle, halfButton);
        }

        private void Initialize(bool isRoot, string btnText, float btnPosX, float btnPosY, string btnToolTipText, string menuTitle, bool halfButton)
        {
            MenuName = $"{ButtonAPI.Identifier}-Menu-{menuTitle}";
            MenuObject = UnityEngine.Object.Instantiate(APIUtils.GetMenuPageTemplate(), APIUtils.GetMenuPageTemplate().transform.parent);
            MenuObject.name = MenuName;
            MenuObject.SetActive(false);
            UnityEngine.Object.DestroyImmediate(MenuObject.GetComponent<LaunchPadQMMenu>());
            MenuPage = MenuObject.AddComponent<UIPage>();
            MenuPage.field_Public_String_0 = MenuName;
            MenuPage.field_Private_Boolean_1 = true;
            MenuPage.field_Protected_MenuStateController_0 = APIUtils.GetQuickMenuInstance().prop_MenuStateController_0;
            MenuPage.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
            MenuPage.field_Private_List_1_UIPage_0.Add(MenuPage);
            APIUtils.GetQuickMenuInstance().prop_MenuStateController_0.field_Private_Dictionary_2_String_UIPage_0.Add(MenuName, MenuPage);
            if (isRoot)
            {
                var list = APIUtils.GetQuickMenuInstance().prop_MenuStateController_0.field_Public_ArrayOf_UIPage_0.ToList();
                list.Add(MenuPage);
                APIUtils.GetQuickMenuInstance().prop_MenuStateController_0.field_Public_ArrayOf_UIPage_0 = list.ToArray();
            }
            MenuObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup").DestroyChildren();
            MenuTitleText = MenuObject.GetComponentInChildren<TextMeshProUGUI>(true);
            MenuTitleText.text = menuTitle;
            IsMenuRoot = isRoot;
            BackButton = MenuObject.transform.GetChild(0).Find("LeftItemContainer/Button_Back").gameObject;
            BackButton.SetActive(true);
            BackButton.GetComponentInChildren<Button>().onClick = new Button.ButtonClickedEvent();
            BackButton.GetComponentInChildren<Button>().onClick.AddListener(new Action(() =>
            {
                if (isRoot)
                {
                    if (btnQMLoc.StartsWith("Menu_"))
                    {
                        APIUtils.GetQuickMenuInstance().prop_MenuStateController_0.Method_Public_Void_String_Boolean_Boolean_0("QuickMenu" + btnQMLoc.Remove(0, 5));
                        return;
                    }
                    APIUtils.GetQuickMenuInstance().prop_MenuStateController_0.Method_Public_Void_String_Boolean_Boolean_0(btnQMLoc);
                    return;
                }
                MenuPage.Method_Protected_Virtual_New_Void_0();
            }));
            MenuObject.transform.GetChild(0).Find("RightItemContainer/Button_QM_Expand").gameObject.SetActive(false);
            MainButton = new QMSingleButton(btnQMLoc, btnPosX, btnPosY, btnText, OpenMe, btnToolTipText, halfButton);
            for (int i = 0; i < MenuObject.transform.childCount; i++)
            {
                if (MenuObject.transform.GetChild(i).name != "Header_H1" && MenuObject.transform.GetChild(i).name != "ScrollRect")
                {
                    UnityEngine.Object.Destroy(MenuObject.transform.GetChild(i).gameObject);
                }
            }
            MenuObject.transform.Find("ScrollRect").GetComponent<ScrollRect>().enabled = false;
            ButtonAPI.allQMNestedButtons.Add(this);
        }
        //
        public void OpenMe()
        {
            APIUtils.GetQuickMenuInstance().prop_MenuStateController_0.Method_Public_Void_String_UIContext_Boolean_TransitionType_0(MenuPage.field_Public_String_0);
        }
        //
        public void CloseMe()
        {
            MenuPage.Method_Public_Virtual_New_Void_0();
        }
        //
        public string GetMenuName()
        {
            return MenuName;
        }
        //
        public GameObject GetMenuObject()
        {
            return MenuObject;
        }
        //
        public QMSingleButton GetMainButton()
        {
            return MainButton;
        }
        //
        public GameObject GetBackButton()
        {
            return BackButton;
        }
    }

    public class QMTabMenu
    {
        protected string btnQMLoc;
        protected GameObject MenuObject;
        protected TextMeshProUGUI MenuTitleText;
        protected UIPage MenuPage;
        protected GameObject MainButton;
        protected GameObject BadgeObject;
        protected TextMeshProUGUI BadgeText;
        protected MenuTab MenuTabComp;
        protected string MenuName;

        public QMTabMenu(string toolTipText, string menuTitle, Sprite img = null)
        {
            Initialize(toolTipText, menuTitle, img);
        }

        private void Initialize(string btnToolTipText, string menuTitle, Sprite img = null)
        {
            MenuName = $"{ButtonAPI.Identifier}-I Like Cats";
            MenuObject = UnityEngine.Object.Instantiate(APIUtils.GetMenuPageTemplate(), APIUtils.GetMenuPageTemplate().transform.parent);
            MenuObject.name = MenuName;
            MenuObject.SetActive(false);
            UnityEngine.Object.DestroyImmediate(MenuObject.GetComponent<LaunchPadQMMenu>());
            MenuPage = MenuObject.AddComponent<UIPage>();
            MenuPage.field_Public_String_0 = MenuName;
            MenuPage.field_Private_Boolean_1 = true;
            MenuPage.field_Protected_MenuStateController_0 = APIUtils.GetQuickMenuInstance().prop_MenuStateController_0;
            MenuPage.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
            MenuPage.field_Private_List_1_UIPage_0.Add(MenuPage);
            APIUtils.GetQuickMenuInstance().prop_MenuStateController_0.field_Private_Dictionary_2_String_UIPage_0.Add(MenuName, MenuPage);
            var list = APIUtils.GetQuickMenuInstance().prop_MenuStateController_0.field_Public_ArrayOf_UIPage_0.ToList();
            list.Add(MenuPage);
            APIUtils.GetQuickMenuInstance().prop_MenuStateController_0.field_Public_ArrayOf_UIPage_0 = list.ToArray();
            MenuObject.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup").DestroyChildren();
            MenuTitleText = MenuObject.GetComponentInChildren<TextMeshProUGUI>(true);
            MenuTitleText.text = menuTitle;
            MenuObject.transform.GetChild(0).Find("RightItemContainer/Button_QM_Expand").gameObject.SetActive(false);
            for (int i = 0; i < MenuObject.transform.childCount; i++)
            {
                if (MenuObject.transform.GetChild(i).name != "Header_H1" && MenuObject.transform.GetChild(i).name != "ScrollRect")
                {
                    UnityEngine.Object.Destroy(MenuObject.transform.GetChild(i).gameObject);
                }
            }
            MenuObject.transform.Find("ScrollRect").GetComponent<ScrollRect>().enabled = false;
            MainButton = UnityEngine.Object.Instantiate(APIUtils.GetTabButtonTemplate(), APIUtils.GetTabButtonTemplate().transform.parent);
            MainButton.name = $"{ButtonAPI.Identifier}-{APIUtils.RandomNumbers()}";
            MenuTabComp = MainButton.GetComponent<MenuTab>();
            MenuTabComp.field_Private_MenuStateController_0 = APIUtils.GetMenuStateControllerInstance();
            MenuTabComp.field_Public_String_0 = MenuName;
            MenuTabComp.GetComponent<StyleElement>().field_Private_Selectable_0 = MenuTabComp.GetComponent<Button>();
            BadgeObject = MainButton.transform.GetChild(0).gameObject;
            BadgeText = BadgeObject.GetComponentInChildren<TextMeshProUGUI>();
            MainButton.GetComponent<Button>().onClick.AddListener(new Action(() =>
            {
                MenuTabComp.GetComponent<StyleElement>().field_Private_Selectable_0 = MenuTabComp.GetComponent<Button>();
            }));
            SetToolTip(btnToolTipText);
            if (img != null)
            {
                SetImage(img);
            }
            MenuTabComp.transform.Find("Background").GetComponent<Image>().color = Color.black;
        }

        public void SetImage(Sprite newImg)
        {
            MainButton.transform.Find("Icon").GetComponent<Image>().sprite = newImg;
            MainButton.transform.Find("Icon").GetComponent<Image>().overrideSprite = newImg;
            MainButton.transform.Find("Icon").GetComponent<Image>().color = Color.white;
            MainButton.transform.Find("Icon").GetComponent<Image>().m_Color = Color.white;
        }

        public void SetToolTip(string newText)
        {
            MainButton.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>().field_Public_String_0 = newText;
        }

        public void SetIndex(int newPosition)
        {
            MainButton.transform.SetSiblingIndex(newPosition);
        }

        public void SetActive(bool newState)
        {
            MainButton.SetActive(newState);
        }

        public void SetBadge(bool showing = true, string text = "")
        {
            if (BadgeObject == null || BadgeText == null)
            {
                return;
            }
            BadgeObject.SetActive(showing);
            BadgeText.text = text;
        }

        public string GetMenuName()
        {
            return MenuName;
        }

        public GameObject GetMenuObject()
        {
            return MenuObject;
        }

        public GameObject GetMainButton()
        {
            return MainButton;
        }
    }

    public class QMSlider
    {
        protected GameObject slider;
        protected GameObject label;
        protected Slider sliderComp;
        protected Text text;
        public QMSlider(QMNestedButton location, float posX, float posY, string sliderLabel, bool Iswhole, Color Slidercolor, Color Sliderback, float minValue, float maxValue, float defaultValue, Action<float> sliderAction, Color? labelColor = null)
        {
            Initialize(location.GetMenuObject().transform, posX, posY, sliderLabel, Iswhole, Slidercolor, Sliderback, minValue, maxValue, defaultValue, sliderAction, labelColor);
        }

        public QMSlider(QMTabMenu location, float posX, float posY, string sliderLabel, bool Iswhole, Color Slidercolor, Color Sliderback, float minValue, float maxValue, float defaultValue, Action<float> sliderAction, Color? labelColor = null)
        {
            Initialize(location.GetMenuObject().transform, posX, posY, sliderLabel, Iswhole, Slidercolor, Sliderback, minValue, maxValue, defaultValue, sliderAction, labelColor);
        }

        public QMSlider(Transform location, float posX, float posY, string sliderLabel, bool Iswhole, Color Slidercolor, Color Sliderback, float minValue, float maxValue, float defaultValue, Action<float> sliderAction, Color? labelColor = null)
        {
            Initialize(location, posX, posY, sliderLabel, Iswhole, Slidercolor, Sliderback, minValue, maxValue, defaultValue, sliderAction, labelColor);
        }

        private void Initialize(Transform location, float posX, float posY, string sliderLabel, bool Iswhole, Color Slidercolor, Color Sliderback, float minValue, float maxValue, float defaultValue, Action<float> sliderAction, Color? labelColor = null)
        {
            slider = UnityEngine.Object.Instantiate(APIUtils.GetSliderTemplate(), location);
            slider.transform.localScale = new Vector3(1, 1, 1);
            slider.name = $"{ButtonAPI.Identifier}-QMSlider-{sliderLabel}";
            label = UnityEngine.Object.Instantiate(GameObject.Find("UserInterface/MenuContent/Screens/Settings/AudioDevicePanel/LevelText"), slider.transform);
            label.name = "QMSlider-Label";
            label.transform.localScale = new Vector3(1, 1, 1);
            label.GetComponent<RectTransform>().sizeDelta = new Vector2(360, 50);
            label.GetComponent<RectTransform>().anchoredPosition = new Vector2(10.4f, 55);
            sliderComp = slider.GetComponent<Slider>();
            sliderComp.wholeNumbers = Iswhole;
            sliderComp.onValueChanged = new Slider.SliderEvent();
            sliderComp.onValueChanged.AddListener(sliderAction);
            sliderComp.onValueChanged.AddListener(new Action<float>(delegate (float f)
            {
                slider.transform.Find("Fill Area/Label").GetComponent<Text>().text = $"{sliderComp.value / maxValue * 100}%";
            }));
            sliderComp.GetComponent<Image>().color = Sliderback;
            sliderComp.transform.Find("Fill Area/Fill").GetComponent<Image>().color = Slidercolor;
            sliderComp.transform.Find("Fill Area/Label").GetComponent<RectTransform>().localPosition = new Vector3(142.5819f, 2.3636f, 0f);
            text = label.GetComponent<Text>();
            text.resizeTextForBestFit = false;
            if (labelColor != null) SetLabelColor((Color)labelColor);
            SetLocation(new Vector2(posX, posY));
            SetLabelText(sliderLabel);
            SetValue(minValue, maxValue, defaultValue);
            ButtonAPI.allQMSliders.Add(this);
        }

        public void SetLocation(Vector2 location)
        {
            slider.GetComponent<RectTransform>().anchoredPosition = location;
        }

        public void SetLabelText(string label)
        {
            text.text = label;
        }

        public void SetLabelColor(Color color)
        {
            text.color = color;
        }

        public void SetValue(float min, float max, float current)
        {
            sliderComp.minValue = min;
            sliderComp.maxValue = max;
            sliderComp.value = current;
        }

        public GameObject GetGameObject()
        {
            return slider;
        }
    }

    public static class APIUtils
    {
        // Used to make sure that the random number generation per created api item has a new number
        private static readonly System.Random rnd = new System.Random();
        // Cached Instances
        private static VRC.UI.Elements.QuickMenu QuickMenuInstance;
        private static MenuStateController MenuStateControllerInstance;
        // Cached Objects to easily call to at any moment needed to
        private static GameObject SingleButtonReference;
        private static GameObject TabButtonReference;
        private static GameObject MenuPageReference;
        private static GameObject SliderReference;
        private static GameObject SliderLabelReference;
        private static Sprite OnIconReference;
        private static Sprite OffIconReference;
        public static SelectedUserMenuQM SelectedUserMenuQM;
        public static VRC.UI.Elements.QuickMenu quickMenu;//Shit I Added
        public static void prefab()
        {
            SelectedUserMenuQM = quickMenu.transform.Find("Container/Window/QMParent/Menu_SelectedUser_Local").GetComponent<SelectedUserMenuQM>();
        }
        // Instance Methods
        public static VRC.UI.Elements.QuickMenu GetQuickMenuInstance()
        {
            if (QuickMenuInstance == null)
                QuickMenuInstance = Resources.FindObjectsOfTypeAll<VRC.UI.Elements.QuickMenu>()[0];
            return QuickMenuInstance;
        }
        public static MenuStateController GetMenuStateControllerInstance()
        {
            if (MenuStateControllerInstance == null)
            {
                MenuStateControllerInstance = GetQuickMenuInstance().GetComponent<MenuStateController>();
            }
            return MenuStateControllerInstance;
        }
        // Template Methods
        public static GameObject SingleButtonTemplate()
        {
            if (SingleButtonReference == null)
            {
                var Buttons = GetQuickMenuInstance().GetComponentsInChildren<UnityEngine.UI.Button>(true);
                foreach (var button in Buttons)
                {
                    if (button.name == "Button_Screenshot")
                    {
                        SingleButtonReference = button.gameObject;
                    }
                };
            }
            return SingleButtonReference;
        }
        public static GameObject GetMenuPageTemplate()
        {
            if (MenuPageReference == null)
            {
                MenuPageReference = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard").gameObject;
            }
            return MenuPageReference;
        }
        public static GameObject GetTabButtonTemplate()
        {
            if (TabButtonReference == null)
            {
                TabButtonReference = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings").gameObject;
            }
            return TabButtonReference;
        }
        public static GameObject GetSliderTemplate()
        {
            if (SliderReference == null)
            {
                SliderReference = GameObject.Find("UserInterface").transform.Find("MenuContent/Screens/Settings/AudioDevicePanel/VolumeSlider").gameObject;
            }
            return SliderReference;
        }
        public static GameObject GetSliderLabelTemplate()
        {
            if (SliderLabelReference == null)
            {
                SliderLabelReference = GameObject.Find("UserInterface").transform.Find("MenuContent/Screens/Settings/AudioDevicePanel/LevelText").gameObject;
            }
            return SliderLabelReference;
        }
        // Icon Sprite Methods
        public static Sprite GetOnIconSprite()
        {
            if (OnIconReference == null)
            {
                OnIconReference = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Notifications/Panel_NoNotifications_Message/Icon").GetComponent<UnityEngine.UI.Image>().sprite;
            }
            return OnIconReference;
        }
        public static Sprite GetOffIconSprite()
        {
            if (OffIconReference == null)
            {
                OffIconReference = GameObject.Find("UserInterface").transform.Find("Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Settings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Buttons_UI_Elements_Row_1/Button_ToggleQMInfo/Icon_Off").GetComponent<UnityEngine.UI.Image>().sprite;
            }
            return OffIconReference;
        }
        // Other Functions
        public static int RandomNumbers()
        {
            return rnd.Next(100000, 999999);
        }
        public static void DestroyChildren(this Transform transform)
        {
            transform.DestroyChildren(null);
        }
        public static void DestroyChildren(this Transform transform, Func<Transform, bool> exclude)
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                if (exclude == null || exclude(transform.GetChild(i)))
                {
                    UnityEngine.Object.DestroyImmediate(transform.GetChild(i).gameObject);
                }
            }
        }
    }
}
