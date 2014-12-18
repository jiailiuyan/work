using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using WorkCommon.ViewModel;

namespace Modules.BottomToolBar
{
    [Export(typeof(BottomToolBarViewModel))]
    public class BottomToolBarViewModel : BaseObject
    {

        [ImportingConstructor]
        public BottomToolBarViewModel(IEventAggregator eventAggregator)
        {

        }


    }

}
