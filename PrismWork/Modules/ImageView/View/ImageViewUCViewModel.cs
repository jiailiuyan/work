using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using WorkCommon.Behaviors;
using WorkCommon.Manager;
using WorkCommon.ViewModel;

namespace ImageView
{
    [Export(typeof(ImageViewUCViewModel))]
    public class ImageViewUCViewModel : BaseObject
    {

        [ImportingConstructor]
        public ImageViewUCViewModel(IEventAggregator eventAggregator)
        {

        }
    }
}
