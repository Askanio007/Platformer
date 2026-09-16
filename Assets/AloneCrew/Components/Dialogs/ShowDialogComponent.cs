using System;
using AloneCrew.Model.Data;
using AloneCrew.Model.Definitions;
using AloneCrew.UI.Hud.Dialogs;
using UnityEngine;

namespace AloneCrew.Components.Dialogs
{
    public class ShowDialogComponent : MonoBehaviour
    {
        [SerializeField] public Mode _mode;
        [SerializeField] private DialogData _bound;
        [SerializeField] private DialogDef _external;

        private DialogBoxController _dialogBox;

        public void Show()
        {
            if (_dialogBox == null)
                    _dialogBox = FindAnyObjectByType<DialogBoxController>();
            _dialogBox.ShowDialog(Data);
        }

        public void Show(DialogDef def)
        {
            _external = def;
            Show();
        }

        public DialogData Data
        {
            get
            {
                switch (_mode)
                {
                    case Mode.Bound: return _bound;
                    case Mode.External: return _external.Data;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
        }
        
        public enum Mode
        {
            Bound,
            External
        }
        
        
    }
}