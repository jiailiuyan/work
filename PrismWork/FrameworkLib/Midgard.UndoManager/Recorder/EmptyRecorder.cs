using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Work.UndoManager.Recorder
{
    /// <summary>
    ///  空的Recorder,提供给
    /// </summary>
    public class EmptyRecorder : BaseRecorder
    {
        public EmptyRecorder()
            : base(null)
        { }
    }
}
