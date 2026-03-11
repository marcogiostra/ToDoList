using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList.Static
{
    public class DEV_GridView
    {
        #region CONSTs
        public static string DWG_GroupPanelText = "Trascina qui un'intestazione di colonna per raggruppare gli elementi in base alla colonna scelta";
        public static string DWG_InsertRowText = "Fai click qui per aggiungere una riga";
        #endregion CONSTs

        /*
        #region LAYOUTS

        public static void RestoreLayout (GridControl pGridControl,string pRadice)
        {
            try
            {
                //se non essite la cartella dell'opereatore loggato, la cre
                string dir_op = OpzioniGenerali.Path_LAYOUTS + "\\" + Parametri.LoggedIDOperatore;
                if (!Directory.Exists(dir_op)) 
                {
                    Directory.CreateDirectory(dir_op);
                }

                string fileName = dir_op + "\\" + pRadice + "_" + Parametri.Versione_Layout + ".xml";
                pGridControl.ForceInitialize();
                pGridControl.MainView.RestoreLayoutFromXml(fileName);

            }
            catch (Exception) { }
        }

        public static void SaveLayout(GridControl pGridControl, string pRadice)
        {
            try
            {
                //se non essite la cartella dell'opereatore loggato, la cre
                string dir_op = OpzioniGenerali.Path_LAYOUTS + "\\" + Parametri.LoggedIDOperatore;
                if (!Directory.Exists(dir_op))
                {
                    Directory.CreateDirectory(dir_op);
                }

                string fileName = dir_op + "\\" + pRadice + "_" + Parametri.Versione_Layout + ".xml";
                pGridControl.ForceInitialize();
                pGridControl.MainView.SaveLayoutToXml(fileName);

            }
            catch (Exception) { }
        }
        #endregion LAYOUTS
*/

        #region GRIDVIEW
        public static void SetGridViewProperty(GridView pView)
        {

        }
        public static void SetBandGridViewProperty(BandedGridView pView)
        {

        }
        #endregion GRIDVIEW

        #region COLUMN
        /// <summary>
        /// Imposta le proprieta base di una colonna
        /// </summary>
        /// <param name="pGridColumn"></param>
        /// <param name="pVisible"></param>
        /// <param name="pEditable"></param>
        /// <param name="pCaption"></param>
        /// <param name="pWidth"></param>
        /// <param name="pIsMoney"></param>
        /// <param name="pIsNumeric"></param>
        /// <param name="pNumeroDecimali"></param>
        public static void SetColumnProperty(GridColumn pGridColumn, bool pVisible, bool pEditable, string pCaption, int pWidth = 0, bool pIsMoney = false, bool pIsNumeric = false, int pNumeroDecimali = 2)
        {
            string formato = string.Empty;

            pGridColumn.Visible = pVisible;
            pGridColumn.OptionsColumn.AllowEdit = pEditable;
            if (!string.IsNullOrEmpty(pCaption))
                pGridColumn.Caption = pCaption;
            if (pWidth > 0)
                pGridColumn.Width = pWidth;
            if (pIsMoney || pIsNumeric)
            {
                string euro = "€ ";
                pGridColumn.DisplayFormat.FormatType = FormatType.Numeric;
                formato = "{0:#,##0";
                if (pNumeroDecimali == 1)
                    formato += ".0";
                else if (pNumeroDecimali == 2)
                    formato += ".00";
                else if (pNumeroDecimali == 3)
                    formato += ".000";
                else if (pNumeroDecimali == 4)
                    formato += ".0000";
                else if (pNumeroDecimali == 5)
                    formato += ".00000";
                else if (pNumeroDecimali == 6)
                    formato += ".000000";
                formato += "}";
                if (pIsMoney)
                    formato = euro + formato;
                pGridColumn.DisplayFormat.FormatString = formato;
            }
        }

        /// <summary>
        /// Imposta l'allineamento di una colonna
        /// </summary>
        /// <param name="pGridColumn"></param>
        /// <param name="pCellAligment"></param>
        /// <param name="pHeaderAlignment"></param>
        public static void SetColumnAlignament(GridColumn pGridColumn, DevExpress.Utils.HorzAlignment pCellAligment, DevExpress.Utils.HorzAlignment pHeaderAlignment)
        {
            pGridColumn.AppearanceCell.TextOptions.HAlignment = pCellAligment;
            pGridColumn.AppearanceHeader.TextOptions.HAlignment = pHeaderAlignment;

        }

        /// <summary>
        /// Imposta il colore di una colonna
        /// </summary>
        /// <param name="pGridColumn"></param>
        /// <param name="pColor"></param>
        public static void SetColumnBackColor(GridColumn pGridColumn, Color pColor)
        {
            pGridColumn.AppearanceCell.BackColor = pColor;
        }

        /// <summary>
        /// Raggruppa una colonna
        /// </summary>
        /// <param name="pGridView"></param>
        /// <param name="pGridColumn"></param>
        /// <param name="pAllowMove"></param>
        public static void GroupByOnColumn(DevExpress.XtraGrid.Views.Grid.GridView pGridView, GridColumn pGridColumn, bool pAllowMove)
        {

            pGridView.BeginSort();
            try
            {
                pGridView.ClearGrouping();
                int position = pGridView.GroupCount;
                position++;
                pGridColumn.GroupIndex = position;
                pGridColumn.OptionsColumn.AllowMove = pAllowMove;
            }
            finally
            {
                pGridView.EndSort();
            }
        }

        /// <summary>
        /// Imposta alcune proprietà di una Colonna
        /// </summary>
        /// <param name="pGridColumn"></param>
        /// <param name="pAllowGroup"></param>
        /// <param name="pAllowSort"></param>
        /// <param name="pAllowFilter"></param>
        public static void SetColumnAllowProprerty(GridColumn pGridColumn, bool pAllowGroup = true, bool pAllowSort = true, bool pAllowFilter = true)
        {
            pGridColumn.OptionsColumn.AllowGroup = (pAllowGroup ? DefaultBoolean.True : DefaultBoolean.False);
            pGridColumn.OptionsColumn.AllowSort = (pAllowSort ? DefaultBoolean.True : DefaultBoolean.False);
            pGridColumn.OptionsFilter.AllowFilter = (pAllowFilter ? true : false);

            //Settings.AllowAutoFilter = DefaultBoolean.False;
        }
        #endregion COLUMN

        #region BAND
        /// <summary>
        /// Imposta alcune proprietà di una BAND
        /// </summary>
        /// <param name="band"></param>
        /// <param name="pFixedstyle"></param>
        /// <param name="pAllowMove"></param>
        /// <param name="pMinwidth"></param>
        /// <param name="pWidth"></param>
        public static void SetBandProperty(GridBand pBand, FixedStyle pFixedstyle = FixedStyle.None, bool pAllowMove = true, int pMinwidth = 30, int pWidth = 60)
        {
            pBand.Fixed = pFixedstyle;
            pBand.OptionsBand.AllowMove = pAllowMove;
            pBand.MinWidth = pMinwidth;
            pBand.Width = pWidth;
        }

        public static void BandAddColumn(GridBand pBand, GridColumn pGridColumn, int pVisibleIndex = -1)
        {
            pBand.Columns.Add((BandedGridColumn)pGridColumn);
            if (pVisibleIndex > -1)
            {
                pGridColumn.VisibleIndex = pVisibleIndex;
            }

        }
        #endregion BAND

        #region SELECTIONROW

        /*
        public static void UNGROUPED_RowSelectByString_Ungrouped(GridView pView, GridColumn pGridColum, string pKey)
        {

            if (pView.RowCount > 0)
            {
                pView.ClearSelection();

                //int rowHandle = pView.LocateByValue("col_xyz", pKey);
                int rowHandle = pView.LocateByValue(pGridColum.Name, pKey);

                // Se l'indice della riga è valido (diverso da GridControl.InvalidRowHandle), seleziona la riga
                if (rowHandle != GridControl.InvalidRowHandle)
                {
                    pView.FocusedRowHandle = rowHandle;  // Imposta il focus sulla riga trovata
                    pView.SelectRow(rowHandle);          // Seleziona la riga
                }
                else
                {
                    pView.FocusedRowHandle = 0;
                }

            }

        }
        */

        public static void SelectionByString(GridView pView, GridColumn pGridColum, string pKey)
        {
            if (pView.RowCount > 0)
            {
                pView.ClearSelection();

                // Disabilita aggiornamento della visualizzazione per evitare flickering
                pView.BeginUpdate();
                try
                {
                    // Scorriamo tutte le righe visibili del GridView
                    for (int i = 0; i < pView.RowCount; i++)
                    {
                        // Ottiene l'indice di riga vero, considerando anche le righe raggruppate
                        int rowHandle = pView.GetRowHandle(i);

                        // Se la riga è un gruppo, aprila
                        if (pView.IsGroupRow(rowHandle))
                        {
                            // Apri il gruppo
                            pView.ExpandGroupRow(rowHandle);
                        }

                        // Controlla se la riga non è un gruppo e se il valore della colonna corrisponde
                        if (!pView.IsGroupRow(rowHandle))
                        {
                            var cellValue = pView.GetRowCellValue(rowHandle, pGridColum)?.ToString();
                            if (pKey == cellValue.ToString())
                            {
                                // Se il valore della cella corrisponde, seleziona la riga
                                pView.FocusedRowHandle = rowHandle;
                                pView.SelectRow(rowHandle);

                                // Esci dalla funzione
                                return;
                            }
                        }
                    }
                }
                finally
                {
                    // Riabilita l'aggiornamento della visualizzazione
                    pView.EndUpdate();
                }
            }
        }
        public static void SelectionByInt(GridView pView, GridColumn pGridColum, int pKey)
        {
            if (pView.RowCount > 0)
            {
                pView.ClearSelection();
                
                // Disabilita aggiornamento della visualizzazione per evitare flickering
                pView.BeginUpdate();
                try
                {
                    // Scorriamo tutte le righe visibili del GridView
                    for (int i = 0; i < pView.RowCount; i++)
                    {
                        // Ottiene l'indice di riga vero, considerando anche le righe raggruppate
                        int rowHandle = pView.GetRowHandle(i);

                        // Se la riga è un gruppo, aprila
                        if (pView.IsGroupRow(rowHandle))
                        {
                            // Apri il gruppo
                            pView.ExpandGroupRow(rowHandle);
                        }

                        // Controlla se la riga non è un gruppo e se il valore della colonna corrisponde
                        if (!pView.IsGroupRow(rowHandle))
                        {
                            var cellValue = pView.GetRowCellValue(rowHandle, pGridColum)?.ToString();
                            if (pKey == Convert.ToInt32(cellValue))
                            {
                                // Se il valore della cella corrisponde, seleziona la riga
                                pView.FocusedRowHandle = rowHandle;
                                pView.ClearSelection();
                                pView.SelectRow(rowHandle);
                                pView.MakeRowVisible(rowHandle);

                                // Esci dalla funzione
                                return;
                            }
                        }
                    }
                }
                finally
                {
                    // Riabilita l'aggiornamento della visualizzazione
                    pView.EndUpdate();
                    pView.RefreshData();
                    Application.DoEvents();
                }

                /*
                 * 
                 *                 string filtro = $"[" + pGridColum.Name + "] = '{pKey}'";
                pView.ActiveFilterString = filtro;

                if (pView.DataRowCount == 0)
                {
                    int rowHandle = pView.GetVisibleRowHandle(0);

                    // Se l'indice della riga è valido (diverso da GridControl.InvalidRowHandle), seleziona la riga
                    if (rowHandle != GridControl.InvalidRowHandle)
                    {
                        pView.FocusedRowHandle = rowHandle;  // Imposta il focus sulla riga trovata
                        pView.SelectRow(rowHandle);          // Seleziona la riga

                        // Rimuove il filtro temporaneo
                        pView.ActiveFilter.Clear();
                    }
                    else
                    {
                        // Rimuove il filtro temporaneo
                        pView.ActiveFilter.Clear();

                        pView.FocusedRowHandle = 0;
                    }
                }
                else
                {
                    bool found = false;
                    for (int i = 0; i < pView.DataRowCount; i++)
                    {
                        // Espandi il gruppo per assicurarti che tutte le righe siano visibili
                        pView.ExpandMasterRow(i);

                        // Trova l'indice della prima riga visibile all'interno del gruppo corrente
                        //int rowHandle = pView.GetVisibleRowHandle(0);
                        int rowHandle = pView.GetVisibleRowHandle(i);

                        // Controlla se la riga corrente soddisfa il filtro
                        if (rowHandle != GridControl.InvalidRowHandle && Convert.ToInt32(pView.GetRowCellValue(rowHandle, pGridColum.Name)) == pKey)
                        {
                            pView.FocusedRowHandle = rowHandle;  // Imposta il focus sulla riga trovata
                            pView.SelectRow(rowHandle);          // Seleziona la riga

                            // Rimuove il filtro temporaneo
                            pView.ActiveFilter.Clear();

                            found = true;
                            // Dopo aver trovato la riga, esci dalla funzione
                            break;
                        }
                    }
                    if (!found)
                    {
                        pView.ExpandMasterRow(0);

                        // Rimuove il filtro temporaneo
                        pView.ActiveFilter.Clear();

                        pView.FocusedRowHandle = 0;
                    }
                }
                */

            }
        }
        public static void SelectionByStringString(GridView pView, GridColumn pGridColum1, string pKey1, GridColumn pGridColum2, string pKey2)
        {
            if (pView.RowCount > 0)
            {
                pView.ClearSelection();

                string filtro = $"[" + pGridColum1.Name + "] = '{pKey1}' AND [" + pGridColum2.Name + "] = '{pKey2}'";
                pView.ActiveFilterString = filtro;

                if (pView.DataRowCount == 0)
                {
                    int rowHandle = pView.GetVisibleRowHandle(0);

                    // Se l'indice della riga è valido (diverso da GridControl.InvalidRowHandle), seleziona la riga
                    if (rowHandle != GridControl.InvalidRowHandle)
                    {
                        pView.FocusedRowHandle = rowHandle;  // Imposta il focus sulla riga trovata
                        pView.SelectRow(rowHandle);          // Seleziona la riga

                        // Rimuove il filtro temporaneo
                        pView.ActiveFilter.Clear();
                    }
                    else
                    {
                        // Rimuove il filtro temporaneo
                        pView.ActiveFilter.Clear();

                        pView.FocusedRowHandle = 0;
                    }
                }
                else
                {
                    bool found = false;
                    for (int i = 0; i < pView.DataRowCount; i++)
                    {
                        // Espandi il gruppo per assicurarti che tutte le righe siano visibili
                        pView.ExpandMasterRow(i);

                        // Trova l'indice della prima riga visibile all'interno del gruppo corrente
                        //int rowHandle = pView.GetVisibleRowHandle(0);
                        int rowHandle = pView.GetVisibleRowHandle(i);

                        // Controlla se la riga corrente soddisfa il filtro
                        if (rowHandle != GridControl.InvalidRowHandle &&
                            pView.GetRowCellValue(rowHandle, pGridColum1.Name).ToString() == pKey1 &&
                            pView.GetRowCellValue(rowHandle, pGridColum2.Name).ToString() == pKey2)
                        {
                            pView.FocusedRowHandle = rowHandle;  // Imposta il focus sulla riga trovata
                            pView.SelectRow(rowHandle);          // Seleziona la riga

                            // Rimuove il filtro temporaneo
                            pView.ActiveFilter.Clear();

                            found = true;
                            // Dopo aver trovato la riga, esci dalla funzione
                            break;
                        }
                    }
                    if (!found)
                    {
                        pView.ExpandMasterRow(0);

                        // Rimuove il filtro temporaneo
                        pView.ActiveFilter.Clear();

                        pView.FocusedRowHandle = 0;
                    }
                }
            }
        }


        private static void CollapseAllGroupsExcept(GridView gridView, BandedGridView bandedGridView, int groupToExpand)
        {
            if (gridView != null)
            {
                // Ottieni il numero di livelli di raggruppamento
                int groupLevel = gridView.GroupCount;

                // Loop attraverso tutte le righe e chiudi i gruppi tranne quello selezionato
                for (int i = 0; i < gridView.DataRowCount; i++)
                {
                    int rowHandle = gridView.GetRowHandle(i);

                    // Se la riga è un gruppo e non è il gruppo da espandere
                    if (gridView.IsGroupRow(rowHandle) && rowHandle != groupToExpand)
                    {
                        // Chiudi il gruppo
                        gridView.SetRowExpanded(rowHandle, false);
                    }
                }
            }
            else
            {
                // Ottieni il numero di livelli di raggruppamento
                int groupLevel = bandedGridView.GroupCount;

                // Loop attraverso tutte le righe e chiudi i gruppi tranne quello selezionato
                for (int i = 0; i < bandedGridView.DataRowCount; i++)
                {
                    int rowHandle = bandedGridView.GetRowHandle(i);

                    // Se la riga è un gruppo e non è il gruppo da espandere
                    if (bandedGridView.IsGroupRow(rowHandle) && rowHandle != groupToExpand)
                    {
                        // Chiudi il gruppo
                        bandedGridView.SetRowExpanded(rowHandle, false);
                    }
                }
            }
        }
        #endregion SELECTIONROW

        #region POPUP
        public static void PopupMenuShowing(object sender, PopupMenuShowingEventArgs e, ContextMenuStrip pCms)
        {
            if (e.HitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                GridView view = sender as GridView;
                view.FocusedRowHandle = e.HitInfo.RowHandle;

                pCms.Show(view.GridControl, e.Point);
            }
        }

        public static void PopupMenuShowingNoAddRow(object sender, PopupMenuShowingEventArgs e, ContextMenuStrip pCms)
        {
            if (e.HitInfo.HitTest == DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitTest.RowCell)
            {
                GridView view = sender as GridView;
                if (!view.IsNewItemRow(e.HitInfo.RowHandle))
                {
                    view.FocusedRowHandle = e.HitInfo.RowHandle;
                    pCms.Show(view.GridControl, e.Point);

                }


            }

        }

        public static void PopupMenuShowingWithVuoto(object sender, PopupMenuShowingEventArgs e, ContextMenuStrip pCms, ContextMenuStrip pCmsVuoto , GridControl pGridControl)
        {

            // Controlla se il menu viene visualizzato su una riga
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                GridView view = sender as GridView;
                view.FocusedRowHandle = e.HitInfo.RowHandle;

                pCms.Show(view.GridControl, e.Point);
            }
            else
            {
                // Se non è su una riga (es. su uno spazio vuoto), mostra il cms2
                pCmsVuoto.Show(pGridControl, e.Point);
            }
        }

        public static void PopupMenuShowingWithVuoto_STD_Interface(object sender, PopupMenuShowingEventArgs e, ContextMenuStrip pCms, ContextMenuStrip pCmsVuoto, GridControl pGridControl, bool pCanInsert)
        {

            // Controlla se il menu viene visualizzato su una riga
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            {
                GridView view = sender as GridView;
                view.FocusedRowHandle = e.HitInfo.RowHandle;

                pCms.Show(view.GridControl, e.Point);
            }
            else
            {
                // Se non è su una riga (es. su uno spazio vuoto), mostra il cms2
                if (pCanInsert)
                    pCmsVuoto.Show(pGridControl, e.Point);
            }
        }
        #endregion POPUP

        #region TEST
        public static bool IsRowGrouping (GridView pView, BandedGridView pBandedView)
        {
            if (pView != null)
            {
                Point pt = pView.GridControl.PointToClient(Control.MousePosition);

                // Calcola le informazioni sull'hit test
                GridHitInfo hitInfo = pView.CalcHitInfo(pt);

                // Controlla se l'utente ha cliccato su una riga effettiva
                if (hitInfo.InRow && !hitInfo.InGroupRow)
                {
                    // Questo è un clic su una riga effettiva
                    return false;
                }
                else if (hitInfo.InGroupRow)
                {
                    // Questo è un clic su una riga effettiva
                    return true;
                }

            }
            else
            {
                Point pt = pBandedView.GridControl.PointToClient(Control.MousePosition);

                // Calcola le informazioni sull'hit test
                GridHitInfo hitInfo = pBandedView.CalcHitInfo(pt);

                // Controlla se l'utente ha cliccato su una riga effettiva
                if (hitInfo.InRow && !hitInfo.InGroupRow)
                {
                    // Questo è un clic su una riga effettiva
                    return false;
                }
                else if (hitInfo.InGroupRow)
                {
                    // Questo è un clic su una riga effettiva
                    return true;
                }
            }

            return false;

        }
        #endregion TEST

    }
}
