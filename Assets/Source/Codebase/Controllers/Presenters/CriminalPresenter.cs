using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Root
{
    public class CriminalPresenter : IPresenter
    {
        private Criminal _criminal;
        private CriminalView _view;

        public CriminalPresenter(Criminal criminal, CriminalView view)
        {
            _criminal = criminal;
            _view = view;
        }

        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }
}
