using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList.Static
{
    public enum WaitCursorMethod
    {
        None,
        WindowsClassic,
        DevExpressDefault,
        DevExpressCustom,
        DevExpressOverlay
    }
    public static class MyCursorHelper
    {


        #region DICHIARAZIONI
        private static WaitCursorMethod _Current = WaitCursorMethod.None;
        private static SplashScreenManager _manager;
        private static IOverlaySplashScreenHandle _overlayHandle;
        private static string _caption = string.Empty;
        private static string _description = string.Empty;
        #endregion DICHIARAZIONI

        #region f()_PUBLICHE
        public static void ShowCursor(WaitCursorMethod method, string caption = "Attendere!", string description = "Operazione in corso...", XtraForm parentForm = null, Type customWaitFormType = null)
        {

            if (_Current != WaitCursorMethod.None)
            {
                if (_Current != method)
                    Close();
                else if (_Current == method && method == WaitCursorMethod.DevExpressDefault && (_caption != caption || _description != description))
                {
                    UpdateDevExpressWait(caption, description);
                    _caption = caption;
                    _description = description;
                    return;
                }
                else if (_Current == method && method == WaitCursorMethod.DevExpressCustom && (_caption != caption || _description != description))
                {
                    UpdateDevExpressWait(caption, description);
                    _caption = caption;
                    _description = description;
                    return;
                }

            }

            _caption = caption;
            _description = description;

            _Current = method;

            switch (method)
            {
                case WaitCursorMethod.WindowsClassic:
                    ShowWaitCursor();
                    break;

                case WaitCursorMethod.DevExpressDefault:
                    ShowDevExpressWait(caption, description);
                    break;

                case WaitCursorMethod.DevExpressCustom:
                    if (parentForm != null && customWaitFormType != null)
                    {
                        ShowCustomWait(parentForm, customWaitFormType, true, true);
                    }
                    break;

                case WaitCursorMethod.DevExpressOverlay:
                    if (parentForm != null)
                    {
                        ShowOverlay(parentForm);
                    }
                    break;
            }
        }

        public static void Close()
        {

            switch (_Current)
            {
                case WaitCursorMethod.WindowsClassic:
                    HideWaitCursor();
                    break;
                case WaitCursorMethod.DevExpressDefault:
                    HideDevExpressWait();
                    break;
                case WaitCursorMethod.DevExpressCustom:
                    HideCustomWait();
                    break;
                case WaitCursorMethod.DevExpressOverlay:
                    HideOverlay();
                    break;
            }

            _Current = WaitCursorMethod.None;
        }

        /// <summary>
        /// Aggiorna testo WaitForm.
        /// </summary>
        public static void UpdateDevExpressWait(string caption, string description)
        {
            try
            {
                if (SplashScreenManager.Default != null &&
                    SplashScreenManager.Default.IsSplashFormVisible)
                {
                    SplashScreenManager.Default.SetWaitFormCaption(caption);
                    SplashScreenManager.Default.SetWaitFormDescription(description);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Aggiorna testo WaitForm custom.
        /// </summary>
        public static void UpdateCustomWait(string caption, string description)
        {
            try
            {
                if (_manager != null &&
                    _manager.IsSplashFormVisible)
                {
                    _manager.SetWaitFormCaption(caption);
                    _manager.SetWaitFormDescription(description);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Imposta un cursore custom.
        /// </summary>
        public static void SetCursor(Cursor cursor)
        {
            Cursor.Current = cursor;
        }

        /// <summary>
        /// Ripristina default.
        /// </summary>
        public static void ResetCursor()
        {
            Cursor.Current = Cursors.Default;
        }
        #endregion f()_PUBLICHE

        #region WINDOWS_CLASSICO

        /// <summary>
        /// Mostra il cursore Wait standard Windows.
        /// </summary>
        private static void ShowWaitCursor()
        {
            Cursor.Current = Cursors.WaitCursor;
            Application.UseWaitCursor = true;
        }

        /// <summary>
        /// Ripristina il cursore standard.
        /// </summary>
        private static void HideWaitCursor()
        {
            Application.UseWaitCursor = false;
            Cursor.Current = Cursors.Default;
        }

        #endregion WINDOWS_CLASSICO

        #region DEVEXPRESS_DEFAULT_WAIT_FORM

        /// <summary>
        /// Mostra la WaitForm DevExpress standard.
        /// </summary>
        private static void ShowDevExpressWait(string caption, string description)
        {
            try
            {
                if (!SplashScreenManager.Default?.IsSplashFormVisible ?? true)
                {
                    SplashScreenManager.ShowDefaultWaitForm(caption, description);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Chiude la WaitForm standard.
        /// </summary>
        private static void HideDevExpressWait()
        {
            try
            {
                if (SplashScreenManager.Default != null &&
                    SplashScreenManager.Default.IsSplashFormVisible)
                {
                    SplashScreenManager.CloseDefaultWaitForm();
                }
            }
            catch
            {
            }
        }

        #endregion DEVEXPRESS_DEFAULT_WAIT_FORM

        #region DEVEXPRESS_CUSTOM_WAIT_FORM

        /// <summary>
        /// Mostra una WaitForm personalizzata.
        /// </summary>
        private static void ShowCustomWait(XtraForm parentForm, Type waitFormType, bool useFadeIn = true, bool useFadeOut = true)
        {
            try
            {
                if (_manager == null)
                {
                    _manager = new SplashScreenManager(
                        parentForm,
                        waitFormType,
                        useFadeIn,
                        useFadeOut);
                }

                if (!_manager.IsSplashFormVisible)
                {
                    _manager.ShowWaitForm();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Chiude la WaitForm personalizzata.
        /// </summary>
        private static void HideCustomWait()
        {
            try
            {
                if (_manager != null &&
                    _manager.IsSplashFormVisible)
                {
                    _manager.CloseWaitForm();
                }
            }
            catch
            {
            }
        }


        #endregion DEVEXPRESS_CUSTOM_WAIT_FORM

        #region DEVEXPRESS_OVERLAY_FORM

        /// <summary>
        /// Mostra overlay loading sopra un controllo.
        /// </summary>
        private static void ShowOverlay(Control control)
        {
            try
            {
                if (_overlayHandle == null)
                {
                    _overlayHandle = SplashScreenManager.ShowOverlayForm(control);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Chiude overlay loading.
        /// </summary>
        private static void HideOverlay()
        {
            try
            {
                if (_overlayHandle != null)
                {
                    SplashScreenManager.CloseOverlayForm(_overlayHandle);
                    _overlayHandle = null;
                }
            }
            catch
            {
            }
        }

        #endregion DEVEXPRESS_OVERLAY_FORM


        /*
        #region IDisposable SCOPE

        /// <summary>
        /// Uso:
        /// using(CursorHelper.WaitScope())
        /// {
        ///     ...
        /// }
        /// </summary>
        public static IDisposable WaitScope()
        {
            return new CursorScope();
        }

        private class CursorScope : IDisposable
        {
            public CursorScope()
            {
                ShowWaitCursor();
            }

            public void Dispose()
            {
                HideWaitCursor();
            }
        }

        #endregion
        */
    }
}
