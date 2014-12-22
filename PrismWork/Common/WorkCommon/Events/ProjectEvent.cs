using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkCommon.Events
{

    public enum ProjectAction
    {
        Show,
        Close
    }

    public class ProjectEventArgs
    {
        public ProjectAction Action { get; set; }
    }

    public class ProjectEvent : Microsoft.Practices.Prism.Events.CompositePresentationEvent<ProjectEventArgs>
    {

    }

}
